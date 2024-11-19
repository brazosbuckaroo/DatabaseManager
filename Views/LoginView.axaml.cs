using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using DatabaseManager.ViewModels;
using ReactiveUI;

namespace DatabaseManager.Views;

/// <summary>
/// The initial view a user will interact with when they login into
/// the server.
/// </summary>
public partial class LoginView : ReactiveUserControl<LoginViewModel>
{
    /// <summary>
    /// The default constructor for this view. Allowing any special wiring to occur 
    /// the XAML previewer to see this view.
    /// </summary>
    public LoginView()
    {
        this.InitializeComponent();

        this.WhenActivated(disposables => 
        {
        });

        AvaloniaXamlLoader.Load(this);
    }
}