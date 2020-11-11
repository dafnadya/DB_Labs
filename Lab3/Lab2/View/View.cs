using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Lab2.Model;
using Lab2.Database;

namespace Lab2.View
{
    public enum Entity
    {
        Null,
        Movie,
        Seance,
        Ticket,
        Exception
    }

    class ViewClass
    {
        public Entity entity;
        private List<Hall> halls;
        private List<Seat> seats;

        public ViewClass(List<Hall> halls, List<Seat> seats)
        {
            this.halls = halls;
            this.seats = seats;
            entity = Entity.Null;
        }

        public int Page()
        {
            while (true)
            {
                Console.WriteLine("Push '<-' '->'\n\nPress Esc to exit");
                ConsoleKeyInfo arrow = Console.ReadKey();
                if (arrow.Key == ConsoleKey.RightArrow) return 1;
                if (arrow.Key == ConsoleKey.LeftArrow) return -1;
                if (arrow.Key == ConsoleKey.Escape) return 0;
            }
        }

        public void PrintHalls()
        {
            Console.WriteLine("Halls:\n");
            if (halls.Count == 0)
            {
                Console.WriteLine("Empty");
            }
            else
            {
                foreach (Hall h in halls)
                {
                    Console.WriteLine(h.ToString());
                    Console.WriteLine("----------------------------------");
                }
            }
        }

        public void PrintSeats()
        {
            Console.WriteLine("Seats:\n");
            if (seats.Count == 0)
            {
                Console.WriteLine("Empty");
            }
            else
            {
                foreach (Seat s in seats)
                {
                    Console.WriteLine(s.ToString());
                    Console.WriteLine("----------------------------------");
                }
            }
        }

        public void PrintMovies(List<Movie> list, int page)
        {
            Console.WriteLine("Movies: (page " + page +")\n");
            if (list.Count == 0)
            {
                Console.WriteLine("Empty");
            }
            else
            {
                foreach (Movie m in list)
                {
                    Console.WriteLine(m.ToString());
                    Console.WriteLine("----------------------------------");
                }
            }
        }

        public void PrintSeances(List<Seance> list, int page)
        {
            Console.WriteLine("Seances: (page " + page + ")\n");
            if (list.Count == 0)
            {
                Console.WriteLine("Empty");
            }
            else
            {
                foreach (Seance s in list)
                {
                    Console.WriteLine(s.ToString());
                    Console.WriteLine("----------------------------------");
                }
            }
        }

        public void PrintTicket(List<Ticket> list, int page)
        {
            Console.WriteLine("Tickets: (page " + page + ")\n");
            if (list.Count == 0)
            {
                Console.WriteLine("Empty");
            }
            else
            {
                foreach (Ticket t in list)
                {
                    Console.WriteLine(t.ToString());
                    Console.WriteLine("----------------------------------");
                }
            }
        }

        public int MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose task:\n1. Entities\n2. Static search through tickets\n3. Full text search through movies\n0. Exit");
            try
            {
                int key = Convert.ToInt32(Console.ReadLine());
                if (key != 1 && key != 2 && key != 3) return -1;
                return key;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public Entity EntitiesMenu()
        {
            Console.Clear();
            Console.Write("ENTITIES\n1. Movies\n2. Seances\n3. Tickets\n");
            Console.WriteLine("\n0. Exit");
            Console.WriteLine("Choose entity:");
            try
            {
                int key = Convert.ToInt32(Console.ReadLine());
                switch (key)
                {
                    case 0:
                        entity = Entity.Null;
                        break;
                    case 1:
                        entity = Entity.Movie;
                        break;
                    case 2:
                        entity = Entity.Seance;
                        break;
                    case 3:
                        entity = Entity.Ticket;
                        break;
                    default: return Entity.Exception;
                }
            }
            catch (Exception)
            {
                entity = Entity.Exception;
            }
            return entity;
        }

        public int OperationsMenu()
        {
            Console.Clear();
            Console.WriteLine("OPERATIONS:\n1. Get by id\n2. Get all\n3. Add new entity\n4. Update\n5. Delete");
            Console.WriteLine("\n\n0. Exit");
            Console.WriteLine("Choose operation:");
            try
            {
                int key = Convert.ToInt32(Console.ReadLine());
                if (key >= 0 && key < 6) return key;
                return -1;

            }
            catch (Exception)
            {
                return -1;
            }

        }

        public int EnterId()
        {
            Console.WriteLine("Enter id:");
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public Movie MovieAddOrUpdateEnter()
        {
            Console.WriteLine("Title of movie:");
            string title = Console.ReadLine();
            Console.WriteLine("Country:");
            string country = Console.ReadLine();
            Console.WriteLine("Release date:");
            DateTime rDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Age limit:");
            int agelimit = Convert.ToInt32(Console.ReadLine());
            Movie movie = new Movie();
            movie.Title = title;
            movie.Country = country;
            movie.ReleaseDate = rDate;
            movie.AgeLimit = agelimit;
            return movie;
        }

        private string MovieIndexesToString(List<Movie> list)
        {
            if (list.Count == 0)
            {
                return "(no movies)";
            }
            else
            {
                string s = "";
                foreach (Movie m in list)
                {
                    s = s + m.Id + ". " + m.Title + " ";
                }
                return s;
            }
        }

        private string HallIndexesToString(List<Hall> list)
        {
            if (list.Count == 0)
            {
                return "(no movies)";
            }
            else
            {
                string s = "";
                foreach (Hall h in list)
                {
                    s = s + h.Id + ", ";
                }
                return s;
            }
        }

        private string SeanceIndexesToString(List<Seance> list)
        {
            if (list.Count == 0)
            {
                return "(no movies)";
            }
            else
            {
                string s = "";
                foreach (Seance m in list)
                {
                    s = s + m.Id + ". " + m.Time + " ";
                }
                return s;
            }
        }

        private string SeatIndexesToString(List<Seat> list)
        {
            if (list.Count == 0)
            {
                return "(no movies)";
            }
            else
            {
                string s = "";
                foreach (Seat m in list)
                {
                    s = s + m.Id + ". " + m.Number + " seat ";
                }
                return s;
            }
        }

        public Seance SeanceAddOrUpdateEnter(List<Movie> movies, List<Hall> halls)
        {
            Console.WriteLine("Price:");
            int price = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Time:");
            DateTime time = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Movie id: (" + MovieIndexesToString(movies) + ")");
            int movie_id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Hall id: (" + HallIndexesToString(halls) + ")");
            int hall_id = Convert.ToInt32(Console.ReadLine());
            Seance seance = new Seance();
            seance.Price = price;
            seance.Time = time;
            seance.MovieId = movie_id;
            seance.HallId = hall_id;
            return seance;
        }

        public Ticket TicketAddOrUpdateGetSeance(List<Seance> seances)
        {
            Console.WriteLine("Buy time:");
            DateTime time = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Seance id: (" + SeanceIndexesToString(seances) + ")");
            int seance_id = Convert.ToInt32(Console.ReadLine());
            Ticket ticket = new Ticket();
            ticket.BuyTime = time;
            ticket.SeanceId = seance_id;
            ticket.SeatId = -1;
            return ticket;
        }

        public Ticket TicketAddOrUpdateGetSeat(List<Seat> seats, Ticket ticket)
        {
            Console.WriteLine("Seat id: (" + SeatIndexesToString(seats) + ")");
            int seat_id = Convert.ToInt32(Console.ReadLine());
            ticket.SeatId = seat_id;
            return ticket;
        }

        public SearchData StaticSearch()
        {
            Console.WriteLine("Enter min date");
            DateTime min = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter max date");
            DateTime max = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Is it free?");
            bool isFree = Convert.ToBoolean(Console.ReadLine());
            return new SearchData(min, max, isFree);
        }

        public string SearchQuery()
        {
            Console.WriteLine("Enter query");
            return Console.ReadLine();
        }

        public void Error(string message)
        {
            Console.WriteLine($"Error occured: {message}");
        }

        public void Wait()
        {
            Console.Write("Press any key to get back: ");
            Console.ReadKey();
        }
    }
}
