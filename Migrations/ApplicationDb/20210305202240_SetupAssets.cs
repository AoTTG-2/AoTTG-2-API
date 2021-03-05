using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AoTTG2.IDS.Migrations.ApplicationDb
{
    public partial class SetupAssets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skins",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Endorsement = table.Column<long>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: true),
                    LegacyOwner = table.Column<string>(maxLength: 100, nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    CompatibleModels = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skins_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SetHumans",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Endorsement = table.Column<long>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: true),
                    LegacyOwner = table.Column<string>(maxLength: 100, nullable: true),
                    HairId = table.Column<Guid>(nullable: true),
                    SkinId = table.Column<Guid>(nullable: true),
                    EyesId = table.Column<Guid>(nullable: true),
                    GlassesId = table.Column<Guid>(nullable: true),
                    FacialId = table.Column<Guid>(nullable: true),
                    OutfitId = table.Column<Guid>(nullable: true),
                    CapeId = table.Column<Guid>(nullable: true),
                    EmblemId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetHumans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetHumans_Skins_CapeId",
                        column: x => x.CapeId,
                        principalTable: "Skins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SetHumans_Skins_EmblemId",
                        column: x => x.EmblemId,
                        principalTable: "Skins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SetHumans_Skins_EyesId",
                        column: x => x.EyesId,
                        principalTable: "Skins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SetHumans_Skins_FacialId",
                        column: x => x.FacialId,
                        principalTable: "Skins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SetHumans_Skins_GlassesId",
                        column: x => x.GlassesId,
                        principalTable: "Skins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SetHumans_Skins_HairId",
                        column: x => x.HairId,
                        principalTable: "Skins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SetHumans_Skins_OutfitId",
                        column: x => x.OutfitId,
                        principalTable: "Skins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SetHumans_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SetHumans_Skins_SkinId",
                        column: x => x.SkinId,
                        principalTable: "Skins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SetTitans",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Endorsement = table.Column<long>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: true),
                    LegacyOwner = table.Column<string>(maxLength: 100, nullable: true),
                    HairId = table.Column<Guid>(nullable: true),
                    EyesId = table.Column<Guid>(nullable: true),
                    BodyId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetTitans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetTitans_Skins_BodyId",
                        column: x => x.BodyId,
                        principalTable: "Skins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SetTitans_Skins_EyesId",
                        column: x => x.EyesId,
                        principalTable: "Skins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SetTitans_Skins_HairId",
                        column: x => x.HairId,
                        principalTable: "Skins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SetTitans_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SetHumans_CapeId",
                table: "SetHumans",
                column: "CapeId");

            migrationBuilder.CreateIndex(
                name: "IX_SetHumans_EmblemId",
                table: "SetHumans",
                column: "EmblemId");

            migrationBuilder.CreateIndex(
                name: "IX_SetHumans_EyesId",
                table: "SetHumans",
                column: "EyesId");

            migrationBuilder.CreateIndex(
                name: "IX_SetHumans_FacialId",
                table: "SetHumans",
                column: "FacialId");

            migrationBuilder.CreateIndex(
                name: "IX_SetHumans_GlassesId",
                table: "SetHumans",
                column: "GlassesId");

            migrationBuilder.CreateIndex(
                name: "IX_SetHumans_HairId",
                table: "SetHumans",
                column: "HairId");

            migrationBuilder.CreateIndex(
                name: "IX_SetHumans_OutfitId",
                table: "SetHumans",
                column: "OutfitId");

            migrationBuilder.CreateIndex(
                name: "IX_SetHumans_OwnerId",
                table: "SetHumans",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_SetHumans_SkinId",
                table: "SetHumans",
                column: "SkinId");

            migrationBuilder.CreateIndex(
                name: "IX_SetTitans_BodyId",
                table: "SetTitans",
                column: "BodyId");

            migrationBuilder.CreateIndex(
                name: "IX_SetTitans_EyesId",
                table: "SetTitans",
                column: "EyesId");

            migrationBuilder.CreateIndex(
                name: "IX_SetTitans_HairId",
                table: "SetTitans",
                column: "HairId");

            migrationBuilder.CreateIndex(
                name: "IX_SetTitans_OwnerId",
                table: "SetTitans",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Skins_OwnerId",
                table: "Skins",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SetHumans");

            migrationBuilder.DropTable(
                name: "SetTitans");

            migrationBuilder.DropTable(
                name: "Skins");
        }
    }
}
