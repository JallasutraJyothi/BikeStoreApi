using AutoMapper;
using Bike_Store_App_WebApi.Data;
using Bike_Store_App_WebApi.DTO;
using Bike_Store_App_WebApi.Exceptions;
using Bike_Store_App_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Bike_Store_App_WebApi.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly BikeStoreContext _context;
        private readonly IMapper _mapper;
        public CategoryService(BikeStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> AddCategory(CategoryDTO categoryDTO)
        {
            var existingCategory = await _context.Categories
        .FirstOrDefaultAsync(c => c.CategoryName == categoryDTO.CategoryName); // Replace with the appropriate unique property

            if (existingCategory != null)
            {
                // Throw an exception with a message indicating the category already exists
                throw new System.InvalidOperationException("Category already exists."); // You can customize this message as needed
            }

            // Map DTO to Category entity and add it to the context
            var category = _mapper.Map<Category>(categoryDTO);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // Fetch the saved category to return with its related entities if needed
            return _mapper.Map<CategoryDTO>(category);
        }
        public async Task DeleteCategory(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
                throw new EntityNotFoundException("Category not found");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetCategoryById(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
                throw new EntityNotFoundException("Category not found");

            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
