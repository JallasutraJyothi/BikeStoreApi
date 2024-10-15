using Bike_Store_App_WebApi.DTO;
using Bike_Store_App_WebApi.Models;

namespace Bike_Store_App_WebApi.Services
{
    public interface ICategoryService
    {
        Task<CategoryDTO> AddCategory(CategoryDTO categoryDTO);
        Task DeleteCategory(int categoryId);
        Task<IEnumerable<CategoryDTO>> GetAllCategories();
        Task<CategoryDTO> GetCategoryById(int categoryId);
    }
}
