using System;

namespace SmartGameCatalog.API.Models
{
    /// <summary>
    /// Representa un videojuego que ha sido jugado por un usuario.
    /// </summary>
    public class PlayedGame
    {
        /// <summary>
        /// Identificador único del registro del juego jugado.
        /// </summary>
        public Guid Id_Played_Game { get; set; }
        
        /// <summary>
        /// Identificador del usuario que jugó el videojuego.
        /// </summary>
        public Guid Id_User { get; set; }
        
        /// <summary>
        /// Identificador del videojuego que fue jugado.
        /// </summary>
        public Guid Id_VideoGame { get; set; }
        
        /// <summary>
        /// Fecha y hora en la que el usuario jugó el videojuego.
        /// </summary>
        public DateTime Played_At { get; set; }
    }
}