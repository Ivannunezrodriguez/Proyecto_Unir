using Microsoft.EntityFrameworkCore;
using SmartGameCatalog.API.Model;

namespace SmartGameCatalog.API.Data
{
    /// <summary>
    /// DbContext para manejar la base de datos usando Entity Framework Core.
    /// </summary>
    public class SmartGameDbContext : DbContext
    {
        /// <summary>
        /// Constructor que recibe las opciones de configuración de la base de datos.
        /// </summary>
        public SmartGameDbContext(DbContextOptions<SmartGameDbContext> options) : base(options) { }

        /// <summary>
        /// Tabla de usuarios registrados en la plataforma.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Tabla de videojuegos basada en IGDB.
        /// </summary>
        public DbSet<Game> Games { get; set; }

        /// <summary>
        /// Tabla de calificaciones de juegos por parte de los usuarios.
        /// </summary>
        public DbSet<Rating> Ratings { get; set; }

        /// <summary>
        /// Tabla de juegos marcados como favoritos por los usuarios.
        /// </summary>
        public DbSet<Favorite> Favorites { get; set; }

        /// <summary>
        /// Tabla de recomendaciones de juegos generadas por IA.
        /// </summary>
        public DbSet<Recommendation> Recommendations { get; set; }

        /// <summary>
        /// Tabla de estado del juego (Jugado/Deseado).
        /// </summary>
        public DbSet<GameStatus> GameStatuses { get; set; }

        /// <summary>
        /// Configura relaciones y restricciones de la base de datos.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Definir clave primaria para GameStatus
            modelBuilder.Entity<GameStatus>().HasKey(gs => gs.StatusId);

            // 🔹 Definir claves foráneas
            modelBuilder.Entity<Rating>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rating>()
                .HasOne<Game>()
                .WithMany()
                .HasForeignKey(r => r.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorite>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorite>()
                .HasOne<Game>()
                .WithMany()
                .HasForeignKey(f => f.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Recommendation>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Recommendation>()
                .HasOne<Game>()
                .WithMany()
                .HasForeignKey(r => r.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameStatus>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(gs => gs.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameStatus>()
                .HasOne<Game>()
                .WithMany()
                .HasForeignKey(gs => gs.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 Agregar índices para optimizar consultas
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Game>()
                .HasIndex(g => g.IgdbId)
                .IsUnique();
        }
    }
}
