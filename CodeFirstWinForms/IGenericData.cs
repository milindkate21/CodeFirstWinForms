using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstWinForms
{
    interface IGenericData<T> where T : class
    {
        IList<T> GetAll<T>() where T : class;
        void Add(T items);
        void Update(T items);
        void Delete(T items) ;
    }
}
