using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CkeckIn.Migrations
{
    public partial class AddedActiveTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "FK_student_testentity_application_user_ApplicationUserId",
            //    table: "student_testentity");
            //
            //migrationBuilder.DropIndex(
            //    name: "IX_student_testentity_ApplicationUserId",
            //    table: "student_testentity");

           // migrationBuilder.DropColumn(
           //     name: "ApplicationUserId",
           //     table: "student_testentity");

            migrationBuilder.AddColumn<Guid>(
                name: "ActiveTestId",
                table: "application_user",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_question_entity_CreatorId",
                table: "question_entity",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_application_user_ActiveTestId",
                table: "application_user",
                column: "ActiveTestId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_application_user_student_testentity_ActiveTestId",
            //    table: "application_user",
            //    column: "ActiveTestId",
            //    principalTable: "student_testentity",
            //    principalColumn: "StudentTestId",
            //    onDelete: ReferentialAction.Restrict);
            //
            //migrationBuilder.AddForeignKey(
            //    name: "FK_question_entity_application_user_CreatorId",
            //    table: "question_entity",
            //    column: "CreatorId",
            //    principalTable: "application_user",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_application_user_student_testentity_ActiveTestId",
                table: "application_user");

            migrationBuilder.DropForeignKey(
                name: "FK_question_entity_application_user_CreatorId",
                table: "question_entity");

            migrationBuilder.DropIndex(
                name: "IX_question_entity_CreatorId",
                table: "question_entity");

            migrationBuilder.DropIndex(
                name: "IX_application_user_ActiveTestId",
                table: "application_user");

            migrationBuilder.DropColumn(
                name: "ActiveTestId",
                table: "application_user");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "student_testentity",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_student_testentity_ApplicationUserId",
                table: "student_testentity",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_student_testentity_application_user_ApplicationUserId",
                table: "student_testentity",
                column: "ApplicationUserId",
                principalTable: "application_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
