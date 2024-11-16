using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using DatabaseManager.ViewModels;
using ReactiveUI;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Threading.Tasks;

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
        this.InitializeComponent();

        this.WhenActivated(disposables =>
        {
            disposables(ViewModel!.LoginViewModel.LoginSettingsInteraction.RegisterHandler(this.DoShowLoginSettingsDialogAsync));
        });

        AvaloniaXamlLoader.Load(this);
    }
    #endregion

    #region METHODS
    /// <summary>
    /// Takes an <see cref="InteractionContext{LoginSettingsWindowViewModel, ApplicationSettingsViewModel}"/> to open a new 
    /// window as a dialog for login settings.
    /// </summary>
    /// <param name="context">
    /// A <see cref="IInteractionContext{LoginSettingsWindowViewModel, ApplicationSettingsViewModel}"/> that takes a 
    /// <see cref="LoginSettingsWindowViewModel"/> and a <see cref="ApplicationSettingsViewModel"/> to allow for setting changes
    /// </param>
    /// <returns>
    /// Returns a Task to allow for <see cref="async"/> operations.
    /// </returns>
    private async Task DoShowLoginSettingsDialogAsync(IInteractionContext<LoginSettingsWindowViewModel, ApplicationSettingsViewModel> context)
    {
        var dialog = new LoginSettingsWindow();

        dialog.DataContext = context.Input;

        context.SetOutput(await dialog.ShowDialog<ApplicationSettingsViewModel>(this).ConfigureAwait(false));
    }
    #endregion
}
