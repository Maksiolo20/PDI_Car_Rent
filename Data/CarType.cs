using Pdi_Car_Rent.Data;
using System.ComponentModel.DataAnnotations;

namespace Pdi_Car_Rent.Data
{
    public class CarType
    {
        [Key]
        public int CarTypeId { get; set; }
        public string Name { get; set; }
        public ICollection<Car> Cars { get; set; }

    }
}
