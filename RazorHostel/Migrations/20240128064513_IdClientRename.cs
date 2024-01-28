using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorHostel.Migrations
{
    /// <inheritdoc />
    public partial class IdClientRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Clients_IdUser",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "Bookings",
                newName: "IdClient");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_IdUser",
                table: "Bookings",
                newName: "IX_Bookings_IdClient");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Clients_IdClient",
                table: "Bookings",
                column: "IdClient",
                principalTable: "Clients",
                principalColumn: "IdClient",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Clients_IdClient",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "IdClient",
                table: "Bookings",
                newName: "IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_IdClient",
                table: "Bookings",
                newName: "IX_Bookings_IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Clients_IdUser",
                table: "Bookings",
                column: "IdUser",
                principalTable: "Clients",
                principalColumn: "IdClient",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
