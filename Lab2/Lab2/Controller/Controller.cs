using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.View;
using Lab2.Model;
using Lab2.Database;

namespace Lab2.Controller
{
    public enum Operation
    {
        GetById = 1,
        GetAll,
        Add,
        Update,
        Delete
    }
    class ControllerClass
    {

        private ViewClass view;
        private HallDAO hallDAO;
        private MovieDAO movieDAO;
        private SeanceDAO seanceDAO;
        private TicketDAO ticketDAO;
        private FullTextSearch fullTextSearch;

        public ControllerClass()
        {
            hallDAO = new HallDAO();
            movieDAO = new MovieDAO();
            seanceDAO = new SeanceDAO();
            ticketDAO = new TicketDAO();
            fullTextSearch = new FullTextSearch();
            view = new ViewClass(hallDAO.GetHalls(), hallDAO.GetSeats());
        }

        public void Start()
        {
            while (true)
            {
                int mode = view.MainMenu();
                if (mode == 0) break;
                else if (mode == 2)
                {
                    FullTextSearch();
                    view.Wait();
                }
                else if (mode == 3)
                {
                    SearchTicketOperation();
                    view.Wait();
                }
                else if (mode == 1)
                {
                    while (true)
                    {
                        Entity entity = view.EntitiesMenu();
                        if (entity == Entity.Null) break;
                        else if (entity != Entity.Exception)
                        {
                                while (true)
                                {
                                    int operation = view.OperationsMenu();
                                    if (operation == 0) break;
                                    try
                                    {
                                        switch ((Operation)operation)
                                        {
                                            case Operation.Add:
                                                AddOperation();
                                                break;
                                            case Operation.GetById:
                                                GetByIdOperation();
                                                break;
                                            case Operation.GetAll:
                                                GetListOperation();
                                                break;
                                            case Operation.Update:
                                                UpdateOperation();
                                                break;
                                            case Operation.Delete:
                                                DeleteOperation();
                                                break;
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        view.Error(e.Message.ToString());
                                    }
                                    if (operation != 2) view.Wait();
                                }
                        }
                    }
                }
            }
        }

        private void AddOperation()
        {
            switch (view.entity)
            {
                case Entity.Movie:
                    Movie m = view.MovieAddOrUpdateEnter();
                    movieDAO.Create(m);
                    break;
                case Entity.Seance:
                    Seance s = view.SeanceAddOrUpdateEnter(movieDAO.GetList(), hallDAO.GetHalls());
                    seanceDAO.Create(s);
                    break;
                case Entity.Ticket:
                    Ticket t = view.TicketAddOrUpdateGetSeance(seanceDAO.GetList());
                    long hall_id = seanceDAO.Get(t.Seance_id).Hall_id;
                    t = view.TicketAddOrUpdateGetSeat(hallDAO.GetSeatsInHall(hall_id), t);
                    ticketDAO.Create(t);
                    break;
            }
        }
        private void GetByIdOperation()
        {
            long id = -1;
            while (id < 0)
            {
                id = view.EnterId();
            }
            switch (view.entity)
            {
                case Entity.Ticket:
                    List<Ticket> tickets = new List<Ticket>() { ticketDAO.Get(id) };
                    view.PrintTicket(tickets);
                    break;
                case Entity.Seance:
                    List<Seance> seances = new List<Seance>() { seanceDAO.Get(id) };
                    view.PrintSeances(seances);
                    break;
                case Entity.Movie:
                    List<Movie> movies = new List<Movie>() { movieDAO.Get(id) };
                    view.PrintMovies(movies);
                    break;
            }
        }
        private void GetListOperation()
        {
            int page = 0;
            while (true)
            {
                switch (view.entity)
                {
                    case Entity.Movie:
                        List<Movie> movies = movieDAO.GetList(page);
                        view.PrintMovies(movies);
                        break;
                    case Entity.Seance:
                        List<Seance> seances = seanceDAO.GetList(page);
                        view.PrintSeances(seances);
                        break;
                    case Entity.Ticket:
                        List<Ticket> tickets = ticketDAO.GetList(page);
                        view.PrintTicket(tickets);
                        break;
                }
                int arrow = view.Page();
                if (arrow == 0) break;
                else page += arrow;
                if (page < 0) page = 0;
            }
        }
        private void UpdateOperation()
        {
            long id = -1;
            while (id < 0)
            {
                id = view.EnterId();
            }
            if (id < 0) throw new Exception("Wrong id");
            switch (view.entity)
            {
                case Entity.Movie:
                    Movie m = view.MovieAddOrUpdateEnter();
                    m.Id = id;
                    movieDAO.Update(m);
                    break;
                case Entity.Seance:
                    Seance s = view.SeanceAddOrUpdateEnter(movieDAO.GetList(), hallDAO.GetHalls());
                    s.Id = id;
                    seanceDAO.Update(s);
                    break;
                /*case Entity.Booking:
                    Booking booking = view.BookingAddOrUpdate();
                    booking.Id = id;
                    bookingDAO.Update(booking);
                    break;*/
            }
        }
        private void DeleteOperation()
        {
            long id = -1;
            while (id < 0)
            {
                id = view.EnterId();
            }
            if (id < 0) throw new Exception("Wrong id");
            switch (view.entity)
            {
                case Entity.Movie:
                    movieDAO.Delete(id);
                    break;
                case Entity.Seance:
                    seanceDAO.Delete(id);
                    break;
                case Entity.Ticket:
                    ticketDAO.Delete(id);
                    break;
            }
        }

        private void SearchTicketOperation()
        {
            List<Ticket> t = ticketDAO.StaticSearch(view.StaticSearch());
            view.PrintTicket(t);
        }

        private void FullTextSearch()
        {
            List<SearchRes> res = new List<SearchRes>();
            string table = "movie";
            string attr;
            int atr = view.MovieAttr();
            if (atr == 1) attr = "title";
            else attr = "country";
            int search = view.FullText();
            string query = view.SearchQuery();
            switch (search)
            {
                case 1:
                    res.AddRange(fullTextSearch.getFullPhrase(attr, table, query));
                    view.PrintFullTextSearch_FullPhrase(res);
                    break;
                case 2:
                    res.AddRange(fullTextSearch.getAllWithNotIncludedWord(attr, table, query));
                    view.PrintFullTextSearch_NotIncludedWord(res);
                    break;
                default: throw new Exception("Wrong entity selected");
            }
        }
    }
}
