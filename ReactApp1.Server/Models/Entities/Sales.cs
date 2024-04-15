namespace ReactApp1.Server.Models.Entities
{
    public class Sales
    {
        public Car Car { get; set; }
        public int Id { get; set; }
        public int CarId { get; set; }
        public string? Description { get; set; }
        public int Percent { get; set; }
    }

}
