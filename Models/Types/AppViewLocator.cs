using DatabaseManager.ViewModels;
using DatabaseManager.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Models.Types;

/// <summary>
/// A basic View Locator to find view for multiple
/// pages for the app.
/// </summary>
public class AppViewLocator
{
    /// <inheritdoc/>
    //public IViewFor? ResolveView<T>(T? viewModel, string? contract = null) => viewModel switch
    //{
    //    LoginViewModel context => new LoginView { DataContext = context },
    //    _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
    //};
}
