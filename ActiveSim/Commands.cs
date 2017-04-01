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

            }
            //Stat(1, "Test", cmd, "black");
        }

        // Method to respond back on the results of the command
        private void Response(int iSess, int iType, string sMsg)
        {
            if (iType == 2)
            {
                _instance.Whisper(iSess, sMsg);
                Stat(1, "Response", "(whispered): " + sMsg, "blue");
                Chat(1, Globals.sBotName, "(whispered): " + sMsg, "blue");
            }
            else
            {
                _instance.Say(sMsg);
                Stat(1, "Response", sMsg, "black");
                Chat(1, Globals.sBotName, sMsg, "black");
            }

        }

        // Method to get citizen number
        private int GetCitnum(string sName)
        {
            _instance.CitizenAttributesByName(sName);
            return _instance.Attributes.CitizenNumber;
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

            // Count number of commands parameters. If it's more than one, assume the second is a citnum to be registered by the Captain
            if (lCmd.Count == 2)
            {
                // check to see if the asker is the Captain
                if (iCitnum.ToString() == Globals.sCaptain)
                {

                    // Make sure the second command is a number
                    try
                    {
                        iCitnum = Convert.ToInt32(lCmd[1]);
                    }
                    catch (Exception e)
                    {
                        Response(iSess, iType, "Error: '" + lCmd[1] + "' is not a valid citnum.");
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
                Response(iSess, iType, "Citnum " + iCitnum + " is already registered for this Sim; no action taken.");
                return;
            }
            reader.Close();

            // Add the person's registration to database, use default values in the context of the current Profile (important! Could have many profiles)
            sql = "INSERT INTO UserSheet VALUES ('" + Globals.sSimProfile + "', '" + iCitnum.ToString() + "', '" + Globals.iCurrency.ToString() + "', '" + Globals.iCarryCap.ToString() + "')";
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

            // We also need to add the citizen to the list of simplayers, checking first to see if he/she is already there
            var value = Globals.SimplayerList.Find(item => item.Equals(sName));
            if (value == null)
            {
                Globals.SimplayerList.Add(sName);
            }

            // Send stats to chat
            ConsolePrint(iSess, 0x000000, true, false, "Registration:\tRegistration successful. Stats for citnum " + iCitnum.ToString() + ":");
            ConsolePrint(iSess, 0x000000, true, false, "Registration:\t[Currency: " + Globals.iCurrency.ToString() + " " + Globals.sCurrencyName + "s] [" + Globals.sCarryName + "s of carrying capacity: " + Globals.iCarryCap.ToString() + "]");
            ConsolePrint(iSess, 0x000000, true, false, "Registration:\t[Permission level: " + level + "]");
            //Response(iSess, iType, "Registration successful. Stats for citnum " + iCitnum.ToString() + ":");
            //Response(iSess, iType, "[Currency: " + Globals.iCurrency.ToString() + " " + Globals.sCurrencyName + "s] [" + Globals.sCarryName + "s of carrying capacity: " + Globals.iCarryCap.ToString() + "]");
            //Response(iSess, iType, "[Permission level: " + level + "]");

            // Diaply HUD
            DrawHUD(iSess);
            

        }

        private void DoDeRegister(string sName, int iType, int iSess, string[] cmd)
        {

            // TODO - need a way for captains to do this on behalf of citizens

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

                    // Make sure the second command is a number
                    try
                    {
                        iCitnum = Convert.ToInt32(lCmd[1]);
                    }
                    catch (Exception e)
                    {
                        Response(iSess, iType, "Error: '" + lCmd[1] + "' is not a valid citnum.");
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
                // We've got rows returned! Respond back with this fact and don't add anything.
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

            // When we delete a Simplayer from the CitnumPermissionLevel table, we need to reload the permissions tables to make sure they're current
            Stat(1, "Permissions", "Reloading permissions tables", "black");
            LoadPerms();

            Response(iSess, iType, "De-registration successful. Citnum " + iCitnum.ToString() + " was removed from the Sim database.");

        }

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
                _instance.HudDestroy(iSess, 1);
                _instance.HudDestroy(iSess, 2);
                _instance.HudDestroy(iSess, 3);
                _instance.HudDestroy(iSess, 4);
                _instance.HudDestroy(iSess, 5);
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

        private void DoPresent(string sName, int iType, int iSess, string[] cmd)
        {
            int iCitnum = GetCitnum(sName);
            Stat(1, "CMD", "Command: present (requested by " + sName + " (" + iCitnum.ToString() + ")", "black");

            // Check permissions
            if (CheckPerms(iCitnum, cmd[0]) == false)
            {
                Response(iSess, iType, "Sorry, " + sName + ", but you do not have permission to use the " + cmd[0] + " command.");
                return;
            }

            List<string> lCmd = new List<string>();
            foreach (string d in cmd)
            {
                lCmd.Add(d);
            }

            string q;

            if (lCmd.Count == 2)
            {
                if (lCmd[1] == "simplayers")
                {
                    q = "Simplayers in world:";
                    foreach (string d in Globals.SimplayerList)
                    {
                        q = q + " " + d;
                    }
                    Response(iSess, iType, q);
                    return;
                }
                if (lCmd[1] == "citizens")
                {
                    q = "Total citizens in world:";
                    foreach (string d in Globals.CitList)
                    {
                        q = q + " " + d;
                    }
                    Response(iSess, iType, q);
                    return;
                }
            }
            else
            {
                q = "Total citizens in world:";
                foreach (string d in Globals.CitList)
                {
                    q = q + " " + d;
                }
                Response(iSess, iType, q);
                return;
            }
        }

        private void DoConsole(string sName, int iType, int iSess, string[] cmd)
        {
            ConsolePrint(iSess, 0x009999, true, false, "Testing:\tThis is a test message.");
        }

    }
}
