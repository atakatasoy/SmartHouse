using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse
{
    public static class Helper
    {
        private static MySqlConnection Connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["BASE"].ConnectionString);

        public static void FetchOptionsFromDb(this List<string> list)
        {
            string komut = "SELECT * FROM rooms;";
            using(DataTable table=new DataTable())
            using (MySqlCommand command = new MySqlCommand(komut, Connection))
            using(MySqlDataAdapter adapter=new MySqlDataAdapter(command))
            {
                Connection.Open();
                adapter.Fill(table);
                for(int i = 0; i < table.Rows.Count; i++)
                {
                    list.Add(table.Rows[i]["roomname"].ToString());
                }
                Connection.Close();
            }
        }
    }
}
