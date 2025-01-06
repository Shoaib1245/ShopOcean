using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.FrontEnd.APIs.Migrations
{
    public partial class CustomerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Tbl_Cart",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Tbl_Cart",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Tbl_Cart");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Tbl_Cart");
        }
    }
}
