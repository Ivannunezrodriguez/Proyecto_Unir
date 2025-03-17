using System;

namespace SmartGameCatalog.API.Models
{
    /// <summary>
    /// Representa un videojuego dentro del catálogo de SmartGameCatalog.
    /// </summary>
    public class VideoGame
    {
        /// <summary>
        /// Identificador único del videojuego.
        /// </summary>
        public Guid Id_VideoGame { get; set; }
        
        /// <summary>
        /// Título del videojuego.
        /// </summary>
        public string Title { get; set; } = string.Empty;
        
        /// <summary>
        /// Descripción breve del videojuego.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// Fecha de lanzamiento del videojuego (opcional).
        /// </summary>
        public DateTime? Release_Date { get; set; }
        
        /// <summary>
        /// URL de la imagen de portada del videojuego.
        /// </summary>
        public string CoverUrl { get; set; } = string.Empty;
        
        /// <summary>
        /// Nombre del desarrollador del videojuego.
        /// </summary>
        public string Developer { get; set; } = string.Empty;
        
        /// <summary>
        /// Plataforma en la que está disponible el videojuego.
        /// </summary>
        public string Platform { get; set; } = string.Empty;
        
        /// <summary>
        /// Puntuación media del videojuego en una escala flotante.
        /// </summary>
        public float Rating { get; set; }
        
        /// <summary>
        /// Identificador de la categoría a la que pertenece el videojuego (opcional).
        /// </summary>
        public Guid? Category_id { get; set; }  
    }
}