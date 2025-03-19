using System;
using System.ComponentModel.DataAnnotations;

namespace SmartGameCatalog.API.Model
{
    /// <summary>
    /// Representa un usuario registrado en la plataforma.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        [Key]
        public int UserId { get; set; }
        
        /// <summary>
        /// Nombre de usuario (único y requerido).
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "El nombre de usuario no puede superar los 100 caracteres.")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico del usuario (debe tener formato válido).
        /// </summary>
        [Required]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [StringLength(100, ErrorMessage = "El correo electrónico no puede superar los 100 caracteres.")]
        public string Email { get; set; } = string.Empty;
        
        /// <summary>
        /// Contraseña del usuario (se recomienda encriptarla antes de guardarla).
        /// </summary>
        [Required]
        [StringLength(255, ErrorMessage = "La contraseña no puede superar los 255 caracteres.")]
        public string Password { get; set; } = string.Empty;
        
        /// <summary>
        /// Rol del usuario (por defecto es "user").
        /// </summary>
        [Required]
        [StringLength(20, ErrorMessage = "El rol no puede superar los 20 caracteres.")]
        public string Role { get; set; } = "user";
        
        /// <summary>
        /// Fecha de creación de la cuenta del usuario.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
