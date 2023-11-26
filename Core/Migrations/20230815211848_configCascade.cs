using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class configCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("114daf06-ee39-47a6-8a74-f8cf27f6abfd"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1a7ab0cd-35b9-4dd3-95b6-6cccee92413d"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b51710b8-c359-4417-9718-a3bc972dd5c6"));

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { new Guid("114daf06-ee39-47a6-8a74-f8cf27f6abfd"), new DateTime(2023, 8, 13, 9, 1, 25, 977, DateTimeKind.Local).AddTicks(4448), false, new DateTime(2023, 8, 13, 9, 1, 25, 977, DateTimeKind.Local).AddTicks(4450), "Developer" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("1a7ab0cd-35b9-4dd3-95b6-6cccee92413d"), new DateTime(2023, 8, 13, 9, 1, 25, 977, DateTimeKind.Local).AddTicks(4445), false, new DateTime(2023, 8, 13, 9, 1, 25, 977, DateTimeKind.Local).AddTicks(4446), "Moderator" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("b51710b8-c359-4417-9718-a3bc972dd5c6"), new DateTime(2023, 8, 13, 9, 1, 25, 977, DateTimeKind.Local).AddTicks(4404), false, new DateTime(2023, 8, 13, 9, 1, 25, 977, DateTimeKind.Local).AddTicks(4441), "Admin" });
        }
    }
}
