using System;

namespace SmartGameCatalog.API.Models
{
    /// <summary>
    /// Representa una compra de un videojuego realizada por un usuario.
    /// </summary>
    public class Purchase
    {
        /// <summary>
        /// Identificador único de la compra.
        /// </summary>
        public Guid Id_Purchase { get; set; }
        
        /// <summary>
        /// Identificador del usuario que realizó la compra.
        /// </summary>
        public Guid Id_User { get; set; }
        
        /// <summary>
        /// Identificador del videojuego comprado.
        /// </summary>
        public Guid Id_VideoGame { get; set; }
        
        /// <summary>
        /// Fecha en la que se realizó la compra.
        /// </summary>
        public DateTime Purchase_Date { get; set; }
        
        /// <summary>
        /// Nombre de la tienda donde se realizó la compra.
        /// </summary>
        public string Store { get; set; } = string.Empty;
        
        /// <summary>
        /// Precio de la compra en la moneda correspondiente.
        /// </summary>
        public float Price { get; set; }
    }
}