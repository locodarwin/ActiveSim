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
