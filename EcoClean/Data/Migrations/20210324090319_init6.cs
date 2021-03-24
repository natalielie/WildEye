using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcoClean.Data.Migrations
{
    public partial class init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SmartDeviceData",
                columns: new[] { "SmartDeviceDataId", "AirPollution", "EnterpriseId", "SmartDeviceDataDate", "WaterPollution" },
                values: new object[] { 7, 499.0, 2, new DateTime(2021, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1.23 });

            migrationBuilder.InsertData(
                table: "SmartDeviceData",
                columns: new[] { "SmartDeviceDataId", "AirPollution", "EnterpriseId", "SmartDeviceDataDate", "WaterPollution" },
                values: new object[] { 8, 1189.0, 3, new DateTime(2021, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2.9300000000000002 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SmartDeviceData",
                keyColumn: "SmartDeviceDataId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SmartDeviceData",
                keyColumn: "SmartDeviceDataId",
                keyValue: 8);
        }
    }
}
