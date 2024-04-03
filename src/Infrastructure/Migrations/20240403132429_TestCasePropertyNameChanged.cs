using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlgoCode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TestCasePropertyNameChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InputParameters",
                table: "TestCases",
                newName: "InputParameter");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InputParameter",
                table: "TestCases",
                newName: "InputParameters");
        }
    }
}
