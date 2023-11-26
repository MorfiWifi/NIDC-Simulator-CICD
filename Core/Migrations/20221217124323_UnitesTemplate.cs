using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class UnitesTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8772e6b2-c95f-409b-a7ec-45877c358512"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d490b7a0-8848-4d8f-a965-4b8d76d7a015"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f401b94f-383d-47a5-868a-f9ec69692dee"));

            migrationBuilder.AddColumn<string>(
                name: "Template",
                table: "Units",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Template",
                table: "Units");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("d490b7a0-8848-4d8f-a965-4b8d76d7a015"), new DateTime(2022, 10, 26, 12, 40, 45, 426, DateTimeKind.Local).AddTicks(242), false, new DateTime(2022, 10, 26, 12, 40, 45, 429, DateTimeKind.Local).AddTicks(2174), "Admin" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("f401b94f-383d-47a5-868a-f9ec69692dee"), new DateTime(2022, 10, 26, 12, 40, 45, 429, DateTimeKind.Local).AddTicks(3173), false, new DateTime(2022, 10, 26, 12, 40, 45, 429, DateTimeKind.Local).AddTicks(3188), "Moderator" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("8772e6b2-c95f-409b-a7ec-45877c358512"), new DateTime(2022, 10, 26, 12, 40, 45, 429, DateTimeKind.Local).AddTicks(3195), false, new DateTime(2022, 10, 26, 12, 40, 45, 429, DateTimeKind.Local).AddTicks(3198), "Developer" });
        }
    }
}
