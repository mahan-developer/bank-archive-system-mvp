using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankArchiveMVP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDocumentIndexForReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TextStatus",
                table: "DocumentIndexes",
                newName: "OcrStatus");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "DocumentIndexes",
                newName: "ExtractedText");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckedAt",
                table: "DocumentIndexes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckedBy",
                table: "DocumentIndexes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckedAt",
                table: "DocumentIndexes");

            migrationBuilder.DropColumn(
                name: "CheckedBy",
                table: "DocumentIndexes");

            migrationBuilder.RenameColumn(
                name: "OcrStatus",
                table: "DocumentIndexes",
                newName: "TextStatus");

            migrationBuilder.RenameColumn(
                name: "ExtractedText",
                table: "DocumentIndexes",
                newName: "Text");
        }
    }
}
