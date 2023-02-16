using CORE.Models;
using DOMAIN.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Entites
{
    public class Message : BaseModel, IBaseModel
    {
        public string? Text { get; set; }

        // relations
        public AppUser? AppUser { get; set; }

        public int AppUserId { get; set; }
    }
}
