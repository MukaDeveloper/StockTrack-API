using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    AccessCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "UserRoleEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleEntity", x => x.Id);
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
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
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
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Measure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    InstitutionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_MATERIALS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ST_MATERIALS_ST_INSTITUTIONS_InstitutionId",
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
                    UserRole = table.Column<int>(type: "int", nullable: false)
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
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    InstitutionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_WAREHOUSES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ST_WAREHOUSES_ST_AREAS_AreaId",
                        column: x => x.AreaId,
                        principalTable: "ST_AREAS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ST_WAREHOUSES_ST_INSTITUTIONS_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "ST_INSTITUTIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ST_MATERIAL_WAREHOUSES",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_MATERIAL_WAREHOUSES", x => new { x.MaterialId, x.WarehouseId });
                    table.ForeignKey(
                        name: "FK_ST_MATERIAL_WAREHOUSES_ST_MATERIALS_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "ST_MATERIALS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ST_MATERIAL_WAREHOUSES_ST_WAREHOUSES_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "ST_WAREHOUSES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    MovimentationBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    AreaId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Event = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
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
                columns: new[] { "Id", "AccessCode", "CEP", "City", "Complement", "Name", "Neightboor", "Nickname", "State", "StreetName", "StreetNumber" },
                values: new object[,]
                {
                    { 1, "", "02110010", "Sao Paulo", "", "Servidor de testes", "Vila Guilherme", "Testes", "SP", "Rua Alcantara", "113" },
                    { 64, "", "02110010", "Sao Paulo", "", "Horácio Augusto da Silveira", "Vila Guilherme", "ETEC Prof. Horácio", "SP", "Rua Alcantara", "113" }
                });

            migrationBuilder.InsertData(
                table: "ST_USERS",
                columns: new[] { "Id", "AccessDate", "Active", "Email", "Name", "PasswordHash", "PasswordSalt", "PhotoUrl" },
                values: new object[] { 1, null, true, "admin@stocktrack.com", "Admin", new byte[] { 186, 129, 113, 19, 117, 163, 86, 204, 227, 22, 155, 226, 122, 182, 242, 245, 242, 1, 222, 224, 179, 19, 112, 232, 20, 7, 108, 51, 31, 68, 58, 242, 110, 177, 91, 239, 35, 6, 162, 133, 224, 210, 48, 56, 52, 29, 204, 197, 64, 102, 55, 156, 5, 238, 215, 154, 133, 222, 41, 170, 189, 59, 106, 24 }, new byte[] { 188, 199, 205, 141, 75, 90, 151, 151, 67, 141, 68, 169, 44, 7, 149, 181, 105, 93, 155, 213, 208, 105, 248, 142, 135, 223, 64, 255, 154, 188, 166, 9, 242, 22, 204, 29, 33, 196, 189, 164, 246, 128, 125, 133, 48, 19, 128, 7, 66, 100, 159, 184, 226, 92, 167, 176, 32, 142, 198, 161, 240, 220, 164, 92, 130, 36, 115, 196, 43, 111, 122, 145, 243, 124, 74, 147, 2, 118, 238, 40, 200, 112, 77, 19, 115, 247, 71, 248, 69, 155, 4, 252, 133, 57, 241, 51, 184, 44, 236, 197, 47, 52, 9, 61, 32, 43, 151, 18, 197, 8, 192, 228, 163, 193, 232, 64, 226, 55, 184, 21, 160, 52, 209, 219, 64, 129, 33, 197 }, "https://imgur.com/mOXzZLE.png" });

            migrationBuilder.InsertData(
                table: "UserRoleEntity",
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
                columns: new[] { "Id", "Active", "CreatedAt", "CreatedBy", "Description", "InstitutionId", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, true, new DateTime(2024, 9, 25, 19, 29, 11, 930, DateTimeKind.Utc).AddTicks(5862), "Admin", "Área de Testes", 1, "Teste", new DateTime(2024, 9, 25, 19, 29, 11, 930, DateTimeKind.Utc).AddTicks(5864), "" });

            migrationBuilder.InsertData(
                table: "ST_MATERIALS",
                columns: new[] { "Id", "Active", "CreatedAt", "CreatedBy", "Description", "ImageURL", "InstitutionId", "Manufacturer", "MaterialType", "Measure", "Name", "Quantity", "RecordNumber", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, true, new DateTime(2024, 9, 25, 19, 29, 11, 930, DateTimeKind.Utc).AddTicks(5913), "", "Notebook ThinkPad", "", 1, "ThinkPad", 0, "", "Notebook", 0f, 123456, new DateTime(2024, 9, 25, 19, 29, 11, 930, DateTimeKind.Utc).AddTicks(5914), "" });

            migrationBuilder.InsertData(
                table: "ST_USER_INSTITUTIONS",
                columns: new[] { "InstitutionId", "UserId", "UserRole" },
                values: new object[,]
                {
                    { 1, 1, 4 },
                    { 64, 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "ST_MOVIMENTATIONS",
                columns: new[] { "Id", "AreaId", "Date", "Description", "Event", "InstitutionId", "MaterialId", "MovimentationBy", "Name", "Quantity", "Reason", "Type", "UserId", "WarehouseId" },
                values: new object[] { 1, 1, new DateTime(2024, 9, 25, 16, 29, 11, 930, DateTimeKind.Local).AddTicks(5966), "Adição de área \"Teste\"", 1, 1, null, "Admin", "Área Teste", 1f, 1, 1, null, null });

            migrationBuilder.InsertData(
                table: "ST_WAREHOUSES",
                columns: new[] { "Id", "Active", "AreaId", "CreatedAt", "CreatedBy", "Description", "InstitutionId", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, true, 1, new DateTime(2024, 9, 25, 19, 29, 11, 930, DateTimeKind.Utc).AddTicks(5883), "", "Almoxarifado de informática", 1, "Informática", new DateTime(2024, 9, 25, 19, 29, 11, 930, DateTimeKind.Utc).AddTicks(5883), "" });

            migrationBuilder.CreateIndex(
                name: "IX_ST_AREAS_InstitutionId",
                table: "ST_AREAS",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_ST_MATERIAL_WAREHOUSES_WarehouseId",
                table: "ST_MATERIAL_WAREHOUSES",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ST_MATERIALS_InstitutionId",
                table: "ST_MATERIALS",
                column: "InstitutionId");

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

            migrationBuilder.CreateIndex(
                name: "IX_ST_WAREHOUSES_InstitutionId",
                table: "ST_WAREHOUSES",
                column: "InstitutionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimentationEventEntity");

            migrationBuilder.DropTable(
                name: "MovimentationReasonEntity");

            migrationBuilder.DropTable(
                name: "MovimentationTypeEntity");

            migrationBuilder.DropTable(
                name: "ST_MATERIAL_WAREHOUSES");

            migrationBuilder.DropTable(
                name: "ST_MOVIMENTATIONS");

            migrationBuilder.DropTable(
                name: "ST_USER_INSTITUTIONS");

            migrationBuilder.DropTable(
                name: "UserRoleEntity");

            migrationBuilder.DropTable(
                name: "ST_MATERIALS");

            migrationBuilder.DropTable(
                name: "ST_WAREHOUSES");

            migrationBuilder.DropTable(
                name: "ST_USERS");

            migrationBuilder.DropTable(
                name: "ST_AREAS");

            migrationBuilder.DropTable(
                name: "ST_INSTITUTIONS");
        }
    }
}
