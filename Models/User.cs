using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace newWebAPI.Models
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool Active { get; set; } = true;
        public string Name { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; } 

    }
}