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
            var category = _mapper.Map<Category>(categoryDTO);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
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
