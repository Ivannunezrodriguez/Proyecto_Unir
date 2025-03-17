using System;

namespace SmartGameCatalog.API.Models
{
    /// <summary>
    /// Representa un usuario dentro del sistema SmartGameCatalog.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public Guid Id_User { get; set; }
        
        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Dirección de correo electrónico del usuario.
        /// </summary>
        public string Email { get; set; } = string.Empty;
        
        /// <summary>
        /// Contraseña encriptada del usuario.
        /// </summary>
        public string Password { get; set; } = string.Empty;
        
        /// <summary>
        /// Fecha de registro del usuario en la plataforma.
        /// </summary>
        public DateTime Registration_Date { get; set; }
    }
}