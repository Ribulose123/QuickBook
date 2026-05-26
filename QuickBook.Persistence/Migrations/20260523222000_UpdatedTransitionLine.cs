using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickBook.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTransitionLine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TransactionLines_AccountId",
                table: "TransactionLines",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLines_Accounts_AccountId",
                table: "TransactionLines",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLines_Accounts_AccountId",
                table: "TransactionLines");

            migrationBuilder.DropIndex(
                name: "IX_TransactionLines_AccountId",
                table: "TransactionLines");
        }
    }
}
