using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class Folders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Folders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Folders_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Identity",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("034adaa6-d15a-4794-8d8e-19650e38ccf0"), new DateTime(2022, 2, 20, 14, 0, 28, 395, DateTimeKind.Local).AddTicks(6479), false, new DateTime(2022, 2, 20, 14, 0, 28, 402, DateTimeKind.Local).AddTicks(4939), "Admin" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("e441ab7a-e294-4920-91af-5335da9ef3be"), new DateTime(2022, 2, 20, 14, 0, 28, 402, DateTimeKind.Local).AddTicks(6552), false, new DateTime(2022, 2, 20, 14, 0, 28, 402, DateTimeKind.Local).AddTicks(6577), "Moderator" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("50a74212-bc4c-4694-8298-402821d273c5"), new DateTime(2022, 2, 20, 14, 0, 28, 402, DateTimeKind.Local).AddTicks(6587), false, new DateTime(2022, 2, 20, 14, 0, 28, 402, DateTimeKind.Local).AddTicks(6593), "Developer" });

            migrationBuilder.CreateIndex(
                name: "IX_Folders_AccountId",
                table: "Folders",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Folders");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("034adaa6-d15a-4794-8d8e-19650e38ccf0"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("50a74212-bc4c-4694-8298-402821d273c5"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e441ab7a-e294-4920-91af-5335da9ef3be"));

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
        }
    }
}
