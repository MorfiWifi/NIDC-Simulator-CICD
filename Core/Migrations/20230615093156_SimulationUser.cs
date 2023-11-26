using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class SimulationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SimulationConfigs_Accounts_AccountId",
                table: "SimulationConfigs");

            migrationBuilder.DropTable(
                name: "FcmTokens",
                schema: "Identity");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "SimulationConfigs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "Simulations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRunning = table.Column<bool>(type: "bit", nullable: false),
                    ConfigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Simulations_SimulationConfigs_ConfigId",
                        column: x => x.ConfigId,
                        principalTable: "SimulationConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimulationUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SimulationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulationUsers_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Identity",
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SimulationUsers_Simulations_SimulationId",
                        column: x => x.SimulationId,
                        principalTable: "Simulations",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("3e240e13-e484-4079-bdfe-d183929c65b5"), new DateTime(2023, 6, 15, 12, 31, 55, 919, DateTimeKind.Local).AddTicks(4187), false, new DateTime(2023, 6, 15, 12, 31, 55, 919, DateTimeKind.Local).AddTicks(4189), "Developer" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("66958a7d-1f9b-4cfe-83b3-6e63a859c0ab"), new DateTime(2023, 6, 15, 12, 31, 55, 919, DateTimeKind.Local).AddTicks(4146), false, new DateTime(2023, 6, 15, 12, 31, 55, 919, DateTimeKind.Local).AddTicks(4179), "Admin" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("fa52825d-209c-4d64-87f7-60dfa5fae119"), new DateTime(2023, 6, 15, 12, 31, 55, 919, DateTimeKind.Local).AddTicks(4183), false, new DateTime(2023, 6, 15, 12, 31, 55, 919, DateTimeKind.Local).AddTicks(4185), "Moderator" });

            migrationBuilder.CreateIndex(
                name: "IX_Simulations_ConfigId",
                table: "Simulations",
                column: "ConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_SimulationUsers_AccountId",
                table: "SimulationUsers",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SimulationUsers_SimulationId",
                table: "SimulationUsers",
                column: "SimulationId");

            migrationBuilder.AddForeignKey(
                name: "FK_SimulationConfigs_Accounts_AccountId",
                table: "SimulationConfigs",
                column: "AccountId",
                principalSchema: "Identity",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SimulationConfigs_Accounts_AccountId",
                table: "SimulationConfigs");

            migrationBuilder.DropTable(
                name: "SimulationUsers");

            migrationBuilder.DropTable(
                name: "Simulations");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3e240e13-e484-4079-bdfe-d183929c65b5"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("66958a7d-1f9b-4cfe-83b3-6e63a859c0ab"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("fa52825d-209c-4d64-87f7-60dfa5fae119"));

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "SimulationConfigs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "FcmTokens",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FcmTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FcmTokens_Accounts_AccountId1",
                        column: x => x.AccountId1,
                        principalSchema: "Identity",
                        principalTable: "Accounts",
                        principalColumn: "Id");
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
                name: "IX_FcmTokens_AccountId1",
                schema: "Identity",
                table: "FcmTokens",
                column: "AccountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SimulationConfigs_Accounts_AccountId",
                table: "SimulationConfigs",
                column: "AccountId",
                principalSchema: "Identity",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
