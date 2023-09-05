using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoWebAPI.Migrations
{
    public partial class CustomerEntityExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AccountBalance",
                table: "Customers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountBalance",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Customers");
        }
    }
}
