using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduBrain.Migrations
{
    /// <inheritdoc />
    public partial class TeacherAddedToClubRep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubReps_Clubs_ClubId",
                table: "ClubReps");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubReps_Students_StudentId",
                table: "ClubReps");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "ClubReps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClubReps_EmployeeId",
                table: "ClubReps",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubReps_Clubs_ClubId",
                table: "ClubReps",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "ClubId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubReps_Students_StudentId",
                table: "ClubReps",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubReps_Teachers_EmployeeId",
                table: "ClubReps",
                column: "EmployeeId",
                principalTable: "Teachers",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubReps_Clubs_ClubId",
                table: "ClubReps");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubReps_Students_StudentId",
                table: "ClubReps");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubReps_Teachers_EmployeeId",
                table: "ClubReps");

            migrationBuilder.DropIndex(
                name: "IX_ClubReps_EmployeeId",
                table: "ClubReps");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ClubReps");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubReps_Clubs_ClubId",
                table: "ClubReps",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "ClubId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubReps_Students_StudentId",
                table: "ClubReps",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
