using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class CreataDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APIDefs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Url = table.Column<string>(type: "TEXT", nullable: true),
                    ApiKey = table.Column<string>(type: "TEXT", nullable: true),
                    ApiType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIDefs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cameras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Link = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsImage = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cameras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalHeadlines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HeadlineDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Headline = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalHeadlines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Link = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsLinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    sport_key = table.Column<string>(type: "TEXT", nullable: true),
                    sport_title = table.Column<string>(type: "TEXT", nullable: true),
                    commence_time = table.Column<DateTime>(type: "TEXT", nullable: true),
                    completed = table.Column<bool>(type: "INTEGER", nullable: false),
                    home_team = table.Column<string>(type: "TEXT", nullable: true),
                    away_team = table.Column<string>(type: "TEXT", nullable: true),
                    last_update = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Sports",
                columns: table => new
                {
                    key = table.Column<string>(type: "TEXT", nullable: false),
                    group = table.Column<string>(type: "TEXT", nullable: false),
                    title = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sports", x => x.key);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherCoordRecords",
                columns: table => new
                {
                    CoordId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    lon = table.Column<double>(type: "REAL", nullable: false),
                    lat = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherCoordRecords", x => x.CoordId);
                });

            migrationBuilder.CreateTable(
                name: "WeatherMainRecords",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    temp = table.Column<double>(type: "REAL", nullable: false),
                    feels_like = table.Column<double>(type: "REAL", nullable: false),
                    temp_min = table.Column<double>(type: "REAL", nullable: false),
                    temp_max = table.Column<double>(type: "REAL", nullable: false),
                    pressure = table.Column<int>(type: "INTEGER", nullable: true),
                    humidity = table.Column<int>(type: "INTEGER", nullable: true),
                    sea_level = table.Column<int>(type: "INTEGER", nullable: true),
                    grnd_level = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherMainRecords", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherSysRecords",
                columns: table => new
                {
                    SysId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    type = table.Column<int>(type: "INTEGER", nullable: true),
                    country = table.Column<string>(type: "TEXT", nullable: true),
                    sunrise = table.Column<int>(type: "INTEGER", nullable: true),
                    sunset = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherSysRecords", x => x.SysId);
                });

            migrationBuilder.CreateTable(
                name: "WeatherWindRecords",
                columns: table => new
                {
                    WindId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    speed = table.Column<double>(type: "REAL", nullable: false),
                    deg = table.Column<int>(type: "INTEGER", nullable: true),
                    gust = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherWindRecords", x => x.WindId);
                });

            migrationBuilder.CreateTable(
                name: "TeamScore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    score = table.Column<string>(type: "TEXT", nullable: true),
                    ScoreRecordid = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamScore_Scores_ScoreRecordid",
                        column: x => x.ScoreRecordid,
                        principalTable: "Scores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeatherRecords",
                columns: table => new
                {
                    RootId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CoordId = table.Column<int>(type: "INTEGER", nullable: true),
                    @base = table.Column<string>(name: "base", type: "TEXT", nullable: true),
                    mainid = table.Column<int>(type: "INTEGER", nullable: true),
                    visibility = table.Column<int>(type: "INTEGER", nullable: true),
                    WindId = table.Column<int>(type: "INTEGER", nullable: true),
                    dt = table.Column<int>(type: "INTEGER", nullable: true),
                    SysId = table.Column<int>(type: "INTEGER", nullable: true),
                    timezone = table.Column<int>(type: "INTEGER", nullable: true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    cod = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherRecords", x => x.RootId);
                    table.ForeignKey(
                        name: "FK_WeatherRecords_WeatherCoordRecords_CoordId",
                        column: x => x.CoordId,
                        principalTable: "WeatherCoordRecords",
                        principalColumn: "CoordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeatherRecords_WeatherMainRecords_mainid",
                        column: x => x.mainid,
                        principalTable: "WeatherMainRecords",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeatherRecords_WeatherSysRecords_SysId",
                        column: x => x.SysId,
                        principalTable: "WeatherSysRecords",
                        principalColumn: "SysId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeatherRecords_WeatherWindRecords_WindId",
                        column: x => x.WindId,
                        principalTable: "WeatherWindRecords",
                        principalColumn: "WindId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Longitude = table.Column<double>(type: "REAL", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    WeatherRecordRootId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_WeatherRecords_WeatherRecordRootId",
                        column: x => x.WeatherRecordRootId,
                        principalTable: "WeatherRecords",
                        principalColumn: "RootId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    WeatherId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    main = table.Column<string>(type: "TEXT", nullable: true),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    icon = table.Column<string>(type: "TEXT", nullable: true),
                    WeatherRecordRootId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.WeatherId);
                    table.ForeignKey(
                        name: "FK_Weather_WeatherRecords_WeatherRecordRootId",
                        column: x => x.WeatherRecordRootId,
                        principalTable: "WeatherRecords",
                        principalColumn: "RootId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_WeatherRecordRootId",
                table: "Cities",
                column: "WeatherRecordRootId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamScore_ScoreRecordid",
                table: "TeamScore",
                column: "ScoreRecordid");

            migrationBuilder.CreateIndex(
                name: "IX_Weather_WeatherRecordRootId",
                table: "Weather",
                column: "WeatherRecordRootId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherRecords_CoordId",
                table: "WeatherRecords",
                column: "CoordId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherRecords_mainid",
                table: "WeatherRecords",
                column: "mainid");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherRecords_SysId",
                table: "WeatherRecords",
                column: "SysId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherRecords_WindId",
                table: "WeatherRecords",
                column: "WindId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APIDefs");

            migrationBuilder.DropTable(
                name: "Cameras");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "LocalHeadlines");

            migrationBuilder.DropTable(
                name: "NewsLinks");

            migrationBuilder.DropTable(
                name: "Sports");

            migrationBuilder.DropTable(
                name: "TeamScore");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Weather");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "WeatherRecords");

            migrationBuilder.DropTable(
                name: "WeatherCoordRecords");

            migrationBuilder.DropTable(
                name: "WeatherMainRecords");

            migrationBuilder.DropTable(
                name: "WeatherSysRecords");

            migrationBuilder.DropTable(
                name: "WeatherWindRecords");
        }
    }
}
