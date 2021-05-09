using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcoClean.Data.Migrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "WaterPollution",
                table: "SmartDeviceData",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "AirPollution",
                table: "SmartDeviceData",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.InsertData(
                table: "SmartDeviceData",
                columns: new[] { "SmartDeviceDataId", "AirPollution", "EnterpriseId", "SmartDeviceDataDate", "WaterPollution" },
                values: new object[,]
                {
                    { 1, 780.0, 1, new DateTime(2020, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2.5 },
                    { 2, 830.0, 1, new DateTime(2020, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2.7999999999999998 },
                    { 3, 640.0, 2, new DateTime(2020, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1.7 },
                    { 4, 797.0, 1, new DateTime(2020, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2.1000000000000001 },
                    { 5, 1246.0, 3, new DateTime(2020, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3.1000000000000001 },
                    { 6, 530.0, 2, new DateTime(2020, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1.1000000000000001 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SmartDeviceData",
                keyColumn: "SmartDeviceDataId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SmartDeviceData",
                keyColumn: "SmartDeviceDataId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SmartDeviceData",
                keyColumn: "SmartDeviceDataId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SmartDeviceData",
                keyColumn: "SmartDeviceDataId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SmartDeviceData",
                keyColumn: "SmartDeviceDataId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SmartDeviceData",
                keyColumn: "SmartDeviceDataId",
                keyValue: 6);

            migrationBuilder.AlterColumn<float>(
                name: "WaterPollution",
                table: "SmartDeviceData",
                type: "real",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "AirPollution",
                table: "SmartDeviceData",
                type: "real",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
