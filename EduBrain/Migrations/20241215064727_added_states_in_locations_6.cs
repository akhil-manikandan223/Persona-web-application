using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduBrain.Migrations
{
    /// <inheritdoc />
    public partial class added_states_in_locations_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_States_StateId",
                table: "Locations");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_States_StateId",
                table: "Locations",
                column: "StateId",
                principalTable: "States",
                principalColumn: "StateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_States_StateId",
                table: "Locations");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_States_StateId",
                table: "Locations",
                column: "StateId",
                principalTable: "States",
                principalColumn: "StateId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
