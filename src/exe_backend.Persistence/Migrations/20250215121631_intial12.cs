using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exe_backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class intial12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "SubscriptionPackages",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SubscriptionPackages",
                newName: "Type");
        }
    }
}
