using CORE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Data
{
    public class Repo<TEntity, TContext> : IRepo<TEntity>
        where TEntity : class, IBaseModel
        where TContext : DbContext
    {

        protected readonly TContext _ctx;

        public Repo(TContext ctx)
        {
            _ctx= ctx;
        }

        public ResultModel<TEntity> Create(TEntity entity)
        {
            try
            {
                entity.CreateDate = DateTime.Now;
                entity.IsDeleted = false;
            
                var result = _ctx.Set<TEntity>().Add(entity);
                if (result.State == EntityState.Added && _ctx.SaveChanges() > 0)
                {
                    return new ResultModel<TEntity>
                    {
                        Data= result.Entity,
                        IsSuccess= true,
                    };
                }
                else
                {
                    return new ResultModel<TEntity>
                    {
                        IsSuccess= false,
                        Exception= new Exception("Unexpected error"),
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultModel<TEntity>
                {
                    IsSuccess = false,
                    Exception = ex,
                };
            }
        }

        public ResultModel<TEntity> Update(TEntity entity)
        {
            try
            {
                entity.UpdateDate = DateTime.Now;

                var result = _ctx.Set<TEntity>().Update(entity);
                if (result.State == EntityState.Modified && _ctx.SaveChanges() > 0)
                {
                    return new ResultModel<TEntity>
                    {
                        Data = result.Entity,
                        IsSuccess = true,
                    };
                }
                else
                {
                    return new ResultModel<TEntity>
                    {
                        IsSuccess = false,
                        Exception = new Exception("Unexpected error"),
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultModel<TEntity>
                {
                    IsSuccess = false,
                    Exception = ex,
                };
            }
        }

        public ResultModel Delete(int Id)
        {
            try
            {
                var query = GetUnique(Id);
                if (query.IsSuccess)
                {
                    if (query.Data is not null)
                    { 
                        var result = _ctx.Set<TEntity>().Remove(query.Data);
                        if (result.State == EntityState.Deleted && _ctx.SaveChanges() > 0)
                        {
                            return new ResultModel
                            {
                                IsSuccess= true
                            };
                        }
                        return new ResultModel
                        {
                            IsSuccess = false,
                            Exception = new Exception("Unexpected error"),
                        };
                    }
                    return new ResultModel
                    {
                        IsSuccess = false,
                        Exception = new Exception("Entity not found"),
                    };
                }
                return query;
            }
            catch (Exception ex)
            {
                return new ResultModel { 
                    IsSuccess = false, 
                    Exception = ex 
                };
            }
        }

        public ResultModel SoftDelete(int Id)
        {
            try
            {
                var query = GetUnique(Id);
                if (query.IsSuccess)
                {
                    if (query.Data is not null)
                    {
                        query.Data.IsDeleted= true;
                        return Update(query.Data);
                    }
                    return new ResultModel
                    {
                        IsSuccess = false,
                        Exception = new Exception("Entity not found"),
                    };
                }
                return query;
            }
            catch (Exception ex)
            {
                return new ResultModel
                {
                    IsSuccess = false,
                    Exception = ex
                };
            }
        }

        public ResultModel<TEntity> GetUnique(int Id, bool tracking = false, params string[] tables)
        {
            try
            {
                var entity = GetExpandedDb(tracking, tables).FirstOrDefault(x => x.Id == Id);

                return new ResultModel<TEntity>
                {
                    Data = entity,
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<TEntity>
                {
                    IsSuccess = false,
                    Exception = ex,
                };
            }
        }

        public ResultModel<TEntity> Get(Expression<Func<TEntity, bool>> filter, bool tracking = false, params string[] tables)
        {
            try
            {
                var entity = GetExpandedDb(tracking, tables).FirstOrDefault(filter);

                return new ResultModel<TEntity> { 
                    Data = entity,
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<TEntity>
                {
                    IsSuccess = false,
                    Exception = ex,
                };
            }
        }

        public ResultModel<IQueryable<TEntity>> GetList(Expression<Func<TEntity, bool>>? filter = null, Expression<Func<TEntity, object>>? orderBy = null, bool ascending = true, int skip = 0, int take = 30, bool tracking = false, params string[] tables)
        {
            try
            {
                var query = ascending 
                    ? GetExpandedDb(tracking, tables).Where(filter ?? (x => true)).OrderBy(orderBy ?? (x => x.Id)).Skip(skip) 
                    : GetExpandedDb(tracking, tables).Where(filter ?? (x => true)).OrderByDescending(orderBy ?? (x => x.Id)).Skip(skip);

                if (take > 0)
                { 
                    query = query.Take(take);
                }

                return new ResultModel<IQueryable<TEntity>>
                {
                    Data= query,
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<IQueryable<TEntity>>
                {
                    Exception= ex,
                    IsSuccess= false,
                };
            } 
        }

        private IQueryable<TEntity> GetExpandedDb(bool tracking, params string[] tables)
        {
            if (tables.Length > 0)
            {
                var expanded = _ctx.Set<TEntity>() as IQueryable<TEntity>;
                
                foreach (var table in tables)
                {
                    expanded = expanded.Include(table);
                }

                return tracking ? expanded : expanded.AsNoTracking();
            }
            return tracking ? _ctx.Set<TEntity>() as IQueryable<TEntity> : _ctx.Set<TEntity>().AsNoTracking() as IQueryable<TEntity>;
        }
    }
}
