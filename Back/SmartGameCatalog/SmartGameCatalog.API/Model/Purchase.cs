using System;

namespace SmartGameCatalog.API.Models
{
    public class Purchase
    {
        public Guid Id_Purchase { get; set; }
        public Guid Id_User { get; set; }
        public Guid Id_VideoGame { get; set; }
        public string Store { get; set; } = string.Empty;
        public float Price { get; set; }
        public string Link { get; set; } = string.Empty;
    }
}
