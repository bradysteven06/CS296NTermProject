using Microsoft.EntityFrameworkCore.Migrations;

namespace Drone_Enthusiast_Community.Migrations
{
    public partial class ResourceModel_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WebAddress",
                table: "Resources",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WebAddress",
                table: "Resources");
        }
    }
}
