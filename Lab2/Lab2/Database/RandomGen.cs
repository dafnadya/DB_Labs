using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Lab2.Database
{
    class RandomGen
    {
        private Random random = new Random();

        public int getRandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }
        
        public string[] getMovies()
        {
            return Movies;
        }

        public bool getRandomBoolean()
        {
            if (random.Next(3) == 1) return true;
            return false;
        }

        public string getRandomFormat()
        {
            if (random.Next(3) == 1) return "2D";
            return "3D";
        }

        public string getRandomCountry()
        {
            return Countries[random.Next(Countries.Length)];
        }

        public DateTime getRandomFutureDate()
        {
            DateTime randomDate = DateTime.Today.AddDays(random.Next(30));
            return randomDate;
        }

        public DateTime getRandomPastDate()
        {
            DateTime randomDate = DateTime.Today.AddDays(-random.Next(30));
            return randomDate;
        }

        public long getRandomIndex(List<long> l)
        {
            int val = random.Next(0, l.Count);
            return l.ElementAt(val);
        }

        private string[] Movies = new string[]
        {
            "Avengers: Endgame",
            "The Lion King",
            "Spider-Man: Far From Home",
            "Captain Marvel",
            "Toy Story 4",
            "Aladdin",
            "Joker",
            "Hobbs & Shaw",
            "Frozen II",
            "Ne Zha",
            "The Grudge",
            "Underwater",
            "Like a Boss",
            "Inherit the Viper",
            "The Informer",
            "My Spy",
            "Dolittle",
            "Bad Boys for Life",
            "The Last Full Measure",
            "The Turning",
            "The Gentlemen",
            "Run",
            "The Rhythm Section",
            "Gretel & Hansel",
            "Birds of Prey"
        };

        private string[] Countries = new string[]
        {
            "Australia",
            "Austria",
            "Belgium",
            "China",
            "Canada",
            "Czech Republic",
            "Denmark",
            "France",
            "Germany",
            "Greece",
            "India",
            "Ireland",
            "Israel",
            "Italy",
            "Japan",
            "Korea, Democratic People's Republic of",
            "Latvia",
            "Mexico",
            "Netherlands",
            "Poland",
            "Portugal",
            "Russian Federation",
            "Slovakia",
            "Spain",
            "Sweden",
            "Switzerland",
            "Turkey",
            "Ukraine",
            "United Kingdom",
            "United States"
        };
    }
}
