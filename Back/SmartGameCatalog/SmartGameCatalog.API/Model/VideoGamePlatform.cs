using System;

namespace SmartGameCatalog.API.Models
{
    /// <summary>
    /// Representa la relación entre un videojuego y una plataforma.
    /// </summary>
    public class VideoGamePlatform
    {
        /// <summary>
        /// Identificador del videojuego.
        /// </summary>
        public Guid Id_VideoGame { get; set; }

        /// <summary>
        /// Nombre de la plataforma en la que está disponible el videojuego.
        /// </summary>
        public string Platform { get; set; } = string.Empty;
    }
}
