using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mobile.Migrations
{
    /// <inheritdoc />
    public partial class dev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Key",
                keyValue: 4,
                column: "Value",
                value: "FALSE");

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Key", "Value" },
                values: new object[] { 6, "TRUE" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Key",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Key",
                keyValue: 4,
                column: "Value",
                value: "false");
        }
    }
}
