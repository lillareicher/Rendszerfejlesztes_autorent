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

            rm1.Id = "r2";
            rm1.UserId = "u2";
            rm1.CarId = "c2";
            rm1.FromDate = new DateTime(2024, 04, 28);
            rm1.ToDate = new DateTime(2024, 04, 30);
            rm1.Created = new DateTime(2024, 04, 26);

            rm1.Id = "r2";
            rm1.UserId = "u2";
            rm1.CarId = "c2";
            rm1.FromDate = new DateTime(2024, 05, 28);
            rm1.ToDate = new DateTime(2024, 05, 30);
            rm1.Created = new DateTime(2024, 05, 26);

            rm1.Id = "r2";
            rm1.UserId = "u2";
            rm1.CarId = "c2";
            rm1.FromDate = new DateTime(2024, 06, 28);
            rm1.ToDate = new DateTime(2024, 06, 30);
            rm1.Created = new DateTime(2024, 06, 26);

            rentals.Add(rm1);
            rentals.Add(rm2);
            rentals.Add(rm3);
            rentals.Add(rm4);

            return rentals;
        }

        public async Task<List<RentalModel>> getRentals (string carId)
        {
            return new List<RentalModel>();
        }
    }
}