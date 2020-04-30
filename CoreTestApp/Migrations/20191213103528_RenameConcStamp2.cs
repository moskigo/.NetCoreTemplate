using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreTestApp.Migrations
{
    public partial class RenameConcStamp2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "AspNetUsers",
                newName: "RowVersion");

            migrationBuilder.AddColumn<string>(
                name: "Worker_RowVersion",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Worker_RowVersion",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "RowVersion",
                table: "AspNetUsers",
                newName: "ConcurrencyStamp");
        }
    }
}
