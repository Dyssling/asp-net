using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiliconApp.Migrations
{
    /// <inheritdoc />
    public partial class CoursesListAddedToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseList",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseList",
                table: "AspNetUsers");
        }
    }
}
