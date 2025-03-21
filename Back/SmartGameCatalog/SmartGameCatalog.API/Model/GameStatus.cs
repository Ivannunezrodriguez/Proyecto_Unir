using System;

namespace SmartGameCatalog.API.Model
{
    /// <summary>
    /// Representa el estado de un juego para un usuario (Jugado/Deseado).
    /// </summary>
    public class GameStatus
    {
        /// <summary>
        /// Identificador único del estado del juego.
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// Identificador del usuario asociado al estado del juego.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Identificador del juego cuyo estado se está registrando.
        /// </summary>
        public int GameId { get; set; }

      /// <summary>
/// Estado del juego: puede ser "Wishlist" (deseo comprarlo), "Owned" (lo tengo), 
/// "Playing" (lo estoy jugando), "Completed" (lo terminé o ya no lo jugaré más), 
/// "Abandoned" (decidí no seguir jugándolo).
/// </summary>
public string Status { get; set; } = string.Empty;


        /// <summary>
        /// Fecha de la última actualización del estado.
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
