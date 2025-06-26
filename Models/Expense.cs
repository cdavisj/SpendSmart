namespace SpendSmart.Models;

// Data annotations: Attributes that configure Entity Framework and validation behavior
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Expense model class - represents an expense record in the database
/// Entity Framework maps this class to a SQL Server table named "Expenses"
/// Each property becomes a column in the database table
/// </summary>
public class Expense
{
    /// <summary>
    /// Primary key property - uniquely identifies each expense record
    /// [Key]: Tells Entity Framework this is the primary key
    /// int: Maps to SQL Server INT IDENTITY column (auto-incrementing)
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Expense amount - stores monetary values with precision
    /// [Column]: Specifies exact SQL Server data type for currency values
    /// decimal(18,2): 18 total digits, 2 after decimal point (e.g., 999999999999999.99)
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal Value { get; set; }

    /// <summary>
    /// Expense description - required text field
    /// [Required]: Creates NOT NULL constraint in SQL Server and validates on form submission
    /// string?: Nullable string type, but [Required] makes it mandatory for users
    /// </summary>
    [Required]
    public string? Description { get; set; }
}
