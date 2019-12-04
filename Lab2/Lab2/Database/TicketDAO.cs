using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Model;
using Npgsql;

namespace Lab2.Database
{
    public struct SearchData
    {
        public DateTime buyTime1;
        public DateTime buyTime2;
        public bool isFree;
        public SearchData(DateTime buyTime1, DateTime buyTime2, bool isFree)
        {
            this.buyTime1 = buyTime1;
            this.buyTime2 = buyTime2;
            this.isFree = isFree;
        }
    }

    class TicketDAO : DAO<Ticket>
    {
        public TicketDAO() : base() { }
        
        public override void Create(Ticket entity)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "INSERT INTO public.ticket (buy_time, seat_id, seance_id) VALUES (:buy_time, :seat_id, :seance_id)";
            command.Parameters.Add(new NpgsqlParameter("buy_time", entity.BuyTime));
            command.Parameters.Add(new NpgsqlParameter("seat_id", entity.Seat_id));
            command.Parameters.Add(new NpgsqlParameter("seance_id", entity.Seance_id));
            Console.WriteLine(command.CommandText);
            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
            }
            catch (PostgresException e)
            {
                throw new Exception(e.MessageText);
            }
            dbconnection.Close();
        }

        public override void Delete(long id)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "DELETE FROM public.ticket WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            command.ExecuteNonQuery();
            dbconnection.Close();
        }

        public override Ticket Get(long id)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "select * from public.ticket t inner join public.seat st on t.seat_id = st.id inner join public.hall hl on st.hall_id = hl.id " +
                                   "inner join public.seance sn on t.seance_id = sn.id  inner join public.movie mv on sn.movie_id = mv.id WHERE t.id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            NpgsqlDataReader reader = command.ExecuteReader();
            Ticket t = null;
            while (reader.Read())
            {
                t = new Ticket(Convert.ToInt64(reader.GetValue(0)), Convert.ToDateTime(reader.GetValue(1)),
                                        Convert.ToInt64(reader.GetValue(4)), Convert.ToBoolean(reader.GetValue(8)), Convert.ToInt32(reader.GetValue(5)), Convert.ToInt32(reader.GetValue(6)),
                                        Convert.ToInt64(reader.GetValue(9)), reader.GetValue(10).ToString(), Convert.ToInt32(reader.GetValue(11)),
                                        Convert.ToInt64(reader.GetValue(12)), Convert.ToInt32(reader.GetValue(13)), Convert.ToDateTime(reader.GetValue(14)),
                                        Convert.ToInt64(reader.GetValue(17)), reader.GetValue(18).ToString(), reader.GetValue(19).ToString(),
                                        Convert.ToDateTime(reader.GetValue(20)), Convert.ToInt32(reader.GetValue(21)));
            }
            dbconnection.Close();
            return t;
        }

        public override List<Ticket> GetList()
        {
            List<Ticket> t_list = new List<Ticket>();
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "select * from public.ticket t inner join public.seat st on t.seat_id = st.id inner join public.hall hl on st.hall_id = hl.id " +
                                   "inner join public.seance sn on t.seance_id = sn.id  inner join public.movie mv on sn.movie_id = mv.id";
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Ticket t = new Ticket(Convert.ToInt64(reader.GetValue(0)), Convert.ToDateTime(reader.GetValue(1)),
                                        Convert.ToInt64(reader.GetValue(4)), Convert.ToBoolean(reader.GetValue(8)), Convert.ToInt32(reader.GetValue(5)), Convert.ToInt32(reader.GetValue(6)),
                                        Convert.ToInt64(reader.GetValue(9)), reader.GetValue(10).ToString(), Convert.ToInt32(reader.GetValue(11)),
                                        Convert.ToInt64(reader.GetValue(12)), Convert.ToInt32(reader.GetValue(13)), Convert.ToDateTime(reader.GetValue(14)),
                                        Convert.ToInt64(reader.GetValue(17)), reader.GetValue(18).ToString(), reader.GetValue(19).ToString(),
                                        Convert.ToDateTime(reader.GetValue(20)), Convert.ToInt32(reader.GetValue(21)));

                t_list.Add(t);
            }
            dbconnection.Close();
            return t_list;
        }

        public override List<Ticket> GetList(int page)
        {
            List<Ticket> t_list = new List<Ticket>();
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "select * from public.ticket t inner join public.seat st on t.seat_id = st.id inner join public.hall hl on st.hall_id = hl.id " +
                                   "inner join public.seance sn on t.seance_id = sn.id  inner join public.movie mv on sn.movie_id = mv.id" +
                " LIMIT 10 OFFSET :offset";
            command.Parameters.Add(new NpgsqlParameter("offset", page * 10));
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Ticket t = new Ticket(Convert.ToInt64(reader.GetValue(0)), Convert.ToDateTime(reader.GetValue(1)),
                                        Convert.ToInt64(reader.GetValue(4)), Convert.ToBoolean(reader.GetValue(8)), Convert.ToInt32(reader.GetValue(5)), Convert.ToInt32(reader.GetValue(6)),
                                        Convert.ToInt64(reader.GetValue(9)), reader.GetValue(10).ToString(), Convert.ToInt32(reader.GetValue(11)),
                                        Convert.ToInt64(reader.GetValue(12)), Convert.ToInt32(reader.GetValue(13)), Convert.ToDateTime(reader.GetValue(14)),
                                        Convert.ToInt64(reader.GetValue(17)), reader.GetValue(18).ToString(), reader.GetValue(19).ToString(),
                                        Convert.ToDateTime(reader.GetValue(20)), Convert.ToInt32(reader.GetValue(21)));

                t_list.Add(t);
            }
            dbconnection.Close();
            return t_list;
        }

        public override void Update(Ticket entity)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "UPDATE public.booking SET buy_time = :buy_time, seat_id = :seat_id WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", entity.Id));
            command.Parameters.Add(new NpgsqlParameter("buy_time", entity.BuyTime));
            command.Parameters.Add(new NpgsqlParameter("seat_id", entity.Seat_id));
            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
            }
            catch (PostgresException)
            {
                throw;
            }
            dbconnection.Close();
        }

        public List<Ticket> StaticSearch(SearchData search)
        {
            if (search.buyTime1 >= search.buyTime2) throw new Exception("Wrong date diapason");
            List<Ticket> t_list = new List<Ticket>();
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "select * from public.ticket t inner join public.seat st on t.seat_id = st.id inner join public.hall hl on st.hall_id = hl.id " +
                                   "inner join public.seance sn on t.seance_id = sn.id  inner join public.movie mv on sn.movie_id = mv.id " +
                                   "where t.buy_time >= :b1 and t.buy_time <= :b2 and st.is_free = :is_free";
            command.Parameters.Add(new NpgsqlParameter("b1", search.buyTime1));
            command.Parameters.Add(new NpgsqlParameter("b2", search.buyTime2));
            command.Parameters.Add(new NpgsqlParameter("is_free", search.isFree));
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Ticket t = new Ticket(Convert.ToInt64(reader.GetValue(0)), Convert.ToDateTime(reader.GetValue(1)),
                                        Convert.ToInt64(reader.GetValue(4)), Convert.ToBoolean(reader.GetValue(8)), Convert.ToInt32(reader.GetValue(5)), Convert.ToInt32(reader.GetValue(6)),
                                        Convert.ToInt64(reader.GetValue(9)), reader.GetValue(10).ToString(), Convert.ToInt32(reader.GetValue(11)),
                                        Convert.ToInt64(reader.GetValue(12)), Convert.ToInt32(reader.GetValue(13)), Convert.ToDateTime(reader.GetValue(14)),
                                        Convert.ToInt64(reader.GetValue(17)), reader.GetValue(18).ToString(), reader.GetValue(19).ToString(),
                                        Convert.ToDateTime(reader.GetValue(20)), Convert.ToInt32(reader.GetValue(21)));
                t_list.Add(t);
            }
            dbconnection.Close();
            return t_list;
        }
    }
}
