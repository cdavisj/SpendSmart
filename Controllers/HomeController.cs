using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using SpendSmart.Models;
// Add database to context
using SpendSmart.Data;

namespace SpendSmart.Controllers;

/// <summary>
/// Main controller class - handles HTTP requests and returns responses in ASP.NET MVC
/// Inherits from Controller base class which provides MVC functionality
/// </summary>
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Constructor - ASP.NET Core's dependency injection automatically provides these services
    /// Logger: For debugging and error tracking
    /// Context: Entity Framework connection to SQL Server database
    /// </summary>
    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    /// <summary>
    /// Home page action - returns the main landing page
    /// IActionResult: Base return type for all MVC controller actions
    /// </summary>
    public IActionResult Index()
    {
        return View(); // Returns Views/Home/Index.cshtml
    }

    /// <summary>
    /// Expenses listing page - displays all expenses from database
    /// Demonstrates: Entity Framework queries, ViewBag usage, and passing data to views
    /// </summary>
    public IActionResult Expenses() 
    {
        // Entity Framework LINQ query - translates to SQL SELECT statement
        var expenses = _context.Expenses.ToList();

        // Calculate total using LINQ - runs in memory after database query
        var total = expenses.Sum(expense => expense.Value);
        
        // ViewBag: Dynamic way to pass additional data from controller to view
        ViewBag.Expenses = total;

        // Pass expenses list as the view model to Views/Home/Expenses.cshtml
        return View(expenses);
    }

    /// <summary>
    /// Create/Edit form page - handles both new expense creation and existing expense editing
    /// Optional parameter: if id is provided, load existing expense; otherwise, create new
    /// </summary>
    public IActionResult CreateEditExpense(int? id)
    {
        if (id != null) 
        {
            // Entity Framework query to find specific expense by primary key
            // SingleOrDefault: Returns one record or null (safer than Single)
            var expense = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
            return View(expense); // Pass existing expense to form
        }

        // No id provided - return empty form for new expense creation
        return View();
    }

    /// <summary>
    /// Delete expense action - removes expense from database and redirects
    /// Required parameter: id of expense to delete
    /// </summary>
    public IActionResult DeleteExpense(int id) 
    {
        // Find expense to delete using Entity Framework
        var expense = _context.Expenses.SingleOrDefault(expense => expense.Id == id);

        if (expense != null)
        {
            // Entity Framework: Mark for deletion and save changes to SQL Server
            _context.Expenses.Remove(expense);
            _context.SaveChanges(); // Executes SQL DELETE statement
        }

        // Redirect to expenses list - Post-Redirect-Get pattern prevents duplicate submissions
        return RedirectToAction("Expenses");
    }

    /// <summary>
    /// Form submission handler - processes create/edit form data
    /// Model binding: ASP.NET automatically maps form fields to Expense object properties
    /// </summary>
    public IActionResult CreateEditExpenseForm(Expense model) 
    {
        if (model.Id == 0) 
        {
            // New expense - Entity Framework tracks this as INSERT
            _context.Expenses.Add(model);
        }
        else
        {
            // Existing expense - Entity Framework tracks this as UPDATE
            _context.Expenses.Update(model);
        }
        
        // SaveChanges: Executes SQL INSERT/UPDATE statements against SQL Server
        _context.SaveChanges();

        // Redirect to prevent form resubmission on browser refresh
        return RedirectToAction("Expenses");
    }

    /// <summary>
    /// Privacy page action - simple static page
    /// </summary>
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Error handling action - displays error page with request details
    /// ResponseCache attribute: Prevents error pages from being cached
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
