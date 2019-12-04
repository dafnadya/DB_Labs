using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Model;
using Npgsql;

namespace Lab2.Database
{
    class HallDAO
    {
        private NpgsqlConnection dbconnection;

        public HallDAO()
        {
            dbconnection = new NpgsqlConnection("Server=127.0.0.1; Port=5432; User Id=postgres; Password=aktriso4ka; Database=Cinema;");
        }

        public List<Hall> GetHalls()
        {
            List<Hall> h_list = new List<Hall>();
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "SELECT * FROM public.hall";
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Hall h = new Hall(Convert.ToInt64(reader.GetValue(0)),
                                  reader.GetValue(1).ToString(), Convert.ToInt32(reader.GetValue(2)));
                h_list.Add(h);
            }
            dbconnection.Close();
            return h_list;
        }

        public void CreateHall(Hall entity)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "INSERT INTO public.hall (format, seats_amount) VALUES (:format, :seats_amount)";
            command.Parameters.Add(new NpgsqlParameter("format", entity.Format));
            command.Parameters.Add(new NpgsqlParameter("seats_amount", entity.SeatsAmount));
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

        public List<Seat> GetSeats()
        {
            List<Seat> s_list = new List<Seat>();
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "SELECT * FROM public.seat";
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Seat s = new Seat(Convert.ToInt64(reader.GetValue(0)), Convert.ToInt32(reader.GetValue(1)),
                    Convert.ToInt32(reader.GetValue(2)), Convert.ToInt64(reader.GetValue(3)), Convert.ToBoolean(reader.GetValue(4)));
                s_list.Add(s);
            }
            dbconnection.Close();
            return s_list;
        }

        public List<Seat> GetSeatsInHall(long hall_id)
        {
            List<Seat> s_list = new List<Seat>();
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "SELECT * FROM public.seat where hall_id = :hall_id";
            command.Parameters.Add(new NpgsqlParameter("hall_id", hall_id));
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Seat s = new Seat(Convert.ToInt64(reader.GetValue(0)), Convert.ToInt32(reader.GetValue(1)),
                    Convert.ToInt32(reader.GetValue(2)), Convert.ToInt64(reader.GetValue(3)), Convert.ToBoolean(reader.GetValue(4)));
                s_list.Add(s);
            }
            dbconnection.Close();
            return s_list;
        }

        public void CreateSeat(Seat entity)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "INSERT INTO public.hall (number, row, hall_id, isFree) VALUES (:number, :row, :hall_id, :isFree)";
            command.Parameters.Add(new NpgsqlParameter("number", entity.Number));
            command.Parameters.Add(new NpgsqlParameter("row", entity.Row));
            command.Parameters.Add(new NpgsqlParameter("hall_id", entity.Hall_id));
            command.Parameters.Add(new NpgsqlParameter("isFree", entity.IsFree));
            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
            }
            catch (PostgresException)
            {
                throw new Exception("Unable to create new seat");
            }
            dbconnection.Close();
        }
    }
}
