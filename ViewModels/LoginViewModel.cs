using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;
using FirebirdSql.Data.FirebirdClient;
using System.Threading.Tasks;
using System.Diagnostics;
using DynamicData.Binding;
using System.Collections.ObjectModel;
using DatabaseManager.Models.Types;
using DatabaseManager.Models.Services;
using System.Reactive;
using DatabaseManager.Views;
using System.Reactive.Disposables;

namespace DatabaseManager.ViewModels;

/// <summary>
/// A <see cref="ViewModelBase"/> made for the login screen of the
/// application.
/// </summary>
public class LoginViewModel : ViewModelBase, IRoutableViewModel
{
    #region PROPERTIES
    /// <summary>
    /// A <see cref="Interaction{LoginSettingsWindowViewModel, ApplicationSettingsViewModel}"/> meant to handle 
    /// the creation of a <see cref="LoginSettingsWindowViewModel"/> to allow for a dialog window to pop
    /// up.
    /// </summary>
    public Interaction<LoginSettingsWindowViewModel, ApplicationSettingsViewModel> LoginSettingsInteraction { get; }

    /// <inheritdoc/>
    public IScreen HostScreen { get; }

    /// <summary>
    /// A <see cref="ISettings"/> used to get application settings and whathave you.
    /// </summary>
    public ISettings Settings { get; set; }

    /// <inheritdoc/>
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    /// <summary>
    /// A <see cref="DashboardViewModel"/> used to load the 
    /// application's main dashboard.
    /// </summary>
    public DashboardViewModel DashboardView { get; }
    #endregion

    #region COMMANDS
    /// <summary>
    /// A <see cref="ReactiveCommand"/> meant to be used in the UI to open settings.
    /// </summary>
    public ReactiveCommand<Unit, Unit> OpenLoginSettingsCommand { get; }

    /// <summary>
    /// A <see cref="ReactiveCommand"/> meant to run the verifcation
    /// service and load the dashboard if verifcation is successful.
    /// </summary>
    public ReactiveCommand<Unit, IRoutableViewModel> LoginCommand { get; }
    #endregion

    #region CONSTRUCTORS
    /// <summary>
    /// A constructor that that makes the ViewModel's <see cref="Interaction{LoginSettingsWindowViewModel, ApplicationSettingsViewModel}"/>
    /// and creates an <see cref="async"/> command.
    /// </summary>
    public LoginViewModel()
    {
        this.LoginSettingsInteraction = new Interaction<LoginSettingsWindowViewModel, ApplicationSettingsViewModel>();
        this.OpenLoginSettingsCommand = ReactiveCommand.CreateFromTask(OpenLoginSettingsDialogAsync);
        this.Settings = new ApplicationSettingsViewModel();
        this.DashboardView = new DashboardViewModel();
        this.LoginCommand = ReactiveCommand.CreateFromObservable(Login);
    }

    /// <summary>
    /// A <see cref="LoginViewModel"/> constructor that allows for injection of a settings
    /// provider.
    /// </summary>
    /// <param name="settingsProvider">
    /// An <see cref="ISettings"/> meant to allow the ability to edit, save, and read settings.
    /// </param>
    /// <param name="screen">
    /// The <see cref="MainWindow"/> that is hosting the view to allow for navigate to the
    /// next view.
    /// </param>
    /// <param name="dashboardViewModel">
    /// The <see cref="DashboardViewModel"/> passed from the <see cref="MainWindow"/>.
    /// </param>
    public LoginViewModel(ISettings settingsProvider, IScreen screen, DashboardViewModel dashboardViewModel)
    {
        this.LoginSettingsInteraction = new Interaction<LoginSettingsWindowViewModel, ApplicationSettingsViewModel>();
        this.OpenLoginSettingsCommand = ReactiveCommand.CreateFromTask(OpenLoginSettingsDialogAsync);
        this.DashboardView = dashboardViewModel;
        this.LoginCommand = ReactiveCommand.CreateFromObservable(Login);
        this.Settings = settingsProvider;
        this.HostScreen = screen;
    }
    #endregion

    #region METHODS
    /// <summary>
    /// A method used to make an <see cref="async"/> <see cref="ICommand"/> that makes <see cref="LoginSettingsWindowViewModel"/> 
    /// to handle the <see cref="Interaction{LoginSettingsWindowViewModel, ApplicationSettingsViewModel}"/> to allow 
    /// for the creation of the dialog window.
    /// </summary>
    /// <returns>
    /// Returns a <see cref="Task"/> to allow for <see cref="async"/> compute.
    /// </returns>
    private async Task OpenLoginSettingsDialogAsync()
    {
        var dialog = new LoginSettingsWindowViewModel();

        await dialog.GetCharacterSetsAsync();
        await dialog.ReadSettingsFromFileAsync();

        this.Settings = await LoginSettingsInteraction.Handle(dialog);
    }

    /// <summary>
    /// The method that will verify a user's credentials and load the next view
    /// if they are verified.
    /// </summary>
    /// <returns>
    /// A <see cref="IObservable{DashboardViewModel}"/> meant to be the
    /// applications main dashboard.
    /// </returns>
    private IObservable<IRoutableViewModel> Login()
    {
        return this.HostScreen.Router.Navigate.Execute(this.DashboardView);
    }
    #endregion
}