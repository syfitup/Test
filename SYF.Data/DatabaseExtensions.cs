using SYF.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYF.Data
{
    public static class DatabaseExtensions
    {
        public static IQueryable<TEntity> Query<TEntity>(this IQueryable<TEntity> qry, ICriteria<TEntity> criteria)
            where TEntity : class
        {
            if (criteria == null) return qry;

            qry = criteria.Apply(qry);

            return qry;
        }

        public static IEnumerable<TEntity> EnumeratedQuery<TEntity>(this IEnumerable<TEntity> qry, IEnumeratedCriteria<TEntity> criteria)
            where TEntity : class
        {
            if (criteria == null) return qry;

            return criteria.PostApply(qry);
        }

        public static IQueryable<TEntity> Paged<TEntity>(this IQueryable<TEntity> qry, int? pageNumber, int? pageSize)
            where TEntity : class
        {
            if (pageSize == null) return qry;

            qry = qry.Take(pageSize.Value);

            var skip = (pageNumber.GetValueOrDefault(1) - 1) * pageSize.Value;
            if (skip > 0) qry = qry.Skip(skip);

            return qry;
        }

        public static PagedResult<TEntity> ToPagedAsync<TEntity>(this IQueryable<TEntity> qry, int pageNumber, int pageSize)
            where TEntity : class
        {
            var resultQry = qry;
            var countQry = qry;

            resultQry = resultQry.Take(pageSize);

            var skip = (pageNumber - 1) * pageSize;
            if (skip > 0) resultQry = resultQry.Skip(skip);

            var results = resultQry.ToArray();
            var totalCount = countQry.Count();

            return new PagedResult<TEntity>(results, totalCount);
        }
    }
}
