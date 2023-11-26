using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class configAccountCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountConfigs_SimulationConfigs_ConfigId",
                table: "AccountConfigs");

            migrationBuilder.DropIndex(
                name: "IX_AccountConfigs_ConfigId",
                table: "AccountConfigs");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("19a589bd-be7c-4d0f-aac5-bb7a57cb41d6"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("28c14441-4058-4bbf-a237-0a258d443f97"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5780ae65-14af-443d-b784-3c385c0e51bb"));

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("3fc3a229-6c45-4dc4-8549-2819782fb533"), new DateTime(2023, 8, 16, 0, 23, 17, 505, DateTimeKind.Local).AddTicks(3262), false, new DateTime(2023, 8, 16, 0, 23, 17, 505, DateTimeKind.Local).AddTicks(3302), "Admin" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("42871abf-411e-4792-b468-3b07bcc1ce24"), new DateTime(2023, 8, 16, 0, 23, 17, 505, DateTimeKind.Local).AddTicks(3311), false, new DateTime(2023, 8, 16, 0, 23, 17, 505, DateTimeKind.Local).AddTicks(3313), "Developer" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("d5c8c1a2-5a2b-4b91-b4e0-aaba9d42f11c"), new DateTime(2023, 8, 16, 0, 23, 17, 505, DateTimeKind.Local).AddTicks(3308), false, new DateTime(2023, 8, 16, 0, 23, 17, 505, DateTimeKind.Local).AddTicks(3309), "Moderator" });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountConfigs_SimulationConfigs_AccountId",
                table: "AccountConfigs",
                column: "AccountId",
                principalTable: "SimulationConfigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountConfigs_SimulationConfigs_AccountId",
                table: "AccountConfigs");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3fc3a229-6c45-4dc4-8549-2819782fb533"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("42871abf-411e-4792-b468-3b07bcc1ce24"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d5c8c1a2-5a2b-4b91-b4e0-aaba9d42f11c"));

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("19a589bd-be7c-4d0f-aac5-bb7a57cb41d6"), new DateTime(2023, 8, 16, 0, 18, 48, 594, DateTimeKind.Local).AddTicks(2021), false, new DateTime(2023, 8, 16, 0, 18, 48, 594, DateTimeKind.Local).AddTicks(2022), "Developer" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("28c14441-4058-4bbf-a237-0a258d443f97"), new DateTime(2023, 8, 16, 0, 18, 48, 594, DateTimeKind.Local).AddTicks(2016), false, new DateTime(2023, 8, 16, 0, 18, 48, 594, DateTimeKind.Local).AddTicks(2018), "Moderator" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("5780ae65-14af-443d-b784-3c385c0e51bb"), new DateTime(2023, 8, 16, 0, 18, 48, 594, DateTimeKind.Local).AddTicks(1978), false, new DateTime(2023, 8, 16, 0, 18, 48, 594, DateTimeKind.Local).AddTicks(2012), "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfigs_ConfigId",
                table: "AccountConfigs",
                column: "ConfigId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountConfigs_SimulationConfigs_ConfigId",
                table: "AccountConfigs",
                column: "ConfigId",
                principalTable: "SimulationConfigs",
                principalColumn: "Id");
        }
    }
}
