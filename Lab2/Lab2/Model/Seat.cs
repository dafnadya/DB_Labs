using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Model
{
    class Seat
    {
        private long id;
        private int number;
        private int row;
        private long hall_id;
        private bool isFree;

        public Seat(long id, int number, int row, long hall_id, bool isFree)
        {
            this.id = id;
            this.number = number;
            this.row = row;
            this.hall_id = hall_id;
            this.isFree = isFree;
        }

        public Seat(long id, int number, int row, long hall_id,
            bool isFree, string format, int seatsAmount)
        {
            this.id = id;
            this.number = number;
            this.row = row;
            this.hall_id = hall_id;
            this.isFree = isFree;
            this.Hall = new Hall(hall_id, format, seatsAmount);
        }

        public override string ToString()
        {
            return "Seat_id: " + id + " Number: " + number + " Row: " + row + " Is free: " + isFree.ToString() + "\n    Hall: " + Hall.ToString();
        }

        public long Id { get => id; set => id = value; }
        public int Number { get => number; set => number = value; }
        public int Row { get => row; set => row = value; }
        public bool IsFree { get => isFree; set => isFree = value; }
        public long Hall_id { get => hall_id; set => hall_id = value; }

        public Hall Hall { get; set; }
    }
}
