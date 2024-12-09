using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class SolicitationMovimentation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SolicitationId",
                table: "ST_MOVIMENTATIONS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ST_AREAS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 8, 23, 59, 12, 881, DateTimeKind.Utc).AddTicks(9493), new DateTime(2024, 12, 8, 23, 59, 12, 881, DateTimeKind.Utc).AddTicks(9493) });

            migrationBuilder.UpdateData(
                table: "ST_MATERIALS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 8, 23, 59, 12, 881, DateTimeKind.Utc).AddTicks(9551), new DateTime(2024, 12, 8, 23, 59, 12, 881, DateTimeKind.Utc).AddTicks(9551) });

            migrationBuilder.UpdateData(
                table: "ST_MOVIMENTATIONS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "SolicitationId" },
                values: new object[] { new DateTime(2024, 12, 8, 20, 59, 12, 881, DateTimeKind.Local).AddTicks(9669), 0 });

            migrationBuilder.InsertData(
                table: "ST_MOVIMENTATIONS",
                columns: new[] { "Id", "AreaId", "Date", "Description", "Event", "InstitutionId", "MaterialId", "MovimentationBy", "Name", "Quantity", "Reason", "SolicitationId", "Type", "UserId", "WarehouseId" },
                values: new object[] { 2, null, new DateTime(2024, 12, 8, 20, 59, 12, 881, DateTimeKind.Local).AddTicks(9675), "Adição de almoxarifado \"Informática\"", 0, 1, null, "Admin", "Informática", 1f, 0, 0, 2, null, 1 });

            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt", "VerifiedAt" },
                values: new object[] { new DateTime(2024, 12, 8, 20, 59, 12, 881, DateTimeKind.Local).AddTicks(9286), new byte[] { 8, 52, 180, 180, 179, 52, 249, 218, 119, 66, 53, 26, 107, 87, 169, 149, 236, 98, 54, 203, 230, 162, 171, 189, 209, 86, 95, 162, 202, 175, 115, 67, 41, 122, 124, 233, 18, 81, 84, 241, 200, 225, 67, 225, 45, 24, 10, 126, 227, 230, 180, 140, 0, 240, 249, 34, 114, 203, 249, 14, 99, 46, 58, 215 }, new byte[] { 167, 75, 119, 140, 232, 158, 142, 139, 57, 144, 91, 135, 113, 38, 101, 48, 57, 42, 7, 89, 194, 167, 150, 180, 113, 121, 80, 6, 10, 184, 227, 114, 166, 44, 27, 166, 202, 31, 247, 235, 43, 1, 109, 39, 223, 44, 63, 87, 85, 86, 153, 244, 145, 105, 114, 123, 13, 210, 85, 228, 38, 61, 3, 103, 26, 175, 250, 14, 77, 12, 137, 61, 73, 1, 190, 71, 42, 160, 249, 16, 6, 142, 240, 103, 142, 182, 218, 50, 226, 73, 124, 160, 44, 164, 252, 97, 34, 250, 187, 74, 161, 182, 144, 104, 49, 34, 144, 110, 242, 231, 140, 251, 41, 85, 231, 173, 1, 151, 72, 213, 128, 227, 23, 69, 164, 96, 250, 240 }, new DateTime(2024, 12, 8, 20, 59, 12, 881, DateTimeKind.Local).AddTicks(9303) });

            migrationBuilder.UpdateData(
                table: "ST_WAREHOUSES",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 8, 23, 59, 12, 881, DateTimeKind.Utc).AddTicks(9519), new DateTime(2024, 12, 8, 23, 59, 12, 881, DateTimeKind.Utc).AddTicks(9520) });

            migrationBuilder.CreateIndex(
                name: "IX_ST_MOVIMENTATIONS_SolicitationId",
                table: "ST_MOVIMENTATIONS",
                column: "SolicitationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ST_MOVIMENTATIONS_ST_SOLICITATIONS_SolicitationId",
                table: "ST_MOVIMENTATIONS",
                column: "SolicitationId",
                principalTable: "ST_SOLICITATIONS",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ST_MOVIMENTATIONS_ST_SOLICITATIONS_SolicitationId",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.DropIndex(
                name: "IX_ST_MOVIMENTATIONS_SolicitationId",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.DeleteData(
                table: "ST_MOVIMENTATIONS",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "SolicitationId",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.UpdateData(
                table: "ST_AREAS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 8, 20, 32, 6, 540, DateTimeKind.Utc).AddTicks(6818), new DateTime(2024, 12, 8, 20, 32, 6, 540, DateTimeKind.Utc).AddTicks(6818) });

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
                column: "Date",
                value: new DateTime(2024, 12, 8, 17, 32, 6, 540, DateTimeKind.Local).AddTicks(6965));

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
    }
}
