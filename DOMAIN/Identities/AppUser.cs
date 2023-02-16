using DOMAIN.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Identities
{
    public class AppUser : IdentityUser<int>
    {
        public virtual ICollection<Message>? Messages { get; set; }

        public string? PhotoPath { get; set; }
    }
}
