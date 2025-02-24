using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Migrations
{
    /// <inheritdoc />
    public partial class setRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
