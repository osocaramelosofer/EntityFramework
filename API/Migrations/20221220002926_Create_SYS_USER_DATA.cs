using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class Create_SYS_USER_DATA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SYS_USER_DATA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USERID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DISPLAY_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PWD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ROLID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ENABLED = table.Column<bool>(type: "bit", nullable: false),
                    LAST_ACCESS = table.Column<DateTime>(type: "datetime2", nullable: false),
                    COMPANY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RESET_PASSWORD = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_USER_DATA", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SYS_USER_DATA");
        }
    }
}
