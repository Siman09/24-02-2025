using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Migrations
{
    /// <inheritdoc />
    public partial class StopTeacherAndStudentCascade123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_classes_ClassID",
                table: "students");

            migrationBuilder.AddForeignKey(
                name: "FK_students_classes_ClassID",
                table: "students",
                column: "ClassID",
                principalTable: "classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_classes_ClassID",
                table: "students");

            migrationBuilder.AddForeignKey(
                name: "FK_students_classes_ClassID",
                table: "students",
                column: "ClassID",
                principalTable: "classes",
                principalColumn: "Id");
        }
    }
}
