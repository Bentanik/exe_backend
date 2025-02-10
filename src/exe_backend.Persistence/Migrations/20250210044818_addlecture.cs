using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exe_backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addlecture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantityLectures",
                table: "Chapter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TotalDurationLectures",
                table: "Chapter",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Lecture",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageLecture_PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageLecture_PublicUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoLecture_PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoLecture_Duration = table.Column<double>(type: "float", nullable: true),
                    ChapterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lecture_Chapter_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lecture_ChapterId",
                table: "Lecture",
                column: "ChapterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lecture");

            migrationBuilder.DropColumn(
                name: "QuantityLectures",
                table: "Chapter");

            migrationBuilder.DropColumn(
                name: "TotalDurationLectures",
                table: "Chapter");
        }
    }
}
