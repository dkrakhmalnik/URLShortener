using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Models;

namespace URLShortener.Services
{
    public interface IRepository<T>
    {
        Task Insert(T obj);
        Task Update(T obj);
        Task<IEnumerable<T>> GetAll();
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
    }
}
