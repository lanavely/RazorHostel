using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auto.Migrations
{
    /// <inheritdoc />
    public partial class QuestionNumberForTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentQuestionNumber",
                table: "Tests",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentQuestionNumber",
                table: "Tests");
        }
    }
}
