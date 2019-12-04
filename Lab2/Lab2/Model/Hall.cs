using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Model
{
    class Hall
    {
        private long id;
        private string format;
        private int seatsAmount;

        public Hall(long id, string format,
                    int seatsAmount)
        {
            this.id = id;
            this.format = format;
            this.seatsAmount = seatsAmount;
        }

        public override string ToString()
        {
            return "Hall_id: " + id + " Format: " + format + " Seats amount: " + seatsAmount;
        }

        public long Id { get => id; set => id = value; }
        public string Format { get => format; set => format = value; }
        public int SeatsAmount { get => seatsAmount; set => seatsAmount = value; }
    }
}
