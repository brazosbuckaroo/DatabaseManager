using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DatabaseManager.Models.Services;
using DatabaseManager.ViewModels;
using DatabaseManager.Views;
using System.Diagnostics;

namespace DatabaseManager;

public partial class App : Application
{
    #region FIELDS
    /// <summary>
    /// An <see cref="ISettings"/> onject meant to load the saved settings from 
    /// a file and put them in memory.
    /// </summary>
    private readonly ISettings _settingsViewModel = new ApplicationSettingsViewModel();
    #endregion

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        _settingsViewModel.ReadFromFileAsync();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(_settingsViewModel)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
