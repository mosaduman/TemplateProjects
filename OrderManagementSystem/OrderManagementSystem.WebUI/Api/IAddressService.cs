using System.Reflection;
using BasicExtensions;
using OrderManagementSystem.Core;
using OrderManagementSystem.Core.Filters;
using OrderManagementSystem.WebUI.Api.Models;

namespace OrderManagementSystem.WebUI.Api;

public interface IAddressService
{
    Task<ServiceResponse<Address>> GetAddressWithId(int id);
    Task<ServiceResponse<List<Address>>> GetAddressesWithCustomerId(int customerId);
    Task<ServiceResponse<List<Address>>> GetAddresses(DataSourceRequest request);
    Task<ServiceResponse> InsertAddresses(List<Address> request);
    Task<ServiceResponse> UpdateAddresses(List<Address> request);
    Task<ServiceResponse> DeleteAddresses(List<int> request);
}