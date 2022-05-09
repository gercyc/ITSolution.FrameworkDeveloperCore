using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITSolution.Framework.Servers.Core.FirstAPI.Migrations
{
    public partial class CreateMenuTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ITS_MENU",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomeMenu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuPai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    MenuText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ControllerClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionController = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true)
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
