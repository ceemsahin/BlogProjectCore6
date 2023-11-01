using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("0057b95d-5c0f-415e-ad50-201c6df5f7cb"), "Cem2", new DateTime(2023, 10, 31, 11, 14, 53, 132, DateTimeKind.Local).AddTicks(7600), null, null, false, null, null, "Core mvc 2" },
                    { new Guid("81ca1870-8ff0-4c5b-8bbd-b6210380d48a"), "Cem", new DateTime(2023, 10, 31, 11, 14, 53, 132, DateTimeKind.Local).AddTicks(7595), null, null, false, null, null, "Core mvc" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "FileName", "FileType", "IsDeleted", "ModifiedBy", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("516eaf4e-3701-4ac4-acbf-410e4da5261f"), "Cem", new DateTime(2023, 10, 31, 11, 14, 53, 132, DateTimeKind.Local).AddTicks(7702), null, null, " images/testimage", "jpg", false, null, null },
                    { new Guid("bf66eb62-30e8-4f1a-a8fe-7d65a84581eb"), "Cem2", new DateTime(2023, 10, 31, 11, 14, 53, 132, DateTimeKind.Local).AddTicks(7707), null, null, " images/testimage2", "png", false, null, null }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "IsDeleted", "ModifiedBy", "ModifiedDate", "Title", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("5c35c46c-f62d-4328-a25d-0a3c5a06792e"), new Guid("81ca1870-8ff0-4c5b-8bbd-b6210380d48a"), "Lorem ipsum", "Cem", new DateTime(2023, 10, 31, 11, 14, 53, 132, DateTimeKind.Local).AddTicks(7355), null, null, new Guid("516eaf4e-3701-4ac4-acbf-410e4da5261f"), false, null, null, "Test1", 15 },
                    { new Guid("6ae012f7-393f-440c-9c9f-99b572b58278"), new Guid("0057b95d-5c0f-415e-ad50-201c6df5f7cb"), "Lorem ipsum2", "Cem2", new DateTime(2023, 10, 31, 11, 14, 53, 132, DateTimeKind.Local).AddTicks(7362), null, null, new Guid("bf66eb62-30e8-4f1a-a8fe-7d65a84581eb"), false, null, null, "Test2", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("5c35c46c-f62d-4328-a25d-0a3c5a06792e"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("6ae012f7-393f-440c-9c9f-99b572b58278"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0057b95d-5c0f-415e-ad50-201c6df5f7cb"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("81ca1870-8ff0-4c5b-8bbd-b6210380d48a"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("516eaf4e-3701-4ac4-acbf-410e4da5261f"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("bf66eb62-30e8-4f1a-a8fe-7d65a84581eb"));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Articles",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
