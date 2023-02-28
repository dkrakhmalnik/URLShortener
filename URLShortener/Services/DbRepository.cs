using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Models;

namespace URLShortener.Services
{
    public class DbRepository : IRepository<UrlData>
    {
        private readonly SQLiteAsyncConnection _db;

        public DbRepository(string path)
        {
            _db = new SQLiteAsyncConnection(path, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
            _db.CreateTableAsync<UrlData>().Wait();
        }
        public async Task<IEnumerable<UrlData>> GetAll()
        {
            return await _db.Table<UrlData>().OrderByDescending(u => u.Id).ToArrayAsync();
        }

        public async Task<UrlData> FirstOrDefault(Expression<Func<UrlData, bool>> predicate)
        {
            return await _db.Table<UrlData>().FirstOrDefaultAsync(predicate);
        }

        public async Task Insert(UrlData url)
        {
            await _db.InsertAsync(url);
        }

        public async Task Update(UrlData obj)
        {
            await _db.UpdateAsync(obj);
        }
    }
}
