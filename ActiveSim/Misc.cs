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

            // Put the collected rows in the dictionary
            foreach (DataRow row in dt.Rows)
            {
                // Add only the 'PermissionGroup' and 'Command' fields
                Globals.CMDPermLevel.Add(row.Field<string>(1), row.Field<string>(2));
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

            if (Globals.CMDPermLevel.TryGetValue(tempPermGroup, out temp))
            {
                // do stuff here!  Need a foreach to wrap this whole thing, prolly



            }



            return false;
        }


    }
}
