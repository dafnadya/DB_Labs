using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Lab2.Model;

namespace Lab2.Database
{
    abstract class DAO<T>
    {
        protected NpgsqlConnection dbconnection;

        public DAO()
        {
            dbconnection = new NpgsqlConnection("Server=127.0.0.1; Port=5432; User Id=postgres; Password=aktriso4ka; Database=Cinema;");
        }

        public abstract T Get(long id);
        public abstract List<T> GetList();
        public abstract List<T> GetList(int page);
        public abstract void Create(T entity);
        public abstract void Update(T entity);
        public abstract void Delete(long id);

        public void RandomDB(HallDAO hallDAO, MovieDAO movieDAO, TicketDAO ticketDAO, SeanceDAO seanceDAO)
        {
            RandomGen random = new RandomGen();

            for (int i = 0; i < 10; i++)
            {
                Hall h = new Hall(-1, random.getRandomFormat(), random.getRandomNumber(40, 61));
                hallDAO.CreateHall(h);
            }

            List<Hall> halls = hallDAO.GetHalls();
            List<long> hallsIndexes = new List<long>();
            foreach (Hall h in halls)
            {
                hallsIndexes.Add(h.Id);
            }

            for (int i = 0; i < halls.Count; i++)
            {
                int seatsAmount = halls.ElementAt(i).SeatsAmount;
                for (int j = 0; j < seatsAmount; j++)
                {
                    Seat s = new Seat(-1, j+1, (int)j/10+1, i, random.getRandomBoolean());
                    hallDAO.CreateSeat(s);
                }
            }
            List<Seat> seats = hallDAO.GetSeats();
            List<long> seatsIndexes = new List<long>();
            foreach (Seat s in seats)
            {
                seatsIndexes.Add(s.Id);
            }

            string[] movieTitles = random.getMovies();
            for (int i = 0; i < 20; i++)
            {
                Movie m = new Movie(-1, movieTitles[i], random.getRandomCountry(), random.getRandomPastDate(), random.getRandomNumber(0, 22));
                movieDAO.Create(m);
            }
            List<Movie> movies = movieDAO.GetList();
            List<long> moviesIndexes = new List<long>();
            foreach (Movie m in movies)
            {
                moviesIndexes.Add(m.Id);
            }

            for (int i = 0; i < 20; i++)
            {
                Seance s = new Seance(-1, random.getRandomNumber(10, 101), random.getRandomFutureDate(),
                    random.getRandomIndex(moviesIndexes), random.getRandomIndex(hallsIndexes));
                seanceDAO.Create(s);
            }
            List<Seance> seances = seanceDAO.GetList();

            foreach (Seance s in seances)
            {
                Ticket t = new Ticket(-1, random.getRandomPastDate(), s.Id, random.getRandomIndex(seatsIndexes));
                ticketDAO.Create(t);
            }
        }
    }
}
