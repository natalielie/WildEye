using Microsoft.EntityFrameworkCore.Migrations;

namespace EcoClean.Data.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Enterprises",
                columns: new[] { "EnterpriseId", "Address", "ClientId", "Kind", "Name", "PhoneNumber", "Product", "Rate" },
                values: new object[] { 1, "Riverside st, 33b", 1, "Gas Station", "WOG", "+40097656789", "Fuel", 0.0 });

            migrationBuilder.InsertData(
                table: "Enterprises",
                columns: new[] { "EnterpriseId", "Address", "ClientId", "Kind", "Name", "PhoneNumber", "Product", "Rate" },
                values: new object[] { 2, "DownCreek st, 16", 2, "Restaurant", "Johnny's", "+40054776512", "Food", 0.0 });

            migrationBuilder.InsertData(
                table: "Enterprises",
                columns: new[] { "EnterpriseId", "Address", "ClientId", "Kind", "Name", "PhoneNumber", "Product", "Rate" },
                values: new object[] { 3, "Central st, 1", 1, "Shoes Fabric", "Shoes On", "+380951332455", "Shoes", 0.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Enterprises",
                keyColumn: "EnterpriseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Enterprises",
                keyColumn: "EnterpriseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Enterprises",
                keyColumn: "EnterpriseId",
                keyValue: 3);
        }
    }
}
