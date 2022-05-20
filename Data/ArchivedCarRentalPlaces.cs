using Pdi_Car_Rent.Models;
using Pdi_Car_Rent.Services;
using System.ComponentModel.DataAnnotations;

namespace Pdi_Car_Rent.Data
{
    public class ArchivedCarRentalPlaces : ICarRentPlace
    {
        [Key]
        public int ArchivedId { get; set; }
        public int Id { get; set; }
        public string PlaceName { get; set; }
        public string Address { get; set; }
        public string WorkerId { get; set; }
        public List<Car>? Cars { get; set; }
    }
}
