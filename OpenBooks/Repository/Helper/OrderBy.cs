using System;
using System.Linq.Expressions;

namespace OpenBooks.Repository.Helper
{
    public class OrderBy<T, G> : IOrderBy
    {
        private readonly Expression<Func<T, G>> expression;

        public OrderBy(Expression<Func<T, G>> expression)
        {
            this.expression = expression;
        }

        public dynamic Expression => expression;
    }
}
