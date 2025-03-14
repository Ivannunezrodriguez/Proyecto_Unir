using System;

namespace SmartGameCatalog.API.Models
{
    public class User
    {
        public Guid Id_User { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime Registration_Date { get; set; }
    }
}
