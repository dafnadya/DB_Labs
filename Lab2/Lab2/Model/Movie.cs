using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Model
{
    class Movie
    {
        private long id;
        private string title;
        private string country;
        private DateTime releaseDate;
        private int ageLimit;

        public Movie(long id, string title, string country,
            DateTime releaseDate, int ageLimit)
        {
            this.id = id;
            this.title = title;
            this.country = country;
            this.releaseDate = releaseDate;
            this.ageLimit = ageLimit;
        }

        public override string ToString()
        {
            return "Movie_id: " + id + " Title: " + title + " Country: " + country + " Release date: " + releaseDate.ToShortDateString() + " Age limit: " + ageLimit;
        }

        public long Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Country { get => country; set => country = value; }
        public DateTime ReleaseDate { get => releaseDate; set => releaseDate = value; }
        public int AgeLimit { get => ageLimit; set => ageLimit = value; }
    }
}
