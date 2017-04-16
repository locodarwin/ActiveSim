using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ActiveSim
{
    public partial class Form1
    {

        private void ButtonInv(int iSess)
        {
            // Get information about the session (name, citnum) and report the command
            int iCitnum = CitGetCitnum(iSess);
            string sName = CitGetName(iCitnum.ToString());
            Stat(1, "HUD CMD", "HUD Command: inv (requested by " + sName + " (" + iCitnum.ToString() + ")", "black");


            // Load up inventory
            string sql = "select * from UserInventory where Citnum = '" + iCitnum.ToString() + "' and SimProfile = '" + Globals.sSimProfile + "'";
            SQLiteCommand sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
            SQLiteDataReader reader = sqlcmd.ExecuteReader();

            // Are there any inventory items for this citnum?
            if (reader.HasRows == false)
            {
                // We've got no rows returned! Respond back with this fact and don't do anything else
                ConsolePrint(iSess, Globals.ColorInv, true, false, "Inventory:\tNo items in inventory.");
            }
            else
            {
                // Yes, there is inventory - display it
                while (reader.Read())
                {

                    string item = reader["Name"].ToString();
                    string value = reader["Value"].ToString();
                    string weight = reader["Weight"].ToString();
                    string type = reader["Type"].ToString();

                    string sOut = item + " [v: " + value + "] [w: " + weight + "] [t: " + type + "]";

                    ConsolePrint(iSess, Globals.ColorInv, true, false, "Inventory:\t" + sOut);

                }
            }
            reader.Close();
        }




    }
}
