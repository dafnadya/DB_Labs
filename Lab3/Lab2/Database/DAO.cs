using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Database
{
    abstract class DAO<T>
    {
        protected CinemaContext _db;

        public DAO(CinemaContext db)
        {
            _db = db;
        }

        public abstract T Get(long id);
        public abstract List<T> GetList();
        public abstract List<T> GetList(int page);
        public abstract void Create(T entity);
        public abstract void Update(T entity);
        public abstract void Delete(long id);
    }
}
