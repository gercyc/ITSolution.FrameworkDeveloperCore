using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITSolution.Framework.Core.Server.Migrations
{
    public partial class AddIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "ITS_ROLES",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITS_ROLES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ITS_USER",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(12)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITS_USER", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ITS_ROLE_CLAIMS",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "varchar(100)", nullable: true),
                    ClaimValue = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITS_ROLE_CLAIMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ITS_ROLE_CLAIMS_ITS_ROLES_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "ITS_ROLES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ITS_USER_CLAIMS",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "varchar(100)", nullable: true),
                    ClaimValue = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITS_USER_CLAIMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ITS_USER_CLAIMS_ITS_USER_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "ITS_USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ITS_USER_LOGINS",
                schema: "dbo",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(200)", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(200)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "varchar(200)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITS_USER_LOGINS", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_ITS_USER_LOGINS_ITS_USER_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "ITS_USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ITS_USER_ROLES",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITS_USER_ROLES", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ITS_USER_ROLES_ITS_ROLES_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "ITS_ROLES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ITS_USER_ROLES_ITS_USER_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "ITS_USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ITS_USER_TOKENS",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITS_USER_TOKENS", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_ITS_USER_TOKENS_ITS_USER_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "ITS_USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ITS_ROLE_CLAIMS_RoleId",
                schema: "dbo",
                table: "ITS_ROLE_CLAIMS",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "dbo",
                table: "ITS_ROLES",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "dbo",
                table: "ITS_USER",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "dbo",
                table: "ITS_USER",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ITS_USER_CLAIMS_UserId",
                schema: "dbo",
                table: "ITS_USER_CLAIMS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ITS_USER_LOGINS_UserId",
                schema: "dbo",
                table: "ITS_USER_LOGINS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ITS_USER_ROLES_RoleId",
                schema: "dbo",
                table: "ITS_USER_ROLES",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ITS_ROLE_CLAIMS",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ITS_USER_CLAIMS",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ITS_USER_LOGINS",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ITS_USER_ROLES",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ITS_USER_TOKENS",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ITS_ROLES",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ITS_USER",
                schema: "dbo");
        }
    }
}
