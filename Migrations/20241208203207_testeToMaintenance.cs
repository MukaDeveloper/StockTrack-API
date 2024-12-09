using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class testeToMaintenance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ST_AREAS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 8, 20, 32, 6, 540, DateTimeKind.Utc).AddTicks(6818), "Conjunto de almoxarifados da área norte", "Norte", new DateTime(2024, 12, 8, 20, 32, 6, 540, DateTimeKind.Utc).AddTicks(6818) });

            migrationBuilder.UpdateData(
                table: "ST_INSTITUTIONS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Nickname" },
                values: new object[] { "Manutenção", "Manutenção" });

            migrationBuilder.UpdateData(
                table: "ST_MATERIALS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 8, 20, 32, 6, 540, DateTimeKind.Utc).AddTicks(6871), new DateTime(2024, 12, 8, 20, 32, 6, 540, DateTimeKind.Utc).AddTicks(6872) });

            migrationBuilder.UpdateData(
                table: "ST_MOVIMENTATIONS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "Description", "Name" },
                values: new object[] { new DateTime(2024, 12, 8, 17, 32, 6, 540, DateTimeKind.Local).AddTicks(6965), "Adição de área \"Norte\"", "Norte" });

            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt", "VerifiedAt" },
                values: new object[] { new DateTime(2024, 12, 8, 17, 32, 6, 540, DateTimeKind.Local).AddTicks(6538), new byte[] { 203, 165, 15, 29, 102, 210, 215, 75, 175, 10, 90, 234, 45, 242, 70, 69, 187, 195, 250, 203, 207, 102, 185, 174, 236, 251, 16, 224, 140, 126, 87, 97, 199, 146, 254, 183, 54, 147, 136, 229, 105, 31, 105, 39, 209, 153, 71, 201, 243, 113, 155, 144, 214, 81, 230, 83, 184, 142, 29, 121, 42, 55, 102, 198 }, new byte[] { 44, 97, 120, 185, 19, 46, 152, 103, 36, 25, 132, 54, 219, 92, 137, 64, 168, 66, 150, 146, 220, 24, 92, 229, 48, 236, 236, 225, 34, 240, 94, 21, 117, 178, 36, 195, 176, 47, 146, 123, 246, 100, 109, 195, 171, 2, 238, 5, 90, 234, 95, 65, 13, 137, 92, 193, 38, 96, 45, 218, 28, 61, 4, 36, 154, 199, 203, 191, 200, 221, 192, 3, 241, 96, 0, 144, 100, 158, 80, 74, 234, 191, 68, 45, 44, 119, 40, 91, 139, 214, 104, 101, 72, 136, 79, 76, 7, 208, 132, 40, 156, 111, 8, 131, 190, 94, 26, 165, 232, 45, 124, 175, 124, 238, 167, 163, 67, 44, 50, 121, 161, 23, 18, 95, 16, 88, 250, 142 }, new DateTime(2024, 12, 8, 17, 32, 6, 540, DateTimeKind.Local).AddTicks(6551) });

            migrationBuilder.UpdateData(
                table: "ST_WAREHOUSES",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 8, 20, 32, 6, 540, DateTimeKind.Utc).AddTicks(6844), new DateTime(2024, 12, 8, 20, 32, 6, 540, DateTimeKind.Utc).AddTicks(6844) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ST_AREAS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 4, 0, 19, 25, 201, DateTimeKind.Utc).AddTicks(9570), "Área de Testes", "Teste", new DateTime(2024, 12, 4, 0, 19, 25, 201, DateTimeKind.Utc).AddTicks(9570) });

            migrationBuilder.UpdateData(
                table: "ST_INSTITUTIONS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Nickname" },
                values: new object[] { "Servidor de testes", "Testes" });

            migrationBuilder.UpdateData(
                table: "ST_MATERIALS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 4, 0, 19, 25, 201, DateTimeKind.Utc).AddTicks(9636), new DateTime(2024, 12, 4, 0, 19, 25, 201, DateTimeKind.Utc).AddTicks(9637) });

            migrationBuilder.UpdateData(
                table: "ST_MOVIMENTATIONS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "Description", "Name" },
                values: new object[] { new DateTime(2024, 12, 3, 21, 19, 25, 201, DateTimeKind.Local).AddTicks(9736), "Adição de área \"Teste\"", "Área Teste" });

            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt", "VerifiedAt" },
                values: new object[] { new DateTime(2024, 12, 3, 21, 19, 25, 201, DateTimeKind.Local).AddTicks(9285), new byte[] { 165, 177, 42, 93, 86, 230, 251, 199, 229, 235, 191, 72, 241, 1, 204, 252, 89, 203, 3, 177, 188, 26, 94, 181, 239, 96, 140, 75, 24, 248, 119, 183, 37, 255, 38, 99, 228, 1, 188, 144, 217, 69, 202, 164, 118, 168, 42, 225, 222, 179, 129, 46, 137, 186, 5, 39, 46, 56, 99, 146, 53, 123, 32, 153 }, new byte[] { 222, 29, 182, 146, 3, 227, 116, 250, 111, 64, 145, 38, 99, 214, 2, 84, 85, 96, 195, 191, 48, 167, 198, 76, 241, 115, 167, 154, 236, 165, 225, 64, 226, 29, 101, 0, 167, 218, 79, 211, 96, 223, 94, 188, 163, 12, 159, 230, 199, 10, 105, 73, 249, 57, 135, 125, 114, 181, 156, 28, 106, 71, 174, 178, 43, 164, 139, 56, 0, 121, 140, 55, 15, 239, 34, 95, 20, 18, 134, 115, 60, 226, 120, 175, 123, 11, 71, 238, 133, 63, 221, 32, 73, 240, 130, 188, 152, 225, 107, 26, 64, 93, 19, 63, 174, 187, 83, 139, 71, 28, 116, 123, 128, 51, 22, 114, 90, 100, 19, 174, 104, 216, 240, 166, 154, 195, 227, 246 }, new DateTime(2024, 12, 3, 21, 19, 25, 201, DateTimeKind.Local).AddTicks(9302) });

            migrationBuilder.UpdateData(
                table: "ST_WAREHOUSES",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 4, 0, 19, 25, 201, DateTimeKind.Utc).AddTicks(9602), new DateTime(2024, 12, 4, 0, 19, 25, 201, DateTimeKind.Utc).AddTicks(9602) });
        }
    }
}
