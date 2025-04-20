using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverType",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderType",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "TrainerSchedules",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverId",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerSchedules_PackageId",
                table: "TrainerSchedules",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerSchedules_Packages_PackageId",
                table: "TrainerSchedules",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainerSchedules_Packages_PackageId",
                table: "TrainerSchedules");

            migrationBuilder.DropIndex(
                name: "IX_TrainerSchedules_PackageId",
                table: "TrainerSchedules");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "TrainerSchedules");

            migrationBuilder.AlterColumn<int>(
                name: "SenderId",
                table: "Messages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ReceiverId",
                table: "Messages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverType",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SenderType",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
