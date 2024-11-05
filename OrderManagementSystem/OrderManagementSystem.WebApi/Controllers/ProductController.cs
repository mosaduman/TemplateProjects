using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Core.Filters;
using OrderManagementSystem.Domain.Models.Databases.SqlServer;
using OrderManagementSystem.Domain.Services.Databases.SqlServer.Product;
using System.Net;

namespace OrderManagementSystem.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }




        [HttpGet]
        [Route("GetProduct/{id}")]
        [ProducesResponseType(typeof(ServiceResponse<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await _productService.GetProductWithIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("GetProducts")]
        [ProducesResponseType(typeof(ServiceResponse<List<Product>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductsAsync([FromBody] DataSourceRequest request)
        {
            var result = await _productService.GetFilteredDataAsync(new ServiceRequest<DataSourceRequest>
            {
                Page = new Page(),
                Result = request,
            });
            return Ok(result);
        }

        [HttpPost]
        [Route("InsertProducts")]
        [ProducesResponseType(typeof(ServiceResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> InsertProductsAsync([FromBody] List<Product> products)
        {
            var result = await _productService.BulkInsertAsync(new ServiceRequest<List<Product>>
            {
                Result = products
            });
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateProducts")]
        [ProducesResponseType(typeof(ServiceResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProductsAsync([FromBody] List<Product> products)
        {
            var result = await _productService.BulkInsertAsync(new ServiceRequest<List<Product>>
            {
                Result = products
            });
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteProducts")]
        [ProducesResponseType(typeof(ServiceResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductsAsync(List<int> productIds)
        {
            var result = await _productService.DeleteProductsWithIdsAsync(productIds);
            return Ok(result);
        }
    }
}
