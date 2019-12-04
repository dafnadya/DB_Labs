using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Model
{
    class Seance
    {
        private long id;
        private int price;
        private DateTime time;
        private long movie_id;
        private long hall_id;

        public Seance(long id, int price, DateTime time,
            long movie_id, long hall_id)
        {
            this.id = id;
            this.price = price;
            this.time = time;
            this.movie_id = movie_id;
            this.hall_id = hall_id;
        }

        public Seance(long id, int price, DateTime time,
            long movie_id, string title, string country,
            DateTime releaseDate, int ageLimit, long hall_id)
        {
            this.id = id;
            this.price = price;
            this.time = time;
            this.movie_id = movie_id;
            this.hall_id = hall_id;
            this.Movie = new Movie(movie_id, title, country, releaseDate, ageLimit);
        }

        public override string ToString()
        {
            return "Seance_id: " + id + " Price: " + price + " Time: " + time.ToShortTimeString() + " Hall: " + hall_id + "\n    Movie: " + Movie.ToString();
        }

        public long Id { get => id; set => id = value; }
        public int Price { get => price; set => price = value; }
        public DateTime Time { get => time; set => time = value; }
        public long Movie_id { get => movie_id; set => movie_id = value; }
        public long Hall_id { get => hall_id; set => hall_id = value; }

        public Movie Movie { get; set; }
    }
}
