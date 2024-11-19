using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using DatabaseManager.ViewModels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ReactiveUI;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace DatabaseManager.Views;

/// <summary>
/// The <see cref="ReactiveWindow{LoginErrorWindowViewModel}"/> for the Login Error. 
/// This should popup if there are any errors "logining" in.
/// </summary>
public partial class LoginErrorWindow : ReactiveWindow<LoginErrorWindowViewModel>
{
    /// <summary>
    /// The default constructor to use so it can wire the 
    /// <see cref="LoginErrorWindowViewModel.AcceptErrorCommand"/>
    /// to the <see cref="LoginErrorWindow.Close"/> method together.
    /// </summary>
    public LoginErrorWindow() 
    {
        this.InitializeComponent();

        // Prevents the window from being closed outside of
        // the command.
        this.Closing += (s, e) =>
        {
            if (!e.IsProgrammatic)
            {
                e.Cancel = true;
            };
        };

        this.WhenActivated(disposables =>
        {
            disposables(ViewModel!.AcceptErrorCommand.Subscribe(this.Close));
        });

        AvaloniaXamlLoader.Load(this);
    }
}