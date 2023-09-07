using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeRentAPI.Migrations
{
    public partial class HouseNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HouseId",
                table: "HouseNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_HouseNumbers_HouseId",
                table: "HouseNumbers",
                column: "HouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseNumbers_Houses_HouseId",
                table: "HouseNumbers",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseNumbers_Houses_HouseId",
                table: "HouseNumbers");

            migrationBuilder.DropIndex(
                name: "IX_HouseNumbers_HouseId",
                table: "HouseNumbers");

            migrationBuilder.DropColumn(
                name: "HouseId",
                table: "HouseNumbers");

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
    }
}
