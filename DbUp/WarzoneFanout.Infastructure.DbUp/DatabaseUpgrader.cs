using CommandLine;
using WarzoneFanout.Infastructure.DbUp;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using DbUp.Engine;
using DbUp;
using System.Reflection;

public static class DatabaseUpgrader
{
    public static int Execute(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DbUpConnection");
        var builder = new SqlConnectionStringBuilder(connectionString);

        var options = configuration.GetRequiredSection("DbUp").Get<DatabaseUpgraderOptions>();

        options.DatabaseServer = builder["Server"].ToString() ?? throw new InvalidOperationException();
        options.DatabaseName = builder["Database"].ToString() ?? throw new InvalidOperationException();
        options.User = builder["User"].ToString() ?? throw new InvalidOperationException();
        options.Password = builder["Password"].ToString() ?? throw new InvalidOperationException();

        int exitCode = RunOptionsAndReturnExitCode(options);

        return exitCode;
    }

    static int RunOptionsAndReturnExitCode(DatabaseUpgraderOptions o)
    {
        var exitCode = 0;

        string connectionStringWithDb = BuildConnectionString(o.DatabaseServer, o.DatabaseName, o.User, o.Password);
        string connectionStringWithoutDb = BuildConnectionString(o.DatabaseServer, "", o.User, o.Password);

        bool serverExists = CheckServerExists(connectionStringWithoutDb);

        if (!serverExists)
        {
            return 1;
        }

        bool databaseExists = CheckDatabaseExists(connectionStringWithoutDb, o.DatabaseName);

        if (!databaseExists)
        {

            bool created = CreateDatabase(connectionStringWithoutDb, o.DatabaseName);
            if (!created)
            {
                return 1;
            }
        }

        var ts = new TimeSpan(2, 0, 0);

        DatabaseUpgradeResult result;

        UpgradeEngine upgrader = DeployChanges.To
            .SqlDatabase(connectionStringWithDb)
            .WithExecutionTimeout(ts)
            .WithScriptsEmbeddedInAssembly(Assembly.GetEntryAssembly(), s => o.IncludeTestData || !s.ToLower().EndsWith("testdata.sql"))
            .WithTransactionPerScript()
            .LogScriptOutput()
            .LogToConsole()
            .Build();

        result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {

            Thread.Sleep(TimeSpan.FromSeconds(5));
            return -1;
        }

        return exitCode;
    }

    private static string BuildConnectionString(string server, string database, string username, string password)
    {
        var conn = new SqlConnectionStringBuilder
        {
            DataSource = server
        };

        if (!string.IsNullOrEmpty(database))
        {
            conn.InitialCatalog = database;
        }

        if (string.IsNullOrEmpty(username))
        {
            conn.IntegratedSecurity = true;
        }
        else
        {
            conn.UserID = username;
            conn.Password = password;
            conn.IntegratedSecurity = false;
        }

        conn.Encrypt = false;

        return conn.ToString();
    }

    private static bool CheckServerExists(string connectionString)
    {
        var res = AttemptServerConnection(connectionString);

        return res;
    }

    private static bool AttemptServerConnection(string connectionString)
    {
        var sqlConnection = new SqlConnection(connectionString);

        try
        {
            sqlConnection.Open();
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }

        return true;
    }

    private static bool CheckDatabaseExists(string connectionString, string databaseName)
    {
        bool ret;
        var sqlConnection = new SqlConnection(connectionString);

        try
        {
            var sql = $"SELECT [database_id] FROM [sys].[databases] where [name] = '{databaseName}'";
            sqlConnection.Open();
            var sqlCommand = new SqlCommand(sql, sqlConnection);
            object databaseid = sqlCommand.ExecuteScalar();

            ret = databaseid != null;
            sqlConnection.Close();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }

        return ret;
    }

    private static bool CreateDatabase(string connectionString, string databaseName)
    {
        bool ret;
        var sqlConnection = new SqlConnection(connectionString);

        try
        {
            var sql = $"CREATE DATABASE [{databaseName}]";
            sqlConnection.Open();
            var sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.ExecuteNonQuery();

            ret = CheckDatabaseExists(connectionString, databaseName);
            sqlConnection.Close();
        }
        catch (Exception)
        {
            ret = false;
        }
        finally
        {
            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }

        return ret;
    }
}
