using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gymapp.Migrations
{
    public partial class addClassPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 16, 15, 5, 4, 461, DateTimeKind.Local).AddTicks(7749));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 12, 16, 15, 5, 4, 461, DateTimeKind.Local).AddTicks(7759));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2022, 12, 16, 15, 5, 4, 461, DateTimeKind.Local).AddTicks(7761));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2022, 12, 16, 15, 5, 4, 461, DateTimeKind.Local).AddTicks(7764));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2022, 12, 16, 15, 5, 4, 461, DateTimeKind.Local).AddTicks(7765));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2022, 12, 16, 15, 5, 4, 461, DateTimeKind.Local).AddTicks(7852));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2022, 12, 16, 15, 5, 4, 461, DateTimeKind.Local).AddTicks(7854));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2022, 12, 16, 15, 5, 4, 461, DateTimeKind.Local).AddTicks(7855));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2022, 12, 16, 15, 5, 4, 461, DateTimeKind.Local).AddTicks(7857));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2022, 12, 16, 15, 5, 4, 461, DateTimeKind.Local).AddTicks(7858));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2022, 12, 16, 15, 5, 4, 461, DateTimeKind.Local).AddTicks(7860));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "91a0915e-03ce-4c50-9a37-87dafd7184d0");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6dcabc58-8c5c-4231-9a66-02c038da7429",
                column: "ConcurrencyStamp",
                value: "7f275e33-afff-4cc8-ba77-aa3850982c07");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "76251a37-7bb0-4f6a-80bd-c454effb7285",
                column: "ConcurrencyStamp",
                value: "cddd4020-0a14-4f4e-a6d9-87bb405758a7");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "81f01616-665c-4703-8f59-c96497bd2a55", "AQAAAAEAACcQAAAAEKvxLCVSiJl6OgdwtvAvP7S1TCaPsCm/WWv3OcFVePvKqSKUPgxXZQSLKfkFAPLsrw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Classes");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 6, 20, 47, 47, 118, DateTimeKind.Local).AddTicks(7506));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 12, 6, 20, 47, 47, 118, DateTimeKind.Local).AddTicks(7518));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2022, 12, 6, 20, 47, 47, 118, DateTimeKind.Local).AddTicks(7520));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2022, 12, 6, 20, 47, 47, 118, DateTimeKind.Local).AddTicks(7521));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2022, 12, 6, 20, 47, 47, 118, DateTimeKind.Local).AddTicks(7523));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2022, 12, 6, 20, 47, 47, 118, DateTimeKind.Local).AddTicks(7524));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2022, 12, 6, 20, 47, 47, 118, DateTimeKind.Local).AddTicks(7525));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2022, 12, 6, 20, 47, 47, 118, DateTimeKind.Local).AddTicks(7527));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2022, 12, 6, 20, 47, 47, 118, DateTimeKind.Local).AddTicks(7528));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2022, 12, 6, 20, 47, 47, 118, DateTimeKind.Local).AddTicks(7530));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2022, 12, 6, 20, 47, 47, 118, DateTimeKind.Local).AddTicks(7531));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "92189c12-096b-4d64-be8d-0802100ece20");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6dcabc58-8c5c-4231-9a66-02c038da7429",
                column: "ConcurrencyStamp",
                value: "9343f001-7ac2-4845-8499-333b1e931dfe");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "76251a37-7bb0-4f6a-80bd-c454effb7285",
                column: "ConcurrencyStamp",
                value: "e2e31082-05d9-40cf-9240-790faa9d864b");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f48f563e-d9e4-48f6-918f-87609200f60b", "AQAAAAEAACcQAAAAENT5MMAuoODUGcRPid6luYg+a523LhgiEcx19GS2s86ULro5LBphl5HAcmOuy6jADQ==" });
        }
    }
}
