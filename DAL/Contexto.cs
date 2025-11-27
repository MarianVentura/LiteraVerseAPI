using LiteraVerseApi.Models;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuarios>().HasData(
     new Usuarios
     {
         UsuarioId = 1,
         UserName = "LiteraVerse_Official",
         Password = "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=" // literaverse2025
     },
     new Usuarios
     {
         UsuarioId = 2,
         UserName = "MidnightInk",
         Password = "YlF8jKPfIAL+L2dmxe9OhwRK9yUoNQcwJw9aKqLQe3M=" // midnight2025
     },
     new Usuarios
     {
         UsuarioId = 3,
         UserName = "PhoenixQuill",
         Password = "bQTNz5rIfLKQVEBkr+/8CsVJfQVcXJIbLxYBQnPFqKI=" // phoenix2025
     },
     new Usuarios
     {
         UsuarioId = 4,
         UserName = "ShadowScribe",
         Password = "Xkn5dNH5VKLzLqKEJ1vJ8q/wqfKIIHlJ5ksH8xQm8pM=" // shadow2025
     },
     new Usuarios
     {
         UsuarioId = 5,
         UserName = "StardustWords",
         Password = "YNQwZLHvLZK+5qKEJ1vJ8q/wqfKIIHlJ5ksH8xQm8pM=" // stardust2025
     }
);

        modelBuilder.Entity<Genres>().HasData(
            new Genres { GenreId = 1, Name = "Fantasía", Description = "Mundos mágicos y aventuras épicas", CreatedAt = new DateTime(2025, 1, 1) },
            new Genres { GenreId = 2, Name = "Romance", Description = "Historias de amor y relaciones", CreatedAt = new DateTime(2025, 1, 1) },
            new Genres { GenreId = 3, Name = "Ciencia Ficción", Description = "Futuros distópicos y tecnología", CreatedAt = new DateTime(2025, 1, 1) },
            new Genres { GenreId = 4, Name = "Misterio", Description = "Enigmas y suspense", CreatedAt = new DateTime(2025, 1, 1) },
            new Genres { GenreId = 5, Name = "Thriller", Description = "Acción y adrenalina", CreatedAt = new DateTime(2025, 1, 1) },
            new Genres { GenreId = 6, Name = "Drama", Description = "Historias emotivas y profundas", CreatedAt = new DateTime(2025, 1, 1) },
            new Genres { GenreId = 7, Name = "Paranormal", Description = "Criaturas sobrenaturales", CreatedAt = new DateTime(2025, 1, 1) },
            new Genres { GenreId = 8, Name = "Slice of Life", Description = "Historias cotidianas", CreatedAt = new DateTime(2025, 1, 1) }
        );

        modelBuilder.Entity<Stories>().HasData(
     new Stories
     {
         StoryId = 1,
         UserId = 2, // MidnightInk
         Title = "The Last Ember",
         Synopsis = "En un mundo donde la magia se extingue, una joven hechicera debe encontrar la última llama ancestral antes de que caiga en manos equivocadas. Una épica aventura llena de magia, traición y redención.",
         CoverImageUrl = "https://images.unsplash.com/photo-1518709268805-4e9042af9f23",
         Genre = "Fantasía",
         Tags = "magia,aventura,medieval",
         IsDraft = false,
         IsPublished = true,
         ViewCount = 15420,
         PublishedAt = new DateTime(2024, 10, 10),
         CreatedAt = new DateTime(2024, 10, 5),
         UpdatedAt = new DateTime(2024, 10, 10)
     },
     new Stories
     {
         StoryId = 2,
         UserId = 4, // ShadowScribe
         Title = "Digital Fate",
         Synopsis = "En el año 2157, la humanidad vive en una realidad virtual permanente. Alex, un hacker rebelde, descubre que todo es una simulación y debe elegir entre la verdad dolorosa o la mentira confortable.",
         CoverImageUrl = "https://images.unsplash.com/photo-1614732414444-096e5f1122d5",
         Genre = "Ciencia Ficción",
         Tags = "cyberpunk,distopía,IA",
         IsDraft = false,
         IsPublished = true,
         ViewCount = 12890,
         PublishedAt = new DateTime(2024, 10, 25),
         CreatedAt = new DateTime(2024, 10, 20),
         UpdatedAt = new DateTime(2024, 10, 25)
     },
     new Stories
     {
         StoryId = 3,
         UserId = 5, // StardustWords
         Title = "Midnight Whispers",
         Synopsis = "Emma, una artista solitaria, comienza a recibir mensajes anónimos llenos de poesía. Cuando descubre la identidad del remitente, su vida cambiará para siempre. Una historia de amor inesperado.",
         CoverImageUrl = "https://images.unsplash.com/photo-1516979187457-637abb4f9353",
         Genre = "Romance",
         Tags = "amor,misterio,contemporáneo",
         IsDraft = false,
         IsPublished = true,
         ViewCount = 18750,
         PublishedAt = new DateTime(2024, 11, 5),
         CreatedAt = new DateTime(2024, 10, 30),
         UpdatedAt = new DateTime(2024, 11, 5)
     },
     new Stories
     {
         StoryId = 4,
         UserId = 3, // PhoenixQuill
         Title = "Echoes of the Void",
         Synopsis = "Una detective es llamada para resolver una serie de asesinatos conectados por un símbolo antiguo. La verdad la llevará a descubrir secretos que la humanidad olvidó hace milenios.",
         CoverImageUrl = "https://images.unsplash.com/photo-1505682634904-d7c8d95cdc50",
         Genre = "Misterio",
         Tags = "detective,crimen,sobrenatural",
         IsDraft = false,
         IsPublished = true,
         ViewCount = 9340,
         PublishedAt = new DateTime(2024, 11, 10),
         CreatedAt = new DateTime(2024, 11, 7),
         UpdatedAt = new DateTime(2024, 11, 10)
     },
     new Stories
     {
         StoryId = 5,
         UserId = 4, // ShadowScribe
         Title = "Neon Souls",
         Synopsis = "En las calles iluminadas por neones de Nueva Tokio, un asesino a sueldo recibe un contrato que lo obligará a enfrentar su pasado. Acción frenética y dilemas morales en cada página.",
         CoverImageUrl = "https://images.unsplash.com/photo-1550745165-9bc0b252726f",
         Genre = "Thriller",
         Tags = "acción,noir,urbano",
         IsDraft = false,
         IsPublished = true,
         ViewCount = 11200,
         PublishedAt = new DateTime(2024, 11, 15),
         CreatedAt = new DateTime(2024, 11, 13),
         UpdatedAt = new DateTime(2024, 11, 15)
     },
     new Stories
     {
         StoryId = 6,
         UserId = 2, // MidnightInk
         Title = "Beneath the Silver Moon",
         Synopsis = "Luna siempre supo que era diferente, pero cuando descubre que es una loba alfa en un mundo donde su especie se creía extinta, deberá liderar una revolución o ver a los suyos desaparecer para siempre.",
         CoverImageUrl = "https://images.unsplash.com/photo-1509023464722-18d996393ca8",
         Genre = "Paranormal",
         Tags = "hombres lobo,fantasía,romance",
         IsDraft = false,
         IsPublished = true,
         ViewCount = 16500,
         PublishedAt = new DateTime(2024, 11, 17),
         CreatedAt = new DateTime(2024, 11, 15),
         UpdatedAt = new DateTime(2024, 11, 17)
     },
     new Stories
     {
         StoryId = 7,
         UserId = 5, // StardustWords
         Title = "Coffee and Conversations",
         Synopsis = "Sarah trabaja en una cafetería donde cada cliente tiene una historia que contar. A través de conversaciones cotidianas, descubre el verdadero significado de la conexión humana.",
         CoverImageUrl = "https://images.unsplash.com/photo-1495474472287-4d71bcdd2085",
         Genre = "Slice of Life",
         Tags = "cotidiano,inspirador,realista",
         IsDraft = false,
         IsPublished = true,
         ViewCount = 7890,
         PublishedAt = new DateTime(2024, 11, 20),
         CreatedAt = new DateTime(2024, 11, 18),
         UpdatedAt = new DateTime(2024, 11, 20)
     },
     new Stories
     {
         StoryId = 8,
         UserId = 3, // PhoenixQuill
         Title = "Shattered Kingdoms",
         Synopsis = "Tres reinos en guerra, una profecía olvidada y un guerrero sin memoria que podría ser la clave para la paz... o la destrucción total. Fantasía épica en su máxima expresión.",
         CoverImageUrl = "https://images.unsplash.com/photo-1519558260268-cde7e03a0152",
         Genre = "Fantasía",
         Tags = "épico,guerra,magia",
         IsDraft = false,
         IsPublished = true,
         ViewCount = 14300,
         PublishedAt = new DateTime(2024, 11, 22),
         CreatedAt = new DateTime(2024, 11, 20),
         UpdatedAt = new DateTime(2024, 11, 22)
     },
     new Stories
     {
         StoryId = 9,
         UserId = 1, // LiteraVerse_Official
         Title = "The Silent Witness",
         Synopsis = "Una niña muda es la única testigo de un crimen brutal. Cuando el asesino descubre su existencia, comienza una carrera contra el tiempo para protegerla antes de que sea demasiado tarde.",
         CoverImageUrl = "https://images.unsplash.com/photo-1481026469463-66327c86e544",
         Genre = "Thriller",
         Tags = "suspense,crimen,psicológico",
         IsDraft = false,
         IsPublished = true,
         ViewCount = 10560,
         PublishedAt = new DateTime(2024, 11, 23),
         CreatedAt = new DateTime(2024, 11, 22),
         UpdatedAt = new DateTime(2024, 11, 23)
     },
     new Stories
     {
         StoryId = 10,
         UserId = 1, // LiteraVerse_Official
         Title = "Hearts Across Time",
         Synopsis = "Una carta antigua lleva a Clara a descubrir una historia de amor del siglo XIX. Mientras investiga, encuentra paralelismos inquietantes con su propia vida. ¿El amor puede trascender el tiempo?",
         CoverImageUrl = "https://images.unsplash.com/photo-1518133910853-2e5f8bb15a98",
         Genre = "Romance",
         Tags = "viajes en el tiempo,histórico,drama",
         IsDraft = false,
         IsPublished = true,
         ViewCount = 13200,
         PublishedAt = new DateTime(2024, 11, 24),
         CreatedAt = new DateTime(2024, 11, 23),
         UpdatedAt = new DateTime(2024, 11, 24)
     }
 );

        modelBuilder.Entity<Chapters>().HasData(
            new Chapters { ChapterId = 1, StoryId = 1, Title = "El Despertar", Content = "La torre de Ébano se alzaba contra el cielo carmesí, última fortaleza de una era olvidada. Lyra despertó sintiendo el calor de la última llama mágica palpitando en su pecho...", ChapterNumber = 1, IsDraft = false, IsPublished = true, PublishedAt = new DateTime(2024, 10, 10), CreatedAt = new DateTime(2024, 10, 10), UpdatedAt = new DateTime(2024, 10, 10) },
            new Chapters { ChapterId = 2, StoryId = 1, Title = "Sombras del Pasado", Content = "El consejo de magos la miraba con desconfianza. Nadie había despertado el don en más de cincuenta años. ¿Era ella realmente la elegida o solo una ilusión desesperada?", ChapterNumber = 2, IsDraft = false, IsPublished = true, PublishedAt = new DateTime(2024, 10, 11), CreatedAt = new DateTime(2024, 10, 11), UpdatedAt = new DateTime(2024, 10, 11) },
            new Chapters { ChapterId = 3, StoryId = 1, Title = "El Viaje Comienza", Content = "Con solo una mochila y un mapa antiguo, Lyra abandonó la seguridad de la torre. El mundo exterior era más peligroso y hermoso de lo que jamás imaginó.", ChapterNumber = 3, IsDraft = false, IsPublished = true, PublishedAt = new DateTime(2024, 10, 12), CreatedAt = new DateTime(2024, 10, 12), UpdatedAt = new DateTime(2024, 10, 12) },

            new Chapters { ChapterId = 4, StoryId = 2, Title = "Desconexión", Content = "El código en la pantalla no tenía sentido. Alex llevaba años hackeando sistemas, pero esto era diferente. Esto era... imposible. El mundo digital comenzaba a desmoronarse.", ChapterNumber = 1, IsDraft = false, IsPublished = true, PublishedAt = new DateTime(2024, 10, 25), CreatedAt = new DateTime(2024, 10, 25), UpdatedAt = new DateTime(2024, 10, 25) },
            new Chapters { ChapterId = 5, StoryId = 2, Title = "La Verdad Duele", Content = "Salir de la Matrix no era una metáfora. Era literal. Y la realidad física era un páramo desolado donde la humanidad apenas sobrevivía en bunkers subterráneos.", ChapterNumber = 2, IsDraft = false, IsPublished = true, PublishedAt = new DateTime(2024, 10, 26), CreatedAt = new DateTime(2024, 10, 26), UpdatedAt = new DateTime(2024, 10, 26) },

            new Chapters { ChapterId = 6, StoryId = 3, Title = "El Primer Mensaje", Content = "La nota apareció debajo de su puerta un martes por la mañana. Papel antiguo, tinta negra, caligrafía perfecta. Las palabras hablaban de su alma de una manera que nadie más había logrado.", ChapterNumber = 1, IsDraft = false, IsPublished = true, PublishedAt = new DateTime(2024, 11, 5), CreatedAt = new DateTime(2024, 11, 5), UpdatedAt = new DateTime(2024, 11, 5) },
            new Chapters { ChapterId = 7, StoryId = 3, Title = "Buscando al Poeta", Content = "Emma se obsesionó con encontrar al autor. Cada pista la acercaba más a descubrir una verdad que cambiaría todo lo que creía saber sobre el amor.", ChapterNumber = 2, IsDraft = false, IsPublished = true, PublishedAt = new DateTime(2024, 11, 6), CreatedAt = new DateTime(2024, 11, 6), UpdatedAt = new DateTime(2024, 11, 6) },
            new Chapters { ChapterId = 8, StoryId = 3, Title = "El Encuentro", Content = "Cuando finalmente lo vio, entendió por qué había permanecido en las sombras. No era quien esperaba. Era alguien que había estado frente a ella todo el tiempo.", ChapterNumber = 3, IsDraft = false, IsPublished = true, PublishedAt = new DateTime(2024, 11, 7), CreatedAt = new DateTime(2024, 11, 7), UpdatedAt = new DateTime(2024, 11, 7) }
        );
    }
}