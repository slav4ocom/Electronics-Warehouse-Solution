using Microsoft.EntityFrameworkCore.Migrations;

namespace CommonModels.Migrations
{
    public partial class AddColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "UserProfiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "UserProfiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNum",
                table: "UserProfiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "School",
                table: "UserProfiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Town",
                table: "UserProfiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "PhoneNum",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "School",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Town",
                table: "UserProfiles");
        }
    }
}
