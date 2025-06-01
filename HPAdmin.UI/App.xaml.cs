using System.IO;
using System.Windows;
using AutoMapper;
using HPAdmin.Data.DbContext;
using HPAdmin.Shared;
using HPAdmin.UI.Heleprs;
using HPAdmin.UI.Mapper;
using HPAdmin.UI.ViewModels;
using HPAdmin.UI.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HPAdmin.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    protected override Window CreateShell()
    {
        DIHelper.Instance.SetProvider(Container);
        return Container.Resolve<MainWindow>(); // Use the instance of the container
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // 1. Načtení konfigurace
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(Configuration.ConfigurationSetting, optional: false, reloadOnChange: true);
        var configuration = builder.Build();
        containerRegistry.RegisterInstance<IConfiguration>(configuration);

        // 2. Registrace AppDbContext
        containerRegistry.Register<AppDbContext>(() =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString(Configuration.ConnectionString));
            return new AppDbContext(optionsBuilder.Options);
        });

        // 3. Registrace AutoMapperu
        var config = new MapperConfiguration(cfg =>
        {
            foreach (var profile in Mappings.TypeMappings
                         .Select(mapping => typeof(MappingProfile<,>).MakeGenericType(mapping.Key, mapping.Value))
                         .Select(profileType => (Profile)Activator.CreateInstance(profileType)!))
            {
                cfg.AddProfile(profile);
            }
        });
        var mapper = config.CreateMapper();
        containerRegistry.RegisterInstance(mapper);

        containerRegistry.Register<MainWindowViewModel>();
        containerRegistry.Register<MainWindow>();

        DIHelper.Create(containerRegistry);
    }

    protected override void InitializeShell(Window shell)
    {
        base.InitializeShell(shell);
        Application.Current.MainWindow = shell;
        Application.Current.MainWindow.Show();
    }
}