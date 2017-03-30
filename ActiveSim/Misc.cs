using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace ActiveSim
{
    public partial class Form1
    {
        private void LoadPerms()
        {
            // Get all rows of the CitnumPermissionLevel table that match the current SimProfile and put in a datatable
            DataTable dt = new DataTable();
            string sql = "select * from CitnumPermissionLevel where SimProfile = '" + Globals.sSimProfile + "'";
            dt = GetRows(sql);

            // Initialize the dictionary, as it may have been used once before
            Globals.CitnumPermLevel.Clear();

            // Put the collected rows in the dictionary
            foreach (DataRow row in dt.Rows)
            {
                // Add only the 'Citnum' and 'PermissionGroup' fields
                Globals.CitnumPermLevel.Add(row.Field<string>(1), row.Field<string>(2));
            }

            // Get all rows of the CMDPermissionLevels table that match the current SimProfile and put in a datatable
            dt.Clear();
            sql = "select * from CMDPermissionLevel where SimProfile = '" + Globals.sSimProfile + "'";
            dt = GetRows(sql);

            // Initialize the dictionary, as it may have been used once before
            Globals.CMDPermLevel.Clear();
            Globals.CMDPermLevel.Columns.Clear();
            Globals.CMDPermLevel.Columns.Add("PermissionGroup", typeof(string));
            Globals.CMDPermLevel.Columns.Add("Command", typeof(string));

            // Put the collected rows in the dictionary
            foreach (DataRow row in dt.Rows)
            {
                // Add only the 'PermissionGroup' and 'Command' fields
                Globals.CMDPermLevel.Rows.Add(row.Field<string>(1), row.Field<string>(2));
            }


            return;
        }


        private bool CheckPerms(int iCitnum, string sCommand)
        {
            // Get all rows of the SimRules table that match the current SimProfile and put in a datatable
            string temp;
            string tempPermGroup;

            Stat(1, "PermCheck", "Checking permissions for Citnum" + iCitnum.ToString(), "black");

            if (Globals.CitnumPermLevel.TryGetValue(iCitnum.ToString(), out temp))
            {
                tempPermGroup = temp;
            }
            else
            {
                Stat(1, "PermCheck", "Perm check failed!", "black");
                return false;
            }

            // test every element in the dictionary for the command/permlevel combo
            bool GotIt = false;
            foreach(DataRow row in Globals.CMDPermLevel.Rows)
            {
                if (row.Field<string>(0) == temp)
                {
                    if (row.Field<string>(1) == sCommand)
                    {
                        GotIt = true;
                        Stat(1, "PermCheck", "Perm check passed!", "black");
                    }
                }
            }
            if (GotIt == false)
            {
                Stat(1, "PermCheck", "Perm check failed!", "black");
            }
            return GotIt;
        }


    }
}
