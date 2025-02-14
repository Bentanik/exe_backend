using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exe_backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubscriptionType",
                table: "Users",
                newName: "UserSubcription_Type");

            migrationBuilder.AddColumn<DateTime>(
                name: "UserSubcription_EndDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UserSubcription_StartDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserSubcription_EndDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserSubcription_StartDate",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserSubcription_Type",
                table: "Users",
                newName: "SubscriptionType");
        }
    }
}
