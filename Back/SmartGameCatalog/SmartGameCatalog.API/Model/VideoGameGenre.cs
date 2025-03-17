using System;

namespace SmartGameCatalog.API.Models
{
    /// <summary>
    /// Representa la relación entre un videojuego y un género.
    /// </summary>
    public class VideoGameGenre
    {
        /// <summary>
        /// Identificador del videojuego.
        /// </summary>
        public Guid Id_VideoGame { get; set; }

        /// <summary>
        /// Nombre del género asociado al videojuego.
        /// </summary>
        public string Genre { get; set; } = string.Empty;
    }
}
