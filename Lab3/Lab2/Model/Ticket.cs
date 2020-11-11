using System;
using System.Collections.Generic;

namespace Lab2.Model
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public DateTime BuyTime { get; set; }
        public int SeatId { get; set; }
        public int SeanceId { get; set; }

        public virtual Seance Seance { get; set; }
        public virtual Seat Seat { get; set; }

        public override string ToString()
        {
            return "Id: " + Id + " Buy time: " + BuyTime.ToShortDateString() + "\n    Seat: " + Seat != null ? Seat.ToString() : SeatId.ToString()
                + "\n    Seance: " + Seance != null ? Seance.ToString() : SeanceId.ToString();
        }
    }
}
