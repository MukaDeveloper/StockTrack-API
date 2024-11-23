using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class AlterSolicitationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpectReturnAt",
                table: "ST_SOLICITATIONS",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ST_SOLICITATION_MATERIALS",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectReturnAt",
                table: "ST_SOLICITATIONS");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ST_SOLICITATION_MATERIALS");

            migrationBuilder.UpdateData(
                table: "ST_AREAS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 21, 23, 33, 9, 574, DateTimeKind.Utc).AddTicks(9614), new DateTime(2024, 11, 21, 23, 33, 9, 574, DateTimeKind.Utc).AddTicks(9614) });

            migrationBuilder.UpdateData(
                table: "ST_MATERIALS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 21, 23, 33, 9, 574, DateTimeKind.Utc).AddTicks(9674), new DateTime(2024, 11, 21, 23, 33, 9, 574, DateTimeKind.Utc).AddTicks(9674) });

            migrationBuilder.UpdateData(
                table: "ST_MOVIMENTATIONS",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 11, 21, 20, 33, 9, 574, DateTimeKind.Local).AddTicks(9737));

            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt", "VerifiedAt" },
                values: new object[] { new DateTime(2024, 11, 21, 20, 33, 9, 574, DateTimeKind.Local).AddTicks(9432), new byte[] { 176, 151, 210, 217, 157, 5, 116, 164, 184, 134, 102, 23, 221, 146, 88, 24, 157, 46, 148, 6, 103, 45, 118, 213, 220, 76, 220, 177, 156, 211, 112, 178, 134, 251, 87, 2, 176, 185, 178, 145, 84, 37, 232, 223, 6, 213, 50, 40, 91, 46, 7, 158, 17, 131, 70, 170, 194, 156, 230, 242, 240, 7, 0, 200 }, new byte[] { 100, 63, 140, 17, 64, 76, 42, 84, 233, 158, 20, 165, 12, 217, 93, 61, 47, 238, 40, 216, 144, 179, 177, 227, 150, 182, 143, 160, 148, 106, 101, 165, 36, 87, 225, 66, 232, 3, 154, 192, 98, 204, 215, 118, 246, 8, 126, 77, 6, 98, 94, 183, 47, 167, 106, 186, 146, 11, 153, 92, 158, 152, 249, 154, 70, 241, 61, 230, 133, 200, 250, 98, 126, 151, 68, 22, 76, 191, 59, 18, 213, 155, 113, 55, 101, 147, 113, 60, 179, 82, 113, 104, 31, 5, 203, 132, 12, 119, 68, 153, 212, 85, 220, 196, 196, 47, 63, 111, 48, 12, 176, 24, 145, 156, 76, 155, 155, 224, 137, 189, 240, 114, 175, 199, 172, 237, 83, 142 }, new DateTime(2024, 11, 21, 20, 33, 9, 574, DateTimeKind.Local).AddTicks(9446) });

            migrationBuilder.UpdateData(
                table: "ST_WAREHOUSES",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 21, 23, 33, 9, 574, DateTimeKind.Utc).AddTicks(9632), new DateTime(2024, 11, 21, 23, 33, 9, 574, DateTimeKind.Utc).AddTicks(9632) });
        }
    }
}
