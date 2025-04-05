using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPresent",
                table: "Attendances");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Attendances");

            migrationBuilder.AddColumn<bool>(
                name: "IsPresent",
                table: "Attendances",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
