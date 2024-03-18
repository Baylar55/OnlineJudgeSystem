using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlgoCode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProblemEntityUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserProblem_Problem_ProblemsId",
                table: "ApplicationUserProblem");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestProblem_Problem_ProblemsId",
                table: "ContestProblem");

            migrationBuilder.DropForeignKey(
                name: "FK_MockAssesmentProblem_Problem_ProblemsId",
                table: "MockAssesmentProblem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProblemSession_Problem_ProblemsId",
                table: "ProblemSession");

            migrationBuilder.DropForeignKey(
                name: "FK_ProblemStudyPlan_Problem_ProblemsId",
                table: "ProblemStudyPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_ProblemTag_Problem_ProblemsId",
                table: "ProblemTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_Problem_ProblemId",
                table: "Submission");

            migrationBuilder.DropTable(
                name: "TestCase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Problem",
                table: "Problem");

            migrationBuilder.RenameTable(
                name: "Problem",
                newName: "Problems");

            migrationBuilder.AddColumn<string>(
                name: "ExpectedOutput",
                table: "Problems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Inputs",
                table: "Problems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MethodName",
                table: "Problems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Problems",
                table: "Problems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserProblem_Problems_ProblemsId",
                table: "ApplicationUserProblem",
                column: "ProblemsId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContestProblem_Problems_ProblemsId",
                table: "ContestProblem",
                column: "ProblemsId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MockAssesmentProblem_Problems_ProblemsId",
                table: "MockAssesmentProblem",
                column: "ProblemsId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProblemSession_Problems_ProblemsId",
                table: "ProblemSession",
                column: "ProblemsId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProblemStudyPlan_Problems_ProblemsId",
                table: "ProblemStudyPlan",
                column: "ProblemsId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProblemTag_Problems_ProblemsId",
                table: "ProblemTag",
                column: "ProblemsId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_Problems_ProblemId",
                table: "Submission",
                column: "ProblemId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserProblem_Problems_ProblemsId",
                table: "ApplicationUserProblem");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestProblem_Problems_ProblemsId",
                table: "ContestProblem");

            migrationBuilder.DropForeignKey(
                name: "FK_MockAssesmentProblem_Problems_ProblemsId",
                table: "MockAssesmentProblem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProblemSession_Problems_ProblemsId",
                table: "ProblemSession");

            migrationBuilder.DropForeignKey(
                name: "FK_ProblemStudyPlan_Problems_ProblemsId",
                table: "ProblemStudyPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_ProblemTag_Problems_ProblemsId",
                table: "ProblemTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_Problems_ProblemId",
                table: "Submission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Problems",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "ExpectedOutput",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "Inputs",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "MethodName",
                table: "Problems");

            migrationBuilder.RenameTable(
                name: "Problems",
                newName: "Problem");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Problem",
                table: "Problem",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TestCase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProblemId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpectedOutput = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inputs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestCase_Problem_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestCase_ProblemId",
                table: "TestCase",
                column: "ProblemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserProblem_Problem_ProblemsId",
                table: "ApplicationUserProblem",
                column: "ProblemsId",
                principalTable: "Problem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContestProblem_Problem_ProblemsId",
                table: "ContestProblem",
                column: "ProblemsId",
                principalTable: "Problem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MockAssesmentProblem_Problem_ProblemsId",
                table: "MockAssesmentProblem",
                column: "ProblemsId",
                principalTable: "Problem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProblemSession_Problem_ProblemsId",
                table: "ProblemSession",
                column: "ProblemsId",
                principalTable: "Problem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProblemStudyPlan_Problem_ProblemsId",
                table: "ProblemStudyPlan",
                column: "ProblemsId",
                principalTable: "Problem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProblemTag_Problem_ProblemsId",
                table: "ProblemTag",
                column: "ProblemsId",
                principalTable: "Problem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_Problem_ProblemId",
                table: "Submission",
                column: "ProblemId",
                principalTable: "Problem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
