using AutoMapper;
using Bike_Store_App_WebApi.DTO;
using Bike_Store_App_WebApi.Exceptions;
using Bike_Store_App_WebApi.Models;
using Bike_Store_App_WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bike_Store_App_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISalesService _salesService;

        public BikeController(IMapper mapper,IProductService productService, ICategoryService categoryService, ISalesService salesService)
        {
            _mapper = mapper;
            _productService = productService;
            _categoryService = categoryService;
            _salesService = salesService;
        }

        // Manage Products
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDTO productDTO)
        {
            var createdProduct = await _productService.AddProduct(productDTO);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.ProductId }, createdProduct);
        }

        [HttpPut("products/{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductDTO productDTO)
        {
            var updatedProduct = await _productService.UpdateProduct(productId, productDTO);
            return Ok(updatedProduct);
        }


        [HttpDelete("products/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            await _productService.DeleteProduct(productId);
            return NoContent();
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet("byCategory/{category}")]
        public async Task<IActionResult> GetBikesByCategory(string category)
        {
            var result = await _productService.GeBikesByCategory(category);
            return Ok(result);
        }

        [HttpGet("byBrand/{brand}")]
        public async Task<IActionResult> GetBikeByBrand(string brand)
        {
            var result = await _productService.GetBikeByBrand(brand);
            if (result == null)
                return NotFound("Bike not found");

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetBikeBySearch(string searchBy, string filterValue)
        {
            var result = await _productService.GetBikeBySearch(searchBy, filterValue);
            if (result == null)
                return NotFound("No bikes found with this criteria");

            return Ok(result);
        }
    }
}
