using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class AccountsOrganizations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                schema: "Identity",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("6624e1e5-705c-488b-abd3-0d6d4c95f74d"), new DateTime(2022, 10, 7, 16, 59, 5, 543, DateTimeKind.Local).AddTicks(2234), false, new DateTime(2022, 10, 7, 16, 59, 5, 545, DateTimeKind.Local).AddTicks(1992), "Admin" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("8b3933ef-b498-477d-ba71-be4fc2db44a8"), new DateTime(2022, 10, 7, 16, 59, 5, 545, DateTimeKind.Local).AddTicks(2530), false, new DateTime(2022, 10, 7, 16, 59, 5, 545, DateTimeKind.Local).AddTicks(2540), "Moderator" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Roles",
                columns: new[] { "Id", "CreateDate", "Deleted", "ModifyDate", "Name" },
                values: new object[] { new Guid("f2a74110-572d-462c-9919-9628dedde723"), new DateTime(2022, 10, 7, 16, 59, 5, 545, DateTimeKind.Local).AddTicks(2544), false, new DateTime(2022, 10, 7, 16, 59, 5, 545, DateTimeKind.Local).AddTicks(2546), "Developer" });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_OrganizationId",
                schema: "Identity",
                table: "Accounts",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Organizations_OrganizationId",
                schema: "Identity",
                table: "Accounts",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Organizations_OrganizationId",
                schema: "Identity",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_OrganizationId",
                schema: "Identity",
                table: "Accounts");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6624e1e5-705c-488b-abd3-0d6d4c95f74d"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8b3933ef-b498-477d-ba71-be4fc2db44a8"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f2a74110-572d-462c-9919-9628dedde723"));

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "Identity",
                table: "Accounts");

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
    }
}
