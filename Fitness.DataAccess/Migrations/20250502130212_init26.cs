﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init26 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistories_PackageId",
                table: "PurchaseHistories",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHistories_Packages_PackageId",
                table: "PurchaseHistories",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHistories_Packages_PackageId",
                table: "PurchaseHistories");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseHistories_PackageId",
                table: "PurchaseHistories");
        }
    }
}
