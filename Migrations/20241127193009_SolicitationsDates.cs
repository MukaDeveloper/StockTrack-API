using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class SolicitationsDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BorroadAt",
                table: "ST_SOLICITATIONS",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnedAt",
                table: "ST_SOLICITATIONS",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ST_AREAS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 27, 19, 30, 9, 399, DateTimeKind.Utc).AddTicks(5282), new DateTime(2024, 11, 27, 19, 30, 9, 399, DateTimeKind.Utc).AddTicks(5282) });

            migrationBuilder.UpdateData(
                table: "ST_MATERIALS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 27, 19, 30, 9, 399, DateTimeKind.Utc).AddTicks(5321), new DateTime(2024, 11, 27, 19, 30, 9, 399, DateTimeKind.Utc).AddTicks(5321) });

            migrationBuilder.UpdateData(
                table: "ST_MOVIMENTATIONS",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 11, 27, 16, 30, 9, 399, DateTimeKind.Local).AddTicks(5385));

            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt", "VerifiedAt" },
                values: new object[] { new DateTime(2024, 11, 27, 16, 30, 9, 399, DateTimeKind.Local).AddTicks(5087), new byte[] { 208, 241, 93, 207, 63, 12, 101, 174, 135, 149, 68, 150, 167, 41, 208, 91, 161, 250, 184, 217, 232, 135, 164, 4, 22, 29, 27, 91, 251, 226, 72, 109, 97, 123, 114, 70, 33, 43, 105, 102, 54, 204, 232, 82, 220, 23, 172, 252, 149, 172, 190, 54, 42, 137, 137, 222, 0, 153, 71, 171, 25, 222, 172, 21 }, new byte[] { 147, 62, 124, 117, 232, 181, 114, 211, 39, 27, 110, 198, 84, 119, 42, 49, 37, 106, 243, 190, 78, 102, 97, 19, 129, 154, 105, 217, 252, 149, 57, 177, 215, 161, 28, 97, 65, 44, 25, 172, 173, 213, 188, 94, 4, 131, 136, 127, 168, 43, 19, 124, 35, 203, 140, 45, 207, 34, 97, 130, 110, 72, 70, 139, 86, 19, 111, 0, 230, 69, 71, 114, 122, 139, 67, 55, 105, 155, 114, 150, 108, 86, 168, 191, 147, 115, 58, 17, 216, 251, 159, 169, 218, 86, 129, 61, 203, 82, 240, 217, 54, 214, 228, 0, 52, 54, 125, 172, 122, 20, 171, 204, 221, 87, 173, 190, 82, 20, 75, 206, 88, 193, 178, 89, 58, 175, 126, 64 }, new DateTime(2024, 11, 27, 16, 30, 9, 399, DateTimeKind.Local).AddTicks(5097) });

            migrationBuilder.UpdateData(
                table: "ST_WAREHOUSES",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 27, 19, 30, 9, 399, DateTimeKind.Utc).AddTicks(5302), new DateTime(2024, 11, 27, 19, 30, 9, 399, DateTimeKind.Utc).AddTicks(5302) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BorroadAt",
                table: "ST_SOLICITATIONS");

            migrationBuilder.DropColumn(
                name: "ReturnedAt",
                table: "ST_SOLICITATIONS");

            migrationBuilder.UpdateData(
                table: "ST_AREAS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 23, 1, 19, 45, 430, DateTimeKind.Utc).AddTicks(1698), new DateTime(2024, 11, 23, 1, 19, 45, 430, DateTimeKind.Utc).AddTicks(1699) });

            migrationBuilder.UpdateData(
                table: "ST_MATERIALS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 23, 1, 19, 45, 430, DateTimeKind.Utc).AddTicks(1757), new DateTime(2024, 11, 23, 1, 19, 45, 430, DateTimeKind.Utc).AddTicks(1758) });

            migrationBuilder.UpdateData(
                table: "ST_MOVIMENTATIONS",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 11, 22, 22, 19, 45, 430, DateTimeKind.Local).AddTicks(1853));

            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt", "VerifiedAt" },
                values: new object[] { new DateTime(2024, 11, 22, 22, 19, 45, 430, DateTimeKind.Local).AddTicks(1391), new byte[] { 139, 129, 107, 201, 86, 14, 211, 180, 254, 212, 73, 36, 3, 219, 229, 184, 133, 25, 148, 222, 108, 168, 140, 150, 199, 31, 134, 99, 154, 102, 198, 158, 218, 140, 246, 235, 144, 104, 86, 217, 176, 2, 130, 154, 121, 114, 84, 228, 67, 6, 58, 141, 137, 43, 70, 1, 217, 46, 124, 67, 35, 136, 252, 1 }, new byte[] { 23, 206, 153, 114, 94, 77, 64, 240, 218, 169, 217, 231, 9, 113, 114, 123, 205, 116, 19, 178, 219, 130, 180, 148, 15, 155, 131, 194, 156, 98, 231, 146, 77, 26, 192, 120, 61, 30, 161, 31, 56, 221, 200, 160, 14, 167, 213, 185, 82, 142, 8, 89, 175, 159, 223, 49, 217, 94, 211, 7, 190, 135, 47, 17, 219, 94, 113, 139, 226, 47, 123, 26, 39, 147, 133, 26, 228, 79, 145, 255, 18, 205, 138, 238, 100, 186, 169, 11, 207, 14, 78, 95, 156, 38, 203, 18, 124, 183, 68, 124, 149, 68, 144, 75, 94, 227, 10, 16, 1, 204, 245, 40, 18, 194, 59, 16, 145, 236, 90, 53, 62, 41, 110, 42, 170, 70, 117, 216 }, new DateTime(2024, 11, 22, 22, 19, 45, 430, DateTimeKind.Local).AddTicks(1404) });

            migrationBuilder.UpdateData(
                table: "ST_WAREHOUSES",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 23, 1, 19, 45, 430, DateTimeKind.Utc).AddTicks(1727), new DateTime(2024, 11, 23, 1, 19, 45, 430, DateTimeKind.Utc).AddTicks(1727) });
        }
    }
}
