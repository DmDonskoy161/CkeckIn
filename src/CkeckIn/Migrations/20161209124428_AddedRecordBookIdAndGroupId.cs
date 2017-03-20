using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CkeckIn.Migrations
{
    public partial class AddedRecordBookIdAndGroupId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "application_user",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RecordBookId",
                table: "application_user",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "application_user");

            migrationBuilder.DropColumn(
                name: "RecordBookId",
                table: "application_user");
        }
    }
}
