using Pdi_Car_Rent.Data;
using PdI_Car_Rent.Models;
using System.ComponentModel.DataAnnotations;

namespace PdI_Car_Rent.Data
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        public string? Name { get; set; }
        public int? RentPriceForHour { get; set; }
        public string? CarInfo { get; set; }
        public int? CarTypeId { get; set; }
        public CarType? CarType { get; set; }
        public ICollection<Rent>? Rents { get; set; }
        //public List<string> Photos { get; set; } = new List<string>();
        public int? CarRentPlaceID { get; set; }
        public CarRentPlaceViewModel? CarRentPlace { get; set; }
    }
}