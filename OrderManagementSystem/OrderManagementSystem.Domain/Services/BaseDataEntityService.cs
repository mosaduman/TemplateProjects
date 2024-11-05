using BasicExtensions;
using Dapper;
using Microsoft.Extensions.Configuration;
using OrderManagementSystem.Core;
using OrderManagementSystem.Core.Filters;
using OrderManagementSystem.Domain.Configurations.Mappers.Dappers;
using OrderManagementSystem.Domain.Models.Databases;
using Z.Dapper.Plus;

namespace OrderManagementSystem.Domain.Services
{
    public abstract class BaseDataEntityService<T> where T : class, IBaseEntity, new()
    {
        private int _commandTimeout;
        protected string ConnectionString;
        protected DbConnection Connection;

        protected BaseDataEntityService(IConfiguration configuration)
        {
            Connection = new DbConnection(configuration);
            ConnectionString = Connection.GetConnectionString();
            var data = configuration.ReadSetting("ConnectionString:CommandTimeout");
            _commandTimeout = int.Parse(string.IsNullOrWhiteSpace(data) ? "10" : data);
            DapperPlusMapRoles.Run();
        }
        protected BaseDataEntityService(IConfiguration configuration, string connectionString)
        {
            Connection = new DbConnection(configuration);
            ConnectionString = Connection.GetConnectionString(connectionString);
            var data = configuration.ReadSetting("ConnectionString:CommandTimeout");
            _commandTimeout = int.Parse(string.IsNullOrWhiteSpace(data) ? "10" : data);
            DapperPlusMapRoles.Run();
        }
        protected BaseDataEntityService(IConfiguration configuration, ConnectionString connectionString)
        {
            Connection = new DbConnection(configuration);
            ConnectionString = Connection.GetConnectionString(connectionString);
            DapperPlusMapRoles.Run();
            var data = configuration.ReadSetting("ConnectionString:CommandTimeout");
            _commandTimeout = int.Parse(string.IsNullOrWhiteSpace(data) ? "10" : data);
        }

        protected async Task<ServiceResponse<T>> GetAsync(string query, object param = null)
        {
            var response = new ServiceResponse<T>();
            try
            {
                await using (var connection = Connection.GetConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    var result = await connection.QueryFirstOrDefaultAsync<T>(query, param, commandTimeout: _commandTimeout);
                    response.Result = result;
                }
            }
            catch (Exception e)
            {
                var errorMessage =
                    $"Method: {System.Reflection.MethodBase.GetCurrentMethod().GetMethodFullName()}. Operation : Generic {System.Reflection.MethodBase.GetCurrentMethod()?.Name} ({typeof(T).Name}) ";
                response.ErrorMessages.Add(errorMessage);
                response.ErrorMessages.Add(e.Message);
                if (e?.StackTrace != null)
                    response.ErrorMessages.Add(e.StackTrace);
                if (e?.InnerException != null)
                    response.ErrorMessages.Add(e.InnerException.Message);

                errorMessage.ErrorLog(ex: e);
            }
            return response;
        }
        public async Task<ServiceResponse<T1>> GetAsync<T1>(string query, object param = null)
        {
            var response = new ServiceResponse<T1>();
            try
            {
                await using (var connection = Connection.GetConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    var result = await connection.QueryFirstOrDefaultAsync<T1>(query, param, commandTimeout: _commandTimeout);
                    response.Result = result;
                }
            }
            catch (Exception e)
            {
                var errorMessage =
                    $"Method: {System.Reflection.MethodBase.GetCurrentMethod().GetMethodFullName()}. Operation : Generic {System.Reflection.MethodBase.GetCurrentMethod()?.Name} ({typeof(T).Name}) ";
                response.ErrorMessages.Add(errorMessage);
                response.ErrorMessages.Add(e.Message);
                if (e?.StackTrace != null)
                    response.ErrorMessages.Add(e.StackTrace);
                if (e?.InnerException != null)
                    response.ErrorMessages.Add(e.InnerException.Message);

                errorMessage.ErrorLog(ex: e);
            }
            return response;
        }
        public async Task<ServiceResponse<List<T1>>> GetListAsync<T1>(string query, object param = null)
        {
            var response = new ServiceResponse<List<T1>>();
            try
            {
                await using (var connection = Connection.GetConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    var result = await connection.QueryAsync<T1>(query, param, commandTimeout: _commandTimeout);
                    response.Result = result.ToList();
                }
            }
            catch (Exception e)
            {
                var errorMessage =
                    $"Method: {System.Reflection.MethodBase.GetCurrentMethod().GetMethodFullName()}. Operation : Generic {System.Reflection.MethodBase.GetCurrentMethod()?.Name} ({typeof(T).Name}) ";
                response.ErrorMessages.Add(errorMessage);
                response.ErrorMessages.Add(e.Message);
                if (e?.StackTrace != null)
                    response.ErrorMessages.Add(e.StackTrace);
                if (e?.InnerException != null)
                    response.ErrorMessages.Add(e.InnerException.Message);

                errorMessage.ErrorLog(ex: e);
            }
            return response;
        }
        public async Task<ServiceResponse<int>> ExecuteAsync(string query, object param = null)
        {
            var response = new ServiceResponse<int>();
            try
            {
                await using (var connection = Connection.GetConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    response.Result = await connection.ExecuteAsync(query, param, commandTimeout: _commandTimeout);
                }
            }
            catch (Exception e)
            {
                var errorMessage =
                    $"Method: {System.Reflection.MethodBase.GetCurrentMethod().GetMethodFullName()}. Operation : Generic {System.Reflection.MethodBase.GetCurrentMethod()?.Name} ({typeof(T).Name}) ";
                response.ErrorMessages.Add(errorMessage);
                response.ErrorMessages.Add(e.Message);
                if (e?.StackTrace != null)
                    response.ErrorMessages.Add(e.StackTrace);
                if (e?.InnerException != null)
                    response.ErrorMessages.Add(e.InnerException.Message);

                errorMessage.ErrorLog(ex: e);
            }
            return response;
        }
        public async Task<ServiceResponse<List<T>>> GetListAsync(string query, object param = null)
        {
            var response = new ServiceResponse<List<T>>();
            try
            {
                await using (var connection = Connection.GetConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    var result = await connection.QueryAsync<T>(query, param, commandTimeout: _commandTimeout);
                    response.Result = result.ToList();
                }
            }
            catch (Exception e)
            {
                var errorMessage =
                    $"Method: {System.Reflection.MethodBase.GetCurrentMethod().GetMethodFullName()}. Operation : Generic {System.Reflection.MethodBase.GetCurrentMethod()?.Name} ({typeof(T).Name}) ";
                response.ErrorMessages.Add(errorMessage);
                response.ErrorMessages.Add(e.Message);
                if (e?.StackTrace != null)
                    response.ErrorMessages.Add(e.StackTrace);
                if (e?.InnerException != null)
                    response.ErrorMessages.Add(e.InnerException.Message);

                errorMessage.ErrorLog(ex: e);
            }
            return response;
        }
        public async Task<ServiceResponse> BulkInsertAsync(ServiceRequest<List<T>> entities)
        {
            var response = new ServiceResponse();
            try
            {
                await using (var connection = Connection.GetConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    await using (var transaction = await connection.BeginTransactionAsync())
                    {
                        try
                        {
                            await transaction.BulkActionAsync(s => s.BulkInsert(entities.Result));
                            await transaction.CommitAsync();
                        }
                        catch (Exception)
                        {
                            await transaction.RollbackAsync();
                            throw;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var errorMessage =
                    $"Method: {System.Reflection.MethodBase.GetCurrentMethod().GetMethodFullName()}. Operation : Generic {System.Reflection.MethodBase.GetCurrentMethod()?.Name} ({typeof(T).Name}) ";
                response.ErrorMessages.Add(errorMessage);
                response.ErrorMessages.Add(e.Message);
                if (e?.StackTrace != null)
                    response.ErrorMessages.Add(e.StackTrace);
                if (e?.InnerException != null)
                    response.ErrorMessages.Add(e.InnerException.Message);

                errorMessage.ErrorLog(ex: e);
            }
            return response;


        }
        public async Task<ServiceResponse> BulkUpdateAsync(ServiceRequest<List<T>> entities)
        {
            var response = new ServiceResponse();
            try
            {
                await using (var connection = Connection.GetConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    await using (var transaction = await connection.BeginTransactionAsync())
                    {
                        try
                        {
                            await transaction.BulkActionAsync(s => s.BulkUpdate(entities.Result));
                            await transaction.CommitAsync();
                        }
                        catch (Exception)
                        {
                            await transaction.RollbackAsync();
                            throw;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var errorMessage =
                    $"Method: {System.Reflection.MethodBase.GetCurrentMethod().GetMethodFullName()}. Operation : Generic {System.Reflection.MethodBase.GetCurrentMethod()?.Name} ({typeof(T).Name}) ";
                response.ErrorMessages.Add(errorMessage);
                response.ErrorMessages.Add(e.Message);
                if (e?.StackTrace != null)
                    response.ErrorMessages.Add(e.StackTrace);
                if (e?.InnerException != null)
                    response.ErrorMessages.Add(e.InnerException.Message);

                errorMessage.ErrorLog(ex: e);
            }
            return response;
        }
        public async Task<ServiceResponse> BulkDeleteAsync(ServiceRequest<List<T>> entities)
        {
            var response = new ServiceResponse();
            try
            {
                await using (var connection = Connection.GetConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    await using (var transaction = await connection.BeginTransactionAsync())
                    {
                        try
                        {
                            await transaction.BulkActionAsync(s => s.BulkDelete(entities.Result));
                            await transaction.CommitAsync();
                        }
                        catch (Exception)
                        {
                            await transaction.RollbackAsync();
                            throw;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var errorMessage =
                    $"Method: {System.Reflection.MethodBase.GetCurrentMethod().GetMethodFullName()}. Operation : Generic {System.Reflection.MethodBase.GetCurrentMethod()?.Name} ({typeof(T).Name}) ";
                response.ErrorMessages.Add(errorMessage);
                response.ErrorMessages.Add(e.Message);
                if (e?.StackTrace != null)
                    response.ErrorMessages.Add(e.StackTrace);
                if (e?.InnerException != null)
                    response.ErrorMessages.Add(e.InnerException.Message);

                errorMessage.ErrorLog(ex: e);
            }
            return response;
        }
        public async Task<ServiceResponse> BulkSaveAsync(ServiceRequest<List<T>> entities)
        {
            var response = new ServiceResponse();
            try
            {
                await using (var connection = Connection.GetConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    await using (var transaction = await connection.BeginTransactionAsync())
                    {
                        try
                        {
                            await transaction.BulkActionAsync(s => s.BulkMerge(entities.Result));
                            await transaction.CommitAsync();
                        }
                        catch (Exception)
                        {
                            await transaction.RollbackAsync();
                            throw;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var errorMessage =
                    $"Method: {System.Reflection.MethodBase.GetCurrentMethod().GetMethodFullName()}. Operation : Generic {System.Reflection.MethodBase.GetCurrentMethod()?.Name} ({typeof(T).Name}) ";
                response.ErrorMessages.Add(errorMessage);
                response.ErrorMessages.Add(e.Message);
                if (e?.StackTrace != null)
                    response.ErrorMessages.Add(e.StackTrace);
                if (e?.InnerException != null)
                    response.ErrorMessages.Add(e.InnerException.Message);

                errorMessage.ErrorLog(ex: e);
            }
            return response;
        }
        public async Task<ServiceResponse<List<T>>> GetFilteredDataAsync(ServiceRequest<DataSourceRequest> request)
        {
            var response = new ServiceResponse<List<T>>();
            try
            {
                using (var connection = Connection.GetConnection(ConnectionString))
                {
                    var sql = $"SELECT * FROM {typeof(T).Name} ";

                    var filteredSql = sql.GetFilterString(request.Result);
                    var filteredCountSql = sql.GetFilterCountString(request.Result);
                    var result = await connection.QueryAsync<T>(filteredSql, commandTimeout: _commandTimeout);
                    var count = await connection.QueryFirstOrDefaultAsync<int>(filteredCountSql, commandTimeout: _commandTimeout);
                    response.Result = result.ToList();
                    response.Page = new Page()
                    {
                        PageNumber = request.Result.PageNumber,
                        PageSize = result.Count(),
                        TotalSize = count
                    };
                }
            }
            catch (Exception e)
            {
                var errorMessage =
                    $"Method: {System.Reflection.MethodBase.GetCurrentMethod().GetMethodFullName()}. Operation : Generic {System.Reflection.MethodBase.GetCurrentMethod()?.Name} ({typeof(T).Name}) ";
                response.ErrorMessages.Add(errorMessage);
                response.ErrorMessages.Add(e.Message);
                if (e?.StackTrace != null)
                    response.ErrorMessages.Add(e.StackTrace);
                if (e?.InnerException != null)
                    response.ErrorMessages.Add(e.InnerException.Message);

                errorMessage.ErrorLog(ex: e);
            }
            return response;
        }
    }
}
