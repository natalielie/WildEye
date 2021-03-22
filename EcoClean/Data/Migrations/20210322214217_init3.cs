using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcoClean.Data.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoilPollution",
                table: "SmartDeviceData");

            migrationBuilder.DropColumn(
                name: "CurrentBudget",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Tax",
                table: "Reports");

            migrationBuilder.AddColumn<int>(
                name: "TaxId",
                table: "Reports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "Rate",
                table: "Enterprises",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);


            migrationBuilder.CreateTable(
                name: "Taxes",
                columns: table => new
                {
                    TaxId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnterpriseId = table.Column<int>(nullable: false),
                    AirPollutionSubstance = table.Column<int>(nullable: false),
                    WaterPollutionSubstance = table.Column<int>(nullable: false),
                    AirEmissions = table.Column<double>(nullable: false),
                    WaterEmissions = table.Column<double>(nullable: false),
                    TaxCost = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxes", x => x.TaxId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Taxes");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "Reports");

            migrationBuilder.AddColumn<float>(
                name: "SoilPollution",
                table: "SmartDeviceData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "CurrentBudget",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tax",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Rate",
                table: "Enterprises",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "CertificateDate",
                table: "Certificates",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
