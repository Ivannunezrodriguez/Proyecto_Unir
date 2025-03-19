using System;

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
        public int RecommendationId { get; set; }

        /// <summary>
        /// Identificador del usuario al que se le recomienda un juego.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Identificador del juego recomendado.
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Razón de la recomendación.
        /// </summary>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// Fecha en la que se generó la recomendación.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
