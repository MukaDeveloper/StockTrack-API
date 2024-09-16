using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class MiddleCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovimentationReasonEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentationReasonEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovimentationTypeEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentationTypeEntity", x => x.Id);
                });

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
                name: "ST_USERS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_USERS", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "ST_AREAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    InstitutionId = table.Column<int>(type: "int", nullable: false),
                    InstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "ST_USER_INSTITUTIONS",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    InstitutionId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_USER_INSTITUTIONS", x => new { x.UserId, x.InstitutionId });
                    table.ForeignKey(
                        name: "FK_ST_USER_INSTITUTIONS_ST_INSTITUTIONS_UserId",
                        column: x => x.UserId,
                        principalTable: "ST_INSTITUTIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ST_USER_INSTITUTIONS_ST_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "ST_USERS",
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
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    AreaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    InstitutionId = table.Column<int>(type: "int", nullable: false),
                    InstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    AreaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    WarehouseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    InstitutionId = table.Column<int>(type: "int", nullable: false),
                    InstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ST_MOVIMENTATIONS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    AreaId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_MOVIMENTATIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ST_MOVIMENTATIONS_ST_AREAS_AreaId",
                        column: x => x.AreaId,
                        principalTable: "ST_AREAS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ST_MOVIMENTATIONS_ST_MATERIALS_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "ST_MATERIALS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ST_MOVIMENTATIONS_ST_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "ST_USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ST_MOVIMENTATIONS_ST_WAREHOUSES_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "ST_WAREHOUSES",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "MovimentationReasonEntity",
                columns: new[] { "Id", "Reason" },
                values: new object[,]
                {
                    { 1, "Insertion" },
                    { 2, "Edit" },
                    { 3, "ReturnFromLoan" },
                    { 4, "ReturnFromMaintenance" },
                    { 5, "Disposal" },
                    { 6, "Loan" },
                    { 7, "SentToMaintenance" },
                    { 8, "Removed" },
                    { 9, "Other" }
                });

            migrationBuilder.InsertData(
                table: "MovimentationTypeEntity",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "ENTRY" },
                    { 2, "EXIT" }
                });

            migrationBuilder.InsertData(
                table: "ST_INSTITUTIONS",
                columns: new[] { "Id", "CEP", "City", "Complement", "Name", "Neightboor", "Nickname", "State", "StreetName", "StreetNumber" },
                values: new object[,]
                {
                    { 1, "02110010", "Sao Paulo", "", "Servidor de testes", "Vila Guilherme", "Testes", "SP", "Rua Alcantara", "113" },
                    { 64, "02110010", "Sao Paulo", "", "Horácio Augusto da Silveira", "Vila Guilherme", "ETEC Prof. Horácio", "SP", "Rua Alcantara", "113" }
                });

            migrationBuilder.InsertData(
                table: "ST_USERS",
                columns: new[] { "Id", "AccessDate", "Active", "Email", "Name", "PasswordHash", "PasswordSalt", "PhotoUrl" },
                values: new object[] { 1, null, true, "admin@stocktrack.com", "Admin", new byte[] { 117, 169, 51, 200, 118, 19, 7, 244, 120, 100, 67, 53, 105, 9, 143, 4, 158, 108, 52, 146, 214, 202, 243, 90, 174, 127, 121, 182, 59, 192, 203, 98, 149, 205, 218, 179, 220, 190, 157, 18, 72, 93, 47, 168, 102, 61, 26, 244, 222, 248, 16, 51, 222, 116, 41, 232, 198, 83, 26, 149, 77, 125, 55, 85 }, new byte[] { 94, 195, 71, 4, 208, 214, 244, 36, 31, 245, 65, 231, 10, 99, 133, 135, 76, 199, 49, 115, 4, 160, 235, 116, 239, 84, 93, 129, 174, 2, 201, 65, 134, 162, 28, 160, 64, 198, 5, 127, 143, 52, 209, 153, 41, 66, 37, 107, 86, 203, 10, 221, 229, 48, 1, 99, 221, 164, 72, 206, 252, 70, 106, 219, 0, 227, 131, 176, 227, 236, 165, 9, 215, 105, 187, 133, 211, 245, 41, 52, 224, 122, 213, 62, 104, 225, 94, 41, 176, 142, 24, 174, 201, 145, 177, 210, 220, 51, 107, 159, 106, 98, 52, 54, 53, 45, 199, 34, 101, 59, 38, 6, 181, 251, 164, 87, 249, 181, 87, 6, 50, 38, 115, 187, 1, 54, 183, 177 }, "https://imgur.com/mOXzZLE.png" });

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

            migrationBuilder.InsertData(
                table: "ST_AREAS",
                columns: new[] { "Id", "Active", "CreatedAt", "CreatedBy", "Description", "InstitutionId", "InstitutionName", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, true, new DateTime(2024, 9, 16, 2, 30, 58, 934, DateTimeKind.Utc).AddTicks(8554), "Admin", "Área de Testes", 1, "Servidor de testes", "Teste", null, null });

            migrationBuilder.InsertData(
                table: "ST_USER_INSTITUTIONS",
                columns: new[] { "InstitutionId", "UserId", "InstitutionName", "UserName", "UserType" },
                values: new object[,]
                {
                    { 1, 1, "Servidor de testes", "Admin", 4 },
                    { 64, 1, "Horácio Augusto da Silveira", "Admin", 3 }
                });

            migrationBuilder.InsertData(
                table: "ST_MOVIMENTATIONS",
                columns: new[] { "Id", "AreaId", "Date", "Description", "InstitutionId", "MaterialId", "Name", "Quantity", "Reason", "Type", "UserId", "WarehouseId" },
                values: new object[] { 1, 1, new DateTime(2024, 9, 15, 23, 30, 58, 934, DateTimeKind.Local).AddTicks(8645), "Adição de área \"Teste\"", 1, null, "Área Teste", 1m, 1, 1, 1, null });

            migrationBuilder.InsertData(
                table: "ST_WAREHOUSES",
                columns: new[] { "Id", "Active", "AreaId", "AreaName", "CreatedAt", "CreatedBy", "Description", "InstitutionId", "InstitutionName", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, true, 1, "Teste", new DateTime(2024, 9, 16, 2, 30, 58, 934, DateTimeKind.Utc).AddTicks(8579), "", "Almoxarifado de informática", 1, "Servidor de testes", "Informática", null, null });

            migrationBuilder.InsertData(
                table: "ST_MATERIALS",
                columns: new[] { "Id", "Active", "AreaId", "AreaName", "CreatedAt", "CreatedBy", "Description", "ImageURL", "InstitutionId", "InstitutionName", "Manufacturer", "MaterialType", "Name", "RecordNumber", "UpdatedAt", "UpdatedBy", "WarehouseId", "WarehouseName" },
                values: new object[] { 1, true, 1, "Teste", new DateTime(2024, 9, 16, 2, 30, 58, 934, DateTimeKind.Utc).AddTicks(8616), "", "Notebook ThinkPad", "", 1, "Servidor de testes", "ThinkPad", 0, "Notebook", 123456, null, null, 1, "Informática" });

            migrationBuilder.CreateIndex(
                name: "IX_ST_AREAS_InstitutionId",
                table: "ST_AREAS",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_ST_MATERIALS_WarehouseId",
                table: "ST_MATERIALS",
                column: "WarehouseId");

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

            migrationBuilder.CreateIndex(
                name: "IX_ST_WAREHOUSES_AreaId",
                table: "ST_WAREHOUSES",
                column: "AreaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimentationReasonEntity");

            migrationBuilder.DropTable(
                name: "MovimentationTypeEntity");

            migrationBuilder.DropTable(
                name: "ST_MOVIMENTATIONS");

            migrationBuilder.DropTable(
                name: "ST_USER_INSTITUTIONS");

            migrationBuilder.DropTable(
                name: "UserTypeEntity");

            migrationBuilder.DropTable(
                name: "ST_MATERIALS");

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
