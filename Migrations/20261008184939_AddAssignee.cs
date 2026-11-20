using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Issue_tracker.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AppUsers_AuthorId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "AssigneeId",
                table: "Issues",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssigneeId",
                table: "Issues",
                column: "AssigneeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AppUsers_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_AppUsers_AssigneeId",
                table: "Issues",
                column: "AssigneeId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AppUsers_AuthorId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_AppUsers_AssigneeId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_AssigneeId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "Issues");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AppUsers_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
