using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Model;
using Npgsql;

namespace Lab2.Database
{
    class SeanceDAO : DAO<Seance>
    {
        public SeanceDAO() : base() { }

        public override void Create(Seance entity)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "INSERT INTO public.seance (price, time, movie_id, hall_id) VALUES (:price, :time, :movie_id, :hall_id)";
            command.Parameters.Add(new NpgsqlParameter("price", entity.Price));
            command.Parameters.Add(new NpgsqlParameter("time", entity.Time));
            command.Parameters.Add(new NpgsqlParameter("movie_id", entity.Movie_id));
            command.Parameters.Add(new NpgsqlParameter("hall_id", entity.Hall_id));
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
            command.CommandText = "DELETE FROM public.seance WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            command.ExecuteNonQuery();
            dbconnection.Close();
        }

        public override Seance Get(long id)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "SELECT * FROM public.seance sn INNER JOIN public.movie mov ON sn.movie_id = mov.id WHERE sn.id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            NpgsqlDataReader reader = command.ExecuteReader();
            Seance s = null;
            while (reader.Read())
            {
                s = new Seance(Convert.ToInt64(reader.GetValue(0)), Convert.ToInt32(reader.GetValue(1)),
                                  Convert.ToDateTime(reader.GetValue(2)), Convert.ToInt64(reader.GetValue(5)),
                                  reader.GetValue(6).ToString(), reader.GetValue(7).ToString(),
                                  Convert.ToDateTime(reader.GetValue(8)), Convert.ToInt32(reader.GetValue(9)),
                                  Convert.ToInt64(reader.GetValue(4)));
            }
            dbconnection.Close();
            return s;
        }

        public override List<Seance> GetList()
        {
            List<Seance> seances = new List<Seance>();
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "SELECT * FROM public.seance sn INNER JOIN public.movie mov ON sn.movie_id = mov.id";
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Seance s = new Seance(Convert.ToInt64(reader.GetValue(0)), Convert.ToInt32(reader.GetValue(1)),
                                  Convert.ToDateTime(reader.GetValue(2)), Convert.ToInt64(reader.GetValue(5)),
                                  reader.GetValue(6).ToString(), reader.GetValue(7).ToString(),
                                  Convert.ToDateTime(reader.GetValue(8)), Convert.ToInt32(reader.GetValue(9)),
                                  Convert.ToInt64(reader.GetValue(4)));
                seances.Add(s);
            }
            dbconnection.Close();
            return seances;
        }

        public override List<Seance> GetList(int page)
        {
            List<Seance> seances = new List<Seance>();
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "SELECT * FROM public.seance sn INNER JOIN public.movie mov ON sn.movie_id = mov.id" +
                " LIMIT 10 OFFSET :offset";
            command.Parameters.Add(new NpgsqlParameter("offset", page * 10));
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Seance s = new Seance(Convert.ToInt64(reader.GetValue(0)), Convert.ToInt32(reader.GetValue(1)),
                                  Convert.ToDateTime(reader.GetValue(2)), Convert.ToInt64(reader.GetValue(5)),
                                  reader.GetValue(6).ToString(), reader.GetValue(7).ToString(),
                                  Convert.ToDateTime(reader.GetValue(8)), Convert.ToInt32(reader.GetValue(9)),
                                  Convert.ToInt64(reader.GetValue(4)));
                seances.Add(s);
            }
            dbconnection.Close();
            return seances;
        }

        public override void Update(Seance entity)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "UPDATE public.seance SET price = :price, time = :time, movie_id = :movie_id, hall_id = :hall_id WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", entity.Id));
            command.Parameters.Add(new NpgsqlParameter("price", entity.Price));
            command.Parameters.Add(new NpgsqlParameter("time", entity.Time));
            command.Parameters.Add(new NpgsqlParameter("movie_id", entity.Movie_id));
            command.Parameters.Add(new NpgsqlParameter("hall_id", entity.Hall_id));
            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
            }
            catch (PostgresException)
            {
                throw new Exception("Unable to edit room");
            }
            dbconnection.Close();
        }
    }
}
