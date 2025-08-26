using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryServicesAPI.Migrations
{
    /// <inheritdoc />
    public partial class addBookCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookCount",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookCount",
                table: "Books");
        }
    }
}
