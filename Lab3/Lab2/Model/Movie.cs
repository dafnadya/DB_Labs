using System;
using System.Collections.Generic;

namespace Lab2.Model
{
    public partial class Movie
    {
        public Movie()
        {
            Seance = new HashSet<Seance>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int AgeLimit { get; set; }

        public override string ToString()
        {
            return "Movie_id: " + Id + " Title: " + Title + " Country: " + Country + " Release date: " + ReleaseDate.ToShortDateString() + " Age limit: " + AgeLimit;
        }

        public virtual ICollection<Seance> Seance { get; set; }
    }
}
