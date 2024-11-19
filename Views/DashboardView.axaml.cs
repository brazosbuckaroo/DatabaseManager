using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using DatabaseManager.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace DatabaseManager.Views;

/// <summary>
/// The class for the <see cref="DashboardView"/>.
/// </summary>
public partial class DashboardView : ReactiveUserControl<DashboardViewModel>
{
    /// <summary>
    /// The default constructor for this class. This is needed to allow wiring any 
    /// <see cref="ReactiveCommand"/> or handling events.
    /// </summary>
    public DashboardView()
    {
        this.InitializeComponent();

        this.WhenActivated(disposables => { });

        AvaloniaXamlLoader.Load(this);
    }
}
