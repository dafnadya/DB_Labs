using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Model;
using Npgsql;

namespace Lab2.Database
{
    class SearchRes
    {
        private long id;
        private string attr;
        private string ts_headline;

        public SearchRes(long id, string attr, string ts_headline)
        {
            this.id = id;
            this.attr = attr;
            this.ts_headline = ts_headline;
        }

        public long Id { get => id; set => id = value; }
        public string Attr { get => attr; set => attr = value; }
        public string Ts_headline { get => ts_headline; set => ts_headline = value; }
    }

    class FullTextSearch
    {
        private NpgsqlConnection dbconnection;

        public FullTextSearch()
        {
            dbconnection = new NpgsqlConnection("Server=127.0.0.1; Port=5432; User Id=postgres; Password=aktriso4ka; Database=Cinema;");
        }

        public List<SearchRes> getFullPhrase(string atr, string table, string phrase)
        {
            List<SearchRes> list = new List<SearchRes>();
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = $"SELECT id, {atr}, ts_headline(\"{atr}\", q) FROM {table}, phraseto_tsquery('{phrase}') AS q WHERE to_tsvector({table}.{atr}) @@ q";
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                SearchRes s = new SearchRes(Convert.ToInt64(reader.GetValue(0).ToString()), reader.GetValue(1).ToString(), reader.GetValue(2).ToString());
                list.Add(s);
            }
            dbconnection.Close();
            return list;
        }


        public List<SearchRes> getAllWithNotIncludedWord(string atr, string table, string phrase)
        {
            List<SearchRes> list = new List<SearchRes>();
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = $"SELECT id, {atr} FROM {table} WHERE NOT(to_tsvector({table}.{atr}) @@ to_tsquery('{phrase}'))";
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                SearchRes s = new SearchRes(Convert.ToInt64(reader.GetValue(0).ToString()), reader.GetValue(1).ToString(), null);
                list.Add(s);
            }
            dbconnection.Close();
            return list;
        }
    }
}
