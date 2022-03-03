using Microsoft.EntityFrameworkCore.Migrations;

namespace Drone_Enthusiast_Community.Migrations
{
    public partial class VideoModel_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageID",
                table: "Drones",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ImageID",
                table: "Drones");
        }
    }
}
