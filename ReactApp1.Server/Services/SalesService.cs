using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ReactApp1.Server.Models.Entities;

namespace ReactApp1.Server.Services
{
    public interface ISalesService
    {
        Task<List<Sales>> ListSales();
        Task<Sales> GetSales(int carId);
    }

    public class SalesService : ISalesService
    {
        private readonly ICarService _carService;
        private readonly DataContext _context;

        public SalesService(ICarService carService, DataContext context) 
        {
            _carService = carService;
            _context = context;
        }

        public async Task<List<Sales>> ListSales()
        {
            var sales = await _context.Sales.ToListAsync();
            return sales;
        }

        public async Task<Sales> GetSales(int carId)
        {
            var salesById = await _context.Sales.FirstOrDefaultAsync(s => s.CarId == carId);
            return salesById;
        }
    }
}
