using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITSolution.Framework.Servers.Core.FirstAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ITS_MENU",
                columns: table => new
                {
                    IdMenu = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeMenu = table.Column<string>(maxLength: 300, nullable: true),
                    MenuPai = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    MenuText = table.Column<string>(maxLength: 500, nullable: true),
                    MenuType = table.Column<string>(maxLength: 50, nullable: true),
                    ControllerClass = table.Column<string>(maxLength: 50, nullable: true),
                    ActionController = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITS_MENU", x => x.IdMenu);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeUsuario = table.Column<string>(maxLength: 50, nullable: false),
                    NomeUtilizador = table.Column<string>(maxLength: 20, nullable: false),
                    Senha = table.Column<string>(maxLength: 20, nullable: false),
                    DataInclusao = table.Column<DateTime>(nullable: false),
                    Skin = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ITS_MENU");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
