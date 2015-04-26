using System.Collections.Generic;
using System.Linq;

namespace SimpleStock.Web.Models
{
    public class PagedResult<TResult>
    {
        public IEnumerable<TResult> Data;
        public Pager Pager { get; set; }
        public Sort Sort { get; set; }

        public static PagedResult<TResult> From(IQueryable<TResult> query, PagedFilter filter)
        {
            var result = new PagedResult<TResult>
            {
                Data = query.Skip(filter.Offset).Take(filter.Limit).ToList(),
                Pager = new Pager
                {
                    TotalCount = query.Count(),
                    Offset = filter.Offset,
                    Limit = filter.Limit
                },
                Sort = new Sort
                {
                    SortBy = filter.SortBy,
                    Desc = filter.Desc
                }
            };

            return result;
        }

    }

    public class Pager
    {
        public int TotalCount { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }

    public class Sort
    {
        public string SortBy { get; set; }
        public bool Desc { get; set; }
    }
}