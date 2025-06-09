#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace HomePlanner.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDtoBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Stamp",
                table: "HomeTasks",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stamp",
                table: "HomeTasks");
        }
    }
}
