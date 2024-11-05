using OrderManagementSystem.Core.Filters;
using OrderManagementSystem.WebUI.Api.Models;

namespace OrderManagementSystem.WebUI.Api;

public interface ICustomerService
{
    Task<ServiceResponse<Customer>> GetCustomerWithId(int id);
    Task<ServiceResponse<List<Customer>>> GetCustomers(DataSourceRequest request);
    Task<ServiceResponse> InsertCustomers(List<Customer> request);
    Task<ServiceResponse> UpdateCustomers(List<Customer> request);
    Task<ServiceResponse> DeleteCustomers(List<int> request);
}