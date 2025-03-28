using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinedDate",
                table: "Trainers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "MobileTelephone",
                table: "Trainers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Trainers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TrainerId",
                table: "Users",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Trainers_TrainerId",
                table: "Users",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Trainers_TrainerId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TrainerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "JoinedDate",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "MobileTelephone",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Trainers");
        }
    }
}
