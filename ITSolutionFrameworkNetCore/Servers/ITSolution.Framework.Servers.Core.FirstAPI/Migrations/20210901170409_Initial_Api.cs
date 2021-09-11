using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITSolution.Framework.Servers.Core.FirstAPI.Migrations
{
    public partial class Initial_Api : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ITS_MENU",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    NomeMenu = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    MenuPai = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<bool>(type: "INTEGER", nullable: false),
                    MenuText = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    MenuType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ControllerClass = table.Column<string>(type: "TEXT", nullable: true),
                    ActionController = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITS_MENU", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ITS_MENU");
        }
    }
}
