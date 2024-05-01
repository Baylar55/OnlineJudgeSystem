using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlgoCode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ContestsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserContest_Contest_ContestsId",
                table: "ApplicationUserContest");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestProblem_Contest_ContestsId",
                table: "ContestProblem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contest",
                table: "Contest");

            migrationBuilder.RenameTable(
                name: "Contest",
                newName: "Contests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contests",
                table: "Contests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserContest_Contests_ContestsId",
                table: "ApplicationUserContest",
                column: "ContestsId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContestProblem_Contests_ContestsId",
                table: "ContestProblem",
                column: "ContestsId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserContest_Contests_ContestsId",
                table: "ApplicationUserContest");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestProblem_Contests_ContestsId",
                table: "ContestProblem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contests",
                table: "Contests");

            migrationBuilder.RenameTable(
                name: "Contests",
                newName: "Contest");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contest",
                table: "Contest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserContest_Contest_ContestsId",
                table: "ApplicationUserContest",
                column: "ContestsId",
                principalTable: "Contest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContestProblem_Contest_ContestsId",
                table: "ContestProblem",
                column: "ContestsId",
                principalTable: "Contest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
