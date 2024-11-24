﻿using FirebirdSql.Data.FirebirdClient;
using ReactiveUI;
using System;

namespace DatabaseManager.ViewModels;

public class DashboardViewModel : ViewModelBase, IRoutableViewModel
{
    #region PROPERTIES
    /// <inheritdoc/>
    public IScreen HostScreen { get; }

    /// <inheritdoc/>
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    /// <summary>
    /// The connection string meant to be used to connect to
    /// the Firebird SQL Server.
    /// </summary>
    public FbConnectionStringBuilder ConnectionString { get; set; }
    #endregion

    #region CONSTRUCTORS
    /// <summary>
    /// a default constructor for dashboard view model but
    /// it is only for Avalonia XML Viewer.
    /// </summary>
    public DashboardViewModel()
    {
        this.ConnectionString = new FbConnectionStringBuilder();
    }

    /// <summary>
    /// A constructor that allows the injection of the 
    /// <see cref="ReactiveUI.IScreen"/> to allow view routing.
    /// </summary>
    /// <param name="hostScreen">
    /// The <see cref="IScreen"/> meant to allow routing back and forth.
    /// </param>
    public DashboardViewModel(IScreen hostScreen)
    {
        this.HostScreen = hostScreen;
    }

    /// <summary>
    /// The constructor that will allow passing a <see cref="FbConnectionStringBuilder"/>
    /// to allow signing into the Firebird SQL Server.
    /// </summary>
    /// <param name="hostScreen">
    /// The <see cref="IScreen"/> meant to allow routing back and forth.
    /// </param>
    /// <param name="connectionString">
    /// A <see cref="FbConnectionStringBuilder"/> to be used as the connection string for the 
    /// Firebird SQL Server.
    /// </param>
    public DashboardViewModel(IScreen hostScreen, FbConnectionStringBuilder connectionString)
    {
        this.HostScreen = hostScreen;
        this.ConnectionString = connectionString;
    }
    #endregion
}