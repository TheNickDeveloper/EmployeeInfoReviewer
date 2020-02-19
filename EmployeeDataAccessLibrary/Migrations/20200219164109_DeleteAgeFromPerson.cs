using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeDataAccessLibrary.Migrations
{
    public partial class DeleteAgeFromPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "People");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "People",
                nullable: false,
                defaultValue: 0);
        }
    }
}
