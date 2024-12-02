using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class aaa : Migration
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
                    VerifiedToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
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
                name: "ST_SOLICITATIONS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    InstitutionId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SolicitedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpectReturnAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BorroadAt = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ST_MOVIMENTATIONS_ST_MATERIALS_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "ST_MATERIALS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ST_MOVIMENTATIONS_ST_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "ST_USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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

            migrationBuilder.CreateTable(
                name: "ST_SOLICITATION_MATERIALS",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    SolicitationId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
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
                values: new object[] { 1, null, new DateTime(2024, 12, 1, 22, 15, 56, 888, DateTimeKind.Local).AddTicks(674), "admin@stocktrack.com", "Admin", new byte[] { 125, 131, 1, 16, 58, 168, 27, 5, 150, 234, 140, 128, 244, 149, 136, 122, 112, 66, 19, 53, 128, 222, 154, 191, 200, 254, 45, 205, 155, 144, 252, 70, 78, 36, 82, 184, 135, 23, 202, 51, 23, 184, 197, 51, 187, 35, 16, 145, 172, 10, 165, 122, 216, 154, 234, 109, 255, 49, 98, 33, 127, 25, 79, 14 }, new byte[] { 202, 228, 130, 205, 131, 130, 224, 204, 64, 129, 174, 5, 95, 228, 23, 119, 32, 48, 166, 13, 27, 231, 20, 94, 248, 176, 40, 45, 4, 252, 186, 45, 194, 86, 215, 177, 125, 4, 5, 102, 80, 4, 38, 108, 252, 51, 246, 128, 176, 62, 19, 107, 122, 97, 53, 86, 253, 132, 76, 222, 144, 89, 157, 100, 242, 114, 67, 133, 5, 176, 67, 137, 43, 137, 100, 214, 146, 234, 203, 242, 108, 233, 220, 205, 127, 36, 223, 173, 91, 135, 85, 210, 196, 22, 209, 199, 114, 207, 38, 24, 169, 250, 97, 32, 100, 186, 137, 30, 50, 212, 87, 170, 246, 226, 54, 146, 196, 138, 88, 142, 204, 64, 91, 100, 52, 159, 136, 30 }, "https://imgur.com/mOXzZLE.png", true, new DateTime(2024, 12, 1, 22, 15, 56, 888, DateTimeKind.Local).AddTicks(688), null, null });

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
                values: new object[] { 1, true, new DateTime(2024, 12, 2, 1, 15, 56, 888, DateTimeKind.Utc).AddTicks(876), "Admin", "Área de Testes", 1, "Teste", new DateTime(2024, 12, 2, 1, 15, 56, 888, DateTimeKind.Utc).AddTicks(877), "" });

            migrationBuilder.InsertData(
                table: "ST_MATERIALS",
                columns: new[] { "Id", "Active", "CreatedAt", "CreatedBy", "Description", "ImageURL", "InstitutionId", "Manufacturer", "MaterialType", "Measure", "Name", "RecordNumber", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, true, new DateTime(2024, 12, 2, 1, 15, 56, 888, DateTimeKind.Utc).AddTicks(934), "", "Notebook ThinkPad", "", 1, "ThinkPad", 0, "UN", "Notebook", 123456, new DateTime(2024, 12, 2, 1, 15, 56, 888, DateTimeKind.Utc).AddTicks(934), "" });

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
                values: new object[] { 1, 1, new DateTime(2024, 12, 1, 22, 15, 56, 888, DateTimeKind.Local).AddTicks(1075), "Adição de área \"Teste\"", 0, 1, null, "Admin", "Área Teste", 1f, 0, 1, null, null });

            migrationBuilder.InsertData(
                table: "ST_WAREHOUSES",
                columns: new[] { "Id", "Active", "AreaId", "CreatedAt", "CreatedBy", "Description", "InstitutionId", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, true, 1, new DateTime(2024, 12, 2, 1, 15, 56, 888, DateTimeKind.Utc).AddTicks(904), "", "Almoxarifado de informática", 1, "Informática", new DateTime(2024, 12, 2, 1, 15, 56, 888, DateTimeKind.Utc).AddTicks(904), "" });

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
                name: "IX_ST_SOLICITATION_MATERIALS_SolicitationId",
                table: "ST_SOLICITATION_MATERIALS",
                column: "SolicitationId");

            migrationBuilder.CreateIndex(
                name: "IX_ST_SOLICITATIONS_UserId_InstitutionId",
                table: "ST_SOLICITATIONS",
                columns: new[] { "UserId", "InstitutionId" });

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
                name: "ST_SOLICITATION_MATERIALS");

            migrationBuilder.DropTable(
                name: "ST_WAREHOUSE_USERS");

            migrationBuilder.DropTable(
                name: "UserRoleEntity");

            migrationBuilder.DropTable(
                name: "ST_MATERIALS");

            migrationBuilder.DropTable(
                name: "ST_SOLICITATIONS");

            migrationBuilder.DropTable(
                name: "ST_WAREHOUSES");

            migrationBuilder.DropTable(
                name: "ST_USER_INSTITUTIONS");

            migrationBuilder.DropTable(
                name: "ST_AREAS");

            migrationBuilder.DropTable(
                name: "ST_USERS");

            migrationBuilder.DropTable(
                name: "ST_INSTITUTIONS");
        }
    }
}
