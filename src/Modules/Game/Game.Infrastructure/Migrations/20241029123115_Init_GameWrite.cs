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
                    CityImagePath = table.Column<string>(type: "text", nullable: false),
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
                    RoomMemberLimit = table.Column<int>(type: "integer", nullable: false),
                    CountryQuantity = table.Column<int>(type: "integer", nullable: false),
                    IsPrivate = table.Column<bool>(type: "boolean", nullable: false),
                    RoomCode = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValue: new DateTime(2024, 10, 29, 12, 31, 14, 868, DateTimeKind.Utc).AddTicks(1845)),
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameType = table.Column<string>(type: "text", nullable: false),
                    CurrentRound = table.Column<int>(type: "integer", nullable: false),
                    EcologyLevel = table.Column<int>(type: "integer", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
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
                    FlagImagePath = table.Column<string>(type: "text", nullable: false),
                    LivingLevel = table.Column<int>(type: "integer", nullable: false),
                    Budget = table.Column<int>(type: "integer", nullable: false),
                    HaveNuclearTechnology = table.Column<bool>(type: "boolean", nullable: false),
                    NuclearTechnology = table.Column<int>(type: "integer", nullable: false),
                    SanctionCount = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Games_GameId",
                        column: x => x.GameId,
                        principalSchema: "Game",
                        principalTable: "Games",
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
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
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
                columns: new[] { "Id", "CountryName", "FlagImagePath" },
                values: new object[,]
                {
                    { new Guid("074cbd39-72be-4e83-89f7-e6e51296d178"), "Германия", "" },
                    { new Guid("24691a70-c8ef-431b-bcc5-cc24e5b1d106"), "Япония", "" },
                    { new Guid("46e7f08f-7a10-4113-a04c-5ed1fdab5462"), "Китай", "" },
                    { new Guid("586fe6e6-070d-4879-98f0-a73a27f14c7f"), "Австралия", "" },
                    { new Guid("643ab2d6-141d-4057-b429-f37de825a0f7"), "Франция", "" },
                    { new Guid("6da48a27-aa52-4f41-adb4-843bc6431bf9"), "Куба", "" },
                    { new Guid("871ca2b4-cdf2-4926-8701-da445c1a3826"), "Южная Африка", "" },
                    { new Guid("e25e46ed-623a-418d-9a20-52b624602b6c"), "Иран", "" },
                    { new Guid("eceb8a54-5060-477d-8698-d81b32cef044"), "США", "" },
                    { new Guid("f2e5b4f3-d37b-4354-8e9d-14f0616c2e87"), "Россия", "" },
                    { new Guid("fa9c178c-817d-4730-ac8e-a0debb1c2ef0"), "Северная Корея", "" }
                });

            migrationBuilder.InsertData(
                schema: "Game",
                table: "CityPatterns",
                columns: new[] { "Id", "CityImagePath", "CityName", "CountryId" },
                values: new object[,]
                {
                    { new Guid("062a9155-81bd-4d83-9c57-7c71b1f45bfa"), "", "Дурбан", new Guid("871ca2b4-cdf2-4926-8701-da445c1a3826") },
                    { new Guid("08a0aa28-6a6a-405e-aed6-1d283103ce33"), "", "Новосибирск", new Guid("f2e5b4f3-d37b-4354-8e9d-14f0616c2e87") },
                    { new Guid("0ac08c48-b4c2-4590-b0ce-d345f4019512"), "", "Мешхед", new Guid("e25e46ed-623a-418d-9a20-52b624602b6c") },
                    { new Guid("0f6ec8bd-d7bf-40aa-a808-a9b3d96a58f3"), "", "Берлин", new Guid("074cbd39-72be-4e83-89f7-e6e51296d178") },
                    { new Guid("17848519-0f15-4314-8c72-a7b753c768a5"), "", "Йоханнесбург", new Guid("871ca2b4-cdf2-4926-8701-da445c1a3826") },
                    { new Guid("1f0a2d29-8e82-4ccb-bd86-df4f17e159fd"), "", "Пекин", new Guid("46e7f08f-7a10-4113-a04c-5ed1fdab5462") },
                    { new Guid("1f329bd1-7a31-4e0d-afea-2af1f0cea8fa"), "", "Чхонджин", new Guid("fa9c178c-817d-4730-ac8e-a0debb1c2ef0") },
                    { new Guid("2a44ec69-48d8-4fce-8e32-b6d3ef7eab9f"), "", "Тегеран", new Guid("e25e46ed-623a-418d-9a20-52b624602b6c") },
                    { new Guid("2f1bbffb-3304-44a0-b36f-fbc32f071d62"), "", "Мюнхен", new Guid("074cbd39-72be-4e83-89f7-e6e51296d178") },
                    { new Guid("30a43945-1d2d-4b9b-ae3f-6c8ae289bcad"), "", "Канберра", new Guid("586fe6e6-070d-4879-98f0-a73a27f14c7f") },
                    { new Guid("340bbfe2-3240-4075-9fe6-d266e7ee5f96"), "", "Претория", new Guid("871ca2b4-cdf2-4926-8701-da445c1a3826") },
                    { new Guid("35732768-d2f8-4949-9cc4-1ff13a22bd5a"), "", "Санкт-Петербург", new Guid("f2e5b4f3-d37b-4354-8e9d-14f0616c2e87") },
                    { new Guid("40a3f5a0-05bc-4de4-a070-db5ba9ed15bb"), "", "Мельбурн", new Guid("586fe6e6-070d-4879-98f0-a73a27f14c7f") },
                    { new Guid("418cf8a7-1dc8-4028-806c-8756fec997f9"), "", "Сидней", new Guid("586fe6e6-070d-4879-98f0-a73a27f14c7f") },
                    { new Guid("551e15d3-969d-425d-9fcc-f5c6c6b77eaa"), "", "Осака", new Guid("24691a70-c8ef-431b-bcc5-cc24e5b1d106") },
                    { new Guid("5d030ce5-950e-4b56-81e6-7d6f44e6f798"), "", "Кередж", new Guid("e25e46ed-623a-418d-9a20-52b624602b6c") },
                    { new Guid("5de2b177-b192-4161-8067-78f084468d6d"), "", "Чикаго", new Guid("eceb8a54-5060-477d-8698-d81b32cef044") },
                    { new Guid("6699724a-0c69-4ac1-b695-2b6ee36cd557"), "", "Кёльн", new Guid("074cbd39-72be-4e83-89f7-e6e51296d178") },
                    { new Guid("6b439105-5111-41c9-87b9-e6507457f27a"), "", "Марсель", new Guid("643ab2d6-141d-4057-b429-f37de825a0f7") },
                    { new Guid("6cff118d-23bc-4e5d-b433-b5d3d65d51d3"), "", "Москва", new Guid("f2e5b4f3-d37b-4354-8e9d-14f0616c2e87") },
                    { new Guid("7db32f17-b8c3-4b84-823a-66ce90704f45"), "", "Екатеринбург", new Guid("f2e5b4f3-d37b-4354-8e9d-14f0616c2e87") },
                    { new Guid("8106cdec-1dfb-4e5d-9953-e8827816b31c"), "", "Йокогама", new Guid("24691a70-c8ef-431b-bcc5-cc24e5b1d106") },
                    { new Guid("8263b844-ca53-4d3a-944c-c6f459244c09"), "", "Токио", new Guid("24691a70-c8ef-431b-bcc5-cc24e5b1d106") },
                    { new Guid("8e3917a5-e3a0-4c7e-962a-a69d643eec76"), "", "Вашингтон", new Guid("eceb8a54-5060-477d-8698-d81b32cef044") },
                    { new Guid("922ae8af-8b85-4e6b-9d6c-20b6b4cc5ba0"), "", "Шанхай", new Guid("46e7f08f-7a10-4113-a04c-5ed1fdab5462") },
                    { new Guid("93c5273a-e257-4c1e-a027-aa23dac50267"), "", "Гавана", new Guid("6da48a27-aa52-4f41-adb4-843bc6431bf9") },
                    { new Guid("99fc3d69-9b66-491d-ad19-aedfe1c32cf1"), "", "Лион", new Guid("643ab2d6-141d-4057-b429-f37de825a0f7") },
                    { new Guid("a32c3f7c-2ced-403f-b151-c6f69c1bb9b6"), "", "Нампхо", new Guid("fa9c178c-817d-4730-ac8e-a0debb1c2ef0") },
                    { new Guid("ad15369a-9bbb-45e8-a0ce-021c05b5a546"), "", "Тяньцзинь", new Guid("46e7f08f-7a10-4113-a04c-5ed1fdab5462") },
                    { new Guid("b149ac02-b833-442d-a824-2be95a6b7103"), "", "Кейптаун", new Guid("871ca2b4-cdf2-4926-8701-da445c1a3826") },
                    { new Guid("b4c91a53-eee7-4b4a-a54d-96df9ff1e49c"), "", "Нью-Йорк", new Guid("eceb8a54-5060-477d-8698-d81b32cef044") },
                    { new Guid("c6eb130b-870c-478b-bac0-ac74d07b62c4"), "", "Париж", new Guid("643ab2d6-141d-4057-b429-f37de825a0f7") },
                    { new Guid("c8208bd9-f147-4d85-80d8-22fde413ae03"), "", "Гамбург", new Guid("074cbd39-72be-4e83-89f7-e6e51296d178") },
                    { new Guid("d81d3c64-becb-479d-915f-3141c8100ee7"), "", "Исфахан", new Guid("e25e46ed-623a-418d-9a20-52b624602b6c") },
                    { new Guid("d8e89406-bfde-4c18-a8e1-17d6546a8744"), "", "Тулуза", new Guid("643ab2d6-141d-4057-b429-f37de825a0f7") },
                    { new Guid("deb75679-616e-4fc6-b985-c1f636652d9c"), "", "Брисбен", new Guid("586fe6e6-070d-4879-98f0-a73a27f14c7f") },
                    { new Guid("eaedb9eb-98c7-4adf-90b0-b4bae5a0374c"), "", "Нагоя", new Guid("24691a70-c8ef-431b-bcc5-cc24e5b1d106") },
                    { new Guid("ebf1700b-8efb-4d31-92ef-b28ec189dc58"), "", "Хамхын", new Guid("fa9c178c-817d-4730-ac8e-a0debb1c2ef0") },
                    { new Guid("edfd1ca7-3afc-4b98-9398-3a5bddcb2241"), "", "Чунцин", new Guid("46e7f08f-7a10-4113-a04c-5ed1fdab5462") },
                    { new Guid("f0da611b-6e60-4e0b-9e36-230abba3a0de"), "", "Санктьяго-де-Куба", new Guid("6da48a27-aa52-4f41-adb4-843bc6431bf9") },
                    { new Guid("f38d9ee1-31dc-4d38-ac21-a0d3f4465914"), "", "Пхеньян", new Guid("fa9c178c-817d-4730-ac8e-a0debb1c2ef0") },
                    { new Guid("f987c5ce-a1e9-48dc-a6a3-201cb371043e"), "", "Камагуэй", new Guid("6da48a27-aa52-4f41-adb4-843bc6431bf9") },
                    { new Guid("fbea6ee5-8d97-47b3-86ef-f4b818b93a51"), "", "Ольгин", new Guid("6da48a27-aa52-4f41-adb4-843bc6431bf9") },
                    { new Guid("febadf7b-937f-4d8e-ab1e-e0739a3e4cac"), "", "Лос-Анджелес", new Guid("eceb8a54-5060-477d-8698-d81b32cef044") }
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
                name: "IX_Games_RoomId",
                schema: "Game",
                table: "Games",
                column: "RoomId",
                unique: true);

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
