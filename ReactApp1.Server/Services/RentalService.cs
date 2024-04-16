using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ReactApp1.Server.Models.Entities;

namespace ReactApp1.Server.Services
{
    public interface IRentalService // interface 
    {
        Task<List<Rental>> ListRentals();
        Task<List<Rental>> GetRentals(int carId);
        Task<bool> ValidDate(int carId, string _fromDate, string _toDate);
        Task<List<Rental>> GetUserRentals(string userName);
        Task<int> CountPrice(int carId, string _fromDate, string _toDate);
        Task<bool> NewReservation(string userName, int carId, string _fromDate, string _toDate);
        Task<bool> DeleteReservation(int rentalId);
    }

    public class RentalService : IRentalService
    {
        private readonly ICarService _carService;
        private readonly IAuthService _authService;
        private readonly DataContext _context;

        public RentalService(ICarService carService, IAuthService authService, DataContext context)
        {
            _carService = carService;
            _authService = authService;
            _context = context;
        }

        public async Task<List<Rental>> ListRentals()
        {
            var rentals = await _context.Rental.ToListAsync();
            return rentals;
        }

        public async Task<List<Rental>> GetRentals(int carId)
        {
            var rentals = await ListRentals();
            List<Rental> rentalsById = new List<Rental>();
            foreach (var rental in rentals)
            {
                if (rental.CarId == carId) { rentalsById.Add(rental); }
            }
            return rentalsById;
        }

        public async Task<List<Rental>> GetUserRentals(string userName)
        {
            var rentals = await ListRentals();
            List<Rental> rentalsByUserId = new List<Rental>();
            foreach (var rental in rentals)
            {
                if (rental.User.UserName == userName) { rentalsByUserId.Add(rental); }
            }
            return rentalsByUserId;
        }

        public async Task<bool> ValidDate(int carId, string _fromDate, string _toDate)
        {
            DateTime fromDate = DateTime.Parse(_fromDate);
            DateTime toDate = DateTime.Parse(_toDate);
            bool validDate = false;
            bool dateIsFree = false;
            bool checkFromDate = true, checkToDate = true, checkInBetween = true;
            var rentals = await ListRentals();
            foreach (var rental in rentals)
            {
                if (rental.CarId == carId)
                {
                    if (fromDate >= rental.FromDate && fromDate <= rental.ToDate) { checkFromDate = false; }
                    if (toDate >= rental.FromDate && toDate <= rental.ToDate) { checkToDate = false; }
                    if ((rental.FromDate >= fromDate && rental.ToDate >= fromDate) && (rental.FromDate <= toDate && rental.ToDate <= toDate)) { checkInBetween = false; }
                }
            }

            if (fromDate <= toDate) { validDate = true; }
            if (checkFromDate && checkToDate) { dateIsFree = true; }
            if (validDate && dateIsFree && checkInBetween) { return true; }
            return false;
        }

        public async Task<int> CountPrice(int carId, string _fromDate, string _toDate)
        {
            DateTime fromDate = DateTime.Parse(_fromDate);
            DateTime toDate = DateTime.Parse(_toDate);
            TimeSpan difference = toDate - fromDate;
            int days = difference.Days + 1;
            List<Car> carTypes = await _carService.ListCars();
            foreach (var carType in carTypes)
            {
                if(carType.Id == carId) 
                {
                    return days * carType.DailyPrice;
                }
            }
            return 0;
        }

        public async Task<bool> NewReservation(string userName, int carId, string _fromDate, string _toDate)
        {

            Rental rm = new Rental();

            DateTime fromDate = DateTime.Parse(_fromDate);
            DateTime toDate = DateTime.Parse(_toDate);
            DateTime created = DateTime.UtcNow;

            rm.CarId = carId;
            rm.UserId = _authService.GetUserId(userName).Result;
            rm.FromDate = fromDate;
            rm.ToDate = toDate;
            rm.Created = created;

            var carExists = await _context.Car.FirstOrDefaultAsync(c => c.Id == carId);
            var idExists = await _context.User.FirstOrDefaultAsync(u => u.Id == rm.UserId);

            if (carExists != null && idExists != null && ValidDate(carId, _fromDate, _toDate).Result)
            {
                await _context.AddAsync(rm);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteReservation(int rentalId)
        {
            var rental = await _context.Rental.FirstAsync(r => r.Id == rentalId);
            if (rental != null && rental.Id == rentalId)
            {
                _context.Rental.Remove(rental);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}