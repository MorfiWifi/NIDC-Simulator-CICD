using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class SimulationFieldsJson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "SimulationFields",
                table: "Simulations",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "SimulationFields",
                table: "Simulations");

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
    }
}
