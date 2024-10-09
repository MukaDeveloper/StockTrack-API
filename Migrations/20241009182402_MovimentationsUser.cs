using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class MovimentationsUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ST_AREAS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 9, 18, 24, 1, 910, DateTimeKind.Utc).AddTicks(2442), new DateTime(2024, 10, 9, 18, 24, 1, 910, DateTimeKind.Utc).AddTicks(2442) });

            migrationBuilder.UpdateData(
                table: "ST_MATERIALS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 9, 18, 24, 1, 910, DateTimeKind.Utc).AddTicks(2527), new DateTime(2024, 10, 9, 18, 24, 1, 910, DateTimeKind.Utc).AddTicks(2527) });

            migrationBuilder.UpdateData(
                table: "ST_MOVIMENTATIONS",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 10, 9, 15, 24, 1, 910, DateTimeKind.Local).AddTicks(2549));

            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 10, 9, 15, 24, 1, 910, DateTimeKind.Local).AddTicks(2225), new byte[] { 159, 125, 3, 33, 204, 90, 99, 160, 90, 235, 193, 208, 253, 116, 239, 114, 107, 239, 84, 70, 201, 186, 179, 224, 106, 163, 171, 181, 160, 16, 193, 170, 103, 170, 90, 77, 145, 41, 45, 192, 145, 85, 104, 170, 242, 188, 240, 128, 108, 64, 27, 8, 99, 3, 120, 57, 145, 192, 14, 143, 236, 16, 67, 5 }, new byte[] { 61, 39, 224, 18, 248, 177, 163, 109, 232, 148, 56, 82, 67, 104, 61, 162, 230, 177, 205, 5, 52, 81, 93, 108, 132, 197, 48, 146, 165, 42, 185, 101, 253, 53, 152, 168, 183, 112, 18, 3, 165, 14, 67, 162, 237, 222, 101, 234, 0, 182, 97, 52, 199, 87, 116, 148, 134, 245, 216, 193, 147, 192, 71, 221, 144, 40, 153, 45, 242, 59, 5, 111, 241, 157, 111, 165, 90, 143, 189, 21, 165, 43, 123, 142, 1, 103, 167, 3, 222, 245, 13, 196, 206, 198, 139, 151, 246, 32, 251, 29, 201, 61, 114, 207, 222, 247, 178, 8, 129, 207, 53, 193, 65, 163, 58, 52, 102, 192, 7, 102, 31, 232, 216, 12, 91, 242, 22, 133 } });

            migrationBuilder.UpdateData(
                table: "ST_WAREHOUSES",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 9, 18, 24, 1, 910, DateTimeKind.Utc).AddTicks(2462), new DateTime(2024, 10, 9, 18, 24, 1, 910, DateTimeKind.Utc).AddTicks(2463) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ST_AREAS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 8, 3, 52, 18, 177, DateTimeKind.Utc).AddTicks(8198), new DateTime(2024, 10, 8, 3, 52, 18, 177, DateTimeKind.Utc).AddTicks(8199) });

            migrationBuilder.UpdateData(
                table: "ST_MATERIALS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 8, 3, 52, 18, 177, DateTimeKind.Utc).AddTicks(8292), new DateTime(2024, 10, 8, 3, 52, 18, 177, DateTimeKind.Utc).AddTicks(8292) });

            migrationBuilder.UpdateData(
                table: "ST_MOVIMENTATIONS",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 10, 8, 0, 52, 18, 177, DateTimeKind.Local).AddTicks(8316));

            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 10, 8, 0, 52, 18, 177, DateTimeKind.Local).AddTicks(8001), new byte[] { 209, 100, 90, 65, 197, 0, 83, 167, 139, 52, 107, 131, 132, 167, 45, 109, 125, 14, 203, 48, 140, 130, 199, 109, 37, 38, 74, 0, 135, 28, 172, 102, 198, 29, 126, 230, 29, 145, 145, 229, 215, 7, 102, 160, 111, 2, 8, 155, 145, 89, 171, 84, 192, 132, 142, 14, 235, 141, 207, 223, 32, 144, 167, 244 }, new byte[] { 105, 66, 151, 21, 154, 231, 69, 218, 194, 160, 206, 65, 1, 228, 194, 250, 236, 93, 6, 155, 51, 247, 236, 200, 136, 151, 87, 243, 29, 186, 137, 246, 91, 158, 3, 31, 165, 242, 194, 110, 180, 47, 3, 209, 161, 54, 178, 87, 212, 95, 104, 3, 86, 119, 12, 181, 118, 233, 109, 183, 118, 115, 158, 132, 75, 80, 110, 142, 237, 158, 192, 21, 111, 55, 170, 97, 155, 168, 112, 47, 4, 64, 14, 108, 219, 69, 37, 19, 94, 211, 236, 120, 121, 238, 30, 248, 193, 166, 221, 53, 197, 253, 41, 231, 93, 44, 136, 57, 80, 50, 145, 135, 56, 234, 113, 203, 152, 224, 138, 61, 40, 72, 158, 64, 211, 50, 111, 182 } });

            migrationBuilder.UpdateData(
                table: "ST_WAREHOUSES",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 8, 3, 52, 18, 177, DateTimeKind.Utc).AddTicks(8254), new DateTime(2024, 10, 8, 3, 52, 18, 177, DateTimeKind.Utc).AddTicks(8254) });
        }
    }
}
