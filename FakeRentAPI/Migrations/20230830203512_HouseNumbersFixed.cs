using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeRentAPI.Migrations
{
    public partial class HouseNumbersFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HousesNumbers",
                table: "HousesNumbers");

            migrationBuilder.RenameTable(
                name: "HousesNumbers",
                newName: "HouseNumbers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HouseNumbers",
                table: "HouseNumbers",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 30, 23, 35, 11, 790, DateTimeKind.Local).AddTicks(680));

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 30, 23, 35, 11, 790, DateTimeKind.Local).AddTicks(692));

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 30, 23, 35, 11, 790, DateTimeKind.Local).AddTicks(694));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HouseNumbers",
                table: "HouseNumbers");

            migrationBuilder.RenameTable(
                name: "HouseNumbers",
                newName: "HousesNumbers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HousesNumbers",
                table: "HousesNumbers",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 30, 23, 30, 53, 207, DateTimeKind.Local).AddTicks(9437));

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 30, 23, 30, 53, 207, DateTimeKind.Local).AddTicks(9453));

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 30, 23, 30, 53, 207, DateTimeKind.Local).AddTicks(9455));
        }
    }
}
