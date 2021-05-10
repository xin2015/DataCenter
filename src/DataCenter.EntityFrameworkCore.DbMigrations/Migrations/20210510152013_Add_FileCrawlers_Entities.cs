using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataCenter.Migrations
{
    public partial class Add_FileCrawlers_Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppFileCrawlers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    DelaySeconds = table.Column<int>(type: "int", nullable: false),
                    UrlDateTimeKind = table.Column<int>(type: "int", nullable: false),
                    UrlFormat = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    FileNameFormat = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    StampFormat = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Periods = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Parameters = table.Column<string>(type: "nvarchar(max)", maxLength: 1048576, nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFileCrawlers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppParameterCombinations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileCrawlerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Periods = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Parameters = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppParameterCombinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppParameterCombinations_AppFileCrawlers_FileCrawlerId",
                        column: x => x.FileCrawlerId,
                        principalTable: "AppFileCrawlers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppFileCrawlerRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParameterCombinationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TargetTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DirectoryName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Stamp = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFileCrawlerRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFileCrawlerRecords_AppParameterCombinations_ParameterCombinationId",
                        column: x => x.ParameterCombinationId,
                        principalTable: "AppParameterCombinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppFileCrawlerRecords_ParameterCombinationId",
                table: "AppFileCrawlerRecords",
                column: "ParameterCombinationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppParameterCombinations_FileCrawlerId",
                table: "AppParameterCombinations",
                column: "FileCrawlerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppFileCrawlerRecords");

            migrationBuilder.DropTable(
                name: "AppParameterCombinations");

            migrationBuilder.DropTable(
                name: "AppFileCrawlers");
        }
    }
}
