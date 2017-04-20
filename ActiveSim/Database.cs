using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace ActiveSim
{
    partial class Form1
    {
        private void SimDataLoad()
        {
            // Get all rows of the SimRules table that match the current SimProfile and put in a datatable
            DataTable dt = new DataTable();
            string sql = "select * from SimRules where SimProfile = '" + Globals.sSimProfile + "'";
            dt = GetRows(sql);

            // Create a dictionary and put the collected rows in it
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                // Add only the 'property' and 'value' fields
                dict.Add(row.Field<string>(1), row.Field<string>(2));
            }

            // Now assign properties from the dictionary into the Globals
            string temp;
            if (dict.TryGetValue("CurrencyName", out temp))
            {
                Globals.sCurrencyName = temp;
            }
            if (dict.TryGetValue("Currency", out temp))
            {
                Globals.iCurrency = Convert.ToInt32(temp);
            }
            if (dict.TryGetValue("CarryName", out temp))
            {
                Globals.sCarryName = temp;
            }
            if (dict.TryGetValue("CarryCap", out temp))
            {
                Globals.iCarryCap = Convert.ToInt32(temp);
            }
            if (dict.TryGetValue("Captain", out temp))
            {
                Globals.sCaptain = temp;
            }
            if (dict.TryGetValue("ColorInv", out temp))
            {
                Globals.ColorInv = Convert.ToInt32(temp, 16);
            }
            if (dict.TryGetValue("ColorPresentList", out temp))
            {
                Globals.ColorPresentList = Convert.ToInt32(temp, 16);
            }
            if (dict.TryGetValue("ColorRegList", out temp))
            {
                Globals.ColorRegList = Convert.ToInt32(temp, 16);
            }

           
        }

        // ItemTableLoad to load the full set of items into a new table for item lookup
        private void ItemTableLoad()
        {

            string sql;
            SQLiteCommand sqlcmd;
            try
            {
                // Drop table if it already exists
                sql = "DROP TABLE Items";
                sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
                sqlcmd.ExecuteNonQuery();
            }
            catch
            {
                // Ignore
            }

            // Create table
            sql = "CREATE TABLE Items(AssetNum INTEGER, Name TEXT, Value INTEGER, Weight INTEGER, Type TEXT)";
            sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
            sqlcmd.ExecuteNonQuery();


            // Load all other tables into the item table (for the current sim)
            sql = "select * from Item_Seeds where SimProfile = '" + Globals.sSimProfile + "'";
            sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
            SQLiteDataReader reader = sqlcmd.ExecuteReader();
            ItemLoader(reader, "Seeds");
            reader.Close();

            sql = "select * from Item_Produce where SimProfile = '" + Globals.sSimProfile + "'";
            sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
            reader = sqlcmd.ExecuteReader();
            ItemLoader(reader, "Produce");
            reader.Close();


        }

        private void ItemLoader(SQLiteDataReader reader, string Type)
        {
            // Any items in the reader?
            if (reader.HasRows == false)
            {
                return;
            }

            string AssetNum, Name, Value, Weight;

            while (reader.Read())
            {
                AssetNum = reader["AssetNum"].ToString();
                Name = reader["Name"].ToString();
                Value = reader["Value"].ToString();
                Weight = reader["Weight"].ToString();

                string sql = "INSERT INTO Items VALUES ('" + AssetNum + "', '" + Name + "', '" + Value + "', '" + Weight + "', '" + Type + "')";
                SQLiteCommand sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
                sqlcmd.ExecuteNonQuery();
            }
            reader.Close();
        }


        public DataTable GetRows(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                SQLiteCommand mycommand = new SQLiteCommand(Globals.m_db);
                mycommand.CommandText = sql;
                SQLiteDataReader reader = mycommand.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return dt;
        }

    }
}
