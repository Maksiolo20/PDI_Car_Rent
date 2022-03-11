using Microsoft.EntityFrameworkCore;
using PdI_Car_Rent.Data;

namespace Pdi_Car_Rent.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
       : base(options)
        { }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<RentStatus> RentStatuses { get; set; }


    }
}
