using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LessonName",
                table: "TrainerSchedules");

            migrationBuilder.RenameColumn(
                name: "LessonType",
                table: "TrainerSchedules",
                newName: "UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TrainerSchedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TrainerSchedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "TrainerSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TrainerSchedules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "TrainerSchedules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Group_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupUser",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUser", x => new { x.GroupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_GroupUser_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainerSchedules_GroupId",
                table: "TrainerSchedules",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerSchedules_UserId",
                table: "TrainerSchedules",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_PackageId",
                table: "Group",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUser_UserId",
                table: "GroupUser",
                column: "UserId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainerSchedules_Group_GroupId",
                table: "TrainerSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerSchedules_Users_UserId",
                table: "TrainerSchedules");

            migrationBuilder.DropTable(
                name: "GroupUser");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropIndex(
                name: "IX_TrainerSchedules_GroupId",
                table: "TrainerSchedules");

            migrationBuilder.DropIndex(
                name: "IX_TrainerSchedules_UserId",
                table: "TrainerSchedules");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TrainerSchedules");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "TrainerSchedules");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "TrainerSchedules");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TrainerSchedules");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "TrainerSchedules");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TrainerSchedules",
                newName: "LessonType");

            migrationBuilder.AddColumn<string>(
                name: "LessonName",
                table: "TrainerSchedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
