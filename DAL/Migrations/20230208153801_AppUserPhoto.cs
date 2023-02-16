using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AppUserPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 8, 10, 38, 1, 670, DateTimeKind.Local).AddTicks(6323),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 14, 0, 59, 566, DateTimeKind.Local).AddTicks(4723));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 8, 10, 38, 1, 670, DateTimeKind.Local).AddTicks(5915),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 14, 0, 59, 566, DateTimeKind.Local).AddTicks(4398));

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 14, 0, 59, 566, DateTimeKind.Local).AddTicks(4723),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 8, 10, 38, 1, 670, DateTimeKind.Local).AddTicks(6323));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 14, 0, 59, 566, DateTimeKind.Local).AddTicks(4398),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 8, 10, 38, 1, 670, DateTimeKind.Local).AddTicks(5915));
        }
    }
}
