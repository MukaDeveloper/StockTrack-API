using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class SolicitationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ST_SOLICITATIONS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    InstitutionId = table.Column<int>(type: "int", nullable: false),
                    SolicitedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_SOLICITATIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ST_SOLICITATIONS_ST_USER_INSTITUTIONS_UserId_InstitutionId",
                        columns: x => new { x.UserId, x.InstitutionId },
                        principalTable: "ST_USER_INSTITUTIONS",
                        principalColumns: new[] { "UserId", "InstitutionId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ST_SOLICITATION_MATERIALS",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    SolicitationId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_SOLICITATION_MATERIALS", x => new { x.MaterialId, x.SolicitationId });
                    table.ForeignKey(
                        name: "FK_ST_SOLICITATION_MATERIALS_ST_MATERIALS_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "ST_MATERIALS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ST_SOLICITATION_MATERIALS_ST_SOLICITATIONS_SolicitationId",
                        column: x => x.SolicitationId,
                        principalTable: "ST_SOLICITATIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ST_SOLICITATION_MATERIALS_SolicitationId",
                table: "ST_SOLICITATION_MATERIALS",
                column: "SolicitationId");

            migrationBuilder.CreateIndex(
                name: "IX_ST_SOLICITATIONS_UserId_InstitutionId",
                table: "ST_SOLICITATIONS",
                columns: new[] { "UserId", "InstitutionId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ST_SOLICITATION_MATERIALS");

            migrationBuilder.DropTable(
                name: "ST_SOLICITATIONS");

            migrationBuilder.UpdateData(
                table: "ST_AREAS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 25, 0, 23, 25, 221, DateTimeKind.Utc).AddTicks(7660), new DateTime(2024, 10, 25, 0, 23, 25, 221, DateTimeKind.Utc).AddTicks(7660) });

            migrationBuilder.UpdateData(
                table: "ST_MATERIALS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 25, 0, 23, 25, 221, DateTimeKind.Utc).AddTicks(7697), new DateTime(2024, 10, 25, 0, 23, 25, 221, DateTimeKind.Utc).AddTicks(7697) });

            migrationBuilder.UpdateData(
                table: "ST_MOVIMENTATIONS",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 10, 24, 21, 23, 25, 221, DateTimeKind.Local).AddTicks(7761));

            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt", "VerifiedAt" },
                values: new object[] { new DateTime(2024, 10, 24, 21, 23, 25, 221, DateTimeKind.Local).AddTicks(7463), new byte[] { 187, 235, 130, 84, 251, 10, 143, 25, 82, 201, 196, 255, 161, 163, 177, 235, 214, 15, 205, 170, 53, 171, 45, 97, 167, 176, 192, 126, 189, 242, 163, 76, 62, 185, 142, 19, 12, 18, 40, 74, 225, 64, 118, 194, 55, 88, 7, 112, 162, 130, 243, 7, 135, 74, 255, 29, 131, 78, 235, 81, 1, 255, 208, 233 }, new byte[] { 0, 82, 50, 227, 195, 195, 27, 90, 179, 128, 245, 153, 98, 206, 216, 226, 126, 223, 83, 135, 80, 77, 64, 167, 2, 37, 220, 16, 225, 82, 87, 165, 179, 254, 128, 65, 70, 230, 41, 128, 188, 202, 51, 45, 143, 13, 61, 64, 232, 59, 78, 108, 162, 233, 120, 70, 150, 228, 53, 89, 253, 80, 96, 216, 186, 60, 53, 64, 27, 30, 197, 109, 199, 142, 253, 62, 7, 162, 163, 137, 212, 248, 158, 182, 106, 223, 79, 119, 251, 166, 131, 132, 18, 101, 13, 54, 143, 164, 87, 10, 39, 109, 229, 86, 80, 162, 129, 11, 104, 138, 147, 64, 188, 38, 164, 42, 92, 6, 220, 193, 61, 236, 159, 189, 202, 245, 248, 186 }, new DateTime(2024, 10, 24, 21, 23, 25, 221, DateTimeKind.Local).AddTicks(7473) });

            migrationBuilder.UpdateData(
                table: "ST_WAREHOUSES",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 25, 0, 23, 25, 221, DateTimeKind.Utc).AddTicks(7679), new DateTime(2024, 10, 25, 0, 23, 25, 221, DateTimeKind.Utc).AddTicks(7680) });
        }
    }
}
