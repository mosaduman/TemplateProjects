using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.WebUI.Api;
using OrderManagementSystem.WebUI.Extensions;

namespace OrderManagementSystem.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReadProducts([Kendo.Mvc.UI.DataSourceRequest] Kendo.Mvc.UI.DataSourceRequest request)
        {
            var r = request.ToFilterRequest("Id");
            var result = await _productService.GetProducts(r);

            return Json(result.Result);

        }
    }
}
