using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using SpendSmart.Models;
// Add database to context
using SpendSmart.Data;

namespace SpendSmart.Controllers;

/// <summary>
/// Handles requests for the main application pages.
/// </summary>
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeController"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="context">The database context.</param>
    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    /// <summary>
    /// Returns the home page.
    /// </summary>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Displays a list of all expenses and the total value.
    /// </summary>
    public IActionResult Expenses() 
    {
        var expenses = _context.Expenses.ToList();
        var total = expenses.Sum(expense => expense.Value);
        ViewBag.Expenses = total;
        return View(expenses);
    }

    /// <summary>
    /// Displays the create/edit expense form.
    /// </summary>
    /// <param name="id">The ID of the expense to edit, or null to create a new expense.</param>
    public IActionResult CreateEditExpense(int? id)
    {
        if (id != null) 
        {
            var expense = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
            return View(expense);
        }
        return View();
    }

    /// <summary>
    /// Deletes an expense and redirects to the expenses list.
    /// </summary>
    /// <param name="id">The ID of the expense to delete.</param>
    public IActionResult DeleteExpense(int id) 
    {
        var expense = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
        if (expense != null)
        {
            _context.Expenses.Remove(expense);
            _context.SaveChanges();
        }
        return RedirectToAction("Expenses");
    }

    /// <summary>
    /// Handles form submission for creating or editing an expense.
    /// </summary>
    /// <param name="model">The expense model from the form.</param>
    public IActionResult CreateEditExpenseForm(Expense model) 
    {
        if (model.Id == 0) 
        {
            _context.Expenses.Add(model);
        }
        else
        {
            _context.Expenses.Update(model);
        }
        _context.SaveChanges();
        return RedirectToAction("Expenses");
    }

    /// <summary>
    /// Returns the privacy page.
    /// </summary>
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Returns the About page.
    /// </summary>
    public IActionResult About()
    {
        return View();
    }

    /// <summary>
    /// Displays the error page.
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
