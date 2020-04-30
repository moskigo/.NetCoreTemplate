using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreTestApp.Migrations
{
    public partial class RemoveRowVersionField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Worker_RowVersion",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Worker_RowVersion",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
