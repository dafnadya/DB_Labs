using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Model;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Database
{
    public struct SearchData
    {
        public DateTime buyTime1;
        public DateTime buyTime2;
        public bool isFree;
        public SearchData(DateTime buyTime1, DateTime buyTime2, bool isFree)
        {
            this.buyTime1 = buyTime1;
            this.buyTime2 = buyTime2;
            this.isFree = isFree;
        }
    }

    class TicketDAO : DAO<Ticket>
    {
        public TicketDAO(CinemaContext db) : base(db) { }
        
        public override void Create(Ticket entity)
        {
            _db.Ticket.Add(entity);
            _db.SaveChanges();
        }

        public override void Delete(long id)
        {
            _db.Ticket.Remove(_db.Ticket.Single(x => x.Id == id));
            _db.SaveChanges();
        }

        public override Ticket Get(long id)
        {
            return _db.Ticket.Single(x => x.Id == id);
        }

        public override List<Ticket> GetList()
        {
            List<Ticket> tickets = _db.Ticket
                                      .Include(b => b.Seance)
                                      .Include(b => b.Seat)
                                      .Include(b => b.Seance.Movie)
                                      .Include(b => b.Seance.Hall)
                                      .ToList();

            return tickets;
        }

        public override List<Ticket> GetList(int page)
        {
            int offset = page >= 0 ? page * 10 : 0;
            List<Ticket> tickets = _db.Ticket
                                      .Include(b => b.Seance)
                                      .Include(b => b.Seat)
                                      .Include(b => b.Seance.Movie)
                                      .Include(b => b.Seance.Hall)
                                      .Skip(offset).Take(10).ToList();

            return tickets;
        }

        public override void Update(Ticket entity)
        {
            Ticket upd = _db.Ticket.Single(x => x.Id == entity.Id);
            upd.BuyTime = entity.BuyTime;
            upd.SeatId = entity.SeatId;
            upd.SeanceId = entity.SeanceId;
            _db.SaveChanges();
        }

        public List<Ticket> StaticSearch(SearchData search)
        {
            return _db.Ticket
                      .Include(b => b.Seance)
                        .Include(b => b.Seat)
                        .Include(b => b.Seance.Movie)
                        .Include(b => b.Seance.Hall)
                      .Where(b => b.BuyTime >= search.buyTime1 && b.BuyTime <= search.buyTime2 && b.Seat.IsFree == search.isFree).ToList();
        }
    }
}
