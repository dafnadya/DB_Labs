using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Model
{
    class Ticket
    {
        private long id;
        private DateTime buyTime;
        private long seat_id;
        private long seance_id;

        public Ticket(long id, DateTime buyTime,
            long seance_id, long seat_id)
        {
            this.id = id;
            this.seance_id = seance_id;
            this.seat_id = seat_id;
            this.buyTime = buyTime;
        }

        public Ticket(long id, DateTime buyTime,
            long seat_id, bool isFree, int number, int row,
            long hall_id, string format, int seatsAmount,
            long seance_id, int price, DateTime time,
            long movie_id, string title, string country,
            DateTime releaseDate, int ageLimit)
        {
            this.id = id;
            this.seance_id = seance_id;
            this.seat_id = seat_id;
            this.buyTime = buyTime;
            this.Seat = new Seat(seat_id, number, row, hall_id, isFree, format, seatsAmount);
            this.Seance = new Seance(seance_id, price, time, movie_id, title, 
                country, releaseDate, ageLimit, hall_id);
        }

        public override string ToString()
        {
            return "Id: " + id + " Buy time: " + buyTime.ToShortDateString() + "\n    Seat: " + Seat.ToString() + "\n    Seance: " + Seance.ToString();
        }

        public long Id { get => id; set => id = value; }
        public long Seance_id { get => seance_id; set => seance_id = value; }
        public long Seat_id { get => seat_id; set => seat_id = value; }
        public DateTime BuyTime { get => buyTime; set => buyTime = value; }

        public Seance Seance { get; set; }
        public Seat Seat { get; set; }
    }
}
