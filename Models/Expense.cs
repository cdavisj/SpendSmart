namespace SpendSmart.Models;

// Data annotations: Attributes that configure Entity Framework and validation behavior
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Represents an expense record.
/// </summary>
public class Expense
{
    /// <summary>
    /// Gets or sets the primary key.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the expense amount.
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal Value { get; set; }

    /// <summary>
    /// Gets or sets the expense description.
    /// </summary>
    [Required]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the expense data and time
    /// </summary>
    [Required]
    public DateTime Date { get; set; }
}
