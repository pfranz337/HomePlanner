using System.IO;
using System.Windows;
using HPAdmin.Data;
using HPAdmin.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HPAdmin;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private readonly IContainer _container = new Container(); // Initialize the container
    private IConfigurationRoot _configuration;
    private static readonly string _configurationSetting = "appsettings.json";

    protected override Window CreateShell() => _container.Resolve<MainWindow>(); // Use the instance of the container

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Načtení konfigurace z appsettings.json
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(_configurationSetting, optional: false, reloadOnChange: true);

        _configuration = builder.Build();

        // Registrace DbContext s použitím připojovacího řetězce
        containerRegistry.RegisterInstance(_configuration);
        containerRegistry.Register<AppDbContext>(factory =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            return new AppDbContext(optionsBuilder.Options);
        });

        // Register services, view models, etc.
        _container.Register<MainWindow>(Reuse.Singleton);
    }
    // Example registration
}