using System;

namespace SmartGameCatalog.API.Models
{
    public class VideoGame
    {
        public Guid Id_VideoGame { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? Release_Date { get; set; }
        public string Developer { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public float Rating { get; set; }
        public Guid? Id_Category { get; set; }
    }
}
