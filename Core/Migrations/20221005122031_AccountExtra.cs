using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class AccountExtra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "ConfigsCount",
                schema: "Identity",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Identity",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SimulationLength",
                schema: "Identity",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("5f84d9cb-347d-4db9-b54e-f936ce8b4366"), new DateTime(2022, 10, 5, 15, 50, 30, 561, DateTimeKind.Local).AddTicks(9799), false, new DateTime(2022, 10, 5, 15, 50, 30, 565, DateTimeKind.Local).AddTicks(4128), "Admin" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("5b2ec1df-cf16-4b9a-85d8-d92e8646d612"), new DateTime(2022, 10, 5, 15, 50, 30, 565, DateTimeKind.Local).AddTicks(6087), false, new DateTime(2022, 10, 5, 15, 50, 30, 565, DateTimeKind.Local).AddTicks(6123), "Moderator" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("c08da12c-06c3-4260-9fa6-10527bf83fed"), new DateTime(2022, 10, 5, 15, 50, 30, 565, DateTimeKind.Local).AddTicks(6135), false, new DateTime(2022, 10, 5, 15, 50, 30, 565, DateTimeKind.Local).AddTicks(6138), "Developer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5b2ec1df-cf16-4b9a-85d8-d92e8646d612"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5f84d9cb-347d-4db9-b54e-f936ce8b4366"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c08da12c-06c3-4260-9fa6-10527bf83fed"));

            migrationBuilder.DropColumn(
                name: "ConfigsCount",
                schema: "Identity",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Identity",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "SimulationLength",
                schema: "Identity",
                table: "Accounts");

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
    }
}
