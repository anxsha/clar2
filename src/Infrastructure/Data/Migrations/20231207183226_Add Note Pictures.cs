using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace neatbook.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNotePictures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NotePicture",
                table: "NotePicture");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Created",
                table: "NotePicture",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "NotePicture",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastModified",
                table: "NotePicture",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "NotePicture",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoteId1",
                table: "NotePicture",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotePicture",
                table: "NotePicture",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_NotePicture_NoteId",
                table: "NotePicture",
                column: "NoteId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotePicture_Notes_NoteId1",
                table: "NotePicture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotePicture",
                table: "NotePicture");

            migrationBuilder.DropIndex(
                name: "IX_NotePicture_NoteId",
                table: "NotePicture");

            migrationBuilder.DropIndex(
                name: "IX_NotePicture_NoteId1",
                table: "NotePicture");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "NotePicture");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "NotePicture");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "NotePicture");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "NotePicture");

            migrationBuilder.DropColumn(
                name: "NoteId1",
                table: "NotePicture");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotePicture",
                table: "NotePicture",
                columns: new[] { "NoteId", "Id" });
        }
    }
}
