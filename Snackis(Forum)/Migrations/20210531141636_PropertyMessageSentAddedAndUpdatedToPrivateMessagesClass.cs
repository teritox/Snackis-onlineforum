using Microsoft.EntityFrameworkCore.Migrations;

namespace Snackis_Forum_.Migrations
{
    public partial class PropertyMessageSentAddedAndUpdatedToPrivateMessagesClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Read",
                table: "PrivateMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Read",
                table: "PrivateMessages");
        }
    }
}
