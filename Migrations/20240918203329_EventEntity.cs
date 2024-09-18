using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class EventEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Event",
                table: "ST_MOVIMENTATIONS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MovimentationBy",
                table: "ST_MOVIMENTATIONS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MovimentationEventEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Event = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentationEventEntity", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MovimentationEventEntity",
                columns: new[] { "Id", "Event" },
                values: new object[,]
                {
                    { 1, "Area" },
                    { 2, "Warehouse" },
                    { 3, "Material" },
                    { 4, "Loan" },
                    { 5, "Maintenance" },
                    { 6, "General" }
                });

            migrationBuilder.UpdateData(
                table: "ST_AREAS",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 18, 20, 33, 28, 773, DateTimeKind.Utc).AddTicks(1142));

            migrationBuilder.UpdateData(
                table: "ST_MATERIALS",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 18, 20, 33, 28, 773, DateTimeKind.Utc).AddTicks(1260));

            migrationBuilder.UpdateData(
                table: "ST_MOVIMENTATIONS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "Event", "MovimentationBy", "UserId" },
                values: new object[] { new DateTime(2024, 9, 18, 17, 33, 28, 773, DateTimeKind.Local).AddTicks(1294), 1, "Admin", null });

            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 243, 156, 53, 214, 19, 165, 58, 64, 12, 2, 228, 132, 139, 221, 181, 255, 198, 72, 154, 51, 111, 23, 87, 59, 176, 157, 130, 197, 108, 194, 27, 104, 255, 171, 37, 54, 128, 80, 193, 52, 118, 169, 198, 38, 133, 148, 57, 38, 206, 201, 153, 86, 21, 222, 170, 88, 240, 21, 139, 128, 20, 25, 51, 221 }, new byte[] { 232, 109, 162, 47, 16, 113, 107, 195, 93, 255, 188, 90, 149, 182, 17, 211, 68, 215, 195, 3, 185, 151, 70, 201, 53, 109, 107, 166, 202, 156, 116, 141, 123, 33, 206, 191, 112, 238, 11, 246, 92, 23, 43, 148, 145, 247, 34, 47, 233, 13, 95, 49, 255, 65, 148, 251, 18, 54, 99, 89, 163, 145, 199, 4, 147, 113, 42, 202, 122, 246, 11, 12, 30, 225, 52, 149, 183, 27, 89, 119, 16, 73, 180, 32, 203, 35, 24, 11, 240, 146, 171, 251, 102, 160, 153, 184, 117, 48, 3, 253, 196, 29, 74, 79, 225, 182, 112, 108, 149, 106, 73, 2, 63, 159, 83, 72, 2, 23, 198, 152, 161, 93, 138, 105, 83, 211, 205, 6 } });

            migrationBuilder.UpdateData(
                table: "ST_WAREHOUSES",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 18, 20, 33, 28, 773, DateTimeKind.Utc).AddTicks(1171));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimentationEventEntity");

            migrationBuilder.DropColumn(
                name: "Event",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.DropColumn(
                name: "MovimentationBy",
                table: "ST_MOVIMENTATIONS");

            migrationBuilder.UpdateData(
                table: "ST_AREAS",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 16, 2, 30, 58, 934, DateTimeKind.Utc).AddTicks(8554));

            migrationBuilder.UpdateData(
                table: "ST_MATERIALS",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 16, 2, 30, 58, 934, DateTimeKind.Utc).AddTicks(8616));

            migrationBuilder.UpdateData(
                table: "ST_MOVIMENTATIONS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "UserId" },
                values: new object[] { new DateTime(2024, 9, 15, 23, 30, 58, 934, DateTimeKind.Local).AddTicks(8645), 1 });

            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 117, 169, 51, 200, 118, 19, 7, 244, 120, 100, 67, 53, 105, 9, 143, 4, 158, 108, 52, 146, 214, 202, 243, 90, 174, 127, 121, 182, 59, 192, 203, 98, 149, 205, 218, 179, 220, 190, 157, 18, 72, 93, 47, 168, 102, 61, 26, 244, 222, 248, 16, 51, 222, 116, 41, 232, 198, 83, 26, 149, 77, 125, 55, 85 }, new byte[] { 94, 195, 71, 4, 208, 214, 244, 36, 31, 245, 65, 231, 10, 99, 133, 135, 76, 199, 49, 115, 4, 160, 235, 116, 239, 84, 93, 129, 174, 2, 201, 65, 134, 162, 28, 160, 64, 198, 5, 127, 143, 52, 209, 153, 41, 66, 37, 107, 86, 203, 10, 221, 229, 48, 1, 99, 221, 164, 72, 206, 252, 70, 106, 219, 0, 227, 131, 176, 227, 236, 165, 9, 215, 105, 187, 133, 211, 245, 41, 52, 224, 122, 213, 62, 104, 225, 94, 41, 176, 142, 24, 174, 201, 145, 177, 210, 220, 51, 107, 159, 106, 98, 52, 54, 53, 45, 199, 34, 101, 59, 38, 6, 181, 251, 164, 87, 249, 181, 87, 6, 50, 38, 115, 187, 1, 54, 183, 177 } });

            migrationBuilder.UpdateData(
                table: "ST_WAREHOUSES",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 16, 2, 30, 58, 934, DateTimeKind.Utc).AddTicks(8579));
        }
    }
}
