using System.Windows;
using HPAdmin.UI.Views.Base;

namespace HPAdmin.UI.Helpers;

public class DIHelper
{
    private IContainerRegistry ContainerRegistry { get; }

    private IContainerProvider ContainerProvider { get; set; }
    
    public IEventAggregator EventAggregator { get; private set; }

    public IRegionManager RegionManager { get; private set; }

    public SessionService SessionService { get; private set; }

    public static DIHelper Instance { get; private set; }



    private DIHelper(IContainerRegistry registry) => ContainerRegistry = registry;



    public static void Create(IContainerRegistry registry)
    {
        if (Instance != null)
            throw new InvalidOperationException("DIHelper is already initialized.");

        Instance ??= new DIHelper(registry);
    }

    public void SetProvider(IContainerProvider provider)
    {
        ContainerProvider ??= provider;
        EventAggregator = provider.Resolve<IEventAggregator>();
        RegionManager = provider.Resolve<IRegionManager>();
        SessionService = provider.Resolve<SessionService>();
    }

    public TView Resolve<TView>() where TView : IView
    {
        if (ContainerProvider == null)
            throw new InvalidOperationException("DIHelper was not initialized. Make sure DIHelper.SetProvider(...) is called in OnInitialized.");

        registerIfNotRegistered<TView>();
        var view = ContainerProvider.Resolve<TView>();

        if (view is DependencyObject dependencyObject)
        {
            ViewModelLocator.SetAutoWireViewModel(dependencyObject, true);
        }

        return view;
    }



    private void registerViewModelForViewIfNeeded<TView>() where TView : IView
    {
        var viewType = typeof(TView);

        if (ContainerRegistry.IsRegistered<TView>()) 
            return;

        var vmTypeName = viewType.FullName?.Replace("View", "ViewModel");
        if (vmTypeName == null) 
            throw new InvalidOperationException($"ViewModel type name for {viewType.Name} could not be determined.");

        var vmType = viewType.Assembly.GetType(vmTypeName);
        if (vmType == null) 
            throw new InvalidOperationException($"ViewModel type for {viewType.Name} not found. Expected name: {vmTypeName}");

        if (!ContainerRegistry.IsRegistered(vmType))
            ContainerRegistry.Register(vmType);
    }

    private void registerIfNotRegistered<TView>() where TView : IView
    {
        registerViewModelForViewIfNeeded<TView>();
        if (!ContainerRegistry.IsRegistered<TView>())
            ContainerRegistry.Register<TView>();
    }
}