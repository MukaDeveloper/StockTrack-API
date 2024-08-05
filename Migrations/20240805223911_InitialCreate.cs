using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockTrack_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ST_ITENS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_ITENS", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ST_ITENS",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Test" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ST_ITENS");
        }
    }
}
