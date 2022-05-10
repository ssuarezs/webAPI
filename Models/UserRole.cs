using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace newWebAPI.Models
{
    public class UserRole
    {
        public Guid UserRoleId { get; set; } = Guid.NewGuid();
        [JsonIgnore]
        public bool Active { get; set; } = true;
        public string Role { get; set; }
        public string Description { get; set; }
        public virtual User User { get; set; }
        public Guid UserId { get; set; }
    }
}