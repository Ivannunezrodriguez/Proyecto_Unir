using System;

namespace SmartGameCatalog.API.Models
{
    public class AIAnalysis
    {
        public Guid Id_Analysis { get; set; }
        public Guid Id_User { get; set; }
        public Guid Id_VideoGame { get; set; }
        public float AI_Score { get; set; }
        public DateTime Generated_At { get; set; }
    }
}
