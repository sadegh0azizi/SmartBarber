using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBarber.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingProviderDateIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ProviderId_Date",
                table: "Bookings",
                columns: new[] { "ProviderId", "Date" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_ProviderId_Date",
                table: "Bookings");
        }
    }
}
