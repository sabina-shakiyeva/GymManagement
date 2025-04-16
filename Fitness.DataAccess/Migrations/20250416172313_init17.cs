using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainerSchedules_Group_GroupId",
                table: "TrainerSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerSchedules_Users_UserId",
                table: "TrainerSchedules");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TrainerSchedules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "TrainerSchedules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerSchedules_Group_GroupId",
                table: "TrainerSchedules",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerSchedules_Users_UserId",
                table: "TrainerSchedules",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainerSchedules_Group_GroupId",
                table: "TrainerSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerSchedules_Users_UserId",
                table: "TrainerSchedules");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TrainerSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "TrainerSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerSchedules_Group_GroupId",
                table: "TrainerSchedules",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerSchedules_Users_UserId",
                table: "TrainerSchedules",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
