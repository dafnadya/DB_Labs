using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Model;
using Npgsql;

namespace Lab2.Database
{
    class MovieDAO : DAO<Movie>
    {
        public MovieDAO() : base() { }

        public override void Create(Movie entity)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "INSERT INTO public.movie (title, country, release_date, age_limit) VALUES (:title, :country, :release_date, :age_limit)";
            command.Parameters.Add(new NpgsqlParameter("title", entity.Title));
            command.Parameters.Add(new NpgsqlParameter("country", entity.Country));
            command.Parameters.Add(new NpgsqlParameter("release_date", entity.ReleaseDate));
            command.Parameters.Add(new NpgsqlParameter("age_limit", entity.AgeLimit));
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
            command.CommandText = "DELETE FROM public.movie WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            command.ExecuteNonQuery();
            dbconnection.Close();
        }

        public override Movie Get(long id)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "SELECT * FROM public.movie WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            NpgsqlDataReader reader = command.ExecuteReader();
            Movie m = null;
            while (reader.Read())
            {
                m = new Movie(Convert.ToInt64(reader.GetValue(0)), reader.GetValue(1).ToString(), 
                    reader.GetValue(2).ToString(), Convert.ToDateTime(reader.GetValue(3)),
                    Convert.ToInt16(reader.GetValue(4)));
            }
            dbconnection.Close();
            return m;
        }

        public override List<Movie> GetList()
        {
            List<Movie> movies = new List<Movie>();
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "SELECT * FROM public.movie";
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Movie mov = new Movie(Convert.ToInt64(reader.GetValue(0)), reader.GetValue(1).ToString(),
                    reader.GetValue(2).ToString(), Convert.ToDateTime(reader.GetValue(3)),
                    Convert.ToInt16(reader.GetValue(4)));
                movies.Add(mov);
            }
            dbconnection.Close();
            return movies;
        }

        public override List<Movie> GetList(int page)
        {
            List<Movie> movies = new List<Movie>();
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "SELECT * FROM public.movie LIMIT 10 OFFSET :offset";
            command.Parameters.Add(new NpgsqlParameter("offset", page * 10));
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Movie mov = new Movie(Convert.ToInt64(reader.GetValue(0)), reader.GetValue(1).ToString(),
                    reader.GetValue(2).ToString(), Convert.ToDateTime(reader.GetValue(3)),
                    Convert.ToInt16(reader.GetValue(4)));
                movies.Add(mov);
            }
            dbconnection.Close();
            return movies;
        }

        public override void Update(Movie entity)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "UPDATE public.movie SET title = :title, country = :country, release_date = :release_date, age_limit = :age_limit WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("title", entity.Id));
            command.Parameters.Add(new NpgsqlParameter("country", entity.Country));
            command.Parameters.Add(new NpgsqlParameter("release_date", entity.ReleaseDate));
            command.Parameters.Add(new NpgsqlParameter("age_limit", entity.AgeLimit));
            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
            }
            catch (PostgresException)
            {
                throw new Exception("Unable to edit movie");
            }
            dbconnection.Close();
        }
    }
}
