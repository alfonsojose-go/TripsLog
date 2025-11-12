using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripsLog.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAccommodationIdInTodo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Accommodations_AccommodationId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_AccommodationId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "AccommodationId",
                table: "Todos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccommodationId",
                table: "Todos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_AccommodationId",
                table: "Todos",
                column: "AccommodationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Accommodations_AccommodationId",
                table: "Todos",
                column: "AccommodationId",
                principalTable: "Accommodations",
                principalColumn: "AccommodationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
