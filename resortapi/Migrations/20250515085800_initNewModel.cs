using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace resortapi.Migrations
{
    /// <inheritdoc />
    public partial class initNewModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdditionalOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalOption", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalOptionBooking",
                columns: table => new
                {
                    AdditionalOptionsId = table.Column<int>(type: "int", nullable: false),
                    BookingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalOptionBooking", x => new { x.AdditionalOptionsId, x.BookingsId });
                    table.ForeignKey(
                        name: "FK_AdditionalOptionBooking_AdditionalOption_AdditionalOptionsId",
                        column: x => x.AdditionalOptionsId,
                        principalTable: "AdditionalOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdditionalOptionBooking_Bookings_BookingsId",
                        column: x => x.BookingsId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalOptionBooking_BookingsId",
                table: "AdditionalOptionBooking",
                column: "BookingsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalOptionBooking");

            migrationBuilder.DropTable(
                name: "AdditionalOption");
        }
    }
}
