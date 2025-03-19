using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGameCatalog.API.Model
{
    /// <summary>
    /// Representa una recomendación de juego basada en IA o preferencias del usuario.
    /// </summary>
    public class Recommendation
    {
        /// <summary>
        /// Identificador único de la recomendación.
        /// </summary>
        [Key]
        public int RecommendationId { get; set; }
        
        /// <summary>
        /// Identificador del usuario al que se le recomienda un juego.
        /// </summary>
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        
        /// <summary>
        /// Identificador del juego recomendado.
        /// </summary>
        [Required]
        [ForeignKey("Game")]
        public int GameId { get; set; }
        
        /// <summary>
        /// Razón de la recomendación (por qué se recomienda este juego).
        /// </summary>
        [Required]
        [StringLength(255, ErrorMessage = "La razón de la recomendación no puede superar los 255 caracteres.")]
        public string Reason { get; set; } = string.Empty;
        
        /// <summary>
        /// Fecha en la que se generó la recomendación.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Relación con el usuario que recibe la recomendación.
        /// </summary>
        public virtual User? User { get; set; }

        /// <summary>
        /// Relación con el juego recomendado.
        /// </summary>
        public virtual Game? Game { get; set; }
    }
}
