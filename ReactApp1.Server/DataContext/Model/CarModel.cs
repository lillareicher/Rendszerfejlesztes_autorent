
namespace ReactApp1.Server.DataContext.Model { 
    public class CarModel
    {
        public string Id { get; set; }
        public string Category_id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string DailyPrice { get; set; }
    }

    public class CarList
    {
        public List<CarModel> Cars { get; set; }
    }
}