using Microsoft.EntityFrameworkCore.Migrations;

namespace SttcBookTrade.Migrations
{
    public partial class AddedBookTradeLinkTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TradeId",
                table: "Books",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookTradeLink",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false),
                    TradeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTradeLink", x => new { x.BookId, x.TradeId });
                    table.ForeignKey(
                        name: "FK_BookTradeLink_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookTradeLink_Trade_TradeId",
                        column: x => x.TradeId,
                        principalTable: "Trade",
                        principalColumn: "TradeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_TradeId",
                table: "Books",
                column: "TradeId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTradeLink_TradeId",
                table: "BookTradeLink",
                column: "TradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Trade_TradeId",
                table: "Books",
                column: "TradeId",
                principalTable: "Trade",
                principalColumn: "TradeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Trade_TradeId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "BookTradeLink");

            migrationBuilder.DropIndex(
                name: "IX_Books_TradeId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "TradeId",
                table: "Books");
        }
    }
}
