using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Snackis_Forum_.Migrations
{
    public partial class PropertyMessageSentAddedToPrivateMessagesClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MessageSent",
                table: "PrivateMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageSent",
                table: "PrivateMessages");
        }
    }
}
