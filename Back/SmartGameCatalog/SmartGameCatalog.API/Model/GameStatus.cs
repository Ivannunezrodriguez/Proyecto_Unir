using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // 🔹 Auto-incremental
        public int StatusId { get; set; }

        /// <summary>
        /// Identificador del usuario asociado al estado del juego.
        /// </summary>
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        /// <summary>
        /// Identificador del juego cuyo estado se está registrando.
        /// </summary>
        [Required]
        [ForeignKey("Game")]
        public int GameId { get; set; }

        /// <summary>
        /// Estado del juego: puede ser "Jugado" o "Deseado".
        /// </summary>
        [Required]
        [StringLength(10)]
        public string Status { get; set; } = "Deseado";  // 🔹 Valor por defecto

        /// <summary>
        /// Fecha de la última actualización del estado.
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Relación con el usuario propietario del estado.
        /// </summary>
        public virtual User? User { get; set; }

        /// <summary>
        /// Relación con el juego asociado al estado.
        /// </summary>
        public virtual Game? Game { get; set; }
    }
}
