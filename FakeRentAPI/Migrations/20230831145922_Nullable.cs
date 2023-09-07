using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeRentAPI.Migrations
{
    public partial class Nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 31, 17, 59, 22, 628, DateTimeKind.Local).AddTicks(6904));

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 31, 17, 59, 22, 628, DateTimeKind.Local).AddTicks(6914));

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 31, 17, 59, 22, 628, DateTimeKind.Local).AddTicks(6916));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 31, 12, 7, 9, 195, DateTimeKind.Local).AddTicks(2636));

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 31, 12, 7, 9, 195, DateTimeKind.Local).AddTicks(2651));

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 31, 12, 7, 9, 195, DateTimeKind.Local).AddTicks(2653));
        }
    }
}
