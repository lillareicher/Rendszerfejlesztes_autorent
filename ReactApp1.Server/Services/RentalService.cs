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
        Task<List<Rental>> GetUserRentals(int userId);
        Task<int> CountPrice(int carId, string _fromDate, string _toDate);
        Task<List<Rental>> NewReservation(int userId, int carId, string _fromDate, string _toDate);
    }

    public class RentalService : IRentalService
    {
        bool firstRun = true;
        int rentCount = 5;
        private readonly ICarService _carService;
        private readonly DataContext _context;

        public RentalService(ICarService carService, DataContext context)
        {
            _carService = carService;
            _context = context;
        }

        public async Task<List<Rental>> ListRentals()
        {
            var rentals = await _context.Rental.ToListAsync();
            //if (firstRun) {

            //    Rental rm1 = new Rental();
            //    Rental rm2 = new Rental();
            //    Rental rm3 = new Rental();
            //    Rental rm4 = new Rental();

            //    rm1.Id = 1;
            //    rm1.UserId = 1;
            //    rm1.CarId = 1;
            //    rm1.FromDate = new DateTime(2024, 03, 28);
            //    rm1.ToDate = new DateTime(2024, 03, 30);
            //    rm1.Created = new DateTime(2024, 03, 26);

            //    rm2.Id = 1;
            //    rm2.UserId = 1;
            //    rm2.CarId = 1;
            //    rm2.FromDate = new DateTime(2024, 04, 28);
            //    rm2.ToDate = new DateTime(2024, 04, 30);
            //    rm2.Created = new DateTime(2024, 04, 26);

            //    rm3.Id = 1";
            //    rm3.UserId = 1;
            //    rm3.CarId = "c2";
            //    rm3.FromDate = new DateTime(2024, 05, 28);
            //    rm3.ToDate = new DateTime(2024, 05, 30);
            //    rm3.Created = new DateTime(2024, 05, 26);

            //    rm4.Id = "r4";
            //    rm4.UserId = "u2";
            //    rm4.CarId = "c2";
            //    rm4.FromDate = new DateTime(2024, 06, 28);
            //    rm4.ToDate = new DateTime(2024, 06, 30);
            //    rm4.Created = new DateTime(2024, 06, 26);

            //    rentals.Add(rm1);
            //    rentals.Add(rm2);
            //    rentals.Add(rm3);
            //    rentals.Add(rm4);

            //    firstRun = false;
            //}

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

        public async Task<List<Rental>> GetUserRentals(int userId)
        {
            var rentals = await ListRentals();
            List<Rental> rentalsByUserId = new List<Rental>();
            foreach (var rental in rentals)
            {
                if (rental.UserId == userId) { rentalsByUserId.Add(rental); }
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

        public async Task<List<Rental>> NewReservation(int userId, int carId, string _fromDate, string _toDate)
        {
            var rentals = await ListRentals();
            Rental rm5 = new Rental();

            DateTime fromDate = DateTime.Parse(_fromDate);
            DateTime toDate = DateTime.Parse(_toDate);
            DateTime created = DateTime.UtcNow;
            int counter = 1;

            rm5.Id = counter;
            rm5.CarId = carId;
            rm5.UserId = userId;
            rm5.FromDate = fromDate;
            rm5.ToDate = toDate;
            rm5.Created = created;

            if (rentals.Count > 0 && ValidDate(carId, _fromDate, _toDate).Result)
            {
                rentals.Add(rm5);
                rentCount++;
            }

            return rentals;
        }
    }
}