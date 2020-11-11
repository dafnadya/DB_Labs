using System;
using System.Collections.Generic;

namespace Lab2.Model
{
    public partial class Hall
    {
        public Hall()
        {
            Seance = new HashSet<Seance>();
            Seat = new HashSet<Seat>();
        }

        public int Id { get; set; }
        public string Format { get; set; }
        public int SeatsAmount { get; set; }

        public override string ToString()
        {
            return "Hall_id: " + Id + " Format: " + Format + " Seats amount: " + SeatsAmount;
        }

        public virtual ICollection<Seance> Seance { get; set; }
        public virtual ICollection<Seat> Seat { get; set; }
    }
}
