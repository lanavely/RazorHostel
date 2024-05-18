using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auto.Migrations
{
    /// <inheritdoc />
    public partial class GroupForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupNumber",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Birthdate",
                table: "AspNetUsers",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Birthdate",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupNumber",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
