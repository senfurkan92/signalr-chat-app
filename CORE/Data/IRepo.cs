using CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Data
{
    public interface IRepo<TEntity> where TEntity : class, IBaseModel
    {
        ResultModel<TEntity> Create(TEntity entity);

        ResultModel<TEntity> Update(TEntity entity);

        ResultModel Delete(int Id);

        ResultModel SoftDelete(int Id);

        ResultModel<TEntity> GetUnique(int Id, bool tracking = false, params string[] tables);

        ResultModel<TEntity> Get(Expression<Func<TEntity, bool>> filter, bool tracking = false, params string[] tables);

        ResultModel<IQueryable<TEntity>> GetList(Expression<Func<TEntity, bool>>? filter = null, Expression<Func<TEntity, object>>? orderBy = null, bool ascending = true, int skip = 0, int take = 30, bool tracking = false, params string[] tables);
    }
}
