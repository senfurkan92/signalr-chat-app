using CORE.Data;
using CORE.Models;
using System.Linq.Expressions;

namespace BLL.Services
{
	public interface IServiceBase<TEntity>
		where TEntity : class, IBaseModel
	{
		ResultModel<TEntity> Create(TEntity entity);

		ResultModel<TEntity> Update(TEntity entity);

		ResultModel Delete(int Id);

		ResultModel SoftDelete(int Id);

		ResultModel<TEntity> GetUnique(int Id, bool tracking = false, params string[] tables);

		ResultModel<TEntity> Get(Expression<Func<TEntity, bool>> filter, bool tracking = false, params string[] tables);

		ResultModel<IQueryable<TEntity>> GetList(Expression<Func<TEntity, bool>>? filter = null, Expression<Func<TEntity, object>>? orderBy = null, bool ascending = true, int skip = 0, int take = 30, bool tracking = false, params string[] tables);
	}

	public class ManagerBase<TEntity, TIDAL> : IServiceBase<TEntity>
		where TEntity : class, IBaseModel
		where TIDAL : IRepo<TEntity>
	{
		protected readonly TIDAL _dal;

		public ManagerBase(TIDAL dal)
		{
			_dal = dal;
		}

		public ResultModel<TEntity> Create(TEntity entity)
		{
			return _dal.Create(entity);
		}

		public ResultModel Delete(int Id)
		{
			return _dal.Delete(Id);
		}

		public ResultModel<TEntity> Get(Expression<Func<TEntity, bool>> filter, bool tracking = false, params string[] tables)
		{
			return _dal.Get(filter, tracking, tables);
		}

		public ResultModel<IQueryable<TEntity>> GetList(Expression<Func<TEntity, bool>>? filter = null, Expression<Func<TEntity, object>>? orderBy = null, bool ascending = true, int skip = 0, int take = 30, bool tracking = false, params string[] tables)
		{
			return _dal.GetList(filter, orderBy, ascending, skip, take, tracking, tables);
		}

		public ResultModel<TEntity> GetUnique(int Id, bool tracking = false, params string[] tables)
		{
			return _dal.GetUnique(Id, tracking, tables);
		}

		public ResultModel SoftDelete(int Id)
		{
			return _dal.SoftDelete(Id);
		}

		public ResultModel<TEntity> Update(TEntity entity)
		{
			return _dal.Update(entity);
		}
	}
}
