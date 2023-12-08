using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace neatbook.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixNotePicturesMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotePicture_Notes_NoteId1",
                table: "NotePicture");

            migrationBuilder.DropIndex(
                name: "IX_NotePicture_NoteId1",
                table: "NotePicture");

            migrationBuilder.DropColumn(
                name: "NoteId1",
                table: "NotePicture");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoteId1",
                table: "NotePicture",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotePicture_NoteId1",
                table: "NotePicture",
                column: "NoteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_NotePicture_Notes_NoteId1",
                table: "NotePicture",
                column: "NoteId1",
                principalTable: "Notes",
                principalColumn: "Id");
        }
    }
}
