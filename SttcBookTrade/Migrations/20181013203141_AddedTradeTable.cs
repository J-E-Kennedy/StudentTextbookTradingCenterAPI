using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SttcBookTrade.Migrations
{
    public partial class AddedTradeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_UserId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Schools_SchoolId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Trade",
                columns: table => new
                {
                    TradeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OffererUserId = table.Column<int>(nullable: false),
                    ReceiverUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trade", x => x.TradeId);
                    table.ForeignKey(
                        name: "FK_Trade_Users_OffererUserId",
                        column: x => x.OffererUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trade_Users_ReceiverUserId",
                        column: x => x.ReceiverUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trade_OffererUserId",
                table: "Trade",
                column: "OffererUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Trade_ReceiverUserId",
                table: "Trade",
                column: "ReceiverUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_UserId",
                table: "Books",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Schools_SchoolId",
                table: "Users",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "SchoolId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_UserId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Schools_SchoolId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Trade");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_UserId",
                table: "Books",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Schools_SchoolId",
                table: "Users",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "SchoolId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
