using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ReactApp1.Server.Models.Entities;
using ReactApp1.Server.Models.Model;

namespace ReactApp1.Server.Services
{
    public interface ISalesService
    {
        Task<List<Sales>> ListSales();
        Task<Sales> GetSales(int carId);
        Task<bool> AddSale(NewSale sale);
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

        public async Task<bool> AddSale(NewSale sale)
        {
            if (sale.Percentage > 0)
            {
                Sales s = new Sales();
                s.Percentage = sale.Percentage;
                s.CarId = sale.CarId;
                s.Description = sale.Description;

                await _context.AddAsync(s);
                await _context.SaveChangesAsync();

                var car = await _context.Car.FirstOrDefaultAsync(s => s.Id == sale.CarId);
                string msg = "A new " + sale.Percentage + "% sale has been added to: " + car.Brand + " " + car.Model;

                await WebSocketHelper.NotifyClients(msg);
                return true;
            }
            return false;

        }
    }
}
