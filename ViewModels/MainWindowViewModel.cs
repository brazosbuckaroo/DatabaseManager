using DatabaseManager.Models.Services;
using DatabaseManager.Views;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DatabaseManager.ViewModels;

/// <summary>
/// A class meant to contain all the ViewModels that the <see cref="MainWindow"/>
/// is meant to know about.
/// </summary>
public class MainWindowViewModel : ViewModelBase, IScreen
{
    #region PROPERTIES
    /// <inheritdoc/>
    public RoutingState Router { get; } = new RoutingState();

    /// <summary>
    /// The ViewModel for the <see cref="LoginSettingsWindow"/>.
    /// </summary>
    public LoginSettingsWindowViewModel LoginSettingsWindowViewModel { get; }

    /// <summary>
    /// The ViewModel meant for the <see cref="LoginView"/>.
    /// </summary>
    public LoginViewModel LoginViewModel { get; }

    /// <summary>
    /// The ViewModel meant for the <see cref="MainView"/>.
    /// </summary>
    public DashboardViewModel DashboardViewModel { get; }
    #endregion

    #region CONSTRUCTORS
    /// <summary>
    /// The default constructor that makes the <see cref="MainWindowViewModel"/> with 
    /// the ViewModels it needs to know about.
    /// </summary>
    public MainWindowViewModel()
    {
        this.LoginViewModel = new LoginViewModel();
        this.DashboardViewModel = new DashboardViewModel();
        this.LoginSettingsWindowViewModel = new LoginSettingsWindowViewModel();

        Router.Navigate.Execute(LoginViewModel);

        this.WhenActivated((CompositeDisposable disposables) => { });
    }

    /// <summary>
    /// A constructor for <see cref="MainWindowViewModel"/> that allows for
    /// dependency injection.
    /// </summary>
    /// <param name="settingsProvider">
    /// The service meant for loading, reading, and editing application settings
    /// </param>
    public MainWindowViewModel(ISettings settingsProvider)
    {
        this.DashboardViewModel = new DashboardViewModel(this);
        this.LoginViewModel = new LoginViewModel(settingsProvider, this, DashboardViewModel);
        this.LoginSettingsWindowViewModel = new LoginSettingsWindowViewModel();

        Router.Navigate.Execute(LoginViewModel);

        this.WhenActivated((CompositeDisposable disposables) => { });
    }
    #endregion
}
