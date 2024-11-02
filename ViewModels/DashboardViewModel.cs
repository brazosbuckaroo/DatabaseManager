using ReactiveUI;
using System;

namespace DatabaseManager.ViewModels;

public class DashboardViewModel : ViewModelBase, IRoutableViewModel
{
    #region PROPERTIES
    /// <summary>
    /// 
    /// </summary>
    public IScreen HostScreen { get; }

    /// <summary>
    /// 
    /// </summary>
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 3);
    #endregion

    #region CONSTRUCTORS
    /// <summary>
    /// 
    /// </summary>
    public DashboardViewModel()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hostScreen"></param>
    public DashboardViewModel(IScreen hostScreen)
    {
        this.HostScreen = hostScreen;
    }
    #endregion
}