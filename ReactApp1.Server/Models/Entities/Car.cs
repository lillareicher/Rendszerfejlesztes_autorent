namespace ReactApp1.Server.Models.Entities
{
    public class Car
    {
        public Category Category { get; set; }
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int DailyPrice { get; set; }
    }

}