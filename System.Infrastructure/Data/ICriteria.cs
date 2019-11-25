using System.Collections.Generic;
using System.Linq;

namespace SYF.Infrastructure.Data
{
    public interface ICriteria<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Apply(IQueryable<TEntity> qry);
    }

    public interface IEnumeratedCriteria<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> PostApply(IEnumerable<TEntity> qry);
    }
}

