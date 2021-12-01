using Microsoft.EntityFrameworkCore.Migrations;

namespace RetroClient.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    GamesPath = table.Column<string>(type: "TEXT", nullable: true),
                    RetroArchPath = table.Column<string>(type: "TEXT", nullable: true),
                    RetroArchCorePath = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoGames",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Platform = table.Column<string>(type: "TEXT", nullable: true),
                    DownloadUrl = table.Column<string>(type: "TEXT", nullable: true),
                    SourceUrl = table.Column<string>(type: "TEXT", nullable: true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    FileExtension = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGames", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSettings");

            migrationBuilder.DropTable(
                name: "VideoGames");
        }
    }
}
