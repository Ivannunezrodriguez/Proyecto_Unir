using System;

namespace SmartGameCatalog.API.Models
{
    /// <summary>
    /// Representa un análisis de IA realizado sobre un videojuego para un usuario específico.
    /// </summary>
    public class AIAnalysis
    {
        /// <summary>
        /// Identificador único del análisis de IA.
        /// </summary>
        public Guid Id_Analysis { get; set; }
        
        /// <summary>
        /// Identificador del usuario al que pertenece el análisis.
        /// </summary>
        public Guid Id_User { get; set; }
        
        /// <summary>
        /// Identificador del videojuego analizado.
        /// </summary>
        public Guid Id_VideoGame { get; set; }
        
        /// <summary>
        /// Puntuación generada por la IA para el videojuego (escala flotante de 0 a 10).
        /// </summary>
        public float AI_Score { get; set; }
        
        /// <summary>
        /// Fecha y hora en la que se generó el análisis.
        /// </summary>
        public DateTime Created_At { get; set; }
    }
}