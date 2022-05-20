using Pdi_Car_Rent.Data;
using Pdi_Car_Rent.Models;

namespace Pdi_Car_Rent.Services
{
    public interface ICarRentPlace : IEntity<int>
    {
        public int Id { get; set; }
        public string PlaceName { get; set; }
        public string Address { get; set; }
        public string WorkerId { get; set; }
        public List<Car>? Cars { get; set; }
    }
}

