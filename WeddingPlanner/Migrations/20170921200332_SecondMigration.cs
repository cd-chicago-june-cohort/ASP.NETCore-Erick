using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WeddingPlanner.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestAtWedding_Users_GuestUserId",
                table: "GuestAtWedding");

            migrationBuilder.DropIndex(
                name: "IX_GuestAtWedding_GuestUserId",
                table: "GuestAtWedding");

            migrationBuilder.DropColumn(
                name: "GuestUserId",
                table: "GuestAtWedding");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Users",
                type: "int4",
                nullable: false,
                oldClrType: typeof(long))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.CreateIndex(
                name: "IX_GuestAtWedding_GuestId",
                table: "GuestAtWedding",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestAtWedding_Users_GuestId",
                table: "GuestAtWedding",
                column: "GuestId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestAtWedding_Users_GuestId",
                table: "GuestAtWedding");

            migrationBuilder.DropIndex(
                name: "IX_GuestAtWedding_GuestId",
                table: "GuestAtWedding");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int4")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<long>(
                name: "GuestUserId",
                table: "GuestAtWedding",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GuestAtWedding_GuestUserId",
                table: "GuestAtWedding",
                column: "GuestUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestAtWedding_Users_GuestUserId",
                table: "GuestAtWedding",
                column: "GuestUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
