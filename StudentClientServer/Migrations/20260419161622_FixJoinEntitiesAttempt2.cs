using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentTrackerServer.Migrations
{
    /// <inheritdoc />
    public partial class FixJoinEntitiesAttempt2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teaches_SubjectId_TeacherId",
                table: "Teaches");

            migrationBuilder.DropIndex(
                name: "IX_Studies_SubjectId_GroupId",
                table: "Studies");

            migrationBuilder.CreateIndex(
                name: "IX_Studies_SubjectId",
                table: "Studies",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Studies_SubjectId",
                table: "Studies");

            migrationBuilder.CreateIndex(
                name: "IX_Teaches_SubjectId_TeacherId",
                table: "Teaches",
                columns: new[] { "SubjectId", "TeacherId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Studies_SubjectId_GroupId",
                table: "Studies",
                columns: new[] { "SubjectId", "GroupId" },
                unique: true);
        }
    }
}
