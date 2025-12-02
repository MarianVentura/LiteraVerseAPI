using LiteraVerseApi.Models;
using LiteraVerseApi.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LiteraVerseApi.DAL;

public class Contexto(DbContextOptions<Contexto> options) : DbContext(options)
{
    public DbSet<Usuarios> Usuarios { get; set; }
    public DbSet<Stories> Stories { get; set; }
    public DbSet<Chapters> Chapters { get; set; }
    public DbSet<Favorites> Favorites { get; set; }
    public DbSet<ReadingProgress> ReadingProgress { get; set; }
    public DbSet<CompletedStories> CompletedStories { get; set; }
    public DbSet<Genres> Genres { get; set; }
    public DbSet<PendingSync> PendingSync { get; set; }
    public DbSet<Settings> Settings { get; set; }
    public DbSet<Sessions> Sessions { get; set; }
    public DbSet<Likes> Likes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuarios>().HasData(
            new Usuarios
            {
                UsuarioId = 1,
                UserName = "LiteraVerse_Official",
                Password = PasswordHasher.HashPassword("literaverse2025")
            },
            new Usuarios
            {
                UsuarioId = 2,
                UserName = "MidnightInk",
                Password = PasswordHasher.HashPassword("midnight2025")
            },
            new Usuarios
            {
                UsuarioId = 3,
                UserName = "PhoenixQuill",
                Password = PasswordHasher.HashPassword("phoenix2025")
            },
            new Usuarios
            {
                UsuarioId = 4,
                UserName = "ShadowScribe",
                Password = PasswordHasher.HashPassword("shadow2025")
            },
            new Usuarios
            {
                UsuarioId = 5,
                UserName = "StardustWords",
                Password = PasswordHasher.HashPassword("stardust2025")
            }
        );

        modelBuilder.Entity<Genres>().HasData(
            new Genres { GenreId = 1, Name = "Fantasía", Description = "Mundos mágicos y aventuras épicas", CreatedAt = new DateTime(2025, 1, 1) },
            new Genres { GenreId = 2, Name = "Romance", Description = "Historias de amor y relaciones", CreatedAt = new DateTime(2025, 1, 1) },
            new Genres { GenreId = 3, Name = "Ciencia Ficción", Description = "Futuro y tecnología", CreatedAt = new DateTime(2025, 1, 1) },
            new Genres { GenreId = 4, Name = "Misterio", Description = "Suspense e intriga", CreatedAt = new DateTime(2025, 1, 1) },
            new Genres { GenreId = 5, Name = "Terror", Description = "Historias de miedo", CreatedAt = new DateTime(2025, 1, 1) },
            new Genres { GenreId = 6, Name = "Aventura", Description = "Acción y exploración", CreatedAt = new DateTime(2025, 1, 1) },
            new Genres { GenreId = 7, Name = "Drama", Description = "Emociones profundas", CreatedAt = new DateTime(2025, 1, 1) }
        );

        modelBuilder.Entity<Stories>().HasData(
            new Stories
            {
                StoryId = 1,
                UserId = 2,
                Title = "The Last Ember",
                Synopsis = "In a world where magic is dying, one girl must reignite the ancient flames.",
                CoverImageUrl = "https://picsum.photos/seed/ember/400/600",
                IsDraft = false,
                IsPublished = true,
                PublishedAt = new DateTime(2024, 11, 15),
                Genre = "Fantasía",
                Tags = "magic, adventure, young adult",
                ViewCount = 1523,
                CreatedAt = new DateTime(2024, 11, 10),
                UpdatedAt = new DateTime(2024, 11, 15)
            },
            new Stories
            {
                StoryId = 2,
                UserId = 3,
                Title = "Echoes of Tomorrow",
                Synopsis = "A time traveler discovers that changing the past comes with impossible choices.",
                CoverImageUrl = "https://picsum.photos/seed/echoes/400/600",
                IsDraft = false,
                IsPublished = true,
                PublishedAt = new DateTime(2024, 11, 20),
                Genre = "Ciencia Ficción",
                Tags = "time travel, sci-fi, thriller",
                ViewCount = 892,
                CreatedAt = new DateTime(2024, 11, 18),
                UpdatedAt = new DateTime(2024, 11, 20)
            },
            new Stories
            {
                StoryId = 3,
                UserId = 4,
                Title = "Shadows in the Mist",
                Synopsis = "A detective investigates a series of murders in a fog-covered Victorian town.",
                CoverImageUrl = "https://picsum.photos/seed/shadows/400/600",
                IsDraft = false,
                IsPublished = true,
                PublishedAt = new DateTime(2024, 11, 25),
                Genre = "Misterio",
                Tags = "detective, mystery, victorian",
                ViewCount = 1205,
                CreatedAt = new DateTime(2024, 11, 22),
                UpdatedAt = new DateTime(2024, 11, 25)
            },
            new Stories
            {
                StoryId = 4,
                UserId = 2,
                Title = "Hearts in Bloom",
                Synopsis = "Two rival florists find unexpected love during the busiest season of the year.",
                CoverImageUrl = "https://picsum.photos/seed/hearts/400/600",
                IsDraft = false,
                IsPublished = true,
                PublishedAt = new DateTime(2024, 12, 1),
                Genre = "Romance",
                Tags = "romance, contemporary, feel-good",
                ViewCount = 756,
                CreatedAt = new DateTime(2024, 11, 28),
                UpdatedAt = new DateTime(2024, 12, 1)
            },
            new Stories
            {
                StoryId = 5,
                UserId = 5,
                Title = "The Stellar Cartographer",
                Synopsis = "Mapping uncharted space, a lone explorer finds a civilization that shouldn't exist.",
                CoverImageUrl = "https://picsum.photos/seed/stellar/400/600",
                IsDraft = false,
                IsPublished = true,
                PublishedAt = new DateTime(2024, 12, 5),
                Genre = "Ciencia Ficción",
                Tags = "space, exploration, first contact",
                ViewCount = 423,
                CreatedAt = new DateTime(2024, 12, 2),
                UpdatedAt = new DateTime(2024, 12, 5)
            },
            new Stories
            {
                StoryId = 6,
                UserId = 3,
                Title = "Crimson Petals",
                Synopsis = "A forbidden romance blooms between a royal guard and a mysterious artist.",
                CoverImageUrl = "https://picsum.photos/seed/crimson/400/600",
                IsDraft = false,
                IsPublished = true,
                PublishedAt = new DateTime(2024, 12, 8),
                Genre = "Romance",
                Tags = "forbidden love, historical, drama",
                ViewCount = 1098,
                CreatedAt = new DateTime(2024, 12, 5),
                UpdatedAt = new DateTime(2024, 12, 8)
            },
            new Stories
            {
                StoryId = 7,
                UserId = 4,
                Title = "The Haunting of Ashwood Manor",
                Synopsis = "A paranormal investigator faces her darkest fears in an abandoned mansion.",
                CoverImageUrl = "https://picsum.photos/seed/haunting/400/600",
                IsDraft = false,
                IsPublished = true,
                PublishedAt = new DateTime(2024, 12, 10),
                Genre = "Terror",
                Tags = "ghosts, paranormal, suspense",
                ViewCount = 654,
                CreatedAt = new DateTime(2024, 12, 7),
                UpdatedAt = new DateTime(2024, 12, 10)
            },
            new Stories
            {
                StoryId = 8,
                UserId = 5,
                Title = "Beyond the Horizon",
                Synopsis = "A sailor discovers an island that exists in multiple dimensions simultaneously.",
                CoverImageUrl = "https://picsum.photos/seed/horizon/400/600",
                IsDraft = false,
                IsPublished = true,
                PublishedAt = new DateTime(2024, 12, 12),
                Genre = "Aventura",
                Tags = "sailing, dimensional travel, discovery",
                ViewCount = 387,
                CreatedAt = new DateTime(2024, 12, 9),
                UpdatedAt = new DateTime(2024, 12, 12)
            }
        );

        modelBuilder.Entity<Chapters>().HasData(
            new Chapters { ChapterId = 1, StoryId = 1, Title = "The Dying Light", Content = "The last ember flickered in the ancient hearth, its crimson glow casting dancing shadows across the stone chamber. Aria pressed her palm against the cold wall, feeling the pulse of old magic fade beneath her fingertips. The elders had warned her this day would come, but she never imagined it would arrive so soon.", ChapterNumber = 1, IsDraft = false, IsPublished = true, PublishedAt = new DateTime(2024, 11, 15), CreatedAt = new DateTime(2024, 11, 10), UpdatedAt = new DateTime(2024, 11, 15) },

            new Chapters { ChapterId = 2, StoryId = 1, Title = "Whispers of the Old Magic", Content = "Legends spoke of a time when fire answered to will alone, when the chosen could summon flames with a thought. Aria traced the ancient runes carved into the hearth's edge, their meaning lost to time but their power still palpable in the air around her.", ChapterNumber = 2, IsDraft = false, IsPublished = true, PublishedAt = new DateTime(2024, 11, 15), CreatedAt = new DateTime(2024, 11, 11), UpdatedAt = new DateTime(2024, 11, 15) },

            new Chapters { ChapterId = 3, StoryId = 1, Title = "The Journey Begins", Content = "With nothing but a weathered map and the dying ember carefully preserved in a crystal vial, Aria set out into the Frozen Wastes. The wind howled around her like vengeful spirits, but she pressed forward, knowing that somewhere beyond the ice lay the Temple of First Flames.", ChapterNumber = 3, IsDraft = false, IsPublished = true, PublishedAt = new DateTime(2024, 11, 16), CreatedAt = new DateTime(2024, 11, 12), UpdatedAt = new DateTime(2024, 11, 16) },

            new Chapters { ChapterId = 4, StoryId = 2, Title = "First Jump", Content = "The temporal device hummed to life, reality bending around her like ripples in a pond. Dr. Sarah Chen gripped the controls as the laboratory dissolved into streams of light. When the world solidified again, she stood in the same room—but everything was different.", ChapterNumber = 1, IsDraft = false, IsPublished = true, PublishedAt = new DateTime(2024, 11, 20), CreatedAt = new DateTime(2024, 11, 18), UpdatedAt = new DateTime(2024, 11, 20) },

            new Chapters { ChapterId = 5, StoryId = 2, Title = "Paradox", Content = "The equations didn't lie. Every change she made created divergent timelines, each one branching into infinite possibilities. But some changes—the ones that mattered most—seemed to resist her alterations, as if the universe itself was protecting certain outcomes.", ChapterNumber = 2, IsDraft = false, IsPublished = true, PublishedAt = new DateTime(2024, 11, 21), CreatedAt = new DateTime(2024, 11, 19), UpdatedAt = new DateTime(2024, 11, 21) },

            new Chapters { ChapterId = 6, StoryId = 3, Title = "Fog and Bone", Content = "Inspector Morrison surveyed the crime scene through the thick mist that perpetually shrouded Whitehaven. The victim lay exactly like the others—positioned with unnatural precision, a single white rose clutched in lifeless fingers. This was the seventh body in as many weeks.", ChapterNumber = 1, IsDraft = false, IsPublished = true, PublishedAt = new DateTime(2024, 11, 25), CreatedAt = new DateTime(2024, 11, 22), UpdatedAt = new DateTime(2024, 11, 25) },

            new Chapters { ChapterId = 7, StoryId = 3, Title = "The Rose Garden Mystery", Content = "The only common thread between victims was their connection to the old Ashworth estate. Each had worked there at some point—gardeners, servants, tutors. And each had left under mysterious circumstances years before meeting their untimely end.", ChapterNumber = 2, IsDraft = false, IsPublished = true, PublishedAt = new DateTime(2024, 11, 26), CreatedAt = new DateTime(2024, 11, 23), UpdatedAt = new DateTime(2024, 11, 26) },

            new Chapters { ChapterId = 8, StoryId = 4, Title = "Rival Blooms", Content = "The wedding season had arrived, and Rosewood Avenue's two flower shops stood directly across from each other like dueling opponents. Maya's Petals and Thornwood Gardens had been competing for the same clients for five years, their rivalry as famous as their arrangements.", ChapterNumber = 1, IsDraft = false, IsPublished = true, PublishedAt = new DateTime(2024, 12, 1), CreatedAt = new DateTime(2024, 11, 28), UpdatedAt = new DateTime(2024, 12, 1) },

            new Chapters { ChapterId = 9, StoryId = 5, Title = "Uncharted Sector", Content = "The star maps ended here, at the edge of explored space. Captain Elena Voss checked her instruments one more time before initiating the quantum jump. Her small vessel, the Cartographer's Dream, shuddered as it pierced the veil into unknown territory.", ChapterNumber = 1, IsDraft = false, IsPublished = true, PublishedAt = new DateTime(2024, 12, 5), CreatedAt = new DateTime(2024, 12, 2), UpdatedAt = new DateTime(2024, 12, 5) },

            new Chapters { ChapterId = 10, StoryId = 5, Title = "The Signal", Content = "Three days into uncharted space, the sensors picked up something impossible—a repeating pattern of electromagnetic pulses. Artificial. Deliberate. And according to her dating algorithms, approximately ten thousand years old.", ChapterNumber = 2, IsDraft = false, IsPublished = true, PublishedAt = new DateTime(2024, 12, 6), CreatedAt = new DateTime(2024, 12, 3), UpdatedAt = new DateTime(2024, 12, 6) }
        );

        modelBuilder.Entity<Favorites>().HasData(
            new Favorites { FavoriteId = 1, UserId = 1, StoryId = 1, AddedAt = new DateTime(2024, 11, 16) },
            new Favorites { FavoriteId = 2, UserId = 1, StoryId = 3, AddedAt = new DateTime(2024, 11, 26) },
            new Favorites { FavoriteId = 3, UserId = 2, StoryId = 2, AddedAt = new DateTime(2024, 11, 21) },
            new Favorites { FavoriteId = 4, UserId = 3, StoryId = 1, AddedAt = new DateTime(2024, 11, 17) },
            new Favorites { FavoriteId = 5, UserId = 4, StoryId = 5, AddedAt = new DateTime(2024, 12, 6) }
        );

        modelBuilder.Entity<ReadingProgress>().HasData(
            new ReadingProgress { ProgressId = 1, UserId = 1, StoryId = 1, ChapterId = 2, ScrollPosition = 0.35, LastReadAt = new DateTime(2024, 12, 1) },
            new ReadingProgress { ProgressId = 2, UserId = 1, StoryId = 3, ChapterId = 1, ScrollPosition = 0.80, LastReadAt = new DateTime(2024, 11, 30) },
            new ReadingProgress { ProgressId = 3, UserId = 2, StoryId = 2, ChapterId = 2, ScrollPosition = 0.15, LastReadAt = new DateTime(2024, 12, 2) },
            new ReadingProgress { ProgressId = 4, UserId = 3, StoryId = 1, ChapterId = 3, ScrollPosition = 0.95, LastReadAt = new DateTime(2024, 11, 29) }
        );

        modelBuilder.Entity<CompletedStories>().HasData(
            new CompletedStories { CompletedId = 1, UserId = 1, StoryId = 2, CompletedAt = new DateTime(2024, 11, 28) },
            new CompletedStories { CompletedId = 2, UserId = 3, StoryId = 4, CompletedAt = new DateTime(2024, 12, 2) }
        );

        modelBuilder.Entity<Likes>().HasData(
            new Likes { LikeId = 1, UserId = 1, StoryId = 1, CreatedAt = new DateTime(2024, 11, 16) },
            new Likes { LikeId = 2, UserId = 1, StoryId = 2, CreatedAt = new DateTime(2024, 11, 21) },
            new Likes { LikeId = 3, UserId = 2, StoryId = 1, CreatedAt = new DateTime(2024, 11, 17) },
            new Likes { LikeId = 4, UserId = 3, StoryId = 3, CreatedAt = new DateTime(2024, 11, 26) },
            new Likes { LikeId = 5, UserId = 4, StoryId = 1, CreatedAt = new DateTime(2024, 11, 18) },
            new Likes { LikeId = 6, UserId = 5, StoryId = 2, CreatedAt = new DateTime(2024, 11, 22) }
        );
    }
}