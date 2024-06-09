using Microsoft.EntityFrameworkCore;
using targheX.Data;
using Microsoft.AspNetCore.Identity;
using targheX.Areas.Identity.Data;
using Microsoft.Extensions.DependencyInjection;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Aggiungo il supporto per il rendering di Razor
builder.Services.AddControllersWithViews();
// Configurazione di Rotativa per la generazione di PDF
RotativaConfiguration.Setup(builder.Environment.ContentRootPath, "Rotativa");

// Configurazione dei servizi di Entity Framework e del database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<DbContextUser>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurazione dei servizi di identità
builder.Services.AddDefaultIdentity<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DbContextUser>();

var app = builder.Build();

// Configurazione della pipeline di richieste HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRotativa();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Configurazione delle rotte
app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Crea i ruoli all'avvio dell'applicazione
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await EnsureRolesCreated(roleManager);
    }
    catch (Exception ex)
    {
        // Log error
        Console.WriteLine(ex.Message);
    }
}

app.Run();

static async Task EnsureRolesCreated(RoleManager<IdentityRole> roleManager)
{
    string[] roleNames = { "Admin", "Ufficio", "Agenzia" };
    foreach (var roleName in roleNames)
    {
        var roleExists = await roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}