using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LiteraVerseAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "PendingSync",
                columns: table => new
                {
                    SyncId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    EntityType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Action = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsSynced = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingSync", x => x.SyncId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Token = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastActivity = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeviceInfo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_Sessions_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    SettingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Theme = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    FontSize = table.Column<int>(type: "INTEGER", nullable: false),
                    NotificationsEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AutoPlayNext = table.Column<bool>(type: "INTEGER", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.SettingId);
                    table.ForeignKey(
                        name: "FK_Settings_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    StoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Synopsis = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    CoverImageUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IsDraft = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPublished = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ViewCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Genre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Tags = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.StoryId);
                    table.ForeignKey(
                        name: "FK_Stories_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chapters",
                columns: table => new
                {
                    ChapterId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    ChapterNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDraft = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPublished = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapters", x => x.ChapterId);
                    table.ForeignKey(
                        name: "FK_Chapters_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "StoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompletedStories",
                columns: table => new
                {
                    CompletedId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    StoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedStories", x => x.CompletedId);
                    table.ForeignKey(
                        name: "FK_CompletedStories_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "StoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompletedStories_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    FavoriteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    StoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.FavoriteId);
                    table.ForeignKey(
                        name: "FK_Favorites_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "StoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    LikeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    StoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.LikeId);
                    table.ForeignKey(
                        name: "FK_Likes_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "StoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReadingProgress",
                columns: table => new
                {
                    ProgressId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    StoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChapterId = table.Column<int>(type: "INTEGER", nullable: false),
                    ScrollPosition = table.Column<double>(type: "REAL", nullable: false),
                    LastReadAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingProgress", x => x.ProgressId);
                    table.ForeignKey(
                        name: "FK_ReadingProgress_Chapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapters",
                        principalColumn: "ChapterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReadingProgress_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "StoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReadingProgress_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "CreatedAt", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mundos mágicos y aventuras épicas", "Fantasía" },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Historias de amor y relaciones", "Romance" },
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Futuro y tecnología", "Ciencia Ficción" },
                    { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Suspense e intriga", "Misterio" },
                    { 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Historias de miedo", "Terror" },
                    { 6, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Acción y exploración", "Aventura" },
                    { 7, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emociones profundas", "Drama" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, "QAOVwOV/Bndoor3AQeQYOpIqcviPc1LJSkPvUPQ3q6g=", "LiteraVerse_Official" },
                    { 2, "grgljYV59mvdK6VNaADThLQroi4QSKj0/7XVyPsLTWk=", "MidnightInk" },
                    { 3, "092udNnc17LMlwKrOXdNiCpIUouKdXzorZ3IcUG5aWw=", "PhoenixQuill" },
                    { 4, "kz0rB4+yE1za3irVPYPkcZXEvUKNJCiVfbDZkWxdkhk=", "ShadowScribe" },
                    { 5, "HxCGc1WiqN+BGBbZxaP8U40hTh7fhIiYEVUEeLqwE+k=", "StardustWords" }
                });

            migrationBuilder.InsertData(
                table: "Stories",
                columns: new[] { "StoryId", "CoverImageUrl", "CreatedAt", "Genre", "IsDraft", "IsPublished", "PublishedAt", "Synopsis", "Tags", "Title", "UpdatedAt", "UserId", "ViewCount" },
                values: new object[,]
                {
                    { 1, "https://picsum.photos/seed/ember/400/600", new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fantasía", false, true, new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "In a world where magic is dying, one girl must reignite the ancient flames.", "magic, adventure, young adult", "The Last Ember", new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1523 },
                    { 2, "https://picsum.photos/seed/echoes/400/600", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ciencia Ficción", false, true, new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "A time traveler discovers that changing the past comes with impossible choices.", "time travel, sci-fi, thriller", "Echoes of Tomorrow", new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 892 },
                    { 3, "https://picsum.photos/seed/shadows/400/600", new DateTime(2024, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Misterio", false, true, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "A detective investigates a series of murders in a fog-covered Victorian town.", "detective, mystery, victorian", "Shadows in the Mist", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 1205 },
                    { 4, "https://picsum.photos/seed/hearts/400/600", new DateTime(2024, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Romance", false, true, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Two rival florists find unexpected love during the busiest season of the year.", "romance, contemporary, feel-good", "Hearts in Bloom", new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 756 },
                    { 5, "https://picsum.photos/seed/stellar/400/600", new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ciencia Ficción", false, true, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mapping uncharted space, a lone explorer finds a civilization that shouldn't exist.", "space, exploration, first contact", "The Stellar Cartographer", new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 423 },
                    { 6, "https://picsum.photos/seed/crimson/400/600", new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Romance", false, true, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "A forbidden romance blooms between a royal guard and a mysterious artist.", "forbidden love, historical, drama", "Crimson Petals", new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1098 },
                    { 7, "https://picsum.photos/seed/haunting/400/600", new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Terror", false, true, new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "A paranormal investigator faces her darkest fears in an abandoned mansion.", "ghosts, paranormal, suspense", "The Haunting of Ashwood Manor", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 654 },
                    { 8, "https://picsum.photos/seed/horizon/400/600", new DateTime(2024, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aventura", false, true, new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "A sailor discovers an island that exists in multiple dimensions simultaneously.", "sailing, dimensional travel, discovery", "Beyond the Horizon", new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 387 }
                });

            migrationBuilder.InsertData(
                table: "Chapters",
                columns: new[] { "ChapterId", "ChapterNumber", "Content", "CreatedAt", "IsDraft", "IsPublished", "PublishedAt", "StoryId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, "The last ember flickered in the ancient hearth, its crimson glow casting dancing shadows across the stone chamber. Aria pressed her palm against the cold wall, feeling the pulse of old magic fade beneath her fingertips. The elders had warned her this day would come, but she never imagined it would arrive so soon.", new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "The Dying Light", new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, "Legends spoke of a time when fire answered to will alone, when the chosen could summon flames with a thought. Aria traced the ancient runes carved into the hearth's edge, their meaning lost to time but their power still palpable in the air around her.", new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Whispers of the Old Magic", new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, "With nothing but a weathered map and the dying ember carefully preserved in a crystal vial, Aria set out into the Frozen Wastes. The wind howled around her like vengeful spirits, but she pressed forward, knowing that somewhere beyond the ice lay the Temple of First Flames.", new DateTime(2024, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(2024, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "The Journey Begins", new DateTime(2024, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, "The temporal device hummed to life, reality bending around her like ripples in a pond. Dr. Sarah Chen gripped the controls as the laboratory dissolved into streams of light. When the world solidified again, she stood in the same room—but everything was different.", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "First Jump", new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 2, "The equations didn't lie. Every change she made created divergent timelines, each one branching into infinite possibilities. But some changes—the ones that mattered most—seemed to resist her alterations, as if the universe itself was protecting certain outcomes.", new DateTime(2024, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(2024, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Paradox", new DateTime(2024, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 1, "Inspector Morrison surveyed the crime scene through the thick mist that perpetually shrouded Whitehaven. The victim lay exactly like the others—positioned with unnatural precision, a single white rose clutched in lifeless fingers. This was the seventh body in as many weeks.", new DateTime(2024, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Fog and Bone", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 2, "The only common thread between victims was their connection to the old Ashworth estate. Each had worked there at some point—gardeners, servants, tutors. And each had left under mysterious circumstances years before meeting their untimely end.", new DateTime(2024, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(2024, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "The Rose Garden Mystery", new DateTime(2024, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 1, "The wedding season had arrived, and Rosewood Avenue's two flower shops stood directly across from each other like dueling opponents. Maya's Petals and Thornwood Gardens had been competing for the same clients for five years, their rivalry as famous as their arrangements.", new DateTime(2024, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Rival Blooms", new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 1, "The star maps ended here, at the edge of explored space. Captain Elena Voss checked her instruments one more time before initiating the quantum jump. Her small vessel, the Cartographer's Dream, shuddered as it pierced the veil into unknown territory.", new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Uncharted Sector", new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 2, "Three days into uncharted space, the sensors picked up something impossible—a repeating pattern of electromagnetic pulses. Artificial. Deliberate. And according to her dating algorithms, approximately ten thousand years old.", new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "The Signal", new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "CompletedStories",
                columns: new[] { "CompletedId", "CompletedAt", "StoryId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 2, new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 3 }
                });

            migrationBuilder.InsertData(
                table: "Favorites",
                columns: new[] { "FavoriteId", "AddedAt", "StoryId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, new DateTime(2024, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1 },
                    { 3, new DateTime(2024, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2 },
                    { 4, new DateTime(2024, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 5, new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 4 }
                });

            migrationBuilder.InsertData(
                table: "Likes",
                columns: new[] { "LikeId", "CreatedAt", "StoryId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, new DateTime(2024, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 3, new DateTime(2024, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 4, new DateTime(2024, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3 },
                    { 5, new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4 },
                    { 6, new DateTime(2024, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 5 }
                });

            migrationBuilder.InsertData(
                table: "ReadingProgress",
                columns: new[] { "ProgressId", "ChapterId", "LastReadAt", "ScrollPosition", "StoryId", "UserId" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.34999999999999998, 1, 1 },
                    { 2, 1, new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.80000000000000004, 3, 1 },
                    { 3, 2, new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.14999999999999999, 2, 2 },
                    { 4, 3, new DateTime(2024, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.94999999999999996, 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_StoryId",
                table: "Chapters",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedStories_StoryId",
                table: "CompletedStories",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedStories_UserId",
                table: "CompletedStories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_StoryId",
                table: "Favorites",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_StoryId",
                table: "Likes",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingProgress_ChapterId",
                table: "ReadingProgress",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingProgress_StoryId",
                table: "ReadingProgress",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingProgress_UserId",
                table: "ReadingProgress",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_UserId",
                table: "Sessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_UserId",
                table: "Settings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_UserId",
                table: "Stories",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedStories");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "PendingSync");

            migrationBuilder.DropTable(
                name: "ReadingProgress");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Chapters");

            migrationBuilder.DropTable(
                name: "Stories");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
