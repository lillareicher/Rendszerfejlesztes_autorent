namespace ReactApp1.Server.Models.Entities
{
    public class Rental
    {
        public User User { get; set; }
        public Car Car { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime? Created { get; set; }
    }

}
