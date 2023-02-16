using DOMAIN.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public class MessageMap : BaseMap<Message>
    {
        public override void Configure(EntityTypeBuilder<Message> builder)
        {
            base.Configure(builder);
            builder.ToTable("Messages");
            builder.Property(x => x.Text).HasColumnType("nvarchar(2000)");
            builder.HasOne(x => x.AppUser).WithMany(x => x.Messages).HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
