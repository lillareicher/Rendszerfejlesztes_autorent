using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.DataContext.Model
{
    public class RentalModel
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? CarId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime? Created {  get; set; }
    }

}
