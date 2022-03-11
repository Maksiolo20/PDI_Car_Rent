namespace PdI_Car_Rent.Models
{
    public class CarRentPlaceViewModel
    {
        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string Address { get; set; }
        public List<CarModel> Cars { get; set; }
    }
}
