using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Pdi_Car_Rent.Data;

public class Pdi_Car_RentUserContext : IdentityDbContext<IdentityUser>
{
    public Pdi_Car_RentUserContext(DbContextOptions<Pdi_Car_RentUserContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        //builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Administrator", NormalizedName = "Administrator".ToUpper() });
        //builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Użytkownik", NormalizedName = "Użytkownik".ToUpper() });
        //builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Pracownik", NormalizedName = "Pracownik".ToUpper() });
        

    }
}
