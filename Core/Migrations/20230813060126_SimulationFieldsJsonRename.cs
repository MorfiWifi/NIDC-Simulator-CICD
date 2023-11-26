using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class SimulationFieldsJsonRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("01714234-f147-4e5a-842c-22043029e2a8"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c048f3fd-7a40-412f-8f2d-6ad30693daac"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("fc8c6f8b-8d1c-4a27-b34d-621265672e27"));

            migrationBuilder.RenameColumn(
                name: "SimulationFields",
                table: "Simulations",
                newName: "SimulationFieldsJson");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "SimulationFieldsJson",
                table: "Simulations",
                newName: "SimulationFields");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("01714234-f147-4e5a-842c-22043029e2a8"), new DateTime(2023, 8, 13, 8, 59, 35, 121, DateTimeKind.Local).AddTicks(1661), false, new DateTime(2023, 8, 13, 8, 59, 35, 121, DateTimeKind.Local).AddTicks(1663), "Moderator" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("c048f3fd-7a40-412f-8f2d-6ad30693daac"), new DateTime(2023, 8, 13, 8, 59, 35, 121, DateTimeKind.Local).AddTicks(1619), false, new DateTime(2023, 8, 13, 8, 59, 35, 121, DateTimeKind.Local).AddTicks(1655), "Admin" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("fc8c6f8b-8d1c-4a27-b34d-621265672e27"), new DateTime(2023, 8, 13, 8, 59, 35, 121, DateTimeKind.Local).AddTicks(1665), false, new DateTime(2023, 8, 13, 8, 59, 35, 121, DateTimeKind.Local).AddTicks(1666), "Developer" });
        }
    }
}
