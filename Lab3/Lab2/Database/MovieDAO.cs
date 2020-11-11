using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Model;

namespace Lab2.Database
{
    class MovieDAO : DAO<Movie>
    {
        public MovieDAO(CinemaContext db) : base(db) { }

        public override void Create(Movie entity)
        {
            _db.Movie.Add(entity);
            _db.SaveChanges();
        }

        public override void Delete(long id)
        {
            _db.Movie.Remove(_db.Movie.Single(x => x.Id == id));
            _db.SaveChanges();
        }

        public override Movie Get(long id)
        {
            return _db.Movie.Single(x => x.Id == id);
        }

        public override List<Movie> GetList()
        {
            return _db.Movie.ToList();
        }

        public override List<Movie> GetList(int page)
        {
            int offset = page >= 0 ? page * 10 : 0;
            List<Movie> list = _db.Movie.Skip(offset).Take(10).ToList();
            return list;
        }

        public override void Update(Movie entity)
        {
            Movie upd = _db.Movie.Single(x => x.Id == entity.Id);
            upd.Title = entity.Title;
            upd.Country = entity.Country;
            upd.ReleaseDate = entity.ReleaseDate;
            upd.AgeLimit = entity.AgeLimit;
            _db.SaveChanges();
        }
    }
}
