using System;
using System.Collections.Generic;

namespace Lab2.Model
{
    public partial class Seat
    {
        public Seat()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public int Number { get; set; }
        public int Row { get; set; }
        public int HallId { get; set; }
        public bool IsFree { get; set; }

        public override string ToString()
        {
            return "Seat_id: " + Id + " Number: " + Number + " Row: " + Row + " Is free: " + IsFree.ToString() 
                + "\n    Hall: " + Hall != null ? Hall.ToString() : HallId.ToString();
        }

        public virtual Hall Hall { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
