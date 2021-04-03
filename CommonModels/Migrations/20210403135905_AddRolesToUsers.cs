using Microsoft.EntityFrameworkCore.Migrations;

namespace CommonModels.Migrations
{
    public partial class AddRolesToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "UserProfiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "UserProfiles");
        }
    }
}
