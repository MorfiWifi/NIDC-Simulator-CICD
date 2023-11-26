using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class Configs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "SimulationConfigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FolderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulationConfigs_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Identity",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SimulationConfigs_Folders_FolderId",
                        column: x => x.FolderId,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("51d71e4e-9adb-4a09-80f6-4f1ea0095177"), new DateTime(2022, 9, 27, 16, 37, 24, 417, DateTimeKind.Local).AddTicks(1859), false, new DateTime(2022, 9, 27, 16, 37, 24, 419, DateTimeKind.Local).AddTicks(1388), "Admin" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("cd000585-d5ff-491a-a467-9cfda02044b1"), new DateTime(2022, 9, 27, 16, 37, 24, 419, DateTimeKind.Local).AddTicks(1863), false, new DateTime(2022, 9, 27, 16, 37, 24, 419, DateTimeKind.Local).AddTicks(1874), "Moderator" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("2c80e4de-5c95-432d-87bd-ff976fd17622"), new DateTime(2022, 9, 27, 16, 37, 24, 419, DateTimeKind.Local).AddTicks(1877), false, new DateTime(2022, 9, 27, 16, 37, 24, 419, DateTimeKind.Local).AddTicks(1879), "Developer" });

            migrationBuilder.CreateIndex(
                name: "IX_SimulationConfigs_AccountId",
                table: "SimulationConfigs",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SimulationConfigs_FolderId",
                table: "SimulationConfigs",
                column: "FolderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimulationConfigs");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2c80e4de-5c95-432d-87bd-ff976fd17622"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("51d71e4e-9adb-4a09-80f6-4f1ea0095177"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cd000585-d5ff-491a-a467-9cfda02044b1"));

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
        }
    }
}
