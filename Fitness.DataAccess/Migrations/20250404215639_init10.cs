using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Subscriptions_SubscriptionId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "SubscriptionId",
                table: "Users",
                newName: "PackageId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_SubscriptionId",
                table: "Users",
                newName: "IX_Users_PackageId");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Equipments",
                newName: "Description");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Equipments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Equipments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Equipments",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Unit",
                table: "Equipments",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Equipments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DurationInMonths = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Packages_PackageId",
                table: "Users",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Packages_PackageId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Equipments");

            migrationBuilder.RenameColumn(
                name: "PackageId",
                table: "Users",
                newName: "SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_PackageId",
                table: "Users",
                newName: "IX_Users_SubscriptionId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Equipments",
                newName: "Type");

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationInMonths = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PackageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Subscriptions_SubscriptionId",
                table: "Users",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
