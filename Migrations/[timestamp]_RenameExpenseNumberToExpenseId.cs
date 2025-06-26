using Microsoft.EntityFrameworkCore.Migrations;

namespace YourNamespace.Migrations
{
    public partial class RenameExpenseNumberToExpenseId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpenseNumber",
                table: "Expenses",
                newName: "ExpenseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpenseId",
                table: "Expenses",
                newName: "ExpenseNumber");
        }
    }
} 