using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoVuiHaiNao.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuizzV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "DurationInSeconds",
                table: "Entities",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DurationInSeconds",
                table: "Entities",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
