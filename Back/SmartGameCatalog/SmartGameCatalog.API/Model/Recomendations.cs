using System;

namespace SmartGameCatalog.API.Models
{
    /// <summary>
    /// Representa una recomendación de videojuego generada para un usuario.
    /// </summary>
    public class Recommendation
    {
        /// <summary>
        /// Identificador único de la recomendación.
        /// </summary>
        public Guid Id_Recommendation { get; set; }
        
        /// <summary>
        /// Identificador del usuario al que se le recomienda el videojuego.
        /// </summary>
        public Guid Id_User { get; set; }
        
        /// <summary>
        /// Identificador del videojuego recomendado.
        /// </summary>
        public Guid Id_Recommended_VideoGame { get; set; }
        
        /// <summary>
        /// Razón o criterio por el cual se realizó la recomendación.
        /// </summary>
        public string Reason { get; set; } = string.Empty;
        
        /// <summary>
        /// Fecha en la que se generó la recomendación.
        /// </summary>
        public DateTime Created_At { get; set; }
    }
}