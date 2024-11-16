using DatabaseManager.Models.Services;
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Models.Types;

/// <summary>
/// A class meant to manage the security service of the
/// application.
/// </summary>
public class SecurityManager : ISecurity
{
    #region METHODS
    /// <inheritdoc/>
    public async Task<bool> VerifyCredentialsAsync(FbConnectionStringBuilder connectionString)
    {
        try
        {
            FbSecurity security = new FbSecurity();
            security.ConnectionString = connectionString.ToString();
            FbUserData userData = await security.DisplayUserAsync(connectionString.UserID);
        }
        catch (FbException error)
        {
            return false;
        }

        return true;
    }

    /// <inheritdoc/>
    public async Task<bool> VerifyAdminAsync(FbConnectionStringBuilder connectionString)
    {
        bool isAdmin = new bool();

        using (FbConnection connection = new FbConnection(connectionString.ToString()))
        {
            await connection.OpenAsync();

            await using (FbTransaction transaction = connection.BeginTransaction())
            {
                FbCommand command = new FbCommand();
                command.CommandText = "SELECT U.SEC$ADMIN FROM SEC$USERS U WHERE U.SEC$USER_NAME = (@A)";
                command.Parameters.Add("@A", FbDbType.VarChar).Value = connectionString.UserID.ToUpper();
                command.Connection = connection;
                command.Transaction = transaction;

                await command.PrepareAsync();

                FbDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                { 
                    isAdmin = (bool)reader.GetValue(0);
                }
            }
        }

        return isAdmin;
    }
    #endregion

    #region CONSTRUCTORS
    /// <summary>
    /// The default constructor for the security manager.
    /// </summary>
    public SecurityManager()
    { 
    }
    #endregion
}

