using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Issue_tracker.Migrations
{
    /// <inheritdoc />
    public partial class AddReporter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReporterId",
                table: "Issues",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ReporterId",
                table: "Issues",
                column: "ReporterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_AppUsers_ReporterId",
                table: "Issues",
                column: "ReporterId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_AppUsers_ReporterId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_ReporterId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ReporterId",
                table: "Issues");
        }
    }
}
