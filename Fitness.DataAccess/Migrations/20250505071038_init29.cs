using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init29 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlans_Users_TrainerId",
                table: "WorkoutPlans");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_EquipmentId",
                table: "WorkoutExercises",
                column: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercises_Equipments_EquipmentId",
                table: "WorkoutExercises",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPlans_Trainers_TrainerId",
                table: "WorkoutPlans",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercises_Equipments_EquipmentId",
                table: "WorkoutExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlans_Trainers_TrainerId",
                table: "WorkoutPlans");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutExercises_EquipmentId",
                table: "WorkoutExercises");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPlans_Users_TrainerId",
                table: "WorkoutPlans",
                column: "TrainerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
