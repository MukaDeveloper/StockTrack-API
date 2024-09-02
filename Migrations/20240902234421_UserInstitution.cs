using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class UserInstitution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ST_USERS_ST_INSTITUTIONS_InstitutionId",
                table: "ST_USERS");

            migrationBuilder.DropIndex(
                name: "IX_ST_USERS_InstitutionId",
                table: "ST_USERS");

            migrationBuilder.DropColumn(
                name: "InstitutionId",
                table: "ST_USERS");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "ST_USERS");

            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "ST_USERS",
                type: "int",
                nullable: true,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.CreateTable(
                name: "ST_USER_INSTITUTIONS",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    InstitutionId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.UpdateData(
                table: "ST_INSTITUTIONS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Nickname" },
                values: new object[] { "Servidor de testes", "Testes" });

            migrationBuilder.InsertData(
                table: "ST_INSTITUTIONS",
                columns: new[] { "Id", "CEP", "City", "Complement", "Name", "Neightboor", "Nickname", "State", "StreetName", "StreetNumber" },
                values: new object[] { 64, "02110010", "Sao Paulo", "", "Horácio Augusto da Silveira", "Vila Guilherme", "ETEC Prof. Horácio", "SP", "Rua Alcantara", "113" });

            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt", "PhotoUrl", "UserType" },
                values: new object[] { new byte[] { 24, 228, 244, 52, 129, 184, 199, 138, 204, 157, 180, 245, 194, 156, 181, 226, 56, 181, 202, 46, 83, 107, 231, 80, 173, 170, 246, 179, 185, 11, 218, 32, 10, 54, 50, 66, 130, 215, 54, 230, 18, 118, 126, 10, 117, 66, 30, 10, 62, 222, 214, 96, 167, 33, 163, 192, 12, 22, 167, 6, 208, 63, 42, 0 }, new byte[] { 170, 76, 43, 217, 88, 165, 138, 27, 193, 156, 114, 200, 203, 207, 52, 239, 93, 7, 199, 131, 61, 225, 201, 206, 84, 92, 58, 82, 82, 239, 47, 86, 16, 150, 43, 220, 151, 127, 83, 95, 48, 17, 6, 245, 78, 231, 245, 163, 92, 34, 13, 2, 216, 71, 35, 39, 200, 230, 152, 234, 67, 12, 187, 205, 89, 156, 163, 36, 133, 105, 1, 201, 114, 29, 56, 41, 72, 66, 71, 81, 219, 43, 108, 193, 96, 122, 139, 117, 89, 52, 173, 143, 10, 225, 200, 30, 187, 225, 48, 212, 227, 120, 189, 32, 122, 44, 151, 147, 141, 138, 171, 127, 200, 23, 57, 11, 196, 232, 214, 119, 149, 238, 45, 107, 252, 151, 20, 153 }, "https://imgur.com/mOXzZLE.png", 4 });

            migrationBuilder.InsertData(
                table: "ST_USER_INSTITUTIONS",
                columns: new[] { "InstitutionId", "UserId" },
                values: new object[] { 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ST_USER_INSTITUTIONS");

            migrationBuilder.DeleteData(
                table: "ST_INSTITUTIONS",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "ST_USERS",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "InstitutionId",
                table: "ST_USERS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "ST_USERS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "ST_INSTITUTIONS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Nickname" },
                values: new object[] { "Horácio Augusto da Silveira", "ETEC Prof. Horácio" });

            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InstitutionId", "PasswordHash", "PasswordSalt", "PhotoUrl", "Token", "UserType" },
                values: new object[] { 1, new byte[] { 34, 35, 199, 89, 240, 163, 116, 55, 67, 203, 123, 95, 236, 2, 169, 69, 234, 181, 244, 36, 105, 78, 254, 156, 33, 185, 159, 10, 188, 53, 34, 68, 70, 162, 238, 211, 187, 31, 102, 43, 65, 100, 104, 56, 108, 41, 228, 247, 177, 158, 142, 171, 105, 223, 232, 244, 252, 33, 165, 232, 100, 87, 118, 232 }, new byte[] { 239, 181, 65, 145, 113, 59, 94, 249, 1, 63, 148, 65, 196, 132, 8, 0, 115, 62, 205, 253, 156, 87, 242, 31, 160, 60, 76, 93, 231, 242, 122, 56, 178, 81, 20, 91, 40, 160, 38, 145, 173, 75, 140, 113, 56, 186, 34, 255, 221, 39, 177, 146, 163, 34, 210, 254, 43, 72, 100, 106, 140, 187, 254, 95, 204, 57, 133, 157, 192, 150, 202, 101, 246, 252, 67, 251, 159, 95, 240, 192, 104, 30, 20, 181, 2, 189, 16, 205, 240, 49, 113, 173, 234, 44, 20, 109, 124, 97, 10, 88, 101, 15, 42, 108, 98, 226, 151, 76, 189, 90, 199, 182, 155, 196, 91, 151, 74, 88, 16, 5, 13, 188, 229, 136, 180, 81, 18, 5 }, "", "", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_ST_USERS_InstitutionId",
                table: "ST_USERS",
                column: "InstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ST_USERS_ST_INSTITUTIONS_InstitutionId",
                table: "ST_USERS",
                column: "InstitutionId",
                principalTable: "ST_INSTITUTIONS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
