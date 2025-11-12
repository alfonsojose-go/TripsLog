using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripsLog.Migrations
{
    /// <inheritdoc />
    public partial class Added_Accommodation_Todos_in_TripModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccomodationId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TodoId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_TripId",
                table: "Todos",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_TripId",
                table: "Accommodations",
                column: "TripId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodations_Trips_TripId",
                table: "Accommodations",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Trips_TripId",
                table: "Todos",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accommodations_Trips_TripId",
                table: "Accommodations");

            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Trips_TripId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_TripId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Accommodations_TripId",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "AccomodationId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TodoId",
                table: "Trips");
        }
    }
}
