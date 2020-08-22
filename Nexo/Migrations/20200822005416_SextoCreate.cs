using Microsoft.EntityFrameworkCore.Migrations;

namespace Nexo.Migrations
{
    public partial class SextoCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Fornecedores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Fornecedores",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Fornecedores");
        }
    }
}
