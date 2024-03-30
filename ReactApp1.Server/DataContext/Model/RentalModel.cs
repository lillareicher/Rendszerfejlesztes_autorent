using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.DataContext.Model
{
    public class RentalModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CarId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Created {  get; set; }
    }

}
