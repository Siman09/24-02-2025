using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Migrations
{
    /// <inheritdoc />
    public partial class addForignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassID",
                table: "students",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_classes_ClassID",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_students_ClassID",
                table: "students");

            migrationBuilder.DropColumn(
                name: "ClassID",
                table: "students");
        }
    }
}
