namespace SmartGameCatalog.API.Model
{
    /// <summary>
    /// Representa un videojuego basado en la API de IGDB.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Identificador único del juego en la base de datos.
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Identificador del juego en la API de IGDB.
        /// </summary>
        public int IgdbId { get; set; }
    }
}
