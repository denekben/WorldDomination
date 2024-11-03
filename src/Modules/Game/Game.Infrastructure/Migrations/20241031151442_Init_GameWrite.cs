using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Game.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init_GameWrite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Game");

            migrationBuilder.CreateTable(
                name: "CountryPatterns",
                schema: "Game",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryName = table.Column<string>(type: "text", nullable: false),
                    NormalizedName = table.Column<string>(type: "text", nullable: false),
                    FlagImagePath = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryPatterns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameUsers",
                schema: "Game",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ProfileImagePath = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CityPatterns",
                schema: "Game",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CityName = table.Column<string>(type: "text", nullable: false),
                    NormalizedName = table.Column<string>(type: "text", nullable: false),
                    CityImagePath = table.Column<string>(type: "text", nullable: false),
                    IsCapital = table.Column<bool>(type: "boolean", nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityPatterns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CityPatterns_CountryPatterns_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "Game",
                        principalTable: "CountryPatterns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                schema: "Game",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomName = table.Column<string>(type: "text", nullable: false),
                    GameType = table.Column<string>(type: "text", nullable: false),
                    HasTeams = table.Column<bool>(type: "boolean", nullable: false),
                    RoomMemberLimit = table.Column<int>(type: "integer", nullable: false),
                    IsPrivate = table.Column<bool>(type: "boolean", nullable: false),
                    RoomCode = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValue: new DateTime(2024, 10, 31, 15, 14, 42, 613, DateTimeKind.Utc).AddTicks(1499)),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_GameUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalSchema: "Game",
                        principalTable: "GameUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                schema: "Game",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    GameType = table.Column<string>(type: "text", nullable: false),
                    HasTeams = table.Column<bool>(type: "boolean", nullable: false),
                    CurrentRound = table.Column<int>(type: "integer", nullable: false),
                    EcologyLevel = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Games_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "Game",
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "Game",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryName = table.Column<string>(type: "text", nullable: false),
                    NormalizedName = table.Column<string>(type: "text", nullable: false),
                    FlagImagePath = table.Column<string>(type: "text", nullable: false),
                    LivingLevel = table.Column<int>(type: "integer", nullable: false),
                    Budget = table.Column<int>(type: "integer", nullable: false),
                    HaveNuclearTechnology = table.Column<bool>(type: "boolean", nullable: false),
                    NuclearTechnology = table.Column<int>(type: "integer", nullable: false),
                    SanctionCount = table.Column<int>(type: "integer", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Games_GameId",
                        column: x => x.GameId,
                        principalSchema: "Game",
                        principalTable: "Games",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Countries_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "Game",
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "Game",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CityName = table.Column<string>(type: "text", nullable: false),
                    NormalizedName = table.Column<string>(type: "text", nullable: false),
                    CityImagePath = table.Column<string>(type: "text", nullable: false),
                    IsAlive = table.Column<bool>(type: "boolean", nullable: false),
                    HaveShield = table.Column<bool>(type: "boolean", nullable: false),
                    DevelopmentLevel = table.Column<int>(type: "integer", nullable: false),
                    LivingLevel = table.Column<int>(type: "integer", nullable: false),
                    Income = table.Column<int>(type: "integer", nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "Game",
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomMembers",
                schema: "Game",
                columns: table => new
                {
                    GameUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ProfileImagePath = table.Column<string>(type: "text", nullable: false),
                    GameRole = table.Column<string>(type: "text", nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: true),
                    RoomMemberRole = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomMembers", x => new { x.GameUserId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_RoomMembers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "Game",
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_RoomMembers_GameUsers_GameUserId",
                        column: x => x.GameUserId,
                        principalSchema: "Game",
                        principalTable: "GameUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomMembers_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "Game",
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Game",
                table: "CountryPatterns",
                columns: new[] { "Id", "CountryName", "FlagImagePath", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("026bf104-7588-4be7-b0cf-712b785504a5"), "Австралия", "", "AUSTRALIA" },
                    { new Guid("20747f9a-e5cc-4916-96f6-c0bd96c5a157"), "Россия", "", "RUSSIA" },
                    { new Guid("279c40f4-fd73-4086-8d54-3b21eb169f57"), "Иран", "", "IRAN" },
                    { new Guid("2f97369a-5c50-4926-8684-092f66401bdb"), "Германия", "", "GERMANY" },
                    { new Guid("45e42d47-dd50-421b-8d7f-4510f68fefb1"), "Южная Африка", "", "SOUTH AFRICA" },
                    { new Guid("5be6d899-da5f-4aa8-b78c-dd13246fe97c"), "Китай", "", "CHINA" },
                    { new Guid("8fee0964-ff1d-4581-be09-f7d01a15ccf8"), "США", "", "UNITED STATES" },
                    { new Guid("a6b41aea-b82f-4ee0-86d1-ce3e0c8396a4"), "Япония", "", "JAPAN" },
                    { new Guid("a959d539-be49-4216-9234-26ebd46f48e9"), "Северная Корея", "", "NORTH KOREA" },
                    { new Guid("e0e24634-9482-4557-b5a7-eb87661dcee2"), "Франция", "", "FRANCE" },
                    { new Guid("ed8be004-d9b6-4ec5-9861-6e7359ad4e30"), "Куба", "", "CUBA" }
                });

            migrationBuilder.InsertData(
                schema: "Game",
                table: "CityPatterns",
                columns: new[] { "Id", "CityImagePath", "CityName", "CountryId", "IsCapital", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("097371d6-d5f3-4c59-a3f3-35817f8c7e15"), "", "Пхеньян", new Guid("a959d539-be49-4216-9234-26ebd46f48e9"), true, "PYONGYANG" },
                    { new Guid("0d45f691-4c1f-4565-aa5a-aaaaa790d7c6"), "", "Гамбург", new Guid("2f97369a-5c50-4926-8684-092f66401bdb"), false, "HAMBURG" },
                    { new Guid("130aeab0-d375-4c33-aa57-eedb2780a667"), "", "Гавана", new Guid("ed8be004-d9b6-4ec5-9861-6e7359ad4e30"), true, "HAVANA" },
                    { new Guid("1c66a20e-fbaa-46a7-a27b-aa280d2e318d"), "", "Кередж", new Guid("279c40f4-fd73-4086-8d54-3b21eb169f57"), false, "KARAJ" },
                    { new Guid("2331501e-5c3d-4974-96a3-a9c520095d86"), "", "Ольгин", new Guid("ed8be004-d9b6-4ec5-9861-6e7359ad4e30"), false, "OLGUIN" },
                    { new Guid("2bb8d811-14ee-4e0a-99ef-591983db4083"), "", "Москва", new Guid("20747f9a-e5cc-4916-96f6-c0bd96c5a157"), true, "MOSCOW" },
                    { new Guid("2e5e2a2b-ae74-4cab-993b-254210f8ca89"), "", "Осака", new Guid("a6b41aea-b82f-4ee0-86d1-ce3e0c8396a4"), false, "OSAKA" },
                    { new Guid("3ea67e8f-6ee0-4982-be64-68642437e87f"), "", "Вашингтон", new Guid("8fee0964-ff1d-4581-be09-f7d01a15ccf8"), true, "WASHINGTON D.C." },
                    { new Guid("4388c4cc-7eb3-4266-8598-a698720590c4"), "", "Йоханнесбург", new Guid("45e42d47-dd50-421b-8d7f-4510f68fefb1"), false, "JOHANNESBURG" },
                    { new Guid("4f098407-b981-4d82-843a-b04f0db5965e"), "", "Кёльн", new Guid("2f97369a-5c50-4926-8684-092f66401bdb"), false, "COLOGNE" },
                    { new Guid("5043fbb5-af03-4b5e-9f12-8a27ec73b4f9"), "", "Новосибирск", new Guid("20747f9a-e5cc-4916-96f6-c0bd96c5a157"), false, "NOVOSIBIRSK" },
                    { new Guid("5d66e62f-119c-4e1f-86cd-ad03bcb80e12"), "", "Лион", new Guid("e0e24634-9482-4557-b5a7-eb87661dcee2"), false, "LYON" },
                    { new Guid("64abe1d3-5b95-4c95-8d41-c45ba9563092"), "", "Пекин", new Guid("5be6d899-da5f-4aa8-b78c-dd13246fe97c"), true, "BEIJING" },
                    { new Guid("683fbe15-edc9-43ee-81bf-49d807fc1644"), "", "Чхонджин", new Guid("a959d539-be49-4216-9234-26ebd46f48e9"), false, "CHONJIN" },
                    { new Guid("686ab953-0a12-40e6-9043-1610a540bccc"), "", "Мешхед", new Guid("279c40f4-fd73-4086-8d54-3b21eb169f57"), false, "MASHHAD" },
                    { new Guid("69953de8-04ab-46b8-bb58-79f994855c1f"), "", "Канберра", new Guid("026bf104-7588-4be7-b0cf-712b785504a5"), true, "CANBERRA" },
                    { new Guid("72fafb3d-aaa9-4ae1-a000-0b67649db2a1"), "", "Берлин", new Guid("2f97369a-5c50-4926-8684-092f66401bdb"), false, "BERLIN" },
                    { new Guid("7af76635-3df1-4f3d-af5b-6cfbccd2037f"), "", "Хамхын", new Guid("a959d539-be49-4216-9234-26ebd46f48e9"), false, "HAMHUNG" },
                    { new Guid("7c47cf7c-7bf4-4a16-a051-c5a3ec26d1c4"), "", "Кейптаун", new Guid("45e42d47-dd50-421b-8d7f-4510f68fefb1"), true, "CAPE TOWN" },
                    { new Guid("7e05ca47-273f-4698-a23a-8e397bb8f905"), "", "Брисбен", new Guid("026bf104-7588-4be7-b0cf-712b785504a5"), false, "BRISBANE" },
                    { new Guid("7e1bbd3d-3f52-4009-a329-8a7e96dd53d3"), "", "Мельбурн", new Guid("026bf104-7588-4be7-b0cf-712b785504a5"), false, "MELBOURNE" },
                    { new Guid("8ec8a013-aa6f-4814-a499-0b57a71b9ca3"), "", "Мюнхен", new Guid("2f97369a-5c50-4926-8684-092f66401bdb"), false, "MUNICH" },
                    { new Guid("8fecd871-7c8e-4a99-a599-9255d81665c7"), "", "Исфахан", new Guid("279c40f4-fd73-4086-8d54-3b21eb169f57"), false, "ISFAHAN" },
                    { new Guid("92b46bf5-e55a-419b-9a68-42b431d30601"), "", "Тяньцзинь", new Guid("5be6d899-da5f-4aa8-b78c-dd13246fe97c"), false, "TIANJIN" },
                    { new Guid("96570f4a-6d29-4eaf-a70b-ec2d4bea01fe"), "", "Йокогама", new Guid("a6b41aea-b82f-4ee0-86d1-ce3e0c8396a4"), false, "YOKOHAMA" },
                    { new Guid("99660a4e-0dfc-4c92-8fce-66c709ef647d"), "", "Санкт-Петербург", new Guid("20747f9a-e5cc-4916-96f6-c0bd96c5a157"), false, "SAINT PETERSBURG" },
                    { new Guid("9f943c40-6d97-4a7d-839d-cfaaec2d5fe1"), "", "Тегеран", new Guid("279c40f4-fd73-4086-8d54-3b21eb169f57"), true, "TEHRAN" },
                    { new Guid("a8f68bf2-7504-4309-bb79-0dd71709d9e1"), "", "Камагуэй", new Guid("ed8be004-d9b6-4ec5-9861-6e7359ad4e30"), false, "CAMAGUEY" },
                    { new Guid("ab4b8d4c-3d36-43a5-80a9-65c421f20729"), "", "Санктьяго-де-Куба", new Guid("ed8be004-d9b6-4ec5-9861-6e7359ad4e30"), false, "SANTIAGO DE CUBA" },
                    { new Guid("b68466b2-4d54-4b36-aea6-f351f9e59612"), "", "Лос-Анджелес", new Guid("8fee0964-ff1d-4581-be09-f7d01a15ccf8"), false, "LOS ANGELES" },
                    { new Guid("bbbf67a4-cbb7-4bd5-be4f-1c758695dd6d"), "", "Шанхай", new Guid("5be6d899-da5f-4aa8-b78c-dd13246fe97c"), false, "SHANGHAI" },
                    { new Guid("bc5d586f-a31a-477e-8e70-77ece368e497"), "", "Претория", new Guid("45e42d47-dd50-421b-8d7f-4510f68fefb1"), false, "PRETORIA" },
                    { new Guid("bc5db3f9-434b-49e5-8ea5-e82c945ad10a"), "", "Чикаго", new Guid("8fee0964-ff1d-4581-be09-f7d01a15ccf8"), false, "CHICAGO" },
                    { new Guid("c394b101-25e8-4be2-95cc-c0038b70eba8"), "", "Нью-Йорк", new Guid("8fee0964-ff1d-4581-be09-f7d01a15ccf8"), false, "NEW YORK CITY" },
                    { new Guid("d8410095-e2f6-4fbb-94b9-3423164baaed"), "", "Токио", new Guid("a6b41aea-b82f-4ee0-86d1-ce3e0c8396a4"), true, "TOKYO" },
                    { new Guid("e7174ba2-709d-4ed2-bf7b-b4542dff5a92"), "", "Париж", new Guid("e0e24634-9482-4557-b5a7-eb87661dcee2"), true, "PARIS" },
                    { new Guid("ebff9251-caae-4860-b86c-67ff04245a2e"), "", "Тулуза", new Guid("e0e24634-9482-4557-b5a7-eb87661dcee2"), false, "TOULOUSE" },
                    { new Guid("f1e59a7d-65b4-4e09-82c4-3eeffeef2d24"), "", "Дурбан", new Guid("45e42d47-dd50-421b-8d7f-4510f68fefb1"), false, "DURBAN" },
                    { new Guid("f30f26fb-1649-4526-9998-cc86d53fe82f"), "", "Екатеринбург", new Guid("20747f9a-e5cc-4916-96f6-c0bd96c5a157"), false, "YEKATERINBURG" },
                    { new Guid("f53d398c-1452-4989-9905-3387c5f4fc4f"), "", "Марсель", new Guid("e0e24634-9482-4557-b5a7-eb87661dcee2"), false, "MARSEILLE" },
                    { new Guid("f78a88a8-08fc-4441-b9fa-6af97c4891d9"), "", "Чунцин", new Guid("5be6d899-da5f-4aa8-b78c-dd13246fe97c"), false, "CHONGQING" },
                    { new Guid("f8fafec3-abb6-4e62-8506-659887dcda2f"), "", "Нагоя", new Guid("a6b41aea-b82f-4ee0-86d1-ce3e0c8396a4"), false, "NAGOYA" },
                    { new Guid("fad816e5-cd56-4aae-b07b-22f99f9086c5"), "", "Сидней", new Guid("026bf104-7588-4be7-b0cf-712b785504a5"), false, "SYDNEY" },
                    { new Guid("ff205331-9d0f-4951-8675-a419a4d849ce"), "", "Нампхо", new Guid("a959d539-be49-4216-9234-26ebd46f48e9"), false, "NAMPO" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                schema: "Game",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CityPatterns_CountryId",
                schema: "Game",
                table: "CityPatterns",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_GameId",
                schema: "Game",
                table: "Countries",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_RoomId",
                schema: "Game",
                table: "Countries",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomMembers_CountryId",
                schema: "Game",
                table: "RoomMembers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomMembers_RoomId",
                schema: "Game",
                table: "RoomMembers",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_CreatorId",
                schema: "Game",
                table: "Rooms",
                column: "CreatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities",
                schema: "Game");

            migrationBuilder.DropTable(
                name: "CityPatterns",
                schema: "Game");

            migrationBuilder.DropTable(
                name: "RoomMembers",
                schema: "Game");

            migrationBuilder.DropTable(
                name: "CountryPatterns",
                schema: "Game");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "Game");

            migrationBuilder.DropTable(
                name: "Games",
                schema: "Game");

            migrationBuilder.DropTable(
                name: "Rooms",
                schema: "Game");

            migrationBuilder.DropTable(
                name: "GameUsers",
                schema: "Game");
        }
    }
}
