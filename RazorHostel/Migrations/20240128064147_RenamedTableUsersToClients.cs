using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorHostel.Migrations
{
    /// <inheritdoc />
    public partial class RenamedTableUsersToClients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_IdUser",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Clients");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "Clients",
                newName: "IdClient");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "IdClient");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Clients_IdUser",
                table: "Bookings",
                column: "IdUser",
                principalTable: "Clients",
                principalColumn: "IdClient",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Clients_IdUser",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "IdClient",
                table: "Users",
                newName: "IdUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_IdUser",
                table: "Bookings",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
