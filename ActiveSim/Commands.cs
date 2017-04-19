using System.Data.SQLite;
using System.Data;
using System.Collections.Generic;
using System;

namespace ActiveSim
{
    public partial class Form1
    {


        private void Commands(string sName, int iType, int iSession, string sMsg)
        {
            // Get first letter of what was said command; if not a command, abort command mode
            if (sMsg[0].ToString() != "/")
            {
                return;
            }

            // Strip all unneeded parts off the command and split into parameters
            string strip = sMsg.Substring(1);
            strip = strip.Trim();
            string[] cmd = strip.Split(' ');

            switch (cmd[0])
            {
                case "version":
                    DoVersion(sName, iType, iSession, cmd);
                    break;
                case "ver":
                    DoVersion(sName, iType, iSession, cmd);
                    break;
                case "register":
                    DoRegister(sName, iType, iSession, cmd);
                    break;
                case "de-register":
                    DoDeRegister(sName, iType, iSession, cmd);
                    break;
                case "hud":
                    DoHUD(sName, iType, iSession, cmd);
                    break;
                case "sim":
                    DoSim(sName, iType, iSession, cmd);
                    break;
                case "present":
                    DoPresent(sName, iType, iSession, cmd);
                    break;
                case "console":
                    DoConsole(sName, iType, iSession, cmd);
                    break;
                case "simplayers":
                    DoSimplayers(sName, iType, iSession, cmd);
                    break;
                case "give":
                    DoGive(sName, iType, iSession, cmd);
                    break;
                case "inv":
                    DoInv(sName, iType, iSession, cmd);
                    break;

            }
            //Stat(1, "Test", cmd, "black");
        }

        // Command VERSION
        private void DoVersion(string sName, int iType, int iSess, string[] cmd)
        {
            int iCitnum = GetCitnum(sName);
            Stat(1, "CMD", "Command: version (requested by " + sName + " (" + iCitnum.ToString() + ")", "black");

            // Check permissions
            if (CheckPerms(iCitnum, cmd[0]) == false)
            {
                Response(iSess, iType, "Sorry, " + sName + ", but you do not have permission to use the " + cmd[0] + " command.");
                return;
            }
            Response(iSess, iType, Globals.sAppName + " " + Globals.sVersion + " - " + Globals.sByline);
        }

        // Command REGISTER
        private void DoRegister(string sName, int iType, int iSess, string[] cmd)
        {
            int iCitnum = GetCitnum(sName);
            Stat(1, "CMD", "Command: register (requested by " + sName + " (" + iCitnum.ToString() + ")", "black");

            // Turn cmd into a list
            List<string> lCmd = new List<string>();
            foreach (string d in cmd)
            {
                lCmd.Add(d);
            }

            // Count number of commands parameters. If it's more than one, assume the second is a citname to be registered by the Captain
            if (lCmd.Count == 2)
            {
                // check to see if the asker is the Captain
                if (iCitnum.ToString() == Globals.sCaptain)
                {
                    // If so, let's set the parameters for the registration (name, citnum) accordingly
                    sName = lCmd[1];
                    iCitnum = GetCitnum(sName);

                    // If the citnum comes back as zero, that means the citizen doesn't exist (by that name)
                    if (iCitnum == 0)
                    {
                        Response(iSess, iType, "Error: '" + lCmd[1] + "' is not a valid citizen.");
                        return;
                    }
                }
                else
                {
                    Response(iSess, iType, "Sorry, you do not have permission to register another citizen.");
                    return;
                }
            }


            // Get the default login table from the database
            // Gotta figure out how to best wrap this up in a class or method group
            string sql = "select * from UserSheet where Citnum = '" + iCitnum.ToString() + "' and SimProfile = '" + Globals.sSimProfile + "'";
            SQLiteCommand sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
            SQLiteDataReader reader = sqlcmd.ExecuteReader();

            // Do we have any entries for this citnum?
            if (reader.HasRows == true)
            {
                // We've got rows returned! Respond back with this fact and don't add anything.
                Response(iSess, iType, sName + " (" + iCitnum.ToString() + ") is already registered for this Sim; no action taken.");
                return;
            }
            reader.Close();

            // Add the person's registration to database, use default values in the context of the current Profile (important! Could have many profiles)
            sql = "INSERT INTO UserSheet VALUES ('" + Globals.sSimProfile + "', '" + iCitnum.ToString() + "', '" + sName + "', '" + Globals.iCurrency.ToString() + "', '" + Globals.iCarryCap.ToString() + "')";
            sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
            sqlcmd.ExecuteNonQuery();

            // Add registered simplayer to the CitnumPermissionLevel table
            string level;
            if (iCitnum.ToString() == Globals.sCaptain)
            {
                level = "Captain";
            }
            else
            {
                level = "Simplayer";
            }
            sql = "INSERT INTO CitnumPermissionLevel VALUES ('" + Globals.sSimProfile + "', '" + iCitnum.ToString() + "', '" + level + "')";
            sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
            sqlcmd.ExecuteNonQuery();

            // When we add a Simplayer to the CitnumPermissionLevel table, we need to reload the permissions tables to make sure they're current
            Stat(1, "Permissions", "Reloading permissions tables", "black");
            LoadPerms();

            // If Captain registered someone else, check to see if the registered person is in-world, and if so start their HUD
            if (lCmd.Count == 2)
            {

                if (CitnumIsInWorld(iCitnum.ToString()) == true)
                {
                    int tSess = CitGetSession(Name);
                    DrawHUD(tSess);
                    CitnumUpdateReg(iCitnum.ToString(), "yes");
                    CitnumUpdatePerm(iCitnum.ToString(), level);

                    // update permission level also

                }

            }
            else    // otherwise assume self-registration and just draw HUD for self
            {
                DrawHUD(iSess);
                CitnumUpdateReg(iCitnum.ToString(), "yes");
                CitnumUpdatePerm(iCitnum.ToString(), level);

                //Update permission level also
            }


            // Send stats to global console
            ConsolePrint(0, 0x990000, true, false, "Registration:\tRegistration successful. Stats for " + sName + " (" + iCitnum.ToString() + "):");
            ConsolePrint(0, 0x990000, true, false, "Registration:\t[Currency: " + Globals.iCurrency.ToString() + " " + Globals.sCurrencyName + "s] [Carry capacity: " + Globals.iCarryCap.ToString() + " " + Globals.sCarryName + "s]");
            ConsolePrint(0, 0x990000, true, false, "Registration:\t[Permission level: " + level + "]");


        }

        // Command DE-REGISTER
        private void DoDeRegister(string sName, int iType, int iSess, string[] cmd)
        {


            int iCitnum = GetCitnum(sName);
            Stat(1, "CMD", "Command: de-register (requested by " + sName + " (" + iCitnum.ToString() + ")", "black");

            // this is where the code would go to have Captains do this on behalf of citizenss. First have to check if it was the captain who
            // called the command. Then read the second parameter of the command for the citnum
            // and add the citizen as a Simplayer
            // Turn cmd into a list
            List<string> lCmd = new List<string>();
            foreach (string d in cmd)
            {
                lCmd.Add(d);
            }

            // Count number of commands parameters. If it's more than one, assume the second is a citnum to be de-registered by the Captain
            if (lCmd.Count == 2)
            {
                // check to see if the asker is the Captain
                if (iCitnum.ToString() == Globals.sCaptain)
                {

                    // If so, let's set the parameters for the registration (name, citnum) accordingly
                    sName = lCmd[1];
                    iCitnum = GetCitnum(sName);

                    // If the citnum comes back as zero, that means the citizen doesn't exist (by that name)
                    if (iCitnum == 0)
                    {
                        Response(iSess, iType, "Error: '" + lCmd[1] + "' is not a valid citizen.");
                        return;
                    }

                }
                else
                {
                    Response(iSess, iType, "Sorry, you do not have permission to de-register another citizen.");
                    return;
                }
            }

            string sql = "select * from UserSheet where Citnum = '" + iCitnum.ToString() + "' and SimProfile = '" + Globals.sSimProfile + "'";
            SQLiteCommand sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
            SQLiteDataReader reader = sqlcmd.ExecuteReader();

            // Do we have any entries for this citnum?
            if (reader.HasRows == false)
            {
                // We've got no rows returned! Respond back with this fact and don't add anything.
                Response(iSess, iType, "Citnum " + iCitnum + " is not registered for this Sim; no action taken.");
                return;
            }
            reader.Close();

            // Remove the person's registration from database
            sql = "DELETE FROM UserSheet WHERE SimProfile = '" + Globals.sSimProfile + "' AND Citnum = '" + iCitnum.ToString() + "'";
            sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
            sqlcmd.ExecuteNonQuery();

            // Remove them also from the citnum permissions level table
            sql = "DELETE FROM CitnumPermissionLevel WHERE SimProfile = '" + Globals.sSimProfile + "' AND Citnum = '" + iCitnum.ToString() + "'";
            sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
            sqlcmd.ExecuteNonQuery();

            // Delete the HUD, and remove from CitTable
            if (lCmd.Count == 2)
            {
                // Get session of the other citizen
                string Name = CitGetName(iCitnum.ToString());
                int tSess = CitGetSession(Name);
                
                CitnumUpdateReg(iCitnum.ToString(), "no");
                if (tSess != 0)
                {
                    EraseHUD(tSess);
                }
                
            }
            else
            {
                CitnumUpdateReg(iCitnum.ToString(), "no");
                EraseHUD(iSess);
            }

            // When we delete a Simplayer from the CitnumPermissionLevel table, we need to reload the permissions tables to make sure they're current
            Stat(1, "Permissions", "Reloading permissions tables", "black");
            LoadPerms();

            Response(iSess, iType, "De-registration successful. Citnum " + iCitnum.ToString() + " was removed from the Sim database.");

        }

        // Command HUD
        private void DoHUD(string sName, int iType, int iSess, string[] cmd)
        {
            int iCitnum = GetCitnum(sName);
            Stat(1, "CMD", "Command: HUD (requested by " + sName + " (" + iCitnum.ToString() + ")", "black");

            // Check permissions
            if (CheckPerms(iCitnum, cmd[0]) == false)
            {
                Response(iSess, iType, "Sorry, " + sName + ", but you do not have permission to use the " + cmd[0] + " command.");
                return;
            }


            if (cmd[1] == "start")
            {

                DrawHUD(iSess);
            }

            if (cmd[1] == "stop")
            {
                EraseHUD(iSess);
            }

            if (cmd[1] == "eraseall")
            {
                EraseHUD(0);
            }


        }

        // Command SIM
        private void DoSim(string sName, int iType, int iSess, string[] cmd)
        {
            int iCitnum = GetCitnum(sName);
            Stat(1, "CMD", "Command: sim (requested by " + sName + " (" + iCitnum.ToString() + ")", "black");

            // Check permissions
            if (CheckPerms(iCitnum, cmd[0]) == false)
            {
                Response(iSess, iType, "Sorry, " + sName + ", but you do not have permission to use the " + cmd[0] + " command.");
                return;
            }

            // For each of these, add the ability to start different profiles sometime

            if (cmd[1] == "start")
            {
                // Disabled the buttons for this and sim config, enable the button for "stop sim"
                butSimStart.Enabled = false;
                butSimConfig.Enabled = false;
                butSimStop.Enabled = true;

                // Load Sim Data
                SimDataLoad();

                // Load permissions dictionaries
                LoadPerms();
                Globals.iSimRun = true;

                // Respond command complete
                Stat(1, "Sim Start", "Started Active Simulator profile '" + Globals.sSimProfile + "'", "black");
                Response(iSess, iType, "Started Active Simulator profile '" + Globals.sSimProfile + "'");

            }
            if (cmd[1] == "stop")
            {
                // Disabled the buttons for this and enable sim config & start sim
                butSimStart.Enabled = true;
                butSimConfig.Enabled = true;
                butSimStop.Enabled = false;

                Globals.iSimRun = false;

                // Respond command complete
                Stat(1, "Sim Stop", "Stopped Active Simulator profile '" + Globals.sSimProfile + "'", "black");
                Response(iSess, iType, "Stopped Active Simulator profile '" + Globals.sSimProfile + "'");
            }

        }

        // Command Present
        private void DoPresent(string sName, int iType, int iSess, string[] cmd)
        {
            int iCitnum = GetCitnum(sName);
            Stat(1, "CMD", "Command: present (requested by " + sName + " (" + iCitnum.ToString() + ")", "black");
            Chat(1, "Bot", "The Captain is " + Globals.sCaptain, "black"); 

            // Check permissions
            if (CheckPerms(iCitnum, cmd[0]) == false)
            {
                Response(iSess, iType, "Sorry, " + sName + ", but you do not have permission to use the " + cmd[0] + " command.");
                return;
            }

            String q = "";
            List<string> Names = new List<string>();
            List<string> Citnums = new List<string>();
            List<string> Registered = new List<string>();
            List<string> PermissionLevel = new List<string>();
            foreach (DataRow dd in Globals.CitTable.Rows)
            {
                Names.Add(dd.Field<string>(0));
                Citnums.Add(dd.Field<string>(4));
                Registered.Add(dd.Field<string>(2));
                PermissionLevel.Add(dd.Field<string>(3));
            }
            int i = 0;
            foreach (string d in Names)
            {
                q = d + " (" + Citnums[i] + ") -- ";
                if (Registered[i] == "yes")
                {
                    q = q + "Registered (" + PermissionLevel[i] + ")";
                }
                else
                {
                    q = q + "Not registered";
                }
                ConsolePrint(iSess, Globals.ColorPresentList, false, false, q);
                i = i + 1;
            }

        }

        // Command Console
        private void DoConsole(string sName, int iType, int iSess, string[] cmd)
        {
            ConsolePrint(iSess, 0x009999, true, false, "Testing:\tThis is a test message.");
        }


        // Command SIMPLAYERS
        private void DoSimplayers(string sName, int iType, int iSess, string[] cmd)
        {


            int iCitnum = GetCitnum(sName);
            Stat(1, "CMD", "Command: simplayers (requested by " + sName + " (" + iCitnum.ToString() + ")", "black");

            // Check permissions
            if (CheckPerms(iCitnum, cmd[0]) == false)
            {
                Response(iSess, iType, "Sorry, " + sName + ", but you do not have permission to use the " + cmd[0] + " command.");
                return;
            }

            string sql = "select * from UserSheet";
            SQLiteCommand sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
            SQLiteDataReader reader = sqlcmd.ExecuteReader();

            // Do we have any entries for this citnum?
            if (reader.HasRows == false)
            {
                // We've got no rows returned! Respond back with this fact and don't add anything.
                Response(iSess, iType, "There are no registered citizens in this Sim.");
            }
            else
            {
                while (reader.Read())
                {
                    // do stuff
                    int num = Convert.ToInt32(reader["Citnum"]);
                    string sOut = reader["Citname"].ToString() + " (";
                    sOut = sOut + num + ") -- ";
                    sOut = sOut + GetPermLevel(num);

                    ConsolePrint(iSess, Globals.ColorRegList, false, false, sOut);

                    //Response(iSess, iType, "There ARE registered citizens in this Sim.");
                }
            }
            reader.Close();
            
        }


        // DoGive /give [citname] [assetnum] [type]
        private void DoGive(string sName, int iType, int iSess, string[] cmd)
        {
            int iCitnum = GetCitnum(sName);
            
            Stat(1, "CMD", "Command: /give (requested by " + sName + " (" + iCitnum.ToString() + ")", "black");

            // Check permissions
            if (CheckPerms(iCitnum, cmd[0]) == false)
            {
                Response(iSess, iType, "Sorry, " + sName + ", but you do not have permission to use the " + cmd[0] + " command.");
                return;
            }

            // Make sure the command has the right number of parameters
            if (cmd.Length != 4)
            {
                Response(iSess, iType, "Error: wrong number of parameters. Usage: /give [citname] [assetnum] [type]");
                return;
            }

            int rCitnum = GetCitnum(cmd[1]);

            // Check to make sure citnum we want to give to is registered
            if (CheckRegistered(cmd[1]) == false)
            {
                Response(iSess, iType, "Error: " + cmd[1] + " (" + rCitnum + ") is not a registered simplayer.");
                return;
            }

            // Give to citnum
            bool rc = AddAssetToInv(rCitnum, Convert.ToInt32(cmd[2]), cmd[3], 4);
            if (rc == true)
            {
                Response(iSess, iType, "AssetNum " + cmd[2] + " of type " + cmd[3] + " given to " + cmd[1]);
            }
            else
            {
                Response(iSess, iType, "Failed: AssetNum " + cmd[2] + " of type " + cmd[3] + " was not given to " + cmd[1]);
            }

        }


        // DoInv /inv and inv [simplayer]
        private void DoInv(string sName, int iType, int iSess, string[] cmd)
        {
            int iCitnum = GetCitnum(sName);
            Stat(1, "CMD", "Command: inv (requested by " + sName + " (" + iCitnum.ToString() + ")", "black");

            // Check permissions
            if (CheckPerms(iCitnum, cmd[0]) == false)
            {
                Response(iSess, iType, "Sorry, " + sName + ", but you do not have permission to use the " + cmd[0] + " command.");
                return;
            }

            // If the command has more than one parameter, test to see if it's the captain who issued it, then set parameters accordingly
            if (cmd.Length > 1)
            {

                // check to see if the asker is the Captain
                if (iCitnum.ToString() == Globals.sCaptain)
                {
                    // If so, let's set the parameters for the registration (name, citnum) accordingly
                    sName = cmd[1];
                    iCitnum = GetCitnum(sName);

                    // check to see if the citnum is real or not
                    if (iCitnum == 0)
                    {
                        Response(iSess, iType, "Error: '" + cmd[1] + "' is not a valid citizen.");
                        return;
                    }
                }
                else
                {
                    Response(iSess, iType, "Sorry, but you do not have permission to view the inventory of another citizen.");
                    return;
                }

            }

            // We need to check to make sure the citnum is registered
            if (CitIsRegistered(sName) == false)
            {
                Response(iSess, iType, "Error: citizen " + sName + " (" + iCitnum + ") is not a registered simplayer.");
                return;
            }

            // Passed all the tests, now let's load up the inventory of the citnum
            string sql = "select * from UserInventory where Citnum = '" + iCitnum.ToString() + "' and SimProfile = '" + Globals.sSimProfile + "'";
            SQLiteCommand sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
            SQLiteDataReader reader = sqlcmd.ExecuteReader();

            // Are there any inventory items for this citnum?
            if (reader.HasRows == false)
            {
                // We've got no rows returned! Respond back with this fact and don't do anything else
                ConsolePrint(iSess, Globals.ColorInv, true, false, "Inventory\tNo items in inventory.");
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

                    ConsolePrint(iSess, Globals.ColorInv, true, false, "Inventory\t" + sOut);

                }
            }
            reader.Close();
            

        }


    }
}
