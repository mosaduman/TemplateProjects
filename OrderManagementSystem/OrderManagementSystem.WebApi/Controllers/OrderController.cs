using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Core.Filters;
using OrderManagementSystem.Domain.Models.Databases.SqlServer;
using OrderManagementSystem.Domain.Services.Databases.SqlServer.Order;
using System.Net;

namespace OrderManagementSystem.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        [Route("GetFullOrder/{id}")]
        [ProducesResponseType(typeof(ServiceResponse<Order>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFullOrderAsync(int id)
        {
            var result = await _orderService.GetFullOrderAsync(new ServiceRequest<int>
            {
                Result = id
            });
            return Ok(result);
        }
        [HttpGet]
        [Route("GetOrder/{id}")]
        [ProducesResponseType(typeof(ServiceResponse<Order>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderAsync(int id)
        {
            var result = await _orderService.GetOrderWithIdAsync(id);
            return Ok(result);
        }




        [HttpPost]
        [Route("GetOrders")]
        [ProducesResponseType(typeof(ServiceResponse<List<Order>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrdersAsync([FromBody] DataSourceRequest request)
        {
            var result = await _orderService.GetFilteredDataAsync(new ServiceRequest<DataSourceRequest>
            {
                Page = new Page(),
                Result = request,
            });
            return Ok(result);
        }

    }
}
