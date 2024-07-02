using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIGSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "IGAccounts",
                columns: new[] { "Id", "DefaultMessageTemplate", "DefaultSubject", "Password", "Username" },
                values: new object[] { 1, "Message", "DeSubject", "HeyGoodness@668800", "rsvtechnologis" });

            migrationBuilder.InsertData(
                table: "IGTemplates",
                columns: new[] { "Id", "Content", "Subject", "TemplateName" },
                values: new object[] { 1, "Hi this ", "Subject Line", "Temp1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IGAccounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IGTemplates",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
