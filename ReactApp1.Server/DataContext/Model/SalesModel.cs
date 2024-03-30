using System.ComponentModel.DataAnnotations;

namespace ReactApp1.Server.DataContext.Model
{
    public class SalesModel
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string Description { get; set; }
        public int Percent {  get; set; }
    }

}
