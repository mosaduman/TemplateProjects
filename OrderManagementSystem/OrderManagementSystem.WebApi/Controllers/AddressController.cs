using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Core.Filters;
using OrderManagementSystem.Domain.Models.Databases.SqlServer;
using OrderManagementSystem.Domain.Services.Databases.SqlServer.Customer;
using System.Net;

namespace OrderManagementSystem.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

     

        [HttpGet]
        [Route("GetAddreses/{customerId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<Address>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAddresesAsync(int customerId)
        {
            var result = await _addressService.GetAddressesWithCustomerIdAsync(customerId);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAddres/{id}")]
        [ProducesResponseType(typeof(ServiceResponse<Address>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAddresAsync(int id)
        {
            var result = await _addressService.GetAddressWithIdAsync(id);
             return Ok(result);
        }

        [HttpPost]
        [Route("GetAddresses")]
        [ProducesResponseType(typeof(ServiceResponse<List<Address>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAddressesAsync([FromBody] DataSourceRequest request)
        {
            var result = await _addressService.GetFilteredDataAsync(new ServiceRequest<DataSourceRequest>
            {
                Page = new Page(),
                Result = request,
            });
            return Ok(result);
        }

        [HttpPost]
        [Route("InsertAddresses")]
        [ProducesResponseType(typeof(ServiceResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> InsertAddressesAsync([FromBody] List<Address> addresses)
        {
            var result = await _addressService.BulkInsertAsync(new ServiceRequest<List<Address>>
            {
                Result = addresses
            });
            return Ok(result);
        }


        [HttpPut]
        [Route("UpdateAddresses")]
        [ProducesResponseType(typeof(ServiceResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAddressesAsync([FromBody] List<Address> addresses)
        {
            var result = await _addressService.BulkInsertAsync(new ServiceRequest<List<Address>>
            {
                Result = addresses
            });
            return Ok(result);
        }


        [HttpDelete]
        [Route("DeleteAddresses")]
        [ProducesResponseType(typeof(ServiceResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAddressesAsync(List<int> addresIds)
        {
            var result = await _addressService.DeleteAddressesWithIdsAsync(addresIds);
            return Ok(result);
        }
    }
}
