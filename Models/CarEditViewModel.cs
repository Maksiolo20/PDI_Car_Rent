using Pdi_Car_Rent.Data;

namespace Pdi_Car_Rent.Models
{
    public class CarEditViewModel
    {
        public int CarId { get; set; }
        public string? Name { get; set; }
        public int? RentPriceForHour { get; set; }
        public string? CarInfo { get; set; }

        public int? CarTypeId { get; set; }
        public int? CarRentPlaceID { get; set; }
        public int RentStatusID { get; set; }
        public RentStatus RentStatus { get; set; } 
    }
}
