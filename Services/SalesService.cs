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
            // Ensure we are only processing daily data for storage
            if (frequency.ToLower() == "daily")
            {
                // Fetch daily sales data grouped by order date
                var salesData = await _context.Orders
                    .GroupBy(o => o.OrderDate.Date)
                    .Select(g => new SalesReport
                    {
                        Date = g.Key,
                        TotalSales = g.Sum(o => o.TotalPrice),
                        TotalOrders = g.Count()
                    })
                    .ToListAsync();

                // List to store daily reports
                foreach (var report in salesData)
                {
                    // Check if a report for the date already exists
                    var existingReport = await _context.SalesReports
                        .FirstOrDefaultAsync(r => r.Date == report.Date);

                    if (existingReport == null) // If no existing report, insert new
                    {
                        await _context.SalesReports.AddAsync(report);
                    }
                    else // If report exists, update it
                    {
                        existingReport.TotalSales += report.TotalSales;
                        existingReport.TotalOrders += report.TotalOrders;
                        _context.SalesReports.Update(existingReport);
                    }
                }

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Return the daily reports
                return salesData;
            }
            else
            {
                // For weekly and monthly reports, derive from existing daily data
                var existingReports = await _context.SalesReports.ToListAsync();

                return frequency.ToLower() switch
                {
                    "weekly" => existingReports
                        .GroupBy(x => x.Date.AddDays(-(int)x.Date.DayOfWeek)) // Group by the start of the week
                        .Select(g => new SalesReport
                        {
                            Date = g.Key,
                            TotalSales = g.Sum(x => x.TotalSales),
                            TotalOrders = g.Sum(x => x.TotalOrders)
                        })
                        .ToList(),
                    "monthly" => existingReports
                        .GroupBy(x => new DateTime(x.Date.Year, x.Date.Month, 1)) // Group by the month
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
}
