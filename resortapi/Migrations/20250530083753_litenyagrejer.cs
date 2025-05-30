using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace resortapi.Migrations
{
    /// <inheritdoc />
    public partial class litenyagrejer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accessibilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessibilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccomodationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccomodationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    PerGuest = table.Column<bool>(type: "bit", nullable: false),
                    PerNight = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceChange = table.Column<float>(type: "real", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceChanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accommodations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxOccupancy = table.Column<int>(type: "int", nullable: false),
                    AccomodationTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accommodations_AccomodationTypes_AccomodationTypeId",
                        column: x => x.AccomodationTypeId,
                        principalTable: "AccomodationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccomodationAccessibility",
                columns: table => new
                {
                    AccessibilityId = table.Column<int>(type: "int", nullable: false),
                    AccomodationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccomodationAccessibility", x => new { x.AccessibilityId, x.AccomodationId });
                    table.ForeignKey(
                        name: "FK_AccomodationAccessibility_Accessibilities_AccessibilityId",
                        column: x => x.AccessibilityId,
                        principalTable: "Accessibilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccomodationAccessibility_Accommodations_AccomodationId",
                        column: x => x.AccomodationId,
                        principalTable: "Accommodations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeOfBooking = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false),
                    CancellationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    AccomodationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Accommodations_AccomodationId",
                        column: x => x.AccomodationId,
                        principalTable: "Accommodations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_AdditionalOptionBooking_AdditionalOptions_AdditionalOptionsId",
                        column: x => x.AdditionalOptionsId,
                        principalTable: "AdditionalOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdditionalOptionBooking_Bookings_BookingsId",
                        column: x => x.BookingsId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingPriceChanges",
                columns: table => new
                {
                    BookingsId = table.Column<int>(type: "int", nullable: false),
                    PriceChangesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingPriceChanges", x => new { x.BookingsId, x.PriceChangesId });
                    table.ForeignKey(
                        name: "FK_BookingPriceChanges_Bookings_BookingsId",
                        column: x => x.BookingsId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingPriceChanges_PriceChanges_PriceChangesId",
                        column: x => x.PriceChangesId,
                        principalTable: "PriceChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    IsChild = table.Column<bool>(type: "bit", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guests_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accessibilities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Tillgänglig med rullstol", "Rullstolsanpassad" },
                    { 2, "Utrustning för hörselskadade", "Hörselhjälpmedel" }
                });

            migrationBuilder.InsertData(
                table: "AccomodationTypes",
                columns: new[] { "Id", "BasePrice", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 800m, "Ett rum med en säng", "Enkelrum" },
                    { 2, 1200m, "Rum med två sängar", "Dubbelrum" },
                    { 3, 2500m, "Lyxsvit med utsikt", "Svit" }
                });

            migrationBuilder.InsertData(
                table: "AdditionalOptions",
                columns: new[] { "Id", "Description", "Name", "PerGuest", "PerNight", "Price" },
                values: new object[,]
                {
                    { 1, "Bufféfrukost varje morgon", "Frukost", true, true, 120m },
                    { 2, "Utcheckning kl 14:00 istället för 11:00", "Sen utcheckning", false, false, 200m },
                    { 3, "Extra städning varje dag", "Daglig städning", false, true, 150m }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PaymentMethod", "Phone", "Type" },
                values: new object[,]
                {
                    { 1, "anna@example.com", "Anna", "Svensson", "Kort", "0701234567", "Privat" },
                    { 2, "lars@firma.se", "Lars", "Andersson", "Faktura", "0739876543", "Företag" }
                });

            migrationBuilder.InsertData(
                table: "PriceChanges",
                columns: new[] { "Id", "PriceChange", "Type" },
                values: new object[,]
                {
                    { 1, 1f, "Weekend-tillägg" },
                    { 2, 1f, "Kompis till chefen" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CustomerId", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Username" },
                values: new object[,]
                {
                    { 1, null, "Adminadmin123#", null, null, "Admin", "admin" },
                    { 2, null, "Reception123#", null, null, "User", "reception" }
                });

            migrationBuilder.InsertData(
                table: "Accommodations",
                columns: new[] { "Id", "AccomodationTypeId", "MaxOccupancy", "Name" },
                values: new object[,]
                {
                    { 1, 1, 1, "101A" },
                    { 2, 2, 2, "202B" },
                    { 3, 3, 4, "Penthouse 1" }
                });

            migrationBuilder.InsertData(
                table: "AccomodationAccessibility",
                columns: new[] { "AccessibilityId", "AccomodationId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 2, 2 },
                    { 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "AccomodationId", "Active", "AmountPaid", "CancellationDate", "Cancelled", "CheckIn", "CheckOut", "Cost", "CustomerId", "TimeOfBooking" },
                values: new object[,]
                {
                    { 1, 2, true, 2640m, null, false, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2640m, 1, new DateTime(2025, 5, 20, 12, 34, 56, 0, DateTimeKind.Unspecified) },
                    { 2, 3, false, 0m, new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 5000m, 2, new DateTime(2025, 5, 21, 9, 10, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "Id", "Age", "BookingId", "FirstName", "IsChild", "LastName" },
                values: new object[,]
                {
                    { 1, 34, 1, "Anna", false, "Svensson" },
                    { 2, 28, 1, "Maria", false, "Nilsson" },
                    { 3, 45, 2, "Lars", false, "Andersson" },
                    { 4, 42, 2, "Eva", false, "Karlsson" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_AccomodationTypeId",
                table: "Accommodations",
                column: "AccomodationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccomodationAccessibility_AccomodationId",
                table: "AccomodationAccessibility",
                column: "AccomodationId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalOptionBooking_BookingsId",
                table: "AdditionalOptionBooking",
                column: "BookingsId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPriceChanges_PriceChangesId",
                table: "BookingPriceChanges",
                column: "PriceChangesId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AccomodationId",
                table: "Bookings",
                column: "AccomodationId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_BookingId",
                table: "Guests",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CustomerId",
                table: "Users",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccomodationAccessibility");

            migrationBuilder.DropTable(
                name: "AdditionalOptionBooking");

            migrationBuilder.DropTable(
                name: "BookingPriceChanges");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Accessibilities");

            migrationBuilder.DropTable(
                name: "AdditionalOptions");

            migrationBuilder.DropTable(
                name: "PriceChanges");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Accommodations");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "AccomodationTypes");
        }
    }
}
