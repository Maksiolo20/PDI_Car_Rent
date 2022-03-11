using Microsoft.EntityFrameworkCore;
using PdI_Car_Rent.Data;
using PdI_Car_Rent.Models;

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
        public DbSet<CarRentPlaceViewModel> CarRentPlace { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<Car>()
                .HasOne<CarType>(x => x.CarType)
                .WithMany(y => y.Cars)
                .HasForeignKey(z => z.CarTypeId);

            modelBuilder.Entity<Rent>()
                .HasOne<Car>(x => x.Car)
                .WithMany(y => y.Rents)
                .HasForeignKey(z => z.RentId);

            modelBuilder.Entity<Rent>()
                .HasOne<RentStatus>(x => x.RentStatus);

            modelBuilder.Entity<Car>()
                .HasOne<CarRentPlaceViewModel>(x => x.CarRentPlace)
                .WithMany(y => y.Cars)
                .HasForeignKey(z => z.CarRentPlaceID);
        }
    }

}

