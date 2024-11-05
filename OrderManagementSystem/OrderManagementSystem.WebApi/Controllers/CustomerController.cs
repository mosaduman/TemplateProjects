using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Core.Filters;
using OrderManagementSystem.Domain.Models.Databases.SqlServer;
using OrderManagementSystem.Domain.Services.Databases.SqlServer.Customer;
using OrderManagementSystem.Domain.Services.Databases.SqlServer.Order;
using System.Net;

namespace OrderManagementSystem.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Route("GetCustomer/{id}")]
        [ProducesResponseType(typeof(ServiceResponse<Customer>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await _customerService.GetCustomerWithIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("GetCustomers")]
        [ProducesResponseType(typeof(ServiceResponse<List<Customer>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomersAsync([FromBody] DataSourceRequest request)
        {
            var result = await _customerService.GetFilteredDataAsync(new ServiceRequest<DataSourceRequest>
            {
                Page = new Page(),
                Result = request,
            });
            return Ok(result);
        }

        [HttpPost]
        [Route("InsertCustomers")]
        [ProducesResponseType(typeof(ServiceResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> InsertCustomersAsync([FromBody] List<Customer> customers)
        {
            var result = await _customerService.BulkInsertAsync(new ServiceRequest<List<Customer>>
            {
                Result = customers
            });
            return Ok(result);
        }


        [HttpPut]
        [Route("UpdateCustomers")]
        [ProducesResponseType(typeof(ServiceResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCustomersAsync([FromBody] List<Customer> customers)
        {
            var result = await _customerService.BulkInsertAsync(new ServiceRequest<List<Customer>>
            {
                Result = customers
            });
            return Ok(result);
        }


        [HttpDelete]
        [Route("DeleteCustomers")]
        [ProducesResponseType(typeof(ServiceResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCustomersAsync(List<int> customerIds)
        {
            var result = await _customerService.DeleteCustomersWithIdsAsync(customerIds);
            return Ok(result);
        }
    }
}
