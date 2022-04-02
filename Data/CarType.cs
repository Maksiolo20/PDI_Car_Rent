using Pdi_Car_Rent.Data;
using Pdi_Car_Rent.Models;
using System.ComponentModel.DataAnnotations;

namespace Pdi_Car_Rent.Data
{
    public class CarType : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Car> Cars { get; set; }

    }
}
