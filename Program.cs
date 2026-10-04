using Microsoft.EntityFrameworkCore;
using StudentManagementMVC.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();
if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/Students"); app.UseHsts(); }
app.UseStaticFiles(); app.UseRouting(); app.UseAuthorization();
using (var scope = app.Services.CreateScope()) { var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>(); db.Database.EnsureCreated(); DbInitializer.Seed(db); }
app.MapControllerRoute(name: "default", pattern: "{controller=Students}/{action=Index}/{id?}");
app.Run();