using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class SimulatonCreatorUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "Creator",
                table: "SimulationUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("6bdac0fe-907b-496d-8a63-f1db665fdbf2"), new DateTime(2023, 6, 23, 18, 12, 6, 21, DateTimeKind.Local).AddTicks(9766), false, new DateTime(2023, 6, 23, 18, 12, 6, 21, DateTimeKind.Local).AddTicks(9767), "Moderator" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("71cb82ae-30b4-4b20-98dd-c7f35c457e98"), new DateTime(2023, 6, 23, 18, 12, 6, 21, DateTimeKind.Local).AddTicks(9769), false, new DateTime(2023, 6, 23, 18, 12, 6, 21, DateTimeKind.Local).AddTicks(9771), "Developer" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("ab4678a5-6e5b-420c-92f3-0b3cdad013cb"), new DateTime(2023, 6, 23, 18, 12, 6, 21, DateTimeKind.Local).AddTicks(9730), false, new DateTime(2023, 6, 23, 18, 12, 6, 21, DateTimeKind.Local).AddTicks(9760), "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6bdac0fe-907b-496d-8a63-f1db665fdbf2"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("71cb82ae-30b4-4b20-98dd-c7f35c457e98"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ab4678a5-6e5b-420c-92f3-0b3cdad013cb"));

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "SimulationUsers");

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
    }
}
