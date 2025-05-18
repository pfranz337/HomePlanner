using System.Windows;
using HPAdmin.UI.ViewModels.Base;

namespace HPAdmin.UI.BindingProxy.Base;

public class BindingProxy<TViewModel> : Freezable where TViewModel : ViewModelBase
{
    protected override Freezable CreateInstanceCore()
        => new BindingProxy<TViewModel>();


    public TViewModel Data
    {
        get => (TViewModel)GetValue(DataProperty);
        set => SetValue(DataProperty, value);
    }

    public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register(nameof(Data), typeof(TViewModel), typeof(BindingProxy<TViewModel>),
            new UIPropertyMetadata(null));
}