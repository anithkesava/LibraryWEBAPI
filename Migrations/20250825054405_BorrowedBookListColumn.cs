using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryServicesAPI.Migrations
{
    /// <inheritdoc />
    public partial class BorrowedBookListColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BorrowedBookList",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BorrowedBookList",
                table: "Users");
        }
    }
}
