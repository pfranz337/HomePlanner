using System.IO;
using System.Reflection;
using System.Windows;
using AutoMapper;
using HPAdmin.Data;
using HPAdmin.Data.Data;
using HPAdmin.Models.Models;
using HPAdmin.ViewModels;
using HPAdmin.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HPAdmin;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private IConfigurationRoot _configuration;
    private static readonly string _configurationSetting = "appsettings.json";

    protected override Window CreateShell() => Container.Resolve<MainWindow>(); // Use the instance of the container

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // 1. Načtení konfigurace
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(_configurationSetting, optional: false, reloadOnChange: true);
        _configuration = builder.Build();
        containerRegistry.RegisterInstance<IConfiguration>(_configuration);

        // 2. Registrace AppDbContext
        containerRegistry.Register<AppDbContext>(() =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            return new AppDbContext(optionsBuilder.Options);
        });

        // 3. Registrace AutoMapperu
        var config = new MapperConfiguration(cfg =>
        {
            foreach (var mapping in Mappings.TypeMappings)
            {
                var profileType = typeof(MappingProfile<,>).MakeGenericType(mapping.Key, mapping.Value);
                var profile = (Profile)Activator.CreateInstance(profileType);
                cfg.AddProfile(profile);
            }
        });
        IMapper mapper = config.CreateMapper();
        containerRegistry.RegisterInstance<IMapper>(mapper);

        containerRegistry.Register<MainWindowViewModel>();
        containerRegistry.Register<MainWindow>();
    }

    protected override void InitializeShell(Window shell)
    {
        base.InitializeShell(shell);
        Application.Current.MainWindow = (Window)shell;
        Application.Current.MainWindow.Show();
    }
}


public class MappingProfile<TDto, TModel> : Profile where TDto : DtoDataBase where TModel : ModelBase<TDto>
{
    public MappingProfile()
    {
        CreateMap<TDto, TModel>()
            .ConstructUsing((src, context) =>
            {
                var modelType = typeof(TModel);
                var instance = Activator.CreateInstance(modelType, src);
                return instance switch
                {
                    null => throw new InvalidOperationException($"Cannot create instance of {modelType}"),
                    TModel model => model,
                    _ => throw new InvalidOperationException($"Cannot cast instance of {modelType} to {typeof(TModel)}")
                };
            });
    }
}

public static class Mappings
{
    public static Dictionary<Type, Type> TypeMappings { get; } = new Dictionary<Type, Type>
    {
        { typeof(HomeTaskDto), typeof(HomeTaskModel) },
        // Přidejte další mapování podle potřeby
    };
}