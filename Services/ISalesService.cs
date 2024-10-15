using Bike_Store_App_WebApi.Models;

namespace Bike_Store_App_WebApi.Services
{
    public interface ISalesService
    {
        Task<IEnumerable<SalesReport>> GetSalesReports(string frequency);
    }
}
