using Bike_Store_App_WebApi.Data;
using Bike_Store_App_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Bike_Store_App_WebApi.Services
{
    public class SalesService:ISalesService
    {
        private readonly BikeStoreContext _context;

        public SalesService(BikeStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalesReport>> GetSalesReports(string frequency)
        {
            // Here, we assume you have an `Order` table in your database to aggregate sales data
            var salesData = await _context.Orders
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new SalesReport
                {
                    Date = g.Key,
                    TotalSales = g.Sum(o => o.TotalPrice),
                    TotalOrders = g.Count()
                })
                .ToListAsync();

            return frequency switch
            {
                "daily" => salesData,
                "weekly" => salesData
                    .GroupBy(x => x.Date.AddDays(-(int)x.Date.DayOfWeek))
                    .Select(g => new SalesReport
                    {
                        Date = g.Key,
                        TotalSales = g.Sum(x => x.TotalSales),
                        TotalOrders = g.Sum(x => x.TotalOrders)
                    })
                    .ToList(),
                "monthly" => salesData
                    .GroupBy(x => new DateTime(x.Date.Year, x.Date.Month, 1))
                    .Select(g => new SalesReport
                    {
                        Date = g.Key,
                        TotalSales = g.Sum(x => x.TotalSales),
                        TotalOrders = g.Sum(x => x.TotalOrders)
                    })
                    .ToList(),
                _ => throw new ArgumentException("Invalid frequency")
            };
        }
    }
}
