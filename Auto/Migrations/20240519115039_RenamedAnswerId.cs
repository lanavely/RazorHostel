using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auto.Migrations
{
    /// <inheritdoc />
    public partial class RenamedAnswerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestions_AnswerOptions_IdSelectedAnswer",
                table: "TestQuestions");

            migrationBuilder.RenameColumn(
                name: "IdSelectedAnswer",
                table: "TestQuestions",
                newName: "AnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_TestQuestions_IdSelectedAnswer",
                table: "TestQuestions",
                newName: "IX_TestQuestions_AnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestions_AnswerOptions_AnswerId",
                table: "TestQuestions",
                column: "AnswerId",
                principalTable: "AnswerOptions",
                principalColumn: "AnswerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestions_AnswerOptions_AnswerId",
                table: "TestQuestions");

            migrationBuilder.RenameColumn(
                name: "AnswerId",
                table: "TestQuestions",
                newName: "IdSelectedAnswer");

            migrationBuilder.RenameIndex(
                name: "IX_TestQuestions_AnswerId",
                table: "TestQuestions",
                newName: "IX_TestQuestions_IdSelectedAnswer");

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestions_AnswerOptions_IdSelectedAnswer",
                table: "TestQuestions",
                column: "IdSelectedAnswer",
                principalTable: "AnswerOptions",
                principalColumn: "AnswerId");
        }
    }
}
