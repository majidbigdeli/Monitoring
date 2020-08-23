using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Monitoring.Core.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    RenderStrategy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentDashboard",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    RenderMode = table.Column<int>(nullable: false),
                    WaitTime = table.Column<int>(nullable: false),
                    RefreshTime = table.Column<int>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentDashboard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentDashboard_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentDashboard_DepartmentId",
                table: "DepartmentDashboard",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentDashboard");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
