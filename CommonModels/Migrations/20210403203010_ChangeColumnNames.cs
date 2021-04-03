using Microsoft.EntityFrameworkCore.Migrations;

namespace CommonModels.Migrations
{
    public partial class ChangeColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartType",
                table: "Homeworks");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Homeworks");

            migrationBuilder.AddColumn<decimal>(
                name: "Points",
                table: "Homeworks",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "WorkType",
                table: "Homeworks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "Homeworks");

            migrationBuilder.DropColumn(
                name: "WorkType",
                table: "Homeworks");

            migrationBuilder.AddColumn<string>(
                name: "PartType",
                table: "Homeworks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Homeworks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
