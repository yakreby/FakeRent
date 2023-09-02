using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeRentAPI.Migrations
{
    public partial class HouseNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HouseNumbers",
                table: "HouseNumbers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "HouseNumbers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HouseNumbers",
                table: "HouseNumbers",
                column: "HouseNo");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 2, 12, 33, 45, 497, DateTimeKind.Local).AddTicks(9961));

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 2, 12, 33, 45, 497, DateTimeKind.Local).AddTicks(9969));

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 2, 12, 33, 45, 497, DateTimeKind.Local).AddTicks(9971));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HouseNumbers",
                table: "HouseNumbers");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "HouseNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HouseNumbers",
                table: "HouseNumbers",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 1, 23, 55, 35, 905, DateTimeKind.Local).AddTicks(3221));

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 1, 23, 55, 35, 905, DateTimeKind.Local).AddTicks(3230));

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 1, 23, 55, 35, 905, DateTimeKind.Local).AddTicks(3231));
        }
    }
}
