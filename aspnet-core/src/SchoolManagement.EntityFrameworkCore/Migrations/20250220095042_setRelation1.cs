using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Migrations
{
    /// <inheritdoc />
    public partial class setRelation1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassesId",
                table: "teachers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassesId",
                table: "students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_teachers_ClassesId",
                table: "teachers",
                column: "ClassesId");

            migrationBuilder.CreateIndex(
                name: "IX_students_ClassesId",
                table: "students",
                column: "ClassesId");

            migrationBuilder.AddForeignKey(
                name: "FK_students_classes_ClassesId",
                table: "students",
                column: "ClassesId",
                principalTable: "classes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_teachers_classes_ClassesId",
                table: "teachers",
                column: "ClassesId",
                principalTable: "classes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_classes_ClassesId",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_teachers_classes_ClassesId",
                table: "teachers");

            migrationBuilder.DropIndex(
                name: "IX_teachers_ClassesId",
                table: "teachers");

            migrationBuilder.DropIndex(
                name: "IX_students_ClassesId",
                table: "students");

            migrationBuilder.DropColumn(
                name: "ClassesId",
                table: "teachers");

            migrationBuilder.DropColumn(
                name: "ClassesId",
                table: "students");
        }
    }
}
