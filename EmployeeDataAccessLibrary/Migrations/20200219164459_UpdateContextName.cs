using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeDataAccessLibrary.Migrations
{
    public partial class UpdateContextName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddresses_People_PersonId",
                table: "EmailAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailAddresses",
                table: "EmailAddresses");

            migrationBuilder.RenameTable(
                name: "EmailAddresses",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_EmailAddresses_PersonId",
                table: "Addresses",
                newName: "IX_Addresses_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_People_PersonId",
                table: "Addresses",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_People_PersonId",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "EmailAddresses");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_PersonId",
                table: "EmailAddresses",
                newName: "IX_EmailAddresses_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailAddresses",
                table: "EmailAddresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddresses_People_PersonId",
                table: "EmailAddresses",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
