using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DataExtensions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("5c35c46c-f62d-4328-a25d-0a3c5a06792e"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("6ae012f7-393f-440c-9c9f-99b572b58278"));

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("96b8c1cf-5196-4a03-b833-a83b2c2b9f73"), new Guid("0057b95d-5c0f-415e-ad50-201c6df5f7cb"), "Lorem ipsum2", "Cem2", new DateTime(2023, 11, 1, 10, 3, 57, 810, DateTimeKind.Local).AddTicks(4427), null, null, new Guid("bf66eb62-30e8-4f1a-a8fe-7d65a84581eb"), false, null, null, "Test2", 1 },
                    { new Guid("c4dc541e-e947-4157-b08f-7e37ad1524ef"), new Guid("81ca1870-8ff0-4c5b-8bbd-b6210380d48a"), "Lorem ipsum", "Cem", new DateTime(2023, 11, 1, 10, 3, 57, 810, DateTimeKind.Local).AddTicks(4420), null, null, new Guid("516eaf4e-3701-4ac4-acbf-410e4da5261f"), false, null, null, "Test1", 15 }
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0057b95d-5c0f-415e-ad50-201c6df5f7cb"),
                column: "CreatedDate",
                value: new DateTime(2023, 11, 1, 10, 3, 57, 810, DateTimeKind.Local).AddTicks(4660));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("81ca1870-8ff0-4c5b-8bbd-b6210380d48a"),
                column: "CreatedDate",
                value: new DateTime(2023, 11, 1, 10, 3, 57, 810, DateTimeKind.Local).AddTicks(4654));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("516eaf4e-3701-4ac4-acbf-410e4da5261f"),
                column: "CreatedDate",
                value: new DateTime(2023, 11, 1, 10, 3, 57, 810, DateTimeKind.Local).AddTicks(4766));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("bf66eb62-30e8-4f1a-a8fe-7d65a84581eb"),
                column: "CreatedDate",
                value: new DateTime(2023, 11, 1, 10, 3, 57, 810, DateTimeKind.Local).AddTicks(4771));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("96b8c1cf-5196-4a03-b833-a83b2c2b9f73"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("c4dc541e-e947-4157-b08f-7e37ad1524ef"));

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("5c35c46c-f62d-4328-a25d-0a3c5a06792e"), new Guid("81ca1870-8ff0-4c5b-8bbd-b6210380d48a"), "Lorem ipsum", "Cem", new DateTime(2023, 10, 31, 11, 14, 53, 132, DateTimeKind.Local).AddTicks(7355), null, null, new Guid("516eaf4e-3701-4ac4-acbf-410e4da5261f"), false, null, null, "Test1", 15 },
                    { new Guid("6ae012f7-393f-440c-9c9f-99b572b58278"), new Guid("0057b95d-5c0f-415e-ad50-201c6df5f7cb"), "Lorem ipsum2", "Cem2", new DateTime(2023, 10, 31, 11, 14, 53, 132, DateTimeKind.Local).AddTicks(7362), null, null, new Guid("bf66eb62-30e8-4f1a-a8fe-7d65a84581eb"), false, null, null, "Test2", 1 }
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0057b95d-5c0f-415e-ad50-201c6df5f7cb"),
                column: "CreatedDate",
                value: new DateTime(2023, 10, 31, 11, 14, 53, 132, DateTimeKind.Local).AddTicks(7600));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("81ca1870-8ff0-4c5b-8bbd-b6210380d48a"),
                column: "CreatedDate",
                value: new DateTime(2023, 10, 31, 11, 14, 53, 132, DateTimeKind.Local).AddTicks(7595));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("516eaf4e-3701-4ac4-acbf-410e4da5261f"),
                column: "CreatedDate",
                value: new DateTime(2023, 10, 31, 11, 14, 53, 132, DateTimeKind.Local).AddTicks(7702));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("bf66eb62-30e8-4f1a-a8fe-7d65a84581eb"),
                column: "CreatedDate",
                value: new DateTime(2023, 10, 31, 11, 14, 53, 132, DateTimeKind.Local).AddTicks(7707));
        }
    }
}
