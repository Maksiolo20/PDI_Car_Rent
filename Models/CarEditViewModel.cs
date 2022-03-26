using Pdi_Car_Rent.Data;

namespace Pdi_Car_Rent.Models
{
    public class CarEditViewModel
    {
        public int CarId { get; set; }
        public string? Name { get; set; }
        public int? RentPriceForHour { get; set; }
        public string? CarInfo { get; set; }

        public CarType CarType { get; set; }
        public int CarTypeId { get; set; }
        public List<CarType> CarTypeList { get; set; } = new List<CarType>();
        public int? CarRentPlaceID { get; set; }
        public List<CarRentPlaceViewModel> CarRentPlaceList { get; set; } = new List<CarRentPlaceViewModel>();
    }
}
