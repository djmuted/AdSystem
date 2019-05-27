using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdSystem.Models
{
    public abstract class PagedResultBase
    {
        public int currentPage { get; set; }
        public int pageCount { get; set; }
        public int pageSize { get; set; }
        public int rowCount { get; set; }

        public int firstRowOnPage
        {

            get { return (currentPage - 1) * pageSize + 1; }
        }

        public int lastRowOnPage
        {
            get { return Math.Min(currentPage * pageSize, rowCount); }
        }
    }

    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> results { get; set; }

        public PagedResult()
        {
            results = new List<T>();
        }
    }
    public static class Pages
    {
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query,
                                         int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>();
            result.currentPage = page;
            result.pageSize = pageSize;
            result.rowCount = query.Count();


            var pageCount = (double)result.rowCount / pageSize;
            result.pageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }
    }
}
