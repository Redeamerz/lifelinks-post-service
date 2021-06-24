using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Post_Service.Migrations
{
	public partial class InitialCreation : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterDatabase()
				.Annotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.CreateTable(
				name: "Post",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
					DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
					userId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
					postContent = table.Column<string>(type: "longtext", nullable: false)
						.Annotation("MySql:CharSet", "utf8mb4"),
					username = table.Column<string>(type: "longtext", nullable: false)
						.Annotation("MySql:CharSet", "utf8mb4"),
					likes = table.Column<long>(type: "bigint", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Post", x => x.Id);
				})
				.Annotation("MySql:CharSet", "utf8mb4");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Post");
		}
	}
}