using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LiteraVerseAPI.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
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
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Futuros distópicos y tecnología", "Ciencia Ficción" },
                    { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enigmas y suspense", "Misterio" },
                    { 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Acción y adrenalina", "Thriller" },
                    { 6, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Historias emotivas y profundas", "Drama" },
                    { 7, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Criaturas sobrenaturales", "Paranormal" },
                    { 8, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Historias cotidianas", "Slice of Life" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", "LiteraVerse_Official" },
                    { 2, "YlF8jKPfIAL+L2dmxe9OhwRK9yUoNQcwJw9aKqLQe3M=", "MidnightInk" },
                    { 3, "bQTNz5rIfLKQVEBkr+/8CsVJfQVcXJIbLxYBQnPFqKI=", "PhoenixQuill" },
                    { 4, "Xkn5dNH5VKLzLqKEJ1vJ8q/wqfKIIHlJ5ksH8xQm8pM=", "ShadowScribe" },
                    { 5, "YNQwZLHvLZK+5qKEJ1vJ8q/wqfKIIHlJ5ksH8xQm8pM=", "StardustWords" }
                });

            migrationBuilder.InsertData(
                table: "Stories",
                columns: new[] { "StoryId", "CoverImageUrl", "CreatedAt", "Genre", "IsDraft", "IsPublished", "PublishedAt", "Synopsis", "Tags", "Title", "UpdatedAt", "UserId", "ViewCount" },
                values: new object[,]
                {
                    { 1, "https://images.unsplash.com/photo-1518709268805-4e9042af9f23", new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fantasía", false, true, new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "En un mundo donde la magia se extingue, una joven hechicera debe encontrar la última llama ancestral antes de que caiga en manos equivocadas. Una épica aventura llena de magia, traición y redención.", "magia,aventura,medieval", "The Last Ember", new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 15420 },
                    { 2, "https://images.unsplash.com/photo-1614732414444-096e5f1122d5", new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ciencia Ficción", false, true, new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "En el año 2157, la humanidad vive en una realidad virtual permanente. Alex, un hacker rebelde, descubre que todo es una simulación y debe elegir entre la verdad dolorosa o la mentira confortable.", "cyberpunk,distopía,IA", "Digital Fate", new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 12890 },
                    { 3, "https://images.unsplash.com/photo-1516979187457-637abb4f9353", new DateTime(2024, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Romance", false, true, new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emma, una artista solitaria, comienza a recibir mensajes anónimos llenos de poesía. Cuando descubre la identidad del remitente, su vida cambiará para siempre. Una historia de amor inesperado.", "amor,misterio,contemporáneo", "Midnight Whispers", new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 18750 },
                    { 4, "https://images.unsplash.com/photo-1505682634904-d7c8d95cdc50", new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Misterio", false, true, new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Una detective es llamada para resolver una serie de asesinatos conectados por un símbolo antiguo. La verdad la llevará a descubrir secretos que la humanidad olvidó hace milenios.", "detective,crimen,sobrenatural", "Echoes of the Void", new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 9340 },
                    { 5, "https://images.unsplash.com/photo-1550745165-9bc0b252726f", new DateTime(2024, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thriller", false, true, new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "En las calles iluminadas por neones de Nueva Tokio, un asesino a sueldo recibe un contrato que lo obligará a enfrentar su pasado. Acción frenética y dilemas morales en cada página.", "acción,noir,urbano", "Neon Souls", new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 11200 },
                    { 6, "https://images.unsplash.com/photo-1509023464722-18d996393ca8", new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Paranormal", false, true, new DateTime(2024, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luna siempre supo que era diferente, pero cuando descubre que es una loba alfa en un mundo donde su especie se creía extinta, deberá liderar una revolución o ver a los suyos desaparecer para siempre.", "hombres lobo,fantasía,romance", "Beneath the Silver Moon", new DateTime(2024, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 16500 },
                    { 7, "https://images.unsplash.com/photo-1495474472287-4d71bcdd2085", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Slice of Life", false, true, new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sarah trabaja en una cafetería donde cada cliente tiene una historia que contar. A través de conversaciones cotidianas, descubre el verdadero significado de la conexión humana.", "cotidiano,inspirador,realista", "Coffee and Conversations", new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 7890 },
                    { 8, "https://images.unsplash.com/photo-1519558260268-cde7e03a0152", new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fantasía", false, true, new DateTime(2024, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tres reinos en guerra, una profecía olvidada y un guerrero sin memoria que podría ser la clave para la paz... o la destrucción total. Fantasía épica en su máxima expresión.", "épico,guerra,magia", "Shattered Kingdoms", new DateTime(2024, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 14300 },
                    { 9, "https://images.unsplash.com/photo-1481026469463-66327c86e544", new DateTime(2024, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thriller", false, true, new DateTime(2024, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Una niña muda es la única testigo de un crimen brutal. Cuando el asesino descubre su existencia, comienza una carrera contra el tiempo para protegerla antes de que sea demasiado tarde.", "suspense,crimen,psicológico", "The Silent Witness", new DateTime(2024, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 10560 },
                    { 10, "https://images.unsplash.com/photo-1518133910853-2e5f8bb15a98", new DateTime(2024, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Romance", false, true, new DateTime(2024, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Una carta antigua lleva a Clara a descubrir una historia de amor del siglo XIX. Mientras investiga, encuentra paralelismos inquietantes con su propia vida. ¿El amor puede trascender el tiempo?", "viajes en el tiempo,histórico,drama", "Hearts Across Time", new DateTime(2024, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 13200 }
                });

            migrationBuilder.InsertData(
                table: "Chapters",
                columns: new[] { "ChapterId", "ChapterNumber", "Content", "CreatedAt", "IsDraft", "IsPublished", "PublishedAt", "StoryId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, "La torre de Ébano se alzaba contra el cielo carmesí, última fortaleza de una era olvidada. Lyra despertó sintiendo el calor de la última llama mágica palpitando en su pecho...", new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "El Despertar", new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, "El consejo de magos la miraba con desconfianza. Nadie había despertado el don en más de cincuenta años. ¿Era ella realmente la elegida o solo una ilusión desesperada?", new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Sombras del Pasado", new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, "Con solo una mochila y un mapa antiguo, Lyra abandonó la seguridad de la torre. El mundo exterior era más peligroso y hermoso de lo que jamás imaginó.", new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "El Viaje Comienza", new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, "El código en la pantalla no tenía sentido. Alex llevaba años hackeando sistemas, pero esto era diferente. Esto era... imposible. El mundo digital comenzaba a desmoronarse.", new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Desconexión", new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 2, "Salir de la Matrix no era una metáfora. Era literal. Y la realidad física era un páramo desolado donde la humanidad apenas sobrevivía en bunkers subterráneos.", new DateTime(2024, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(2024, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "La Verdad Duele", new DateTime(2024, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 1, "La nota apareció debajo de su puerta un martes por la mañana. Papel antiguo, tinta negra, caligrafía perfecta. Las palabras hablaban de su alma de una manera que nadie más había logrado.", new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "El Primer Mensaje", new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 2, "Emma se obsesionó con encontrar al autor. Cada pista la acercaba más a descubrir una verdad que cambiaría todo lo que creía saber sobre el amor.", new DateTime(2024, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(2024, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Buscando al Poeta", new DateTime(2024, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 3, "Cuando finalmente lo vio, entendió por qué había permanecido en las sombras. No era quien esperaba. Era alguien que había estado frente a ella todo el tiempo.", new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "El Encuentro", new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
