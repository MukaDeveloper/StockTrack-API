using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class BaseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "ST_USERS");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ST_WAREHOUSES",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ST_WAREHOUSES",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ST_WAREHOUSES",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ST_WAREHOUSES",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ST_WAREHOUSES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ST_USERS",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "ST_USER_INSTITUTIONS",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ST_MATERIALS",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ST_MATERIALS",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ST_MATERIALS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ST_MATERIALS",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ST_MATERIALS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ST_AREAS",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ST_AREAS",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ST_AREAS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ST_AREAS",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ST_AREAS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ST_AREAS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Active", "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy" },
                values: new object[] { true, new DateTime(2024, 9, 4, 20, 58, 22, 753, DateTimeKind.Utc).AddTicks(7625), "", null, null });

            migrationBuilder.UpdateData(
                table: "ST_MATERIALS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Active", "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy" },
                values: new object[] { true, new DateTime(2024, 9, 4, 20, 58, 22, 753, DateTimeKind.Utc).AddTicks(7706), "", null, null });

            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Active", "PasswordHash", "PasswordSalt" },
                values: new object[] { true, new byte[] { 42, 254, 147, 239, 45, 224, 112, 97, 104, 9, 205, 95, 55, 183, 243, 1, 16, 41, 247, 98, 218, 210, 43, 162, 53, 226, 80, 111, 163, 93, 196, 226, 131, 54, 223, 113, 186, 95, 221, 193, 156, 90, 68, 151, 12, 114, 176, 226, 183, 118, 11, 26, 210, 219, 242, 13, 232, 122, 19, 84, 228, 161, 254, 122 }, new byte[] { 190, 145, 33, 66, 143, 151, 182, 220, 137, 197, 242, 79, 229, 153, 101, 184, 115, 182, 157, 51, 131, 150, 166, 73, 179, 62, 226, 101, 159, 27, 111, 72, 159, 53, 95, 107, 120, 81, 223, 89, 109, 57, 18, 121, 178, 100, 64, 248, 207, 92, 97, 254, 116, 239, 236, 45, 109, 155, 192, 39, 2, 25, 196, 245, 205, 27, 221, 231, 174, 106, 150, 67, 63, 179, 228, 110, 68, 5, 35, 241, 92, 102, 175, 52, 243, 139, 209, 223, 92, 43, 28, 210, 120, 77, 224, 109, 97, 139, 175, 101, 221, 11, 204, 150, 12, 50, 83, 98, 250, 26, 91, 138, 142, 230, 206, 204, 8, 78, 39, 150, 234, 88, 169, 181, 156, 227, 153, 175 } });

            migrationBuilder.UpdateData(
                table: "ST_USER_INSTITUTIONS",
                keyColumns: new[] { "InstitutionId", "UserId" },
                keyValues: new object[] { 1, 1 },
                column: "UserType",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ST_USER_INSTITUTIONS",
                keyColumns: new[] { "InstitutionId", "UserId" },
                keyValues: new object[] { 64, 1 },
                column: "UserType",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ST_WAREHOUSES",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Active", "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy" },
                values: new object[] { true, new DateTime(2024, 9, 4, 20, 58, 22, 753, DateTimeKind.Utc).AddTicks(7670), "", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "ST_WAREHOUSES");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ST_WAREHOUSES");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ST_WAREHOUSES");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ST_WAREHOUSES");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ST_WAREHOUSES");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "ST_USERS");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "ST_USER_INSTITUTIONS");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "ST_MATERIALS");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ST_MATERIALS");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ST_MATERIALS");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ST_MATERIALS");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ST_MATERIALS");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "ST_AREAS");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ST_AREAS");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ST_AREAS");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ST_AREAS");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ST_AREAS");

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "ST_USERS",
                type: "int",
                nullable: true,
                defaultValue: 1);

            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt", "UserType" },
                values: new object[] { new byte[] { 227, 137, 245, 69, 184, 199, 106, 151, 92, 104, 223, 131, 41, 39, 77, 217, 62, 236, 48, 63, 190, 248, 196, 65, 77, 151, 169, 213, 41, 58, 214, 170, 170, 94, 155, 190, 63, 30, 173, 214, 171, 128, 74, 47, 129, 220, 194, 84, 222, 246, 164, 193, 220, 162, 8, 231, 63, 54, 118, 48, 11, 104, 3, 58 }, new byte[] { 252, 223, 167, 168, 229, 240, 149, 235, 49, 90, 99, 27, 15, 253, 200, 219, 252, 192, 213, 220, 223, 69, 189, 83, 243, 103, 81, 154, 44, 110, 162, 241, 50, 174, 9, 17, 138, 24, 6, 37, 169, 136, 202, 109, 244, 137, 170, 195, 15, 60, 236, 56, 198, 183, 21, 38, 85, 191, 171, 23, 115, 21, 194, 255, 217, 249, 142, 26, 197, 224, 249, 125, 181, 9, 251, 231, 90, 48, 157, 227, 19, 82, 231, 9, 199, 130, 147, 144, 251, 135, 28, 159, 191, 9, 77, 179, 21, 130, 83, 73, 15, 24, 108, 194, 152, 238, 242, 68, 149, 90, 19, 202, 93, 27, 118, 134, 72, 158, 117, 226, 93, 69, 246, 103, 226, 215, 0, 195 }, 4 });
        }
    }
}
