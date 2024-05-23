using Microsoft.EntityFrameworkCore;
using targheX.Data;
using Microsoft.AspNetCore.Identity;
using targheX.Areas.Identity.Data;
using Microsoft.Extensions.DependencyInjection;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Aggiungo il supporto per il rendering di Razor
builder.Services.AddControllersWithViews();

RotativaConfiguration.Setup(builder.Environment.ContentRootPath, "Rotativa");

// Aggiungo i servizi di identità
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<DbContextUser>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DbContextUser>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRotativa();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();