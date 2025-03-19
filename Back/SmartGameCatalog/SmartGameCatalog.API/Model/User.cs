using System;

namespace SmartGameCatalog.API.Model
{
    /// <summary>
    /// Representa un usuario de la plataforma.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Nombre de usuario.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña del usuario (se recomienda almacenar de forma segura encriptada).
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Rol del usuario (ejemplo: "admin" o "user").
        /// </summary>
        public string Role { get; set; } = "user";

        /// <summary>
        /// Fecha de creación de la cuenta del usuario.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
