using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpendSmart.Data;

// Create web application builder - sets up ASP.NET Core host and services
var builder = WebApplication.CreateBuilder(args);

// === SERVICE CONFIGURATION (Dependency Injection Container) ===

// Get database connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Register Entity Framework with SQL Server provider
// This enables dependency injection of ApplicationDbContext throughout the app
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(connectionString));

// Add developer exception page for database-related errors (shows detailed EF error info)
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure ASP.NET Core Identity for user authentication and authorization
// RequireConfirmedAccount: Users must confirm email before signing in
// AddEntityFrameworkStores: Use our ApplicationDbContext for Identity data storage
builder.Services.AddDefaultIdentity<IdentityUser>(options => 
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Register MVC services - enables controllers, views, and model binding
builder.Services.AddControllersWithViews();

// Build the web application with configured services
var app = builder.Build();

// === MIDDLEWARE PIPELINE CONFIGURATION (Request Processing) ===
// Middleware executes in the order listed below for each HTTP request

if (app.Environment.IsDevelopment())
{
    // Development: Show detailed database migration info when DB is out of date
    app.UseMigrationsEndPoint();
}
else
{
    // Production: Show generic error page for unhandled exceptions
    app.UseExceptionHandler("/Home/Error");
    
    // HTTP Strict Transport Security - forces HTTPS for security
    app.UseHsts();
}

// Force redirect from HTTP to HTTPS
app.UseHttpsRedirection();

// Serve static files (CSS, JavaScript, images) from wwwroot folder
app.UseStaticFiles();

// Enable routing - matches URLs to controller actions
app.UseRouting();

// Enable authentication and authorization checks
app.UseAuthorization();

// Configure default MVC routing pattern: /{controller}/{action}/{id?}
// Example: /Home/Expenses/5 maps to HomeController.Expenses(id: 5)
// Defaults: controller=Home, action=Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Enable Razor Pages for Identity UI (login, register pages)
app.MapRazorPages();

// Start the web application and begin listening for HTTP requests
app.Run();
