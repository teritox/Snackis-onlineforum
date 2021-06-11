using Microsoft.EntityFrameworkCore.Migrations;

namespace Snackis_Forum_.Migrations
{
    public partial class AddTitleIdToSubjectToEnableCreationOfMultipleForums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SitetitleId",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SitetitleId",
                table: "Subjects");
        }
    }
}
