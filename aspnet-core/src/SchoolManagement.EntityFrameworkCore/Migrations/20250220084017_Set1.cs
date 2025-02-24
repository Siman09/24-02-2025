using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Migrations
{
    /// <inheritdoc />
    public partial class Set1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_classes_ClassID",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_teachers_classes_classesId",
                table: "teachers");

            migrationBuilder.DropIndex(
                name: "IX_teachers_classesId",
                table: "teachers");

            migrationBuilder.DropIndex(
                name: "IX_students_ClassID",
                table: "students");

            migrationBuilder.DropColumn(
                name: "classesId",
                table: "teachers");

            migrationBuilder.AddColumn<int>(
                name: "classesId",
                table: "students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_students_classesId",
                table: "students",
                column: "classesId");

            migrationBuilder.AddForeignKey(
                name: "FK_students_classes_classesId",
                table: "students",
                column: "classesId",
                principalTable: "classes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_classes_classesId",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_students_classesId",
                table: "students");

            migrationBuilder.DropColumn(
                name: "classesId",
                table: "students");

            migrationBuilder.AddColumn<int>(
                name: "classesId",
                table: "teachers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_teachers_classesId",
                table: "teachers",
                column: "classesId");

            migrationBuilder.CreateIndex(
                name: "IX_students_ClassID",
                table: "students",
                column: "ClassID");

            migrationBuilder.AddForeignKey(
                name: "FK_students_classes_ClassID",
                table: "students",
                column: "ClassID",
                principalTable: "classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_teachers_classes_classesId",
                table: "teachers",
                column: "classesId",
                principalTable: "classes",
                principalColumn: "Id");
        }
    }
}
