using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class Repag2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ST_MATERIALS");

            migrationBuilder.CreateTable(
                name: "ST_MATERIALS_STATUS",
                columns: table => new
                {
                    Status = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_MATERIALS_STATUS", x => new { x.MaterialId, x.Status });
                    table.ForeignKey(
                        name: "FK_ST_MATERIALS_STATUS_ST_MATERIALS_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "ST_MATERIALS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "ST_AREAS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 16, 21, 6, 25, 904, DateTimeKind.Utc).AddTicks(371), new DateTime(2024, 10, 16, 21, 6, 25, 904, DateTimeKind.Utc).AddTicks(371) });

            migrationBuilder.UpdateData(
                table: "ST_MATERIALS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 16, 21, 6, 25, 904, DateTimeKind.Utc).AddTicks(432), new DateTime(2024, 10, 16, 21, 6, 25, 904, DateTimeKind.Utc).AddTicks(432) });

            migrationBuilder.InsertData(
                table: "ST_MATERIALS_STATUS",
                columns: new[] { "MaterialId", "Status", "Quantity" },
                values: new object[,]
                {
                    { 1, 0, 3f },
                    { 1, 1, 1f },
                    { 1, 2, 1f },
                    { 1, 3, 5f },
                    { 1, 4, 1f }
                });

            migrationBuilder.InsertData(
                table: "ST_MATERIAL_WAREHOUSES",
                columns: new[] { "MaterialId", "WarehouseId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "ST_MOVIMENTATIONS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "Event", "Reason", "Type" },
                values: new object[] { new DateTime(2024, 10, 16, 18, 6, 25, 904, DateTimeKind.Local).AddTicks(537), 0, 0, 1 });

            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 10, 16, 18, 6, 25, 903, DateTimeKind.Local).AddTicks(9956), new byte[] { 170, 160, 228, 147, 0, 192, 183, 217, 73, 247, 166, 214, 82, 151, 226, 60, 226, 244, 158, 52, 51, 248, 213, 71, 140, 38, 85, 155, 195, 192, 42, 59, 150, 95, 206, 111, 75, 120, 65, 29, 198, 110, 224, 220, 246, 212, 139, 148, 225, 153, 139, 132, 197, 163, 68, 177, 132, 215, 78, 76, 75, 73, 131, 12 }, new byte[] { 133, 11, 139, 97, 17, 247, 148, 165, 184, 201, 245, 111, 32, 105, 13, 94, 66, 57, 15, 245, 21, 68, 137, 162, 218, 21, 149, 125, 41, 211, 95, 206, 97, 226, 244, 113, 42, 204, 156, 33, 198, 231, 252, 205, 51, 73, 35, 187, 88, 220, 171, 30, 76, 133, 86, 36, 170, 208, 151, 93, 142, 146, 49, 230, 104, 218, 148, 30, 6, 206, 3, 238, 73, 79, 104, 42, 180, 28, 32, 78, 160, 233, 204, 226, 248, 218, 163, 186, 161, 238, 169, 193, 52, 173, 228, 96, 76, 117, 200, 212, 120, 23, 51, 227, 1, 174, 189, 40, 75, 73, 185, 219, 216, 10, 196, 28, 196, 228, 32, 115, 149, 55, 105, 12, 165, 194, 125, 215 } });

            migrationBuilder.UpdateData(
                table: "ST_USER_INSTITUTIONS",
                keyColumns: new[] { "InstitutionId", "UserId" },
                keyValues: new object[] { 1, 1 },
                column: "UserRole",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ST_USER_INSTITUTIONS",
                keyColumns: new[] { "InstitutionId", "UserId" },
                keyValues: new object[] { 2, 1 },
                column: "UserRole",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ST_WAREHOUSES",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 16, 21, 6, 25, 904, DateTimeKind.Utc).AddTicks(401), new DateTime(2024, 10, 16, 21, 6, 25, 904, DateTimeKind.Utc).AddTicks(401) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ST_MATERIALS_STATUS");

            migrationBuilder.DeleteData(
                table: "ST_MATERIAL_WAREHOUSES",
                keyColumns: new[] { "MaterialId", "WarehouseId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.AddColumn<float>(
                name: "Quantity",
                table: "ST_MATERIALS",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "ST_AREAS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 16, 20, 37, 35, 315, DateTimeKind.Utc).AddTicks(6253), new DateTime(2024, 10, 16, 20, 37, 35, 315, DateTimeKind.Utc).AddTicks(6254) });

            migrationBuilder.UpdateData(
                table: "ST_MATERIALS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Quantity", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 16, 20, 37, 35, 315, DateTimeKind.Utc).AddTicks(6322), 3f, new DateTime(2024, 10, 16, 20, 37, 35, 315, DateTimeKind.Utc).AddTicks(6323) });

            migrationBuilder.UpdateData(
                table: "ST_MOVIMENTATIONS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "Event", "Reason", "Type" },
                values: new object[] { new DateTime(2024, 10, 16, 17, 37, 35, 315, DateTimeKind.Local).AddTicks(6348), 1, 1, 2 });

            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 10, 16, 17, 37, 35, 315, DateTimeKind.Local).AddTicks(5906), new byte[] { 150, 206, 138, 251, 156, 163, 58, 146, 126, 193, 106, 194, 211, 225, 158, 226, 237, 46, 116, 141, 78, 2, 158, 101, 126, 33, 82, 218, 53, 107, 204, 142, 60, 152, 166, 8, 100, 225, 125, 141, 145, 38, 223, 8, 28, 160, 30, 96, 75, 215, 48, 238, 45, 252, 212, 28, 87, 53, 91, 224, 229, 112, 215, 207 }, new byte[] { 218, 2, 218, 161, 114, 77, 41, 65, 98, 13, 165, 210, 228, 202, 89, 184, 68, 83, 112, 1, 87, 85, 182, 139, 31, 196, 111, 193, 35, 37, 144, 166, 30, 196, 5, 165, 67, 238, 139, 159, 13, 141, 235, 148, 140, 174, 130, 60, 226, 202, 227, 140, 72, 102, 84, 101, 74, 84, 20, 218, 36, 17, 46, 221, 226, 130, 60, 171, 41, 101, 193, 182, 13, 161, 63, 212, 136, 174, 201, 84, 129, 230, 244, 216, 241, 0, 76, 33, 102, 115, 1, 31, 146, 215, 13, 165, 117, 119, 195, 161, 189, 126, 131, 78, 247, 53, 48, 201, 224, 12, 123, 155, 169, 40, 0, 11, 64, 12, 37, 105, 115, 122, 49, 155, 202, 91, 88, 27 } });

            migrationBuilder.UpdateData(
                table: "ST_USER_INSTITUTIONS",
                keyColumns: new[] { "InstitutionId", "UserId" },
                keyValues: new object[] { 1, 1 },
                column: "UserRole",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ST_USER_INSTITUTIONS",
                keyColumns: new[] { "InstitutionId", "UserId" },
                keyValues: new object[] { 2, 1 },
                column: "UserRole",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ST_WAREHOUSES",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 16, 20, 37, 35, 315, DateTimeKind.Utc).AddTicks(6283), new DateTime(2024, 10, 16, 20, 37, 35, 315, DateTimeKind.Utc).AddTicks(6284) });
        }
    }
}
