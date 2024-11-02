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

    /// <summary>
    /// 
    /// </summary>
    public IScreen HostScreen { get; }

    /// <summary>
    /// A <see cref="ISettings"/> used to get application settings and whathave you.
    /// </summary>
    public ISettings Settings { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 3);
    #endregion

    #region COMMANDS
    /// <summary>
    /// A <see cref="ICommand"/> meant to be used in the UI to open settings.
    /// </summary>
    public ICommand OpenLoginSettingsCommand { get; }
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
    }

    /// <summary>
    /// A <see cref="LoginViewModel"/> constructor that allows for injection of a settings
    /// provider.
    /// </summary>
    /// <param name="settingsProvider">
    /// An <see cref="ISettings"/> meant to allow the ability to edit, save, and read settings.
    /// </param>
    public LoginViewModel(ISettings settingsProvider)
    {
        this.LoginSettingsInteraction = new Interaction<LoginSettingsWindowViewModel, ApplicationSettingsViewModel>();
        this.OpenLoginSettingsCommand = ReactiveCommand.CreateFromTask(OpenLoginSettingsDialogAsync);
        this.Settings = settingsProvider;
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
    #endregion
}