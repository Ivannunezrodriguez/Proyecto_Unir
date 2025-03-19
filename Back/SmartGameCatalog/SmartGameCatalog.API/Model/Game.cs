using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGameCatalog.API.Model
{
    /// <summary>
    /// Representa un videojuego basado en la API de IGDB.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Identificador único del juego en la base de datos.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // 🔹 Auto-incremental
        public int GameId { get; set; }
        
        /// <summary>
        /// Identificador del juego en la API de IGDB.
        /// </summary>
        [Required]
        public int IgdbId { get; set; }

        /// <summary>
        /// Lista de calificaciones asociadas a este juego.
        /// </summary>
        public virtual List<Rating>? Ratings { get; set; }

        /// <summary>
        /// Lista de favoritos asociados a este juego.
        /// </summary>
        public virtual List<Favorite>? Favorites { get; set; }

        /// <summary>
        /// Lista de recomendaciones asociadas a este juego.
        /// </summary>
        public virtual List<Recommendation>? Recommendations { get; set; }

        /// <summary>
        /// Estados de los juegos para usuarios (Jugado/Deseado).
        /// </summary>
        public virtual List<GameStatus>? GameStatuses { get; set; }
    }
}
