using Microsoft.EntityFrameworkCore.Migrations;

namespace EcoClean.Data.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "Clients");



            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Clients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Clients",
                nullable: false,
                type: "int")
                .Annotation("SqlServer:Identity", "1, 1");


            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
