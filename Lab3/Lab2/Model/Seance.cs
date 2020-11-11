using System;
using System.Collections.Generic;

namespace Lab2.Model
{
    public partial class Seance
    {
        public Seance()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public int Price { get; set; }
        public DateTime Time { get; set; }
        public int MovieId { get; set; }
        public int HallId { get; set; }

        public override string ToString()
        {
            return "Seance_id: " + Id + " Price: " + Price + " Time: " + Time.ToShortTimeString() + " Hall: " + HallId 
                + "\n    Movie: " + Movie != null ? Movie.ToString() : MovieId.ToString();
        }

        public virtual Hall Hall { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
