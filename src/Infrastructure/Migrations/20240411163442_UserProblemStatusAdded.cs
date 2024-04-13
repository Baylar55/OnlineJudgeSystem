using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlgoCode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserProblemStatusAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProblemStatuses_AspNetUsers_UserId",
                table: "UserProblemStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProblemStatuses_Sessions_SessionId",
                table: "UserProblemStatuses");

            migrationBuilder.DropTable(
                name: "UserSessionStatuses");

            migrationBuilder.DropIndex(
                name: "IX_UserProblemStatuses_SessionId",
                table: "UserProblemStatuses");

            migrationBuilder.DropIndex(
                name: "IX_UserProblemStatuses_UserId",
                table: "UserProblemStatuses");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserProblemStatuses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserProblemStatuses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "UserSessionStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSessionStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSessionStatuses_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProblemStatuses_SessionId",
                table: "UserProblemStatuses",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProblemStatuses_UserId",
                table: "UserProblemStatuses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessionStatuses_SessionId",
                table: "UserSessionStatuses",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProblemStatuses_AspNetUsers_UserId",
                table: "UserProblemStatuses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProblemStatuses_Sessions_SessionId",
                table: "UserProblemStatuses",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
