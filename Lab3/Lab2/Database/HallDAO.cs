using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Model;

namespace Lab2.Database
{
    class HallDAO
    {
        private CinemaContext dbconnection;

        public HallDAO(CinemaContext db)
        {
            dbconnection = db;
        }

        public List<Hall> GetHalls()
        {
            return dbconnection.Hall.ToList();
        }

        public List<Seat> GetSeats()
        {
            return dbconnection.Seat.ToList();
        }

        public List<Seat> GetSeatsInHall(long hall_id)
        {
            return dbconnection.Seat.Where(b => b.HallId == hall_id).ToList();
        }
    }
}
