using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class AccountUnits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6624e1e5-705c-488b-abd3-0d6d4c95f74d"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8b3933ef-b498-477d-ba71-be4fc2db44a8"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f2a74110-572d-462c-9919-9628dedde723"));

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Coefficient = table.Column<double>(type: "float", nullable: false),
                    System = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountUnits_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Identity",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountUnits_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("d490b7a0-8848-4d8f-a965-4b8d76d7a015"), new DateTime(2022, 10, 26, 12, 40, 45, 426, DateTimeKind.Local).AddTicks(242), false, new DateTime(2022, 10, 26, 12, 40, 45, 429, DateTimeKind.Local).AddTicks(2174), "Admin" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("f401b94f-383d-47a5-868a-f9ec69692dee"), new DateTime(2022, 10, 26, 12, 40, 45, 429, DateTimeKind.Local).AddTicks(3173), false, new DateTime(2022, 10, 26, 12, 40, 45, 429, DateTimeKind.Local).AddTicks(3188), "Moderator" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("8772e6b2-c95f-409b-a7ec-45877c358512"), new DateTime(2022, 10, 26, 12, 40, 45, 429, DateTimeKind.Local).AddTicks(3195), false, new DateTime(2022, 10, 26, 12, 40, 45, 429, DateTimeKind.Local).AddTicks(3198), "Developer" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountUnits_AccountId",
                table: "AccountUnits",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUnits_UnitId",
                table: "AccountUnits",
                column: "UnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountUnits");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8772e6b2-c95f-409b-a7ec-45877c358512"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d490b7a0-8848-4d8f-a965-4b8d76d7a015"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f401b94f-383d-47a5-868a-f9ec69692dee"));

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("6624e1e5-705c-488b-abd3-0d6d4c95f74d"), new DateTime(2022, 10, 7, 16, 59, 5, 543, DateTimeKind.Local).AddTicks(2234), false, new DateTime(2022, 10, 7, 16, 59, 5, 545, DateTimeKind.Local).AddTicks(1992), "Admin" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("8b3933ef-b498-477d-ba71-be4fc2db44a8"), new DateTime(2022, 10, 7, 16, 59, 5, 545, DateTimeKind.Local).AddTicks(2530), false, new DateTime(2022, 10, 7, 16, 59, 5, 545, DateTimeKind.Local).AddTicks(2540), "Moderator" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("f2a74110-572d-462c-9919-9628dedde723"), new DateTime(2022, 10, 7, 16, 59, 5, 545, DateTimeKind.Local).AddTicks(2544), false, new DateTime(2022, 10, 7, 16, 59, 5, 545, DateTimeKind.Local).AddTicks(2546), "Developer" });
        }
    }
}
