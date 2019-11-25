using System.Linq;

namespace SYF.Infrastructure.Data
{
    public abstract class BaseCriteria<TEntity> : ICriteria<TEntity> where TEntity : class
    {
        public abstract IQueryable<TEntity> Apply(IQueryable<TEntity> qry);
    }
}
