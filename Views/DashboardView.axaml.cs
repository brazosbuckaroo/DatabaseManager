using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using DatabaseManager.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace DatabaseManager.Views;

public partial class DashboardView : ReactiveUserControl<DashboardViewModel>
{
    public DashboardView()
    {
        this.InitializeComponent();

        this.WhenActivated(disposables => { });

        AvaloniaXamlLoader.Load(this);
    }
}
