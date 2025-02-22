using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoVuiHaiNao.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuizzV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DurationInSeconds",
                table: "Entities",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "DurationInSeconds",
                table: "Entities",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
