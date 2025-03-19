using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGameCatalog.API.Model
{
    /// <summary>
    /// Representa una calificación otorgada por un usuario a un juego.
    /// </summary>
    public class Rating
    {
        /// <summary>
        /// Identificador único de la calificación.
        /// </summary>
        [Key]
        public int RatingId { get; set; }
        
        /// <summary>
        /// Identificador del usuario que realizó la calificación.
        /// </summary>
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        
        /// <summary>
        /// Identificador del juego calificado.
        /// </summary>
        [Required]
        [ForeignKey("Game")]
        public int GameId { get; set; }
        
        /// <summary>
        /// Puntuación otorgada al juego (escala de 1 a 10).
        /// </summary>
        [Required]
        [Range(1, 10, ErrorMessage = "La puntuación debe estar entre 1 y 10.")]
        public int Score { get; set; }
        
        /// <summary>
        /// Reseña escrita por el usuario sobre el juego.
        /// </summary>
        [Required]
        [StringLength(500, ErrorMessage = "La reseña no puede superar los 500 caracteres.")]
        public string Review { get; set; } = string.Empty;
        
        /// <summary>
        /// Fecha en la que se realizó la calificación.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Relación con el usuario que realizó la calificación.
        /// </summary>
        public virtual User? User { get; set; }

        /// <summary>
        /// Relación con el juego calificado.
        /// </summary>
        public virtual Game? Game { get; set; }
    }
}
