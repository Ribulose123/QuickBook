using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickBook.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CustomerUpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Customers",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customers",
                newName: "id");
        }
    }
}
