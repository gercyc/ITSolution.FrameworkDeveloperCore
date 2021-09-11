using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITSolution.Framework.Core.Server.Migrations
{
    public partial class IdentityInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ITS_ROLES",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(38)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(100)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITS_ROLES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ITS_USER",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(38)", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "varchar(200)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(200)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(80)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(80)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(12)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITS_USER", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ITS_ROLE_CLAIMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "varchar(38)", nullable: false),
                    ClaimType = table.Column<string>(type: "varchar(100)", nullable: true),
                    ClaimValue = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITS_ROLE_CLAIMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ITS_ROLE_CLAIMS_ITS_ROLES_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ITS_ROLES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ITS_USER_CLAIMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "varchar(38)", nullable: false),
                    ClaimType = table.Column<string>(type: "varchar(100)", nullable: true),
                    ClaimValue = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITS_USER_CLAIMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ITS_USER_CLAIMS_ITS_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "ITS_USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ITS_USER_LOGINS",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(200)", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(200)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "varchar(200)", nullable: true),
                    UserId = table.Column<string>(type: "varchar(38)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITS_USER_LOGINS", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_ITS_USER_LOGINS_ITS_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "ITS_USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ITS_USER_ROLES",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(38)", nullable: false),
                    RoleId = table.Column<string>(type: "varchar(38)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITS_USER_ROLES", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ITS_USER_ROLES_ITS_ROLES_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ITS_ROLES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ITS_USER_ROLES_ITS_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "ITS_USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ITS_USER_TOKENS",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(38)", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITS_USER_TOKENS", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_ITS_USER_TOKENS_ITS_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "ITS_USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ITS_ROLE_CLAIMS_RoleId",
                table: "ITS_ROLE_CLAIMS",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "ITS_ROLES",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "ITS_USER",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "ITS_USER",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ITS_USER_CLAIMS_UserId",
                table: "ITS_USER_CLAIMS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ITS_USER_LOGINS_UserId",
                table: "ITS_USER_LOGINS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ITS_USER_ROLES_RoleId",
                table: "ITS_USER_ROLES",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ITS_ROLE_CLAIMS");

            migrationBuilder.DropTable(
                name: "ITS_USER_CLAIMS");

            migrationBuilder.DropTable(
                name: "ITS_USER_LOGINS");

            migrationBuilder.DropTable(
                name: "ITS_USER_ROLES");

            migrationBuilder.DropTable(
                name: "ITS_USER_TOKENS");

            migrationBuilder.DropTable(
                name: "ITS_ROLES");

            migrationBuilder.DropTable(
                name: "ITS_USER");
        }
    }
}
