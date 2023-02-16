using CORE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public class BaseMap<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IBaseModel
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.CreateDate).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.UpdateDate).IsRequired().HasDefaultValue(DateTime.Now);
        }
    }
}
