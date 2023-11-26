using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class AccountConfigs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2673c98f-0670-42d3-9a5f-e88ab0acfd73"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3bd07626-72ae-4ccf-8c77-747197cb655f"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("96afc574-34ee-497e-8968-73a26eab5fc5"));

            migrationBuilder.CreateTable(
                name: "AccountConfigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConfigId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountConfigs_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Identity",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountConfigs_SimulationConfigs_ConfigId",
                        column: x => x.ConfigId,
                        principalTable: "SimulationConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("7504d8ca-6c06-412f-a82b-af7838c0309a"), new DateTime(2022, 12, 23, 15, 42, 2, 563, DateTimeKind.Local).AddTicks(5340), false, new DateTime(2022, 12, 23, 15, 42, 2, 567, DateTimeKind.Local).AddTicks(5847), "Admin" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("c094be70-d89e-4a5c-b272-eff4cbd85bf3"), new DateTime(2022, 12, 23, 15, 42, 2, 567, DateTimeKind.Local).AddTicks(7669), false, new DateTime(2022, 12, 23, 15, 42, 2, 567, DateTimeKind.Local).AddTicks(7685), "Moderator" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("cc75a0f7-a49f-44a4-a809-b4ba256cc601"), new DateTime(2022, 12, 23, 15, 42, 2, 567, DateTimeKind.Local).AddTicks(7691), false, new DateTime(2022, 12, 23, 15, 42, 2, 567, DateTimeKind.Local).AddTicks(7694), "Developer" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfigs_AccountId",
                table: "AccountConfigs",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfigs_ConfigId",
                table: "AccountConfigs",
                column: "ConfigId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountConfigs");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7504d8ca-6c06-412f-a82b-af7838c0309a"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c094be70-d89e-4a5c-b272-eff4cbd85bf3"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cc75a0f7-a49f-44a4-a809-b4ba256cc601"));

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("2673c98f-0670-42d3-9a5f-e88ab0acfd73"), new DateTime(2022, 12, 17, 16, 13, 23, 44, DateTimeKind.Local).AddTicks(7039), false, new DateTime(2022, 12, 17, 16, 13, 23, 48, DateTimeKind.Local).AddTicks(6258), "Admin" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("96afc574-34ee-497e-8968-73a26eab5fc5"), new DateTime(2022, 12, 17, 16, 13, 23, 48, DateTimeKind.Local).AddTicks(7556), false, new DateTime(2022, 12, 17, 16, 13, 23, 48, DateTimeKind.Local).AddTicks(7568), "Moderator" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("3bd07626-72ae-4ccf-8c77-747197cb655f"), new DateTime(2022, 12, 17, 16, 13, 23, 48, DateTimeKind.Local).AddTicks(7572), false, new DateTime(2022, 12, 17, 16, 13, 23, 48, DateTimeKind.Local).AddTicks(7574), "Developer" });
        }
    }
}
