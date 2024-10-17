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
            try
            {
                var createdProduct = await _productService.AddProduct(productDTO);
                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.ProductId }, createdProduct);
            }
            catch (System.InvalidOperationException ex)
            {
                // Return a Conflict response if the product already exists
                return Conflict(ex.Message); // HTTP 409 Conflict
            }
            catch (Exception ex)
            {
                // Handle other exceptions as needed
                return StatusCode(500, "Internal server error: " + ex.Message); // HTTP 500 Internal Server Error
            }
        }

        [HttpPut("products/{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return BadRequest("Product data must be provided.");
            }

            try
            {
                // Call the service to update the product
                var updatedProduct = await _productService.UpdateProduct(productId, productDTO);

                // Return a response with the updated product data
                return Ok(updatedProduct); // HTTP 200 OK with the updated product
            }
            catch (EntityNotFoundException ex)
            {
                // Return a Not Found response if the product does not exist
                return NotFound(ex.Message); // HTTP 404 Not Found
            }
            catch (System.InvalidOperationException ex)
            {
                // Handle case where inventory entry is not found
                return Conflict(ex.Message); // HTTP 409 Conflict
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return StatusCode(500, "Internal server error: " + ex.Message); // HTTP 500 Internal Server Error
            }
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
            var result = await _productService.GetBikesByCategory(category);
            return Ok(result);
        }

        [HttpGet("byBrand/{brand}")]
        public async Task<IActionResult> GetBikeByBrand(string brand)
        {
            var product = await _productService.GetBikeByBrand(brand);
            if (product == null)
                return NotFound("Bike not found");

            var productDTO = _mapper.Map<ProductDTO>(product);
            return Ok(productDTO);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetBikeBySearch(string searchBy, string filterValue)
        {
            var product = await _productService.GetBikeBySearch(searchBy, filterValue);
            if (product == null)
                return NotFound("No bikes found with this criteria");

            var productDTO = _mapper.Map<ProductDTO>(product);
            return Ok(productDTO);
        }
    }
}
