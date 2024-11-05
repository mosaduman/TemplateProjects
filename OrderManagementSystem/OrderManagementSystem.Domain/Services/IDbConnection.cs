using Microsoft.Extensions.Configuration;

namespace OrderManagementSystem.Domain.Services
{
    public interface IDbConnection
    {
        string GetConnectionString();
        string GetConnectionString(ConnectionString connectionString);
        string GetConnectionString(string connectionString);
        System.Data.Common.DbConnection GetConnection();
        System.Data.Common.DbConnection GetConnection(string connectionString);
        System.Data.Common.DbConnection GetConnection(ConnectionString connectionString);
        bool TestConnection();
    }
}
