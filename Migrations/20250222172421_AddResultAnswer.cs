using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoVuiHaiNao.Migrations
{
    /// <inheritdoc />
    public partial class AddResultAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChosenAnswerId",
                table: "Entities",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ResultAnswer_IsCorrect",
                table: "Entities",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResultAnswer_QuestionId",
                table: "Entities",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResultId",
                table: "Entities",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entities_ChosenAnswerId",
                table: "Entities",
                column: "ChosenAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Entities_ResultAnswer_QuestionId",
                table: "Entities",
                column: "ResultAnswer_QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Entities_ResultId",
                table: "Entities",
                column: "ResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entities_Entities_ChosenAnswerId",
                table: "Entities",
                column: "ChosenAnswerId",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entities_Entities_ResultAnswer_QuestionId",
                table: "Entities",
                column: "ResultAnswer_QuestionId",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entities_Entities_ResultId",
                table: "Entities",
                column: "ResultId",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entities_Entities_ChosenAnswerId",
                table: "Entities");

            migrationBuilder.DropForeignKey(
                name: "FK_Entities_Entities_ResultAnswer_QuestionId",
                table: "Entities");

            migrationBuilder.DropForeignKey(
                name: "FK_Entities_Entities_ResultId",
                table: "Entities");

            migrationBuilder.DropIndex(
                name: "IX_Entities_ChosenAnswerId",
                table: "Entities");

            migrationBuilder.DropIndex(
                name: "IX_Entities_ResultAnswer_QuestionId",
                table: "Entities");

            migrationBuilder.DropIndex(
                name: "IX_Entities_ResultId",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "ChosenAnswerId",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "ResultAnswer_IsCorrect",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "ResultAnswer_QuestionId",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "Entities");
        }
    }
}
