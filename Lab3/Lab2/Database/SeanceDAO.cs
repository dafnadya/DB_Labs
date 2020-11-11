using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Model;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Database
{
    class SeanceDAO : DAO<Seance>
    {
        public SeanceDAO(CinemaContext  db) : base(db) { }

        public override void Create(Seance entity)
        {
            _db.Seance.Add(entity);
            _db.SaveChanges();
        }

        public override void Delete(long id)
        {
            _db.Seance.Remove(_db.Seance.Single(x => x.Id == id));
            _db.SaveChanges();
        }

        public override Seance Get(long id)
        {
            return _db.Seance.Single(x => x.Id == id);
        }

        public override List<Seance> GetList()
        {
            List <Seance> seances = _db.Seance.Include(s => s.Movie).Include(s => s.Hall).ToList();
            return seances;
        }

        public override List<Seance> GetList(int page)
        {
            int offset = page >= 0 ? page * 10 : 0;
            List<Seance> seances = _db.Seance.Include(s => s.Movie).Include(s => s.Hall).Skip(offset).Take(10).ToList();
            return seances;
        }

        public override void Update(Seance entity)
        {
            Seance upd = _db.Seance.Single(x => x.Id == entity.Id);
            upd.Price = entity.Price;
            upd.Time = entity.Time;
            upd.MovieId = entity.MovieId;
            upd.HallId = entity.HallId;
            _db.SaveChanges();
        }
    }
}
