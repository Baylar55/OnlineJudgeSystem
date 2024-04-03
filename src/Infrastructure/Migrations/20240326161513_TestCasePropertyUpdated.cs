using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlgoCode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TestCasePropertyUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Input",
                table: "TestCases",
                newName: "Inputs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Inputs",
                table: "TestCases",
                newName: "Input");
        }
    }
}
