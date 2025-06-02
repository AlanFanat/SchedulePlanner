using Microsoft.EntityFrameworkCore.Migrations;

namespace SchedulePlanner.Db.Migrations.Database
{
    public partial class AddRepeatsCountToLesson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LessonsCount",
                table: "Lessons",
                newName: "RepeatsCount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RepeatsCount",
                table: "Lessons",
                newName: "LessonsCount");
        }
    }
}
