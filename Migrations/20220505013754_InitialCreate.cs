using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace newWebAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Active", "DateCreated", "LastName", "Name" },
                values: new object[] { new Guid("23749a20-099d-44ea-bed8-c8279192883a"), true, new DateTime(2022, 5, 4, 20, 37, 53, 973, DateTimeKind.Local).AddTicks(7037), "Suarez", "San" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Active", "DateCreated", "LastName", "Name" },
                values: new object[] { new Guid("c4ffad0d-f414-4ab6-b62e-d01cab7ef0f5"), true, new DateTime(2022, 5, 4, 20, 37, 53, 973, DateTimeKind.Local).AddTicks(7052), "Romero", "Juan" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "Active", "Description", "Role", "UserId" },
                values: new object[] { new Guid("45825bf4-457e-4d38-8fed-d271948b87df"), true, null, "Admin", new Guid("23749a20-099d-44ea-bed8-c8279192883a") });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "Active", "Description", "Role", "UserId" },
                values: new object[] { new Guid("693b25ad-12cf-495f-82f3-af07a70bb7c5"), true, null, "User", new Guid("c4ffad0d-f414-4ab6-b62e-d01cab7ef0f5") });

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
