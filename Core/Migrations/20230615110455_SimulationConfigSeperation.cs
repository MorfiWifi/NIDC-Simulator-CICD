using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class SimulationConfigSeperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "ConfigDetails",
                table: "Simulations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("33d5daf1-bedb-422d-ac60-586488dc9a97"), new DateTime(2023, 6, 15, 14, 4, 55, 553, DateTimeKind.Local).AddTicks(4381), false, new DateTime(2023, 6, 15, 14, 4, 55, 553, DateTimeKind.Local).AddTicks(4413), "Admin" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("72bbed94-078e-4251-8316-d6998dd7c1d4"), new DateTime(2023, 6, 15, 14, 4, 55, 553, DateTimeKind.Local).AddTicks(4418), false, new DateTime(2023, 6, 15, 14, 4, 55, 553, DateTimeKind.Local).AddTicks(4419), "Moderator" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("fa20de09-2618-434f-b425-2cb57e8fcee5"), new DateTime(2023, 6, 15, 14, 4, 55, 553, DateTimeKind.Local).AddTicks(4456), false, new DateTime(2023, 6, 15, 14, 4, 55, 553, DateTimeKind.Local).AddTicks(4458), "Developer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("33d5daf1-bedb-422d-ac60-586488dc9a97"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("72bbed94-078e-4251-8316-d6998dd7c1d4"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("fa20de09-2618-434f-b425-2cb57e8fcee5"));

            migrationBuilder.DropColumn(
                name: "ConfigDetails",
                table: "Simulations");

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
        }
    }
}
