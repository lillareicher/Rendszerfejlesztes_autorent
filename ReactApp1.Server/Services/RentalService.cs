using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.DataContext.Model;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace ReactApp1.Server.Services
{
    public interface IRentalService // interface 
    {
        Task<List<RentalModel>> ListRentals();
        Task<List<RentalModel>> GetRentals(string carId);
        Task<bool> ValidDate(string CarId, DateTime FromDate, DateTime ToDate);
    }

    public class RentalService : IRentalService
    {

        public RentalService() { }

        public async Task<List<RentalModel>> ListRentals()
        {
            List<RentalModel> rentals = new List<RentalModel>();
            //string text = File.ReadAllText(@"./Rentals.json");
            //var rental = JsonSerializer.Deserialize<RentalModel>(text);
            List<RentalModel> rentalModels = new List<RentalModel>();

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

            rm3.Id = "r2";
            rm3.UserId = "u2";
            rm3.CarId = "c2";
            rm3.FromDate = new DateTime(2024, 05, 28);
            rm3.ToDate = new DateTime(2024, 05, 30);
            rm3.Created = new DateTime(2024, 05, 26);

            rm4.Id = "r2";
            rm4.UserId = "u2";
            rm4.CarId = "c2";
            rm4.FromDate = new DateTime(2024, 06, 28);
            rm4.ToDate = new DateTime(2024, 06, 30);
            rm4.Created = new DateTime(2024, 06, 26);

            rentals.Add(rm1);
            rentals.Add(rm2);
            rentals.Add(rm3);
            rentals.Add(rm4);

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

        public async Task<bool> ValidDate(string CarId, DateTime FromDate, DateTime ToDate)
        {
            bool validDate = false;
            bool dateIsFree = false;
            bool checkFromDate = false , checkToDate = false;
            var rentals = await ListRentals();
            foreach (var rental in rentals)
            {
                if (rental.CarId == CarId)
                {
                    if (!(FromDate >= rental.FromDate && FromDate <= rental.ToDate)) { checkFromDate = true;}
                    if (!(ToDate >= rental.FromDate && ToDate <= rental.ToDate)) { checkToDate = true; }
                }
            }

            if (FromDate <= ToDate) { validDate = true; }
            if (checkFromDate && checkToDate) { dateIsFree = true; }
            if(validDate && dateIsFree) { return true; }
            return false;
        }
    }
}