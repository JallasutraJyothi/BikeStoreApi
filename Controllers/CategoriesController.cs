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
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISalesService _salesService;

        public CategoriesController(IMapper mapper ,IProductService productService, ICategoryService categoryService, ISalesService salesService)
        {
            _mapper = mapper;
            _productService = productService;
            _categoryService = categoryService;
            _salesService = salesService;
        }

        // Manage Categories
        [HttpPost("categories")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                var createdCategory = await _categoryService.AddCategory(categoryDTO);
                return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.CategoryId }, createdCategory);
            }
            catch (System.InvalidOperationException ex)
            {
                // Return a Conflict response if the category already exists
                return Conflict(ex.Message); // HTTP 409 Conflict
            }
            catch (Exception ex)
            {
                // Handle other exceptions as needed
                return StatusCode(500, "Internal server error: " + ex.Message); // HTTP 500 Internal Server Error
            }
        }

        [HttpDelete("categories/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            await _categoryService.DeleteCategory(categoryId);
            return NoContent();
        }


        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryById(id);
                return Ok(category);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
