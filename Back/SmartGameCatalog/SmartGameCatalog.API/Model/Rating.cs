using System;

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
        public int RatingId { get; set; }

        /// <summary>
        /// Identificador del usuario que realizó la calificación.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Identificador del juego calificado.
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Puntuación otorgada al juego (escala de 1 a 10).
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Reseña escrita por el usuario sobre el juego.
        /// </summary>
        public string Review { get; set; } = string.Empty;

        /// <summary>
        /// Fecha en la que se realizó la calificación.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
