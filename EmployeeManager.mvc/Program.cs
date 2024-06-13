using EmployeeManager.Mvc.Models;
using EmployeeManager.Mvc.Security;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.GetConnectionString("AppDb");
builder.Services.AddDbContext<AppDbContext>(
options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppDb"))
);

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AppDb")));
builder.Services.AddIdentity<AppIdentityUser, AppIdentityRole>()
  .AddEntityFrameworkStores<AppIdentityDbContext>();
builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/Security/SignIn";
    opt.AccessDeniedPath = "/Security/AccessDenied";
});
builder.Services.AddControllersWithViews();


var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=EmployeeManager}/{action=List}/{id?}");
});

//app.MapGet("/", () => "Hello World!");

app.Run();
