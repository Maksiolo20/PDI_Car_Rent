using Pdi_Car_Rent.Data;
using System.ComponentModel.DataAnnotations;

namespace Pdi_Car_Rent.Models
{
    public class CarRentPlaceViewModel : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string PlaceName { get; set; }
        public string Address { get; set; }
        public List<Car>? Cars { get; set; }
    }
}
