using System;

namespace SmartGameCatalog.API.Models
{
    public class Recommendation
    {
        public Guid Id_Recommendation { get; set; }
        public Guid Id_User { get; set; }
        public Guid Id_VideoGame { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
