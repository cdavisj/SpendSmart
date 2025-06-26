using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpendSmart.Models; // Import our Expense model

namespace SpendSmart.Data;

/// <summary>
/// Database context class - Entity Framework's main class for database operations
/// Inherits from IdentityDbContext: Provides ASP.NET Identity tables (users, roles, etc.)
/// Acts as a bridge between C# objects and SQL Server database
/// </summary>
public class ApplicationDbContext : IdentityDbContext
{
    /// <summary>
    /// DbSet property - represents the Expenses table in SQL Server
    /// Entity Framework uses this to generate SQL queries (SELECT, INSERT, UPDATE, DELETE)
    /// Each DbSet<T> property becomes a table where T is the model class
    /// </summary>
    public DbSet<Expense> Expenses { get; set; }

    /// <summary>
    /// Constructor - receives database configuration from dependency injection
    /// ASP.NET Core automatically provides the options with connection string and provider info
    /// </summary>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) // Pass options to IdentityDbContext base class
    {
        // Constructor body intentionally empty - base class handles database setup
    }
}
