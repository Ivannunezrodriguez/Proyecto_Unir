using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGameCatalog.API.Model
{
    /// <summary>
    /// Representa un juego marcado como favorito por un usuario.
    /// </summary>
    public class Favorite
    {
        /// <summary>
        /// Identificador único del favorito.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // 🔹 Auto-incremental
        public int FavoriteId { get; set; }
        
        /// <summary>
        /// Identificador del usuario que marcó el juego como favorito.
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Relación con el usuario (navegación).
        /// </summary>
        public virtual User? User { get; set; }
        
        /// <summary>
        /// Identificador del juego marcado como favorito.
        /// </summary>
        [Required]
        public int GameId { get; set; }

        /// <summary>
        /// Relación con el juego (navegación).
        /// </summary>
        public virtual Game? Game { get; set; }

        /// <summary>
        /// Fecha en la que el usuario agregó el juego a favoritos.
        /// </summary>
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
