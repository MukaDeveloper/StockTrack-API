using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class AuthCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ST_INSTITUTIONS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Complement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Neightboor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_INSTITUTIONS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ST_MOVIMENTATIONS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_MOVIMENTATIONS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ST_AREAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstitutionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_AREAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ST_AREAS_ST_INSTITUTIONS_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "ST_INSTITUTIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ST_USERS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InstitutionId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_USERS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ST_USERS_ST_INSTITUTIONS_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "ST_INSTITUTIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ST_WAREHOUSES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_WAREHOUSES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ST_WAREHOUSES_ST_AREAS_AreaId",
                        column: x => x.AreaId,
                        principalTable: "ST_AREAS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ST_MATERIALS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordNumber = table.Column<int>(type: "int", nullable: false),
                    MaterialType = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_MATERIALS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ST_MATERIALS_ST_WAREHOUSES_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "ST_WAREHOUSES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ST_INSTITUTIONS",
                columns: new[] { "Id", "CEP", "City", "Complement", "Name", "Neightboor", "Nickname", "State", "StreetName", "StreetNumber" },
                values: new object[] { 1, "02110010", "Sao Paulo", "", "Horácio Augusto da Silveira", "Vila Guilherme", "ETEC Prof. Horácio", "SP", "Rua Alcantara", "113" });

            migrationBuilder.InsertData(
                table: "ST_USERS",
                columns: new[] { "Id", "AccessDate", "Email", "InstitutionId", "Name", "PasswordHash", "PasswordSalt", "PhotoUrl", "Token", "UserType" },
                values: new object[] { 1, null, "admin@stocktrack.com", 1, "Admin", new byte[] { 34, 35, 199, 89, 240, 163, 116, 55, 67, 203, 123, 95, 236, 2, 169, 69, 234, 181, 244, 36, 105, 78, 254, 156, 33, 185, 159, 10, 188, 53, 34, 68, 70, 162, 238, 211, 187, 31, 102, 43, 65, 100, 104, 56, 108, 41, 228, 247, 177, 158, 142, 171, 105, 223, 232, 244, 252, 33, 165, 232, 100, 87, 118, 232 }, new byte[] { 239, 181, 65, 145, 113, 59, 94, 249, 1, 63, 148, 65, 196, 132, 8, 0, 115, 62, 205, 253, 156, 87, 242, 31, 160, 60, 76, 93, 231, 242, 122, 56, 178, 81, 20, 91, 40, 160, 38, 145, 173, 75, 140, 113, 56, 186, 34, 255, 221, 39, 177, 146, 163, 34, 210, 254, 43, 72, 100, 106, 140, 187, 254, 95, 204, 57, 133, 157, 192, 150, 202, 101, 246, 252, 67, 251, 159, 95, 240, 192, 104, 30, 20, 181, 2, 189, 16, 205, 240, 49, 113, 173, 234, 44, 20, 109, 124, 97, 10, 88, 101, 15, 42, 108, 98, 226, 151, 76, 189, 90, 199, 182, 155, 196, 91, 151, 74, 88, 16, 5, 13, 188, 229, 136, 180, 81, 18, 5 }, "", "", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_ST_AREAS_InstitutionId",
                table: "ST_AREAS",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_ST_MATERIALS_WarehouseId",
                table: "ST_MATERIALS",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ST_USERS_InstitutionId",
                table: "ST_USERS",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_ST_WAREHOUSES_AreaId",
                table: "ST_WAREHOUSES",
                column: "AreaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ST_MATERIALS");

            migrationBuilder.DropTable(
                name: "ST_MOVIMENTATIONS");

            migrationBuilder.DropTable(
                name: "ST_USERS");

            migrationBuilder.DropTable(
                name: "ST_WAREHOUSES");

            migrationBuilder.DropTable(
                name: "ST_AREAS");

            migrationBuilder.DropTable(
                name: "ST_INSTITUTIONS");
        }
    }
}
