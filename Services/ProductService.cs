using AutoMapper;
using Bike_Store_App_WebApi.Data;
using Bike_Store_App_WebApi.DTO;
using Bike_Store_App_WebApi.Exceptions;
using Bike_Store_App_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Bike_Store_App_WebApi.Services
{
    public class ProductService:IProductService
    {
        private readonly BikeStoreContext _context;
        private readonly IMapper _mapper;
        public ProductService(BikeStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDTO> AddProduct(ProductDTO productDTO)
        {
            var existingProduct = await _context.Products
        .FirstOrDefaultAsync(p => p.ProductName == productDTO.ProductName); 

            if (existingProduct != null)
            {
                
                throw new System.InvalidOperationException("Product already exists.");
            }

            var product = _mapper.Map<Product>(productDTO);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var savedProduct = await _context.Products
                .Include(p => p.Brand) 
                .Include(p => p.Category) 
                .FirstOrDefaultAsync(p => p.ProductId == product.ProductId);

            return _mapper.Map<ProductDTO>(savedProduct);
        }

        public async Task<ProductDTO> UpdateProduct(int productId, ProductDTO productDTO)
        {
            var existingProduct = await _context.Products
        .Include(p => p.Brand)   
        .Include(p => p.Category)
        .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (existingProduct == null)
                throw new EntityNotFoundException("Product not found");

            
            existingProduct.ProductName = productDTO.ProductName;
            existingProduct.Description = productDTO.Description;
            existingProduct.Price = productDTO.Price;
            existingProduct.StockQuantity = productDTO.StockQuantity;
            existingProduct.CategoryId = productDTO.CategoryId;
            existingProduct.BrandId = productDTO.BrandId; 

           
            await _context.SaveChangesAsync();

            
            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(i => i.ProductId == productId);

            if (inventory != null)
            {
                inventory.Quantity = productDTO.StockQuantity; 
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new System.InvalidOperationException("Inventory entry not found for this product.");
            }

            return _mapper.Map<ProductDTO>(existingProduct);
        }


        public async Task DeleteProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new EntityNotFoundException("Product not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            var products = await _context.Products
        .Include(p => p.Brand)  
        .Include(p => p.Category) 
        .ToListAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetProductById(int productId)
        {
            var product = await _context.Products
        .Include(p => p.Brand) 
        .Include(p => p.Category) 
        .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
                throw new EntityNotFoundException("Product not found");

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<List<ProductDTO>> GetBikesByCategory(string category)
        {
            return await _context.Products
                .Where(p => p.Category.CategoryName == category)
                .Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryName = p.Category.CategoryName,
                    BrandName = p.Brand.BrandName,
                    StockQuantity = p.StockQuantity,

                    Image = p.Image
                }).ToListAsync();
        }

        public async Task<Product> GetBikeByBrand(string brand)
        {
            return await _context.Products
        .Include(p => p.Brand) 
        .Include(p => p.Category)
        .FirstOrDefaultAsync(p => p.Brand.BrandName == brand);
        }

        public async Task<Product> GetBikeBySearch(string searchBy, string filterValue)
        {
            IQueryable<Product> query = _context.Products
        .Include(p => p.Brand)
        .Include(p => p.Category); 

            if (searchBy == "Name")
            {
                return await query.FirstOrDefaultAsync(p => p.ProductName.Contains(filterValue));
            }
            else if (searchBy == "Category")
            {
                return await query.FirstOrDefaultAsync(p => p.Category.CategoryName == filterValue);
            }
            else if (searchBy == "Price")
            {
                if (double.TryParse(filterValue, out double parsedPrice))
                {
                    return await query.FirstOrDefaultAsync(p => p.Price == parsedPrice);
                }
                else
                {
                    return null;
                }
            }
            else if (searchBy == "Brand")
            {
                return await query.FirstOrDefaultAsync(p => p.Brand.BrandName.Contains(filterValue));
            }

            return null;
        }

    }
}
