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

        public void PrintMovies(List<Movie> list)
        {
            Console.WriteLine("Movies:\n");
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

        public void PrintSeances(List<Seance> list)
        {
            Console.WriteLine("Seances:\n");
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

        public void PrintTicket(List<Ticket> list)
        {
            Console.WriteLine("Tickets:\n");
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


        public void PrintFullTextSearch_FullPhrase(List<SearchRes> res)
        {
            if (res.Count == 0 || res[0] == null)
            {
                Console.WriteLine("No result");
            }
            else
            {
                foreach (SearchRes s in res)
                {
                    Console.WriteLine("Id: " + s.Id + " Attr: " + s.Attr + " ts_headline " + s.Ts_headline);
                }
            }
        }

        public void PrintFullTextSearch_NotIncludedWord(List<SearchRes> res)
        {
            if (res.Count == 0 || res[0] == null)
            {
                Console.WriteLine("No result");
            }
            else
            {
                foreach (SearchRes s in res)
                {
                    Console.WriteLine("Id: " + s.Id + " Attr: " + s.Attr);
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

        public long EnterId()
        {
            Console.WriteLine("Enter id:");
            try
            {
                return Convert.ToInt64(Console.ReadLine());
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
            return new Movie(-1, title, country, rDate, agelimit);
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
            long movie_id = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Hall id: (" + HallIndexesToString(halls) + ")");
            long hall_id = Convert.ToInt64(Console.ReadLine());
            return new Seance(-1, price, time, movie_id, hall_id);
        }

        public Ticket TicketAddOrUpdateGetSeance(List<Seance> seances)
        {
            Console.WriteLine("Buy time:");
            DateTime time = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Seance id: (" + SeanceIndexesToString(seances) + ")");
            long seance_id = Convert.ToInt64(Console.ReadLine());
            return new Ticket(-1, time, seance_id, -1);
        }

        public Ticket TicketAddOrUpdateGetSeat(List<Seat> seats, Ticket ticket)
        {
            Console.WriteLine("Seat id: (" + SeatIndexesToString(seats) + ")");
            long seat_id = Convert.ToInt64(Console.ReadLine());
            ticket.Seat_id = seat_id;
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

        public int MovieAttr()
        {
            Console.WriteLine("CHOOSE ATRIBUTE of Movie");
            Console.WriteLine("1. Title");
            Console.WriteLine("2. Country");
            int key = 0;
            while (key != 1 && key != 2)
            {
                key = Convert.ToInt32(Console.ReadLine());
            }
            return key;

        }

        public int FullText()
        {
            Console.WriteLine("CHOOSE SEARCH");
            Console.WriteLine("1. Full phrase");
            Console.WriteLine("2. Not included word");
            int key = 0;
            while (key != 1 && key != 2)
            {
                key = Convert.ToInt32(Console.ReadLine());
            }
            return key;

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
