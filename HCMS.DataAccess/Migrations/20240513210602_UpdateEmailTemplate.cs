using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmailTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MailAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultMessageTemplate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultSubject = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailAccounts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "Content", "Subject", "TemplateName" },
                values: new object[] { 1, "Hi this ", "Subject Line", "Temp1" });

            migrationBuilder.InsertData(
                table: "MailAccounts",
                columns: new[] { "Id", "DefaultMessageTemplate", "DefaultSubject", "Email", "Password" },
                values: new object[] { 1, "Fill This !", "Default Subject", "devingainsrsv@gmail.com", "vjtg drmk mmhm logq" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "MailAccounts");
        }
    }
}
