namespace SmartGameCatalog.API.Model
{
    /// <summary>
    /// Representa un juego marcado como favorito por un usuario.
    /// </summary>
    public class Favorite
    {
        /// <summary>
        /// Identificador único del favorito.
        /// </summary>
        public int FavoriteId { get; set; }

        /// <summary>
        /// Identificador del usuario que marcó el juego como favorito.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Identificador del juego marcado como favorito.
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Fecha en la que el usuario agregó el juego a favoritos.
        /// </summary>
        public DateTime AddedAt { get; set; }
    }
}
