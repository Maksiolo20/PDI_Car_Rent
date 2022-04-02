using Microsoft.EntityFrameworkCore;
using Pdi_Car_Rent.Data;
using Pdi_Car_Rent.Mapper;
using Pdi_Car_Rent.Models;

var options = new DbContextOptionsBuilder<DatabaseContext>()
   .UseInMemoryDatabase(databaseName: "PDI_Car_Rent");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof (MapperConfig));

builder.Services.AddDbContext<DatabaseContext>(x =>
    x.UseInMemoryDatabase(databaseName: "PDI_Car_Rent"));

builder.Services.AddScoped(typeof(IRepositoryService<>), typeof(RepositoryService<>));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

