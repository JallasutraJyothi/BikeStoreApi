
using Bike_Store_App_WebApi.Data;
using Bike_Store_App_WebApi.MappingProfile;
using Bike_Store_App_WebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace BikeStoreApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();


            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddDbContext<BikeStoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IInventoryService, InventoryService>();
            builder.Services.AddScoped<ISalesService, SalesService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
