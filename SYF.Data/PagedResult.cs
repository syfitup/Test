using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYF.Data
{
    public class PagedResult<TEntity> where TEntity : class
    {
        public PagedResult()
        {
            Items = Enumerable.Empty<TEntity>();
        }

        public PagedResult(IEnumerable<TEntity> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }

        public IEnumerable<TEntity> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
