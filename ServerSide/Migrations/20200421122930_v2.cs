using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerSide.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurPromille",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "TopPromille",
                table: "Persons");

            migrationBuilder.AddColumn<float>(
                name: "CurPermille",
                table: "Persons",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TopPermille",
                table: "Persons",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurPermille",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "TopPermille",
                table: "Persons");

            migrationBuilder.AddColumn<float>(
                name: "CurPromille",
                table: "Persons",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TopPromille",
                table: "Persons",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
