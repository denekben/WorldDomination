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
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    RoomCode = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValue: new DateTime(2024, 10, 25, 20, 36, 17, 885, DateTimeKind.Utc).AddTicks(2162)),
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ProfileImagePath = table.Column<string>(type: "text", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomMemberRole = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    GameRole = table.Column<string>(type: "text", nullable: true),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomMembers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "Game",
                        principalTable: "Countries",
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
                    { new Guid("19ee1048-02f5-40d2-9447-2083238fd993"), "Франция", "" },
                    { new Guid("2e40ef1e-c71c-4332-9e81-d30835d53e11"), "США", "" },
                    { new Guid("33cedfd6-2f81-4c55-b306-269cf8db2921"), "Китай", "" },
                    { new Guid("58329df2-da01-46f5-b1cd-73e8ab7c8515"), "Австралия", "" },
                    { new Guid("592cdfe4-b207-4f81-bb3c-78c5aebfbf4a"), "Куба", "" },
                    { new Guid("7a9aebdc-e7b1-43b3-835d-3b82e234c49f"), "Северная Корея", "" },
                    { new Guid("875e7a51-c5b2-48cd-a0e4-ad6ad9b776c5"), "Германия", "" },
                    { new Guid("b21c3b08-4eaf-48fa-bb91-b11af819558e"), "Россия", "" },
                    { new Guid("c6fa0d7a-36bc-44c2-bb7e-1b1d026d2355"), "Иран", "" },
                    { new Guid("e5ac752f-307d-4440-9908-6f8212c3cdbf"), "Япония", "" },
                    { new Guid("f2f5b102-a83a-46a4-a674-d5f7b018dec7"), "Южная Африка", "" }
                });

            migrationBuilder.InsertData(
                schema: "Game",
                table: "CityPatterns",
                columns: new[] { "Id", "CityImagePath", "CityName", "CountryId" },
                values: new object[,]
                {
                    { new Guid("09d941c6-2296-403e-b8d1-0f85a0c8de77"), "", "Камагуэй", new Guid("592cdfe4-b207-4f81-bb3c-78c5aebfbf4a") },
                    { new Guid("1305e8cd-6a4a-494c-bff3-7ec5001cf826"), "", "Чикаго", new Guid("2e40ef1e-c71c-4332-9e81-d30835d53e11") },
                    { new Guid("14af488a-0603-4844-8f3f-4971899912d4"), "", "Чунцин", new Guid("33cedfd6-2f81-4c55-b306-269cf8db2921") },
                    { new Guid("28816d25-7b69-4148-be64-0d60369dbeb5"), "", "Йокогама", new Guid("e5ac752f-307d-4440-9908-6f8212c3cdbf") },
                    { new Guid("2daf5654-6267-4650-9bb2-851e218252f2"), "", "Санкт-Петербург", new Guid("b21c3b08-4eaf-48fa-bb91-b11af819558e") },
                    { new Guid("2f9ca9ce-a39c-4ff5-8471-76ec2cad317b"), "", "Лион", new Guid("19ee1048-02f5-40d2-9447-2083238fd993") },
                    { new Guid("31be4ca4-6bb7-436b-b4be-61a70eb91ac9"), "", "Сидней", new Guid("58329df2-da01-46f5-b1cd-73e8ab7c8515") },
                    { new Guid("3d91ae35-977c-4ea9-95ee-815d5eea03c4"), "", "Кёльн", new Guid("875e7a51-c5b2-48cd-a0e4-ad6ad9b776c5") },
                    { new Guid("41461c65-156f-42c6-a515-a95032df9c31"), "", "Екатеринбург", new Guid("b21c3b08-4eaf-48fa-bb91-b11af819558e") },
                    { new Guid("4389da51-334e-450e-9570-548c1d374ce6"), "", "Гамбург", new Guid("875e7a51-c5b2-48cd-a0e4-ad6ad9b776c5") },
                    { new Guid("47bc5911-6dff-4faa-bc45-d3b5af813890"), "", "Берлин", new Guid("875e7a51-c5b2-48cd-a0e4-ad6ad9b776c5") },
                    { new Guid("5131bb8e-c57b-47b7-8e7d-c08e497556df"), "", "Пекин", new Guid("33cedfd6-2f81-4c55-b306-269cf8db2921") },
                    { new Guid("55695aad-09d3-4027-a22b-d388f28737f6"), "", "Шанхай", new Guid("33cedfd6-2f81-4c55-b306-269cf8db2921") },
                    { new Guid("5787f307-9315-4731-ace4-10a01243d37c"), "", "Нагоя", new Guid("e5ac752f-307d-4440-9908-6f8212c3cdbf") },
                    { new Guid("59637a08-21f9-413a-a15d-5549dc4f636e"), "", "Дурбан", new Guid("f2f5b102-a83a-46a4-a674-d5f7b018dec7") },
                    { new Guid("5dc51bae-fe15-45d7-b230-8dfac15150a6"), "", "Вашингтон", new Guid("2e40ef1e-c71c-4332-9e81-d30835d53e11") },
                    { new Guid("6b7f952e-ef92-470c-896f-c221a4cd530b"), "", "Пхеньян", new Guid("7a9aebdc-e7b1-43b3-835d-3b82e234c49f") },
                    { new Guid("71c2cd0a-15f7-49bb-9cd9-375e2849e5ed"), "", "Тулуза", new Guid("19ee1048-02f5-40d2-9447-2083238fd993") },
                    { new Guid("720c5844-15a6-45cc-9cf3-dc1c711b8059"), "", "Новосибирск", new Guid("b21c3b08-4eaf-48fa-bb91-b11af819558e") },
                    { new Guid("781ce8a9-13a9-4b17-badb-36ea7822e62b"), "", "Чхонджин", new Guid("7a9aebdc-e7b1-43b3-835d-3b82e234c49f") },
                    { new Guid("7efa60a8-f18d-4029-9b42-8fa84f7a8044"), "", "Мельбурн", new Guid("58329df2-da01-46f5-b1cd-73e8ab7c8515") },
                    { new Guid("81291e5d-7b8e-47f1-97e7-62db66faeb73"), "", "Лос-Анджелес", new Guid("2e40ef1e-c71c-4332-9e81-d30835d53e11") },
                    { new Guid("814daf02-ac4b-426d-8058-a84ffc7bb8d8"), "", "Нью-Йорк", new Guid("2e40ef1e-c71c-4332-9e81-d30835d53e11") },
                    { new Guid("890fb171-5863-4107-bd50-e3f2d217ffc9"), "", "Ольгин", new Guid("592cdfe4-b207-4f81-bb3c-78c5aebfbf4a") },
                    { new Guid("921210e4-850f-4226-bd0e-0eead064c1fb"), "", "Токио", new Guid("e5ac752f-307d-4440-9908-6f8212c3cdbf") },
                    { new Guid("93ef9d73-22a4-4569-a0fc-dd55f9137d63"), "", "Хамхын", new Guid("7a9aebdc-e7b1-43b3-835d-3b82e234c49f") },
                    { new Guid("9783c293-c568-40a2-82a9-ee67b0b120a7"), "", "Париж", new Guid("19ee1048-02f5-40d2-9447-2083238fd993") },
                    { new Guid("9b0b51b7-1386-4ec1-8499-6abce5e0f0f6"), "", "Тегеран", new Guid("c6fa0d7a-36bc-44c2-bb7e-1b1d026d2355") },
                    { new Guid("ab1cf4ec-651d-461c-9fe5-26f90c4b3db7"), "", "Претория", new Guid("f2f5b102-a83a-46a4-a674-d5f7b018dec7") },
                    { new Guid("b0c04272-98fa-4ea2-a150-04f0a6ad3c98"), "", "Исфахан", new Guid("c6fa0d7a-36bc-44c2-bb7e-1b1d026d2355") },
                    { new Guid("bfee2fef-c600-4f4a-aeb5-142cfb8fb7d7"), "", "Йоханнесбург", new Guid("f2f5b102-a83a-46a4-a674-d5f7b018dec7") },
                    { new Guid("c35cda50-96b4-4664-a757-8d81f6f98d0b"), "", "Осака", new Guid("e5ac752f-307d-4440-9908-6f8212c3cdbf") },
                    { new Guid("c4b3bbe7-0709-470f-93bf-34d1b1054ab1"), "", "Канберра", new Guid("58329df2-da01-46f5-b1cd-73e8ab7c8515") },
                    { new Guid("ce4406b2-1e9f-4e6a-82ec-58dd4f1a8ee4"), "", "Кейптаун", new Guid("f2f5b102-a83a-46a4-a674-d5f7b018dec7") },
                    { new Guid("ce8fef66-4289-4182-b5b4-11ca9066b9b1"), "", "Гавана", new Guid("592cdfe4-b207-4f81-bb3c-78c5aebfbf4a") },
                    { new Guid("cf11d6b6-2669-4e23-9e50-ab50ce518ffe"), "", "Мюнхен", new Guid("875e7a51-c5b2-48cd-a0e4-ad6ad9b776c5") },
                    { new Guid("cf747b82-6483-4536-b248-9b3fc5bf3c12"), "", "Кередж", new Guid("c6fa0d7a-36bc-44c2-bb7e-1b1d026d2355") },
                    { new Guid("e0afc709-6126-4c51-9f59-eacfe5f5ad64"), "", "Нампхо", new Guid("7a9aebdc-e7b1-43b3-835d-3b82e234c49f") },
                    { new Guid("e1c79484-33bc-447f-9367-84d831e4177f"), "", "Марсель", new Guid("19ee1048-02f5-40d2-9447-2083238fd993") },
                    { new Guid("eae513cf-01e0-4783-af28-95f0fec70377"), "", "Санктьяго-де-Куба", new Guid("592cdfe4-b207-4f81-bb3c-78c5aebfbf4a") },
                    { new Guid("ebc715d7-45ba-4318-8a5c-e98b82f7f1ee"), "", "Москва", new Guid("b21c3b08-4eaf-48fa-bb91-b11af819558e") },
                    { new Guid("ec812c2b-f5d6-4bb6-8997-034020c3f8f2"), "", "Тяньцзинь", new Guid("33cedfd6-2f81-4c55-b306-269cf8db2921") },
                    { new Guid("ed47facd-90a5-41e9-bf4b-207f8a19e8a0"), "", "Брисбен", new Guid("58329df2-da01-46f5-b1cd-73e8ab7c8515") },
                    { new Guid("f29a683a-5dfe-4d1a-9c47-3655e3f31d2f"), "", "Мешхед", new Guid("c6fa0d7a-36bc-44c2-bb7e-1b1d026d2355") }
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
