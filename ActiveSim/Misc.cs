using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using AW;

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

            bool GotIt = false;

            // If it's the capain, automatically return true
            if (iCitnum.ToString() == Globals.sCaptain)
            {
                Stat(1, "PermCheck", "Permission check automatic pass - Captain issued", "black");
                return true;
            }

            // Get all rows of the SimRules table that match the current SimProfile and put in a datatable
            string temp;
            string tempPermGroup;

            Stat(1, "PermCheck", "Checking permissions for Citnum " + iCitnum.ToString(), "black");

            if (Globals.CitnumPermLevel.TryGetValue(iCitnum.ToString(), out temp))
            {
                tempPermGroup = temp;
            }
            else
            {
                Stat(1, "PermCheck", "Permission check failed!", "black");
                return false;
            }

            // test every element in the dictionary for the command/permlevel combo
            
            foreach(DataRow row in Globals.CMDPermLevel.Rows)
            {
                if (row.Field<string>(0) == temp)
                {
                    if (row.Field<string>(1) == sCommand)
                    {
                        GotIt = true;
                        Stat(1, "PermCheck", "Permission check passed!", "black");
                    }
                }
            }
            if (GotIt == false)
            {
                Stat(1, "PermCheck", "Permission check failed!", "black");
            }
            return GotIt;
        }

        private bool CheckRegistered(string sName)
        {
            int iCitnum = GetCitnum(sName);
            bool bStat;

            string sql = "select * from UserSheet where Citnum = '" + iCitnum.ToString() + "' and SimProfile = '" + Globals.sSimProfile + "'";
            SQLiteCommand sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
            SQLiteDataReader reader = sqlcmd.ExecuteReader();

            // Do we have any entries for this citnum?
            if (reader.HasRows == true)
            {
                // We've got rows returned! The user is registered; return true
                bStat = true;
            }
            else
            {
                bStat = false;
            }
            reader.Close();
            return bStat;
        }

        private void ConsolePrint(int iSess, int color, bool bold, bool italics, string msg)
        {
            _instance.Attributes.ConsoleColor = color;
            _instance.Attributes.ConsoleBold = bold;
            _instance.Attributes.ConsoleItalics = italics;
            _instance.Attributes.ConsoleMessage = "        " + msg;
            var rc = _instance.ConsoleMessage(iSess);
            if (rc != AW.Result.Success)
            {
                Console.WriteLine(rc);
            }

        }

        private void DrawHUD(int iSess)
        {
            
            _instance.Attributes.HudElementType = AW.HudType.Image;
            _instance.Attributes.HudElementText = "/hud/attack-yes.jpg";
            _instance.Attributes.HudElementId = 1;
            _instance.Attributes.HudElementSession = iSess;
            _instance.Attributes.HudElementOrigin = AW.HudOrigin.Left;
            _instance.Attributes.HudElementOpacity = 1.0f;
            _instance.Attributes.HudElementX = 0;
            _instance.Attributes.HudElementY = 0;
            _instance.Attributes.HudElementZ = 2;
            _instance.Attributes.HudElementFlags = AW.HudElementFlag.Clicks;
            _instance.Attributes.HudElementColor = 0xFFFFFF;
            _instance.Attributes.HudElementSizeX = 64;
            _instance.Attributes.HudElementSizeY = 64;

            _instance.HudCreate();

            _instance.Attributes.HudElementType = AW.HudType.Image;
            _instance.Attributes.HudElementText = "/hud/trade-yes.jpg";
            _instance.Attributes.HudElementId = 2;
            _instance.Attributes.HudElementSession = iSess;
            _instance.Attributes.HudElementOrigin = AW.HudOrigin.Left;
            _instance.Attributes.HudElementOpacity = 1.0f;
            _instance.Attributes.HudElementX = 64;
            _instance.Attributes.HudElementY = 0;
            _instance.Attributes.HudElementZ = 2;
            _instance.Attributes.HudElementFlags = AW.HudElementFlag.Clicks;
            _instance.Attributes.HudElementColor = 0xFFFFFF;
            _instance.Attributes.HudElementSizeX = 64;
            _instance.Attributes.HudElementSizeY = 64;

            _instance.HudCreate();


            _instance.Attributes.HudElementType = AW.HudType.Image;
            _instance.Attributes.HudElementText = "/hud/ntdibut.jpg";
            _instance.Attributes.HudElementId = 3;
            _instance.Attributes.HudElementSession = iSess;
            _instance.Attributes.HudElementOrigin = AW.HudOrigin.Left;
            _instance.Attributes.HudElementOpacity = 1.0f;
            _instance.Attributes.HudElementX = 0;
            _instance.Attributes.HudElementY = 64;
            _instance.Attributes.HudElementZ = 2;
            _instance.Attributes.HudElementFlags = AW.HudElementFlag.Clicks;
            _instance.Attributes.HudElementColor = 0xFFFFFF;
            _instance.Attributes.HudElementSizeX = 128;
            _instance.Attributes.HudElementSizeY = 32;

            _instance.HudCreate();

            _instance.Attributes.HudElementType = AW.HudType.Image;
            _instance.Attributes.HudElementText = "/hud/ntdibut.jpg";
            _instance.Attributes.HudElementId = 4;
            _instance.Attributes.HudElementSession = iSess;
            _instance.Attributes.HudElementOrigin = AW.HudOrigin.Left;
            _instance.Attributes.HudElementOpacity = 1.0f;
            _instance.Attributes.HudElementX = 0;
            _instance.Attributes.HudElementY = 96;
            _instance.Attributes.HudElementZ = 2;
            _instance.Attributes.HudElementFlags = AW.HudElementFlag.Clicks;
            _instance.Attributes.HudElementColor = 0xFFFFFF;
            _instance.Attributes.HudElementSizeX = 128;
            _instance.Attributes.HudElementSizeY = 32;

            _instance.HudCreate();

            _instance.Attributes.HudElementType = AW.HudType.Image;
            _instance.Attributes.HudElementText = "/hud/ntdibut.jpg";
            _instance.Attributes.HudElementId = 5;
            _instance.Attributes.HudElementSession = iSess;
            _instance.Attributes.HudElementOrigin = AW.HudOrigin.Left;
            _instance.Attributes.HudElementOpacity = 1.0f;
            _instance.Attributes.HudElementX = 0;
            _instance.Attributes.HudElementY = 128;
            _instance.Attributes.HudElementZ = 2;
            _instance.Attributes.HudElementFlags = AW.HudElementFlag.Clicks;
            _instance.Attributes.HudElementColor = 0xFFFFFF;
            _instance.Attributes.HudElementSizeX = 128;
            _instance.Attributes.HudElementSizeY = 32;

            _instance.HudCreate();

        }

        private void EraseHUD(int iSess)
        {
            _instance.HudDestroy(iSess, 1);
            _instance.HudDestroy(iSess, 2);
            _instance.HudDestroy(iSess, 3);
            _instance.HudDestroy(iSess, 4);
            _instance.HudDestroy(iSess, 5);
        }


        // Method to get citizen number
        private int GetCitnum(string sName)
        {
            _instance.CitizenAttributesByName(sName);
            return _instance.Attributes.CitizenNumber;
        }




        private string CitTableAdd(string Name, int iSess, string Citnum)
        {
            string Registered;
            string PermLevel = "Citizen";
            
            // Check to see if the citizen is already registered for the sim, if not, their perm level is "Citizen," which has no permissions
            if (CheckRegistered(Name) == true)
            {
                Registered = "yes";
            }
            else
            {
                Registered = "no";
                PermLevel = "Citizen";
            }

            // If registered, get his/her permission level
            if (Registered == "yes")
            {
                string temp;
                int iCitnum = GetCitnum(Name);
                if (Globals.CitnumPermLevel.TryGetValue(iCitnum.ToString(), out temp))
                {
                    PermLevel = temp; // Found the permlevel
                }
                else
                {
                    PermLevel = "Simplayer";  // default for registered Simplayers, in case for some reason not found in Permlevel Dictionary
                }
            }
            Globals.CitTable.Rows.Add(Name, iSess, Registered, PermLevel, Citnum);
            return Registered;
        }

        private bool CitIsRegistered(string Name)
        {
            DataRow[] check = Globals.CitTable.Select("Name = '" + Name + "'");
            string Registered = "no";
            int rows = check.Count();
            if (rows != 0)
            {
                foreach (DataRow z in check)
                {
                    Registered = z.Field<string>(2);
                }
            }
            if (Registered == "yes")
            {
                return true;
            }

            return false;
        }

        private void CitnameUpdateReg(string Name, string Reg)
        {
            foreach (DataRow dr in Globals.CitTable.Rows)
            {
                if (dr[0].ToString() == Name)
                {
                    dr[2] = Reg;
                }
            }

        }

        private void CitnumUpdatePerm(string Citnum, string Perm)
        {

            foreach (DataRow dr in Globals.CitTable.Rows)
            {
                if (dr[4].ToString() == Citnum)
                {
                    dr[3] = Perm;
                    //Console.WriteLine("I actually updated my perm status in the CitTable after registering.");
                }
            }

        }

        private void CitnumUpdateReg(string Citnum, string Reg)
        {

            foreach (DataRow dr in Globals.CitTable.Rows)
            {
                if (dr[4].ToString() == Citnum)
                {
                    dr[2] = Reg;
                    //Console.WriteLine("I actually updated my reg status in the CitTable after registering.");
                }
            }

        }

        private bool CitnumIsInWorld(string Citnum)
        {
            foreach (DataRow dr in Globals.CitTable.Rows)
            {
                if (dr[4].ToString() == Citnum)
                {
                    return true;
                }
            }
            return false;
        




        //    DataRow[] check = Globals.CitTable.Select("Name = '" + Name + "'");
        //    int rows = check.Count();
        //    if (rows != 0)
        //    {
        //        return true;
        //    }
        //    return false;
        }

        private string CitGetCitnum(string Name)
        {
            string Citnum = "0";
            foreach (DataRow dr in Globals.CitTable.Rows)
            {
                if (dr["Name"] == Name)
                {
                    Citnum = dr["Citnum"].ToString();
                }
            }
            return Citnum;

        }

        private int CitGetSession(string Name)
        {
            DataRow[] check = Globals.CitTable.Select("Name = '" + Name + "'");
            int Sess = 0;
            int rows = check.Count();
            if (rows != 0)
            {
                foreach (DataRow z in check)
                {
                    Sess = z.Field<int>(1);
                }
            }
            return Sess;
        }

        private string CitGetName(string Citnum)
        {
            DataRow[] check = Globals.CitTable.Select("Citnum = '" + Citnum + "'");
            string Name = "Name";
            int rows = check.Count();
            if (rows != 0)
            {
                foreach (DataRow z in check)
                {
                    Name = z.Field<string>(4);
                }
            }
            return Name;
        }


    }
}
