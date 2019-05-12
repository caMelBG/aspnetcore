using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace huncho.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private HunchoDbContext _dbContext;
        private DbSet<T> _table;

        public Repository(HunchoDbContext context)
        {
            _dbContext = context;
            _table = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _table.AsEnumerable();
        }

        public T GetById(object id)
        {
            return _table.Find(id);
        }

        public void Insert(T entity)
        {
            _table.Add(entity);
        }

        public void Update(T entity)
        {
            _table.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T entity = _table.Find(id);
            _table.Remove(entity);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
