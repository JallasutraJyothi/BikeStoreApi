using Bike_Store_App_WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bike_Store_App_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISalesService _salesService;

        public SalesController(IProductService productService, ICategoryService categoryService, ISalesService salesService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _salesService = salesService;
        }
        // View Sales Reports
        [HttpGet("sales/reports")]
        public async Task<IActionResult> GetSalesReports(string frequency)
        {
            var reports = await _salesService.GetSalesReports(frequency);
            return Ok(reports);
        }
    }
}
