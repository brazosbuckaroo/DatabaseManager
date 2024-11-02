using Avalonia.Controls;
using Avalonia.ReactiveUI;
using DatabaseManager.Models.Types;
using DatabaseManager.ViewModels;
using FirebirdSql.Data.FirebirdClient;
using ReactiveUI;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Reactive;
using Avalonia.Markup.Xaml;

namespace DatabaseManager.Views;

/// <summary>
/// An object that represents the programs main window as a <see cref="ReactiveWindow{MainWindowViewModel}"/>
/// </summary>
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    #region CONSTRUCTORS
    /// <summary>
    /// A constructor that makes the main window.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();

        this.WhenActivated(action =>
        {
            action(ViewModel!.LoginViewModel.LoginSettingsInteraction.RegisterHandler(this.DoShowLoginSettingsDialogAsync));
        });
    }
    #endregion

    #region METHODS
    /// <summary>
    /// Takes an <see cref="InteractionContext{LoginSettingsWindowViewModel, ApplicationSettingsViewModel}"/> to open a new 
    /// window as a dialog for login settings.
    /// </summary>
    /// <param name="context">
    /// A <see cref="InteractionContext{LoginSettingsWindowViewModel, ApplicationSettingsViewModel}"/> that takes a 
    /// <see cref="LoginSettingsWindowViewModel"/> and a <see cref="ApplicationSettingsViewModel"/> to allow for setting changes
    /// </param>
    /// <returns>
    /// Returns a Task to allow for <see cref="async"/> operations.
    /// </returns>
    private async Task DoShowLoginSettingsDialogAsync(InteractionContext<LoginSettingsWindowViewModel, ApplicationSettingsViewModel> context)
    {
        var dialog = new LoginSettingsWindow();

        dialog.DataContext = context.Input;

        context.SetOutput(await dialog.ShowDialog<ApplicationSettingsViewModel>(this));
    }
    #endregion
}
