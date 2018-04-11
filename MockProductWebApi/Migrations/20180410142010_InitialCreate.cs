using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MockProductWebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MockProducts",
                columns: table => new
                {
                    MockProductId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MockProducts", x => x.MockProductId);
                });

            migrationBuilder.CreateTable(
                name: "MockProductReviews",
                columns: table => new
                {
                    MockProductReviewId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MockProductId = table.Column<int>(nullable: true),
                    MockProductRating = table.Column<float>(nullable: false),
                    MockProductReviewContent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MockProductReviews", x => x.MockProductReviewId);
                    table.ForeignKey(
                        name: "FK_MockProductReviews_MockProducts_MockProductId",
                        column: x => x.MockProductId,
                        principalTable: "MockProducts",
                        principalColumn: "MockProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MockProductReviews_MockProductId",
                table: "MockProductReviews",
                column: "MockProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MockProductReviews");

            migrationBuilder.DropTable(
                name: "MockProducts");
        }
    }
}
