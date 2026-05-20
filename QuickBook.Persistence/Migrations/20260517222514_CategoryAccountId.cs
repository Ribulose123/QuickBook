using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickBook.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CategoryAccountId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Categories",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Categories");
        }
    }
}
