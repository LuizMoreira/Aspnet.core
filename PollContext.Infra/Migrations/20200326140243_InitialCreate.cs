using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PollContext.Infra.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Polls",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    View = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionsPoll",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Poll_Id = table.Column<Guid>(nullable: false),
                    Qty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionsPoll", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionsPoll_Polls_Poll_Id",
                        column: x => x.Poll_Id,
                        principalTable: "Polls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OptionsPoll_Poll_Id",
                table: "OptionsPoll",
                column: "Poll_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OptionsPoll");

            migrationBuilder.DropTable(
                name: "Polls");
        }
    }
}
