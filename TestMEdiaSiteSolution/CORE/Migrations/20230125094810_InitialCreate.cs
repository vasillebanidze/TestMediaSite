using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "MediaTypes",
                schema: "dbo",
                columns: table => new
                {
                    MediaTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaTypeTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaTypeId", x => x.MediaTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                schema: "dbo",
                columns: table => new
                {
                    MediaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaTypeId = table.Column<int>(type: "int", nullable: false),
                    MediaTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaId", x => x.MediaId);
                    table.ForeignKey(
                        name: "FK_Medias_MediaTypes_MediaTypeId",
                        column: x => x.MediaTypeId,
                        principalSchema: "dbo",
                        principalTable: "MediaTypes",
                        principalColumn: "MediaTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WatchLists",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MediaId = table.Column<int>(type: "int", nullable: false),
                    Watched = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserId_MediaId", x => new { x.UserId, x.MediaId });
                    table.ForeignKey(
                        name: "FK_WatchLists_Medias_MediaId",
                        column: x => x.MediaId,
                        principalSchema: "dbo",
                        principalTable: "Medias",
                        principalColumn: "MediaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medias_MediaTypeId",
                schema: "dbo",
                table: "Medias",
                column: "MediaTypeId");

            migrationBuilder.CreateIndex(
                name: "UQ_MediaTypeTitle",
                schema: "dbo",
                table: "MediaTypes",
                column: "MediaTypeTitle",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WatchLists_MediaId",
                schema: "dbo",
                table: "WatchLists",
                column: "MediaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchLists",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Medias",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MediaTypes",
                schema: "dbo");
        }
    }
}
