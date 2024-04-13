using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlgoCode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewEntitiesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProblemSession_Session_SessionsId",
                table: "ProblemSession");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_AspNetUsers_UserId",
                table: "Session");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Session",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Problems");

            migrationBuilder.RenameTable(
                name: "Session",
                newName: "Sessions");

            migrationBuilder.RenameIndex(
                name: "IX_Session_UserId",
                table: "Sessions",
                newName: "IX_Sessions_UserId");

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Submissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserProblemStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProblemId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProblemStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProblemStatuses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProblemStatuses_Problems_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProblemStatuses_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSessionStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    ProblemId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "IX_Submissions_SessionId",
                table: "Submissions",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProblemStatuses_ProblemId",
                table: "UserProblemStatuses",
                column: "ProblemId");

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
                name: "FK_ProblemSession_Sessions_SessionsId",
                table: "ProblemSession",
                column: "SessionsId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_AspNetUsers_UserId",
                table: "Sessions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Sessions_SessionId",
                table: "Submissions",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProblemSession_Sessions_SessionsId",
                table: "ProblemSession");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_AspNetUsers_UserId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Sessions_SessionId",
                table: "Submissions");

            migrationBuilder.DropTable(
                name: "UserProblemStatuses");

            migrationBuilder.DropTable(
                name: "UserSessionStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_SessionId",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Submissions");

            migrationBuilder.RenameTable(
                name: "Sessions",
                newName: "Session");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_UserId",
                table: "Session",
                newName: "IX_Session_UserId");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Problems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Session",
                table: "Session",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProblemSession_Session_SessionsId",
                table: "ProblemSession",
                column: "SessionsId",
                principalTable: "Session",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Session_AspNetUsers_UserId",
                table: "Session",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
