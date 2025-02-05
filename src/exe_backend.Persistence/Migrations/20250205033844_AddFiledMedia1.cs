using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exe_backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFiledMedia1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublicMediaUrl",
                table: "Users",
                newName: "PublicAvatarUrl");

            migrationBuilder.RenameColumn(
                name: "PublicMediaId",
                table: "Users",
                newName: "PublicAvatarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublicAvatarUrl",
                table: "Users",
                newName: "PublicMediaUrl");

            migrationBuilder.RenameColumn(
                name: "PublicAvatarId",
                table: "Users",
                newName: "PublicMediaId");
        }
    }
}
