using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.DataContext.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace ReactApp1.Server.Services
{
    public interface IRentalService // interface 
    {
        Task<List<RentalModel>> ListRentals();
        Task<List<RentalModel>> GetRentals(string carId);
        Task<bool> ValidDate(string carId, string _fromDate, string _toDate);
        Task<List<RentalModel>> GetUserRentals(string userId);
        Task<int> CountPrice(string carId, string _fromDate, string _toDate);
        Task<List<RentalModel>> newReservation(string userId, string carId, string _fromDate, string _toDate);
    }

    public class RentalService : IRentalService
    {
        List<RentalModel> rentals = new List<RentalModel>();
        bool firstRun = true;
        int rentCount = 5;
        private readonly ICarService _carService;
        public RentalService(ICarService carService) 
        {
            _carService = carService;
        }

        public async Task<List<RentalModel>> ListRentals()
        {
            if (firstRun) {

                RentalModel rm1 = new RentalModel();
                RentalModel rm2 = new RentalModel();
                RentalModel rm3 = new RentalModel();
                RentalModel rm4 = new RentalModel();

                rm1.Id = "r1";
                rm1.UserId = "u1";
                rm1.CarId = "c1";
                rm1.FromDate = new DateTime(2024, 03, 28);
                rm1.ToDate = new DateTime(2024, 03, 30);
                rm1.Created = new DateTime(2024, 03, 26);

                rm2.Id = "r2";
                rm2.UserId = "u2";
                rm2.CarId = "c2";
                rm2.FromDate = new DateTime(2024, 04, 28);
                rm2.ToDate = new DateTime(2024, 04, 30);
                rm2.Created = new DateTime(2024, 04, 26);

                rm3.Id = "r3";
                rm3.UserId = "u3";
                rm3.CarId = "c2";
                rm3.FromDate = new DateTime(2024, 05, 28);
                rm3.ToDate = new DateTime(2024, 05, 30);
                rm3.Created = new DateTime(2024, 05, 26);

                rm4.Id = "r4";
                rm4.UserId = "u2";
                rm4.CarId = "c2";
                rm4.FromDate = new DateTime(2024, 06, 28);
                rm4.ToDate = new DateTime(2024, 06, 30);
                rm4.Created = new DateTime(2024, 06, 26);

                rentals.Add(rm1);
                rentals.Add(rm2);
                rentals.Add(rm3);
                rentals.Add(rm4);

                firstRun = false;
            }

            return rentals;
        }

        public async Task<List<RentalModel>> GetRentals (string carId)
        {
            var rentals = await ListRentals();
            List<RentalModel> rentalsById = new List<RentalModel>();
            foreach (var rental in rentals)
            {
                if(rental.CarId == carId) { rentalsById.Add(rental); }
            }
            return rentalsById;
        }
        
        public async Task<List<RentalModel>> GetUserRentals(string userId)
        {
            var rentals = await ListRentals();
            List<RentalModel> rentalsByUserId = new List<RentalModel>();
            foreach (var rental in rentals)
            {
                if (rental.UserId == userId) { rentalsByUserId.Add(rental); }
            }
            return rentalsByUserId;
        }

        public async Task<bool> ValidDate(string carId, string _fromDate, string _toDate)
        {
            DateTime fromDate = DateTime.Parse(_fromDate);
            DateTime toDate = DateTime.Parse(_toDate);
            bool validDate = false;
            bool dateIsFree = false;
            bool checkFromDate = true , checkToDate = true, checkInBetween = true;
            var rentals = await ListRentals();
            foreach (var rental in rentals)
            {
                if (rental.CarId == carId)
                {
                    if (fromDate >= rental.FromDate && fromDate <= rental.ToDate) { checkFromDate = false;}
                    if (toDate >= rental.FromDate && toDate <= rental.ToDate) { checkToDate = false; }
                    if ((rental.FromDate >= fromDate && rental.ToDate >= fromDate) && (rental.FromDate <= toDate && rental.ToDate <= toDate)) { checkInBetween = false; }
                }
            }

            if (fromDate <= toDate) { validDate = true; }
            if (checkFromDate && checkToDate) { dateIsFree = true; }
            if(validDate && dateIsFree && checkInBetween) { return true; }
            return false;
        }

        public async Task<int> CountPrice(string carId, string _fromDate, string _toDate)
        {
            DateTime fromDate = DateTime.Parse(_fromDate);
            DateTime toDate = DateTime.Parse(_toDate);
            TimeSpan difference = toDate - fromDate;
            int days = difference.Days+1;
            List<CarModel> carTypes = await _carService.ListCars();
            foreach(var carType in carTypes)
            {
                if(carType.Id == carId)
                {
                    return days * carType.DailyPrice;
                }
            }
            return 0;
        }

        public async Task<List<RentalModel>> newReservation(string userId, string carId, string _fromDate, string _toDate)
        {
            var rentals = await ListRentals();
            var valid = await ValidDate(carId, _fromDate, _toDate);
            RentalModel rm5 = new RentalModel();

            DateTime fromDate = DateTime.Parse(_fromDate);
            DateTime toDate = DateTime.Parse(_toDate);
            DateTime created = DateTime.UtcNow;
            string counter = string.Format("r{0}", rentCount);

            rm5.Id = counter;
            rm5.CarId = carId;
            rm5.UserId = userId;
            rm5.FromDate = fromDate;
            rm5.ToDate = toDate;
            rm5.Created = created;

            if (rentals.Count > 0 && valid)
            {
                rentals.Add(rm5);
            }           

            return rentals;
        }
    }
}