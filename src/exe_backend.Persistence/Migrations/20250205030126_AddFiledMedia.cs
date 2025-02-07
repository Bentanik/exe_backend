using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exe_backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFiledMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicMediaId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicMediaUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicMediaId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PublicMediaUrl",
                table: "Users");
        }
    }
}
