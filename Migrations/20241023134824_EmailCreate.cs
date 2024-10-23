using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class EmailCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialTypeEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTypeEntity", x => x.Id);
                });

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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccessDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Verified = table.Column<bool>(type: "bit", nullable: false),
                    VerifiedToken = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VerifiedScheduled = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_USER_INSTITUTIONS", x => new { x.UserId, x.InstitutionId });
                    table.ForeignKey(
                        name: "FK_ST_USER_INSTITUTIONS_ST_INSTITUTIONS_InstitutionId",
                        column: x => x.InstitutionId,
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

            migrationBuilder.CreateTable(
                name: "ST_WAREHOUSE_USERS",
                columns: table => new
                {
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_WAREHOUSE_USERS", x => new { x.UserId, x.WarehouseId });
                    table.ForeignKey(
                        name: "FK_ST_WAREHOUSE_USERS_ST_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "ST_USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ST_WAREHOUSE_USERS_ST_WAREHOUSES_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "ST_WAREHOUSES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "MaterialTypeEntity",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "LOAN" },
                    { 2, "CONSUMPTION" }
                });

            migrationBuilder.InsertData(
                table: "MovimentationEventEntity",
                columns: new[] { "Id", "Event" },
                values: new object[,]
                {
                    { 1, "ENTRY" },
                    { 2, "EDIT" },
                    { 3, "EXIT" }
                });

            migrationBuilder.InsertData(
                table: "MovimentationReasonEntity",
                columns: new[] { "Id", "Reason" },
                values: new object[,]
                {
                    { 1, "INSERTION" },
                    { 2, "EDIT" },
                    { 3, "RETURNFROMLOAN" },
                    { 4, "RETURNFROMMAINTENANCE" },
                    { 5, "DISPOSAL" },
                    { 6, "LOAN" },
                    { 7, "SENTTOMAINTENANCE" },
                    { 8, "REMOVED" },
                    { 9, "OTHER" }
                });

            migrationBuilder.InsertData(
                table: "MovimentationTypeEntity",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "USER" },
                    { 2, "AREA" },
                    { 3, "WAREHOUSE" },
                    { 4, "MATERIAL" },
                    { 5, "LOAN" },
                    { 6, "MAINTENANCE" },
                    { 7, "GENERAL" },
                    { 8, "CONSUMPTION" }
                });

            migrationBuilder.InsertData(
                table: "ST_INSTITUTIONS",
                columns: new[] { "Id", "AccessCode", "CEP", "City", "Complement", "Name", "Neightboor", "Nickname", "State", "StreetName", "StreetNumber" },
                values: new object[,]
                {
                    { 1, "000", "02110010", "Sao Paulo", "", "Servidor de testes", "Vila Guilherme", "Testes", "SP", "Rua Alcantara", "113" },
                    { 2, "064", "02110010", "Sao Paulo", "", "Horácio Augusto da Silveira", "Vila Guilherme", "ETEC Prof. Horácio", "SP", "Rua Alcantara", "113" }
                });

            migrationBuilder.InsertData(
                table: "ST_USERS",
                columns: new[] { "Id", "AccessDate", "CreatedAt", "Email", "Name", "PasswordHash", "PasswordSalt", "PhotoUrl", "Verified", "VerifiedAt", "VerifiedScheduled", "VerifiedToken" },
                values: new object[] { 1, null, new DateTime(2024, 10, 23, 10, 48, 23, 343, DateTimeKind.Local).AddTicks(6286), "admin@stocktrack.com", "Admin", new byte[] { 128, 50, 154, 158, 125, 100, 250, 158, 10, 102, 165, 131, 100, 13, 102, 82, 229, 47, 72, 173, 177, 70, 139, 140, 182, 141, 10, 26, 43, 218, 197, 220, 106, 223, 229, 150, 155, 48, 207, 141, 201, 16, 167, 204, 92, 13, 62, 138, 80, 178, 69, 255, 145, 72, 138, 105, 113, 135, 192, 41, 33, 195, 240, 176 }, new byte[] { 123, 220, 14, 26, 14, 16, 178, 19, 13, 174, 80, 104, 130, 111, 13, 203, 83, 179, 48, 251, 1, 38, 227, 47, 245, 138, 65, 104, 107, 27, 100, 40, 193, 16, 200, 79, 122, 125, 150, 215, 70, 162, 187, 185, 210, 129, 179, 137, 98, 220, 172, 44, 113, 138, 125, 40, 202, 195, 172, 34, 252, 112, 101, 189, 140, 95, 95, 91, 73, 142, 242, 57, 69, 243, 97, 146, 146, 190, 65, 214, 229, 240, 124, 177, 48, 172, 199, 43, 133, 228, 171, 157, 202, 84, 111, 180, 196, 136, 192, 169, 214, 74, 144, 197, 64, 51, 134, 1, 20, 2, 175, 208, 153, 236, 68, 150, 163, 233, 213, 217, 97, 27, 94, 148, 1, 170, 7, 232 }, "https://imgur.com/mOXzZLE.png", false, null, null, null });

            migrationBuilder.InsertData(
                table: "UserRoleEntity",
                columns: new[] { "Id", "Role" },
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
                values: new object[] { 1, true, new DateTime(2024, 10, 23, 13, 48, 23, 343, DateTimeKind.Utc).AddTicks(6635), "Admin", "Área de Testes", 1, "Teste", new DateTime(2024, 10, 23, 13, 48, 23, 343, DateTimeKind.Utc).AddTicks(6635), "" });

            migrationBuilder.InsertData(
                table: "ST_MATERIALS",
                columns: new[] { "Id", "Active", "CreatedAt", "CreatedBy", "Description", "ImageURL", "InstitutionId", "Manufacturer", "MaterialType", "Measure", "Name", "RecordNumber", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, true, new DateTime(2024, 10, 23, 13, 48, 23, 343, DateTimeKind.Utc).AddTicks(6714), "", "Notebook ThinkPad", "", 1, "ThinkPad", 0, "UN", "Notebook", 123456, new DateTime(2024, 10, 23, 13, 48, 23, 343, DateTimeKind.Utc).AddTicks(6714), "" });

            migrationBuilder.InsertData(
                table: "ST_USER_INSTITUTIONS",
                columns: new[] { "InstitutionId", "UserId", "Active", "UserRole" },
                values: new object[,]
                {
                    { 1, 1, true, 3 },
                    { 2, 1, true, 2 }
                });

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
                table: "ST_MOVIMENTATIONS",
                columns: new[] { "Id", "AreaId", "Date", "Description", "Event", "InstitutionId", "MaterialId", "MovimentationBy", "Name", "Quantity", "Reason", "Type", "UserId", "WarehouseId" },
                values: new object[] { 1, 1, new DateTime(2024, 10, 23, 10, 48, 23, 343, DateTimeKind.Local).AddTicks(6853), "Adição de área \"Teste\"", 0, 1, null, "Admin", "Área Teste", 1f, 0, 1, null, null });

            migrationBuilder.InsertData(
                table: "ST_WAREHOUSES",
                columns: new[] { "Id", "Active", "AreaId", "CreatedAt", "CreatedBy", "Description", "InstitutionId", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, true, 1, new DateTime(2024, 10, 23, 13, 48, 23, 343, DateTimeKind.Utc).AddTicks(6674), "", "Almoxarifado de informática", 1, "Informática", new DateTime(2024, 10, 23, 13, 48, 23, 343, DateTimeKind.Utc).AddTicks(6674), "" });

            migrationBuilder.InsertData(
                table: "ST_MATERIAL_WAREHOUSES",
                columns: new[] { "MaterialId", "WarehouseId" },
                values: new object[] { 1, 1 });

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
                name: "IX_ST_USER_INSTITUTIONS_InstitutionId",
                table: "ST_USER_INSTITUTIONS",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_ST_WAREHOUSE_USERS_WarehouseId",
                table: "ST_WAREHOUSE_USERS",
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
                name: "MaterialTypeEntity");

            migrationBuilder.DropTable(
                name: "MovimentationEventEntity");

            migrationBuilder.DropTable(
                name: "MovimentationReasonEntity");

            migrationBuilder.DropTable(
                name: "MovimentationTypeEntity");

            migrationBuilder.DropTable(
                name: "ST_MATERIAL_WAREHOUSES");

            migrationBuilder.DropTable(
                name: "ST_MATERIALS_STATUS");

            migrationBuilder.DropTable(
                name: "ST_MOVIMENTATIONS");

            migrationBuilder.DropTable(
                name: "ST_USER_INSTITUTIONS");

            migrationBuilder.DropTable(
                name: "ST_WAREHOUSE_USERS");

            migrationBuilder.DropTable(
                name: "UserRoleEntity");

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
