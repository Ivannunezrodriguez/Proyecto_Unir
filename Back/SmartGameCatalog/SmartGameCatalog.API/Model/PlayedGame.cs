using System;

namespace SmartGameCatalog.API.Models
{
    public class PlayedGame
    {
        public Guid Id_Played_Game { get; set; }
        public Guid Id_User { get; set; }
        public Guid Id_VideoGame { get; set; }
        public DateTime Played_At { get; set; }
    }
}
