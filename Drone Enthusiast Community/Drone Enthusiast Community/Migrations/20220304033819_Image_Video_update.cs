using Microsoft.EntityFrameworkCore.Migrations;

namespace Drone_Enthusiast_Community.Migrations
{
    public partial class Image_Video_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Images",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Images");
        }
    }
}
