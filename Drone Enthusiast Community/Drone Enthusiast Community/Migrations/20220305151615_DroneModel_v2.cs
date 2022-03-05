using Microsoft.EntityFrameworkCore.Migrations;

namespace Drone_Enthusiast_Community.Migrations
{
    public partial class DroneModel_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageID",
                table: "Drones");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Drones",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageExtension",
                table: "Drones",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Drones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Drones");

            migrationBuilder.DropColumn(
                name: "ImageExtension",
                table: "Drones");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Drones");

            migrationBuilder.AddColumn<int>(
                name: "ImageID",
                table: "Drones",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
