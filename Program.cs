using Microsoft.EntityFrameworkCore;
using Pdi_Car_Rent.Data;
using Pdi_Car_Rent.Mapper;
using Pdi_Car_Rent.Models;
using Microsoft.AspNetCore.Identity;

var options = new DbContextOptionsBuilder<DatabaseContext>()
   .UseInMemoryDatabase(databaseName: "PDI_Car_Rent");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("Pdi_Car_RentContextConnection");builder.Services.AddDbContext<Pdi_Car_RentContext>(options =>
    options.UseSqlServer(connectionString));builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<Pdi_Car_RentContext>();
// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof (MapperConfig));

builder.Services.AddDbContext<DatabaseContext>(x =>
    x.UseInMemoryDatabase(databaseName: "PDI_Car_Rent"));

builder.Services.AddScoped<IRepositoryService<CarType>,RepositoryService<CarType>>();
builder.Services.AddScoped<IRepositoryService<CarRentPlaceViewModel>, RepositoryService<CarRentPlaceViewModel>>();
builder.Services.AddScoped<IRepositoryService<Car>, RepositoryService<Car>>();
//builder.Services.AddScoped<IEntity<int>,I

var app = builder.Build();



//using (var context = new DatabaseContext(options))
//{
//    var customer = new Customer
//    {
//        FirstName = "Elizabeth",
//        LastName = "Lincoln",
//        Address = "23 Tsawassen Blvd."
//    };

//    context.Customers.Add(customer);
//    context.SaveChanges();

//}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => {
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
    });



app.Run();

