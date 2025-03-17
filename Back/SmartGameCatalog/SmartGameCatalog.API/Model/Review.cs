using System;

namespace SmartGameCatalog.API.Models
{
    /// <summary>
    /// Representa una reseña realizada por un usuario sobre un videojuego.
    /// </summary>
    public class Review
    {
        /// <summary>
        /// Identificador único de la reseña.
        /// </summary>
        public Guid Id_Review { get; set; }
        
        /// <summary>
        /// Identificador del usuario que realizó la reseña.
        /// </summary>
        public Guid Id_User { get; set; }
        
        /// <summary>
        /// Identificador del videojuego reseñado.
        /// </summary>
        public Guid Id_VideoGame { get; set; }
        
        /// <summary>
        /// Comentario proporcionado por el usuario en la reseña.
        /// </summary>
        public string Comment { get; set; } = string.Empty;
        
        /// <summary>
        /// Puntuación dada al videojuego por el usuario (escala flotante).
        /// </summary>
        public float Rating { get; set; }
        
        /// <summary>
        /// Fecha en la que se realizó la reseña.
        /// </summary>
        public DateTime Date { get; set; }
    }
}