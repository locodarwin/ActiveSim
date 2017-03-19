using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Response(iSess, iType, "You asked to register.");

        }


    }
}
