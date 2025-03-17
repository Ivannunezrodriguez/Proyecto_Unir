using System;

namespace SmartGameCatalog.API.Models
{
    /// <summary>
    /// Representa una categoría de videojuegos dentro del catálogo.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Identificador único de la categoría.
        /// </summary>
        public Guid Id_Category { get; set; }
        
        /// <summary>
        /// Nombre de la categoría.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}