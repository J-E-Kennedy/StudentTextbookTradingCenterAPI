using Microsoft.EntityFrameworkCore.Migrations;

namespace SttcBookTrade.Migrations
{
    public partial class AddedIsWishlisted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWishlisted",
                table: "Books",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWishlisted",
                table: "Books");
        }
    }
}
