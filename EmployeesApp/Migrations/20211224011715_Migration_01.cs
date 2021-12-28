using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeesApp.Migrations
{
    public partial class Migration_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "GrossSalary",
                schema: "dbo",
                table: "Employee",
                type: "decimal(12,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrossSalary",
                schema: "dbo",
                table: "Employee");
        }
    }
}
