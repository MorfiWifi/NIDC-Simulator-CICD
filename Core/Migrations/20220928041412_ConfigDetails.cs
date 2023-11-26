using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class ConfigDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "ConfigDetails",
                table: "SimulationConfigs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("9a95d2c4-1dac-48cf-84a8-c835ae00c8f4"), new DateTime(2022, 9, 28, 7, 44, 11, 723, DateTimeKind.Local).AddTicks(6358), false, new DateTime(2022, 9, 28, 7, 44, 11, 725, DateTimeKind.Local).AddTicks(7687), "Admin" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("d66fc1f9-605c-474c-a2d7-87c912d4ae10"), new DateTime(2022, 9, 28, 7, 44, 11, 725, DateTimeKind.Local).AddTicks(8167), false, new DateTime(2022, 9, 28, 7, 44, 11, 725, DateTimeKind.Local).AddTicks(8178), "Moderator" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("1c070dfc-f5b5-488c-95ce-77f98899bf11"), new DateTime(2022, 9, 28, 7, 44, 11, 725, DateTimeKind.Local).AddTicks(8182), false, new DateTime(2022, 9, 28, 7, 44, 11, 725, DateTimeKind.Local).AddTicks(8184), "Developer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1c070dfc-f5b5-488c-95ce-77f98899bf11"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9a95d2c4-1dac-48cf-84a8-c835ae00c8f4"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d66fc1f9-605c-474c-a2d7-87c912d4ae10"));

            migrationBuilder.DropColumn(
                name: "ConfigDetails",
                table: "SimulationConfigs");

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
        }
    }
}
