using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlgoCode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SetCascadeDeleteForSessionSubmissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Sessions_SessionId",
                table: "Submissions");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Sessions_SessionId",
                table: "Submissions",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Sessions_SessionId",
                table: "Submissions");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Sessions_SessionId",
                table: "Submissions",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id");
        }
    }
}
