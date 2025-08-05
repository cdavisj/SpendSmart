using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpendSmart.Models; // Import our Expense model

namespace SpendSmart.Data;

/// <summary>
/// Entity Framework database context for the application, including Identity tables and Expenses.
/// </summary>
public class ApplicationDbContext : IdentityDbContext
{
    /// <summary>
    /// Gets or sets the Expenses table.
    /// </summary>
    public DbSet<Expense> Expenses { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
