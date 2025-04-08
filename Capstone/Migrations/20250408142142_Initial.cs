using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Capstone.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carrelli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrelli", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FasceDiPrezzo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FasceDiPrezzo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paesi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paesi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Itinerari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Durata = table.Column<int>(type: "int", nullable: false),
                    PaeseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itinerari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Itinerari_Paesi_PaeseId",
                        column: x => x.PaeseId,
                        principalTable: "Paesi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarrelloItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItinerarioId = table.Column<int>(type: "int", nullable: false),
                    FasciaDiPrezzoId = table.Column<int>(type: "int", nullable: false),
                    Prezzo = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false),
                    CarrelloId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrelloItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrelloItems_Carrelli_CarrelloId",
                        column: x => x.CarrelloId,
                        principalTable: "Carrelli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrelloItems_FasceDiPrezzo_FasciaDiPrezzoId",
                        column: x => x.FasciaDiPrezzoId,
                        principalTable: "FasceDiPrezzo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrelloItems_Itinerari_ItinerarioId",
                        column: x => x.ItinerarioId,
                        principalTable: "Itinerari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItinerarioFascePrezzo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItinerarioId = table.Column<int>(type: "int", nullable: false),
                    FasciaDiPrezzoId = table.Column<int>(type: "int", nullable: false),
                    Prezzo = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItinerarioFascePrezzo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItinerarioFascePrezzo_FasceDiPrezzo_FasciaDiPrezzoId",
                        column: x => x.FasciaDiPrezzoId,
                        principalTable: "FasceDiPrezzo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItinerarioFascePrezzo_Itinerari_ItinerarioId",
                        column: x => x.ItinerarioId,
                        principalTable: "Itinerari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItinerarioGiorni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Giorno = table.Column<int>(type: "int", nullable: false),
                    Titolo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItinerarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItinerarioGiorni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItinerarioGiorni_Itinerari_ItinerarioId",
                        column: x => x.ItinerarioId,
                        principalTable: "Itinerari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "424b0d3d-d302-49d2-b8c7-f40579e73c82", "424b0d3d-d302-49d2-b8c7-f40579e73c82", "User", "USER" },
                    { "aee0ff4f-202b-4b80-ac32-63644a072953", "aee0ff4f-202b-4b80-ac32-63644a072953", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "FasceDiPrezzo",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Economica" },
                    { 2, "Standard" },
                    { 3, "Lusso" }
                });

            migrationBuilder.InsertData(
                table: "Paesi",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Thailandia" },
                    { 2, "Cina" },
                    { 3, "Giappone" }
                });

            migrationBuilder.InsertData(
                table: "Itinerari",
                columns: new[] { "Id", "Durata", "Nome", "PaeseId" },
                values: new object[,]
                {
                    { 1, 9, "Tour in Thailandia", 1 },
                    { 2, 12, "Tour in Cina", 2 },
                    { 3, 15, "Tour in Giappone", 3 }
                });

            migrationBuilder.InsertData(
                table: "ItinerarioFascePrezzo",
                columns: new[] { "Id", "FasciaDiPrezzoId", "ItinerarioId", "Prezzo" },
                values: new object[,]
                {
                    { 1, 1, 1, 999.99m },
                    { 2, 2, 1, 1499.99m },
                    { 3, 3, 1, 1999.99m },
                    { 4, 1, 2, 1200.00m },
                    { 5, 2, 2, 1700.00m },
                    { 6, 3, 2, 2200.00m },
                    { 7, 1, 3, 1500.00m },
                    { 8, 2, 3, 2000.00m },
                    { 9, 3, 3, 2500.00m }
                });

            migrationBuilder.InsertData(
                table: "ItinerarioGiorni",
                columns: new[] { "Id", "Descrizione", "Giorno", "ItinerarioId", "Titolo" },
                values: new object[,]
                {
                    { 1, "Arrivo all'aeroporto internazionale di Bangkok e trasferimento in hotel.", 1, 1, "Arrivo in Thailandia" },
                    { 2, "Visita al Tempio del Buddha di Smeraldo e alla Grande Sala del Trono.", 2, 1, "Visita al Tempio" },
                    { 3, "Esplorazione dei famosi mercati galleggianti e pranzo tipico.", 3, 1, "Mercati galleggianti" },
                    { 4, "Visita al Palazzo Reale e al Museo Nazionale.", 4, 1, "Tour della città" },
                    { 5, "Escursione a Ayutthaya, la storica capitale del regno del Siam.", 5, 1, "Visita a Ayutthaya" },
                    { 6, "Arrivo a Ko Samui per una giornata di relax sulle spiagge esotiche.", 6, 1, "Isola di Ko Samui" },
                    { 7, "Giornata dedicata allo snorkeling e visita delle isole vicine a Ko Samui.", 7, 1, "Snorkeling e visite alle isole" },
                    { 8, "Trasferimento a Chiang Mai e visita ai templi locali.", 8, 1, "Visita a Chiang Mai" },
                    { 9, "Tempo libero per shopping e ritorno all'aeroporto per il volo di ritorno.", 9, 1, "Partenza" },
                    { 10, "Arrivo a Pechino, trasferimento in hotel e visita alla Piazza Tiananmen.", 1, 2, "Arrivo in Cina" },
                    { 11, "Visita alla Città Proibita e al Tempio del Cielo.", 2, 2, "Città Proibita" },
                    { 12, "Escursione alla Grande Muraglia Cinese.", 3, 2, "Grande Muraglia" },
                    { 13, "Visita alla zona moderna di Pechino, comprensiva di un tour del quartiere commerciale.", 4, 2, "Pechino Moderna" },
                    { 14, "Volo per Xian e visita all'Esercito di Terracotta.", 5, 2, "Xian e l'Esercito di Terracotta" },
                    { 15, "Arrivo a Chengdu e visita al centro di ricerca per la protezione del panda gigante.", 6, 2, "Visita a Chengdu" },
                    { 16, "Visita a un monastero buddista e passeggiata nei parchi locali.", 7, 2, "Tour di Chengdu" },
                    { 17, "Visita ai templi e mercati storici della vecchia Pechino.", 8, 2, "Pechino Antica" },
                    { 18, "Partenza per Shanghai e visita al Bund e alla zona moderna della città.", 9, 2, "Viaggio a Shanghai" },
                    { 19, "Visita ai templi antichi e al Giardino Yu.", 10, 2, "Shanghai Antica" },
                    { 20, "Giornata dedicata allo shopping nei quartieri più famosi di Shanghai.", 11, 2, "Shopping a Shanghai" },
                    { 21, "Trasferimento all'aeroporto per il volo di ritorno.", 12, 2, "Partenza" },
                    { 22, "Arrivo a Tokyo e sistemazione in hotel.", 1, 3, "Arrivo in Giappone" },
                    { 23, "Visita al Tempio di Senso-ji e al mercato di Nakamise.", 2, 3, "Tempio di Asakusa" },
                    { 24, "Visita al Palazzo Imperiale e al parco circostante.", 3, 3, "Visita al Palazzo Imperiale" },
                    { 25, "Visita al Santuario Toshogu e al Parco Nazionale di Nikko.", 4, 3, "Escursione a Nikko" },
                    { 26, "Visita ai templi di Kyoto, incluso il famoso Tempio d’Oro.", 5, 3, "Visita a Kyoto" },
                    { 27, "Escursione a Nara, famosa per il Parco dei Cervi e il Tempio Todai-ji.", 6, 3, "Nara" },
                    { 28, "Esplorazione di Osaka, con visita al Castello di Osaka e al quartiere Dotonbori.", 7, 3, "Osaka" },
                    { 29, "Visita al Parco della Pace e al Museo della Pace di Hiroshima.", 8, 3, "Hiroshima" },
                    { 30, "Escursione all'isola di Miyajima, famosa per il Torii galleggiante.", 9, 3, "Miyajima" },
                    { 31, "Visita alla città di Kobe, famosa per la carne di manzo Kobe.", 10, 3, "Kobe" },
                    { 32, "Visita alla zona termale di Hakone e alle sue splendide vedute del Monte Fuji.", 11, 3, "Hakone" },
                    { 33, "Giornata dedicata a Tokyo Disneyland.", 12, 3, "Tokyo Disneyland" },
                    { 34, "Giornata di shopping a Shibuya e Shinjuku.", 13, 3, "Shopping a Tokyo" },
                    { 35, "Escursione al Monte Fuji e visita ai suoi laghi.", 14, 3, "Escursione al Monte Fuji" },
                    { 36, "Trasferimento all'aeroporto di Tokyo per il volo di ritorno.", 15, 3, "Partenza" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CarrelloItems_CarrelloId",
                table: "CarrelloItems",
                column: "CarrelloId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrelloItems_FasciaDiPrezzoId",
                table: "CarrelloItems",
                column: "FasciaDiPrezzoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrelloItems_ItinerarioId",
                table: "CarrelloItems",
                column: "ItinerarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Itinerari_PaeseId",
                table: "Itinerari",
                column: "PaeseId");

            migrationBuilder.CreateIndex(
                name: "IX_ItinerarioFascePrezzo_FasciaDiPrezzoId",
                table: "ItinerarioFascePrezzo",
                column: "FasciaDiPrezzoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItinerarioFascePrezzo_ItinerarioId",
                table: "ItinerarioFascePrezzo",
                column: "ItinerarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ItinerarioGiorni_ItinerarioId",
                table: "ItinerarioGiorni",
                column: "ItinerarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CarrelloItems");

            migrationBuilder.DropTable(
                name: "ItinerarioFascePrezzo");

            migrationBuilder.DropTable(
                name: "ItinerarioGiorni");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Carrelli");

            migrationBuilder.DropTable(
                name: "FasceDiPrezzo");

            migrationBuilder.DropTable(
                name: "Itinerari");

            migrationBuilder.DropTable(
                name: "Paesi");
        }
    }
}
