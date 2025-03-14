using System;

namespace SmartGameCatalog.API.Models
{
    public class Review
    {
        public Guid Id_Review { get; set; }
        public Guid Id_User { get; set; }
        public Guid Id_VideoGame { get; set; }
        public string Comment { get; set; } = string.Empty;
        public float Rating { get; set; }
        public DateTime Date { get; set; }
    }
}
