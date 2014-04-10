using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BITC.Library.Data
{
    public partial class DataContext<TData> : IOrderedQueryable<TData>
    {
        public DataContext()
        {
            //Provider = new DataProvider();s            
            Expression = Expression.Constant(this);
        }

        public DataContext(DataProvider _provider, Expression _expression)
        {

        }

        #region Property

        public Type ElementType
        {
            get { return typeof(TData); }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get;
            private set;
        }

        public IQueryProvider Provider
        {
            get;
            private set;
        }

        #endregion


        public IEnumerator<TData> GetEnumerator()
        {
            return (Provider.Execute<IEnumerable<TData>>(Expression)).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (Provider.Execute<System.Collections.IEnumerable>(Expression)).GetEnumerator();
        }


    }
}
