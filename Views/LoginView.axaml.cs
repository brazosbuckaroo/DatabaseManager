using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using DatabaseManager.ViewModels;
using ReactiveUI;

namespace DatabaseManager.Views;

public partial class LoginView : ReactiveUserControl<LoginViewModel>
{
    public LoginView()
    {
        this.InitializeComponent();

        this.WhenActivated(disposables => 
        {
        });

        AvaloniaXamlLoader.Load(this);
    }
}