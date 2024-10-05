using ProgressSoft.Core.Entites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProgressSoft.Core.Helper
{
    public static class Helper
    {
        public static IQueryable<T> ApplyFilter<T>(IQueryable<T>? query, string? filterValue, Expression<Func<T, bool>>? predicate)
        {
            if (!string.IsNullOrEmpty(filterValue) && predicate != null)
            {
                query = query.Where(predicate);
            }
            return query;
        }
    }

}
