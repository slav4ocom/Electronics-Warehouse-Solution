using Microsoft.EntityFrameworkCore.Migrations;

namespace Console_Manager.Migrations
{
    public partial class AddCollumn_AlaBala : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlaBala",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlaBala",
                table: "Articles");
        }
    }
}
