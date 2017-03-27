using System.Data.SQLite;
using System.Data;

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
            Response(iSess, iType, Globals.sAppName + " " + Globals.sVersion + " - " + Globals.sByline);
        }

        // Command REGISTER
        private void DoRegister(string sName, int iType, int iSess, string[] cmd)
        {
            int iCitnum = GetCitnum(sName);
            Stat(1, "CMD", "Command: register (requested by " + sName + " (" + iCitnum.ToString() + ")", "black");

            // check database for existing

            // if not, add to database and assign defaults to each parameter

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
            
            Response(iSess, iType, "Registration successful.");
            Response(iSess, iType, "Stats for citnum " + iCitnum.ToString() + ":");
            Response(iSess, iType, Globals.sCurrencyName + "s: " + Globals.iCurrency.ToString());
            Response(iSess, iType, Globals.sCarryName + "s of carrying capacity: " + Globals.iCarryCap.ToString());

        }

        private void DoDeRegister(string sName, int iType, int iSess, string[] cmd)
        {

            // TODO - need a way for captains to do this on behalf of users

            int iCitnum = GetCitnum(sName);
            Stat(1, "CMD", "Command: de-register (requested by " + sName + " (" + iCitnum.ToString() + ")", "black");

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

            Response(iSess, iType, "De-registration successful. Citnum " + iCitnum.ToString() + " was removed from the Sim database.");

        }

        private void DoHUD(string sName, int iType, int iSession, string[] cmd)
        {
            int iCitnum = GetCitnum(sName);
            Stat(1, "CMD", "Command: HUD (requested by " + sName + " (" + iCitnum.ToString() + ")", "black");

            if (cmd[1] == "start")
            {

                _instance.Attributes.HudElementType = AW.HudType.Image;
                _instance.Attributes.HudElementText = "https://s1-ssl.dmcdn.net/TLTGc/200x200-7Am.jpg";
                _instance.Attributes.HudElementId = 1;
                _instance.Attributes.HudElementSession = iSession;
                _instance.Attributes.HudElementOrigin = AW.HudOrigin.Left;
                _instance.Attributes.HudElementOpacity = 0.5f;
                _instance.Attributes.HudElementX = 0;
                _instance.Attributes.HudElementY = 0;
                _instance.Attributes.HudElementZ = 2;
                _instance.Attributes.HudElementFlags = AW.HudElementFlag.Clicks;
                _instance.Attributes.HudElementColor = 0xDDFF00;
                _instance.Attributes.HudElementSizeX = 200;
                _instance.Attributes.HudElementSizeY = 200;

                _instance.HudCreate();


                _instance.Attributes.HudElementType = AW.HudType.Text;
                _instance.Attributes.HudElementText = "Test Text";
                _instance.Attributes.HudElementId = 2;
                _instance.Attributes.HudElementSession = iSession;
                _instance.Attributes.HudElementOrigin = AW.HudOrigin.Left;
                _instance.Attributes.HudElementOpacity = 1.0f;
                _instance.Attributes.HudElementX = 0;
                _instance.Attributes.HudElementY = 0;
                _instance.Attributes.HudElementZ = 1;
                _instance.Attributes.HudElementFlags = AW.HudElementFlag.Clicks;
                _instance.Attributes.HudElementColor = 0x000000;
                _instance.Attributes.HudElementSizeX = 100;
                _instance.Attributes.HudElementSizeY = 100;

                _instance.HudCreate();


            }

            if (cmd[1] == "stop")
            {
                _instance.HudDestroy(iSession, 1);
                _instance.HudDestroy(iSession, 2);
            }


        }
    }
}
