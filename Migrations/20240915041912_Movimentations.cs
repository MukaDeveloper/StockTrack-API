using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class Movimentations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "ST_MOVIMENTATIONS",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "ST_MOVIMENTATIONS",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ST_MOVIMENTATIONS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "InstitutionId",
                table: "ST_MOVIMENTATIONS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "ST_MOVIMENTATIONS",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Reason",
                table: "ST_MOVIMENTATIONS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ST_MOVIMENTATIONS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ST_MOVIMENTATIONS",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "ST_MOVIMENTATIONS",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MovimentationTypeEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentationTypeEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypeEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypeEntity", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MovimentationTypeEntity",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ENTRY" },
                    { 2, "EXIT" }
                });

            migrationBuilder.UpdateData(
                table: "ST_AREAS",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 15, 4, 19, 11, 449, DateTimeKind.Utc).AddTicks(3370));

            migrationBuilder.UpdateData(
                table: "ST_MATERIALS",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 15, 4, 19, 11, 449, DateTimeKind.Utc).AddTicks(3421));

            migrationBuilder.InsertData(
                table: "ST_MOVIMENTATIONS",
                columns: new[] { "Id", "AreaId", "Date", "Description", "InstitutionId", "MaterialId", "Reason", "Type", "UserId", "WarehouseId" },
                values: new object[] { 1, 1, new DateTime(2024, 9, 15, 1, 19, 11, 449, DateTimeKind.Local).AddTicks(3481), "Adição de área \"Teste\"", 1, null, 1, 1, 1, null });

            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 131, 1, 90, 77, 67, 131, 37, 97, 137, 164, 61, 209, 74, 34, 141, 27, 226, 162, 5, 216, 77, 32, 33, 140, 242, 151, 253, 106, 164, 232, 170, 127, 200, 248, 126, 115, 118, 244, 205, 118, 172, 170, 98, 48, 147, 214, 82, 13, 204, 34, 102, 19, 164, 88, 179, 136, 196, 61, 89, 198, 94, 128, 59, 149 }, new byte[] { 37, 131, 8, 79, 182, 210, 25, 52, 207, 45, 204, 250, 133, 58, 231, 253, 84, 199, 25, 134, 145, 219, 150, 13, 116, 131, 150, 225, 31, 2, 167, 187, 136, 245, 250, 149, 182, 178, 34, 204, 188, 109, 163, 100, 77, 228, 109, 88, 213, 161, 212, 228, 164, 159, 202, 51, 204, 118, 114, 88, 36, 242, 159, 187, 36, 54, 70, 76, 99, 204, 176, 254, 54, 49, 130, 86, 215, 138, 131, 247, 12, 125, 12, 54, 129, 65, 180, 214, 15, 255, 28, 195, 242, 208, 113, 30, 0, 192, 71, 241, 79, 152, 42, 92, 159, 222, 210, 96, 158, 7, 148, 192, 182, 166, 240, 242, 70, 135, 6, 251, 128, 249, 251, 135, 239, 154, 91, 62 } });

            migrationBuilder.UpdateData(
                table: "ST_WAREHOUSES",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 15, 4, 19, 11, 449, DateTimeKind.Utc).AddTicks(3400));

            migrationBuilder.InsertData(
                table: "UserTypeEntity",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "USER" },
                    { 2, "WAREHOUSEMAN" },
                    { 3, "COORDINATOR" },
                    { 4, "SUPPORT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ST_MOVIMENTATIONS_AreaId",
                table: "ST_MOVIMENTATIONS",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ST_MOVIMENTATIONS_MaterialId",
                table: "ST_MOVIMENTATIONS",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ST_MOVIMENTATIONS_UserId",
                table: "ST_MOVIMENTATIONS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ST_MOVIMENTATIONS_WarehouseId",
                table: "ST_MOVIMENTATIONS",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ST_MOVIMENTATIONS_ST_AREAS_AreaId",
                table: "ST_MOVIMENTATIONS",
                column: "AreaId",
                principalTable: "ST_AREAS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ST_MOVIMENTATIONS_ST_MATERIALS_MaterialId",
                table: "ST_MOVIMENTATIONS",
                column: "MaterialId",
                principalTable: "ST_MATERIALS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ST_MOVIMENTATIONS_ST_USERS_UserId",
                table: "ST_MOVIMENTATIONS",
                column: "UserId",
                principalTable: "ST_USERS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ST_MOVIMENTATIONS_ST_WAREHOUSES_WarehouseId",
                table: "ST_MOVIMENTATIONS",
                column: "WarehouseId",
                principalTable: "ST_WAREHOUSES",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ST_MOVIMENTATIONS_ST_AREAS_AreaId",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.DropForeignKey(
                name: "FK_ST_MOVIMENTATIONS_ST_MATERIALS_MaterialId",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.DropForeignKey(
                name: "FK_ST_MOVIMENTATIONS_ST_USERS_UserId",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.DropForeignKey(
                name: "FK_ST_MOVIMENTATIONS_ST_WAREHOUSES_WarehouseId",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.DropTable(
                name: "MovimentationTypeEntity");

            migrationBuilder.DropTable(
                name: "UserTypeEntity");

            migrationBuilder.DropIndex(
                name: "IX_ST_MOVIMENTATIONS_AreaId",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.DropIndex(
                name: "IX_ST_MOVIMENTATIONS_MaterialId",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.DropIndex(
                name: "IX_ST_MOVIMENTATIONS_UserId",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.DropIndex(
                name: "IX_ST_MOVIMENTATIONS_WarehouseId",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.DeleteData(
                table: "ST_MOVIMENTATIONS",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.DropColumn(
                name: "InstitutionId",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.UpdateData(
                table: "ST_AREAS",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 4, 23, 46, 21, 834, DateTimeKind.Utc).AddTicks(6900));

            migrationBuilder.UpdateData(
                table: "ST_MATERIALS",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 4, 23, 46, 21, 834, DateTimeKind.Utc).AddTicks(6931));

            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 250, 205, 250, 114, 75, 65, 123, 172, 104, 219, 219, 124, 123, 5, 50, 203, 139, 187, 85, 65, 49, 223, 153, 211, 34, 134, 8, 11, 254, 94, 21, 230, 168, 117, 147, 255, 151, 83, 33, 224, 35, 136, 206, 21, 193, 72, 218, 202, 240, 82, 162, 179, 252, 10, 130, 62, 215, 113, 210, 42, 36, 185, 129, 72 }, new byte[] { 211, 226, 18, 248, 186, 109, 17, 231, 25, 25, 105, 65, 247, 238, 135, 208, 64, 53, 91, 215, 232, 56, 167, 60, 120, 143, 221, 61, 222, 232, 2, 178, 33, 193, 59, 29, 176, 101, 13, 142, 93, 108, 253, 227, 236, 0, 218, 210, 34, 93, 75, 52, 44, 8, 174, 207, 181, 162, 147, 36, 185, 2, 7, 123, 19, 194, 92, 184, 35, 252, 138, 138, 14, 13, 159, 37, 203, 80, 192, 113, 157, 7, 101, 123, 186, 158, 102, 28, 63, 3, 23, 202, 153, 207, 239, 82, 211, 31, 234, 96, 187, 203, 247, 128, 175, 69, 132, 56, 94, 46, 125, 141, 40, 168, 12, 79, 95, 245, 92, 145, 218, 195, 21, 125, 69, 222, 147, 207 } });

            migrationBuilder.UpdateData(
                table: "ST_WAREHOUSES",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 4, 23, 46, 21, 834, DateTimeKind.Utc).AddTicks(6918));
        }
    }
}
