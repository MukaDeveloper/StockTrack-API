using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class Institution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 12, 255, 16, 57, 22, 148, 72, 242, 178, 179, 181, 193, 163, 112, 10, 97, 245, 23, 29, 5, 250, 15, 169, 27, 219, 103, 142, 20, 40, 231, 171, 222, 183, 39, 186, 162, 157, 67, 157, 187, 43, 100, 116, 238, 132, 187, 162, 170, 188, 136, 150, 238, 36, 132, 78, 133, 243, 25, 136, 26, 101, 213, 173, 204 }, new byte[] { 241, 234, 227, 162, 226, 150, 78, 46, 77, 230, 241, 189, 70, 184, 244, 191, 183, 252, 211, 126, 2, 41, 213, 79, 185, 162, 66, 205, 72, 202, 87, 100, 189, 87, 226, 60, 248, 90, 116, 244, 6, 212, 210, 106, 1, 61, 148, 101, 252, 55, 206, 45, 56, 176, 10, 92, 63, 36, 207, 186, 195, 34, 204, 160, 56, 243, 11, 222, 246, 59, 41, 191, 113, 47, 238, 71, 127, 156, 13, 189, 97, 66, 135, 219, 176, 223, 193, 224, 117, 30, 162, 200, 168, 108, 27, 252, 243, 157, 77, 112, 175, 253, 215, 145, 196, 67, 170, 38, 97, 99, 10, 165, 125, 172, 54, 167, 92, 171, 60, 113, 99, 246, 133, 65, 177, 43, 203, 104 } });

            migrationBuilder.InsertData(
                table: "ST_USER_INSTITUTIONS",
                columns: new[] { "InstitutionId", "UserId" },
                values: new object[] { 64, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ST_USER_INSTITUTIONS",
                keyColumns: new[] { "InstitutionId", "UserId" },
                keyValues: new object[] { 64, 1 });

            migrationBuilder.UpdateData(
                table: "ST_USERS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 24, 228, 244, 52, 129, 184, 199, 138, 204, 157, 180, 245, 194, 156, 181, 226, 56, 181, 202, 46, 83, 107, 231, 80, 173, 170, 246, 179, 185, 11, 218, 32, 10, 54, 50, 66, 130, 215, 54, 230, 18, 118, 126, 10, 117, 66, 30, 10, 62, 222, 214, 96, 167, 33, 163, 192, 12, 22, 167, 6, 208, 63, 42, 0 }, new byte[] { 170, 76, 43, 217, 88, 165, 138, 27, 193, 156, 114, 200, 203, 207, 52, 239, 93, 7, 199, 131, 61, 225, 201, 206, 84, 92, 58, 82, 82, 239, 47, 86, 16, 150, 43, 220, 151, 127, 83, 95, 48, 17, 6, 245, 78, 231, 245, 163, 92, 34, 13, 2, 216, 71, 35, 39, 200, 230, 152, 234, 67, 12, 187, 205, 89, 156, 163, 36, 133, 105, 1, 201, 114, 29, 56, 41, 72, 66, 71, 81, 219, 43, 108, 193, 96, 122, 139, 117, 89, 52, 173, 143, 10, 225, 200, 30, 187, 225, 48, 212, 227, 120, 189, 32, 122, 44, 151, 147, 141, 138, 171, 127, 200, 23, 57, 11, 196, 232, 214, 119, 149, 238, 45, 107, 252, 151, 20, 153 } });
        }
    }
}
