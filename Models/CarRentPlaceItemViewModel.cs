using Pdi_Car_Rent.Data;

namespace PdI_Car_Rent.Models
{
    public class CarRentPlaceItemViewModel
    {
        private readonly DatabaseContext _context;
        public List<CarRentPlaceViewModel> CarRentPlacesList { get; set; } = new List<CarRentPlaceViewModel>();
        public CarRentPlaceItemViewModel(DatabaseContext context)
        {
            _context = context;
            CarRentPlacesList = _context.CarRentPlace.ToList();
        }
    }
}
