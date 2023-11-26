using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class Organization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0adabbc4-57d9-4ac5-9b17-64fcaca03fc5"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5995e97d-5926-49a3-86c6-a15113923070"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a6708f7b-aa14-4d90-92e0-f629a64e7cd5"));

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniqueUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationAdmins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationAdmins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationAdmins_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Identity",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationAdmins_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("4d5f1026-c675-4cf0-bca8-ef20c41c1260"), new DateTime(2022, 2, 19, 18, 21, 32, 920, DateTimeKind.Local).AddTicks(805), false, new DateTime(2022, 2, 19, 18, 21, 32, 928, DateTimeKind.Local).AddTicks(2530), "Admin" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("a9e4a120-4eff-4515-a1d2-b1f8a7cd7f4e"), new DateTime(2022, 2, 19, 18, 21, 32, 928, DateTimeKind.Local).AddTicks(4108), false, new DateTime(2022, 2, 19, 18, 21, 32, 928, DateTimeKind.Local).AddTicks(4128), "Moderator" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("f14f6ef7-ff00-4bed-9a86-ca9c76e10297"), new DateTime(2022, 2, 19, 18, 21, 32, 928, DateTimeKind.Local).AddTicks(4139), false, new DateTime(2022, 2, 19, 18, 21, 32, 928, DateTimeKind.Local).AddTicks(4144), "Developer" });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationAdmins_AccountId",
                table: "OrganizationAdmins",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationAdmins_OrganizationId",
                table: "OrganizationAdmins",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationAdmins");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4d5f1026-c675-4cf0-bca8-ef20c41c1260"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a9e4a120-4eff-4515-a1d2-b1f8a7cd7f4e"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f14f6ef7-ff00-4bed-9a86-ca9c76e10297"));

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("5995e97d-5926-49a3-86c6-a15113923070"), new DateTime(2022, 2, 8, 15, 49, 50, 856, DateTimeKind.Local).AddTicks(2477), false, new DateTime(2022, 2, 8, 15, 49, 50, 860, DateTimeKind.Local).AddTicks(9525), "Admin" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("0adabbc4-57d9-4ac5-9b17-64fcaca03fc5"), new DateTime(2022, 2, 8, 15, 49, 50, 861, DateTimeKind.Local).AddTicks(780), false, new DateTime(2022, 2, 8, 15, 49, 50, 861, DateTimeKind.Local).AddTicks(798), "Moderator" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("a6708f7b-aa14-4d90-92e0-f629a64e7cd5"), new DateTime(2022, 2, 8, 15, 49, 50, 861, DateTimeKind.Local).AddTicks(804), false, new DateTime(2022, 2, 8, 15, 49, 50, 861, DateTimeKind.Local).AddTicks(808), "Developer" });
        }
    }
}
