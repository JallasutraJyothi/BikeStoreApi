using Bike_Store_App_WebApi.DTO;
using Bike_Store_App_WebApi.Models;

namespace Bike_Store_App_WebApi.Services
{
    public interface IProductService
    {
        Task<ProductDTO> AddProduct(ProductDTO productDTO);
        Task<ProductDTO> UpdateProduct(int productId, ProductDTO productDTO);
        Task DeleteProduct(int productId);
        Task<IEnumerable<ProductDTO>> GetAllProducts();
        Task<ProductDTO?> GetProductById(int productId);
        Task<List<ProductDTO>> GeBikesByCategory(string category);
        Task<Product> GetBikeByBrand(string brand);
        Task<Product> GetBikeBySearch(string searchBy, string filterValue);
    }
}
