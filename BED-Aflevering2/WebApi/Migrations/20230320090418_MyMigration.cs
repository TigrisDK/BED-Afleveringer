using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class MyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddresLine2",
                table: "Models",
                newName: "AddressLine2");

            migrationBuilder.RenameColumn(
                name: "AddresLine1",
                table: "Models",
                newName: "AddressLine1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddressLine2",
                table: "Models",
                newName: "AddresLine2");

            migrationBuilder.RenameColumn(
                name: "AddressLine1",
                table: "Models",
                newName: "AddresLine1");
        }
    }
}
