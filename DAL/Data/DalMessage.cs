using CORE.Data;
using DAL.Context;
using DOMAIN.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public interface IDalMessage : IRepo<Message>
    {
    }

    public class DalMessage : Repo<Message, AppDbContext>, IDalMessage
    {
        public DalMessage(AppDbContext ctx) : base(ctx)
        {

        }
    }
}
