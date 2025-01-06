using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.FrontEnd.APIs.Migrations
{
    public partial class AddNewColmn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "tbl_ReviewAndComments");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "tbl_ReviewAndComments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "tbl_ReviewAndComments");

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "tbl_ReviewAndComments",
                type: "int",
                nullable: true);
        }
    }
}
