using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Migrations
{
    /// <inheritdoc />
    public partial class delete_class : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_exams_students_StudentId",
                table: "exams");

            migrationBuilder.DropForeignKey(
                name: "FK_exams_teachers_TeacherId",
                table: "exams");

            migrationBuilder.DropForeignKey(
                name: "FK_teachers_classes_ClassId",
                table: "teachers");

            migrationBuilder.DropIndex(
                name: "IX_teachers_ClassId",
                table: "teachers");

            migrationBuilder.AddColumn<int>(
                name: "classesId",
                table: "teachers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_teachers_classesId",
                table: "teachers",
                column: "classesId");

            migrationBuilder.AddForeignKey(
                name: "FK_exams_students_StudentId",
                table: "exams",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_exams_teachers_TeacherId",
                table: "exams",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_teachers_classes_classesId",
                table: "teachers",
                column: "classesId",
                principalTable: "classes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_exams_students_StudentId",
                table: "exams");

            migrationBuilder.DropForeignKey(
                name: "FK_exams_teachers_TeacherId",
                table: "exams");

            migrationBuilder.DropForeignKey(
                name: "FK_teachers_classes_classesId",
                table: "teachers");

            migrationBuilder.DropIndex(
                name: "IX_teachers_classesId",
                table: "teachers");

            migrationBuilder.DropColumn(
                name: "classesId",
                table: "teachers");

            migrationBuilder.CreateIndex(
                name: "IX_teachers_ClassId",
                table: "teachers",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_exams_students_StudentId",
                table: "exams",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_exams_teachers_TeacherId",
                table: "exams",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_teachers_classes_ClassId",
                table: "teachers",
                column: "ClassId",
                principalTable: "classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
