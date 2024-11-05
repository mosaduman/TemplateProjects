using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using OrderManagementSystem.Core;
using System.Data.SqlClient;

namespace OrderManagementSystem.Domain.Services
{
    public class DbConnection : IDbConnection
    {
        private string _connectionString;
        private readonly IConfiguration _configuration;
        private System.Data.Common.DbConnection _dbConnection;
        public readonly DatabaseType _databaseType;

        public DbConnection(IConfiguration configuration)
        {
            _configuration = configuration;
            _databaseType = (DatabaseType)Convert.ToInt16(_configuration.ReadSetting("ConnectionString:DatabaseType"));
        }
        public DbConnection(IConfiguration configuration, DatabaseType databaseType)
        {
            _configuration = configuration;
            _databaseType = databaseType;
        }
        public string GetConnectionString()
        {
            try
            {
                ConnectionString connection;
                switch (_databaseType)
                {
                    case DatabaseType.MsSql:
                        connection = new ConnectionString()
                        {
                            DatabaseName = _configuration.ReadSetting("ConnectionString:Mssql:DatabaseName"),
                            Port = _configuration.ReadSetting("ConnectionString:Mssql:Port"),
                            ServerIp = _configuration.ReadSetting("ConnectionString:Mssql:ServerIp"),
                            Timeout = _configuration.ReadSetting("ConnectionString:Mssql:Timeout"),
                            UserName = _configuration.ReadSetting("ConnectionString:Mssql:Username"),
                            UserPassword = _configuration.ReadSetting("ConnectionString:Mssql:UserPassword")
                        };
                        break;
                    case DatabaseType.Oracle:
                        connection = new ConnectionString()
                        {
                            DatabaseName = _configuration.ReadSetting("ConnectionString:Oracle:DatabaseName"),
                            Port = _configuration.ReadSetting("ConnectionString:Oracle:Port"),
                            ServerIp = _configuration.ReadSetting("ConnectionString:Oracle:ServerIp"),
                            Timeout = _configuration.ReadSetting("ConnectionString:Oracle:Timeout"),
                            UserName = _configuration.ReadSetting("ConnectionString:Oracle:Username"),
                            UserPassword = _configuration.ReadSetting("ConnectionString:Oracle:UserPassword")
                        };
                        break;
                    default:
                        connection = null;
                        break;

                }
                return GetConnectionString(connection);
            }
            catch (Exception e)
            {
                throw new Exception($"Configden Connection String Okunamadı. -> {e.Message}; ");
            }

        }
        public string GetConnectionString(ConnectionString connectionString)
        {
            try
            {
                switch (_databaseType)
                {
                    case DatabaseType.MsSql:
                        _connectionString = $"Data Source={connectionString.ServerIp}{(!string.IsNullOrWhiteSpace(connectionString.Port) ? $",{connectionString.Port}" : "")}; " +
                                            "Persist Security Info=True; " +
                                            $"Initial Catalog={connectionString.DatabaseName}; " +
                                            $"User ID={connectionString.UserName}; " +
                                            $"Password={connectionString.UserPassword}; " +
                                            $"{(!string.IsNullOrWhiteSpace(connectionString.Timeout) ? $"Timeout={connectionString.Timeout};" : "Timeout = 720")}";
                        return _connectionString;
                    case DatabaseType.Oracle:
                        _connectionString = $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={connectionString.ServerIp})" +
                                            $"(PORT={(!string.IsNullOrWhiteSpace(connectionString.Port) ? connectionString.Port : "1521")}))" +
                                            $"(CONNECT_DATA = (SERVICE_NAME ={connectionString.DatabaseName}))); " +
                                            $"User Id={connectionString.UserName}; " +
                                            $"Password={connectionString.UserPassword}; " +
                                            $"enlist=true; " +
                                            $"{(!string.IsNullOrWhiteSpace(connectionString.Timeout) ? $"Connection Timeout = {connectionString.Timeout};" : "Connection Timeout = 720")}";
                        return _connectionString;
                    default:
                        return null;

                }



            }
            catch (Exception e)
            {
                throw new Exception($"Modelden Connection String Okunamadı. -> {e.Message}");
            }

        }
        public string GetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
            return _connectionString;
        }
        public System.Data.Common.DbConnection GetConnection()
        {
            switch (_databaseType)
            {
                case DatabaseType.MsSql:
                    _dbConnection = new SqlConnection(GetConnectionString());
                    break;
                case DatabaseType.Oracle:
                    _dbConnection = new OracleConnection(GetConnectionString());
                    break;
                default:
                    _dbConnection = null;
                    break;
            }

            return _dbConnection;
        }
        public System.Data.Common.DbConnection GetConnection(string connectionString)
        {
            switch (_databaseType)
            {
                case DatabaseType.MsSql:
                    _dbConnection = new SqlConnection(GetConnectionString(connectionString));
                    break;
                case DatabaseType.Oracle:
                    _dbConnection = new OracleConnection(GetConnectionString(connectionString));
                    break;
                default:
                    _dbConnection = null;
                    break;
            }

            return _dbConnection;
        }
        public System.Data.Common.DbConnection GetConnection(ConnectionString connectionString)
        {
            switch (_databaseType)
            {
                case DatabaseType.MsSql:
                    _dbConnection = new SqlConnection(GetConnectionString(connectionString));
                    break;
                case DatabaseType.Oracle:
                    _dbConnection = new OracleConnection(GetConnectionString(connectionString));
                    break;
                default:
                    _dbConnection = null;
                    break;
            }

            return _dbConnection;
        }

        public bool TestConnection()
        {
            try
            {
                return _databaseType switch
                {
                    DatabaseType.MsSql => _dbConnection.QueryFirst<int>("select 1+1") == 2,
                    DatabaseType.Oracle => _dbConnection.QueryFirst<int>("select 1+1 from dual") == 2,
                    _ => false
                };
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
