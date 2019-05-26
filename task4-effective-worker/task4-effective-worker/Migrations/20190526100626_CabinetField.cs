using Microsoft.EntityFrameworkCore.Migrations;

namespace task4_effective_worker.Migrations
{
    public partial class CabinetField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cabinet",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cabinet",
                table: "Employees");
        }
    }
}
