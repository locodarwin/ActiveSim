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

        private void OnEventChat(IInstance sender)
        {

            // Echo the chat (or whisper) to the chat window
            if (sender.Attributes.ChatType == ChatTypes.Whisper)
            {
                Chat(1, sender.Attributes.AvatarName, "(whispered): " + sender.Attributes.ChatMessage, "blue");
            }
            else
            {
                Chat(1, sender.Attributes.AvatarName, sender.Attributes.ChatMessage, "black");
            }

            // If the simulation is not started, go no further, unless it's the SIM command
            if (Globals.iSimRun == false)
            {
                string testcmd = sender.Attributes.ChatMessage.Substring(0, 4);
                if (testcmd != "/sim")
                {
                    return;
                }
            }


            // If command is whispered rather than stated aloud in chat
            int iType;
            if (sender.Attributes.ChatType == ChatTypes.Whisper)
            {
                iType = 2;
            }
            else
            {
                iType = 1;
            }

            // Send the chat to the parser with session ID, speaker, message, chattype, etc.
            Commands(sender.Attributes.AvatarName, iType, sender.Attributes.ChatSession, sender.Attributes.ChatMessage);

        }

        private void OnEventAvatarAdd(IInstance sender)
        {

            string Name = sender.Attributes.AvatarName;
            int Sess = sender.Attributes.AvatarSession;
            string Citnum = sender.Attributes.AvatarCitizen.ToString();

            // check to see if it's a bot
            if (Citnum == "0")
            {
                // Avatar is a bot or tourist; show in status log but do nothing else
                Stat(1, "Bot Add", "Bot " + Name + " has joined the world.", "blue");
                return;
            }


            // If the entity is already in the CitsInWorld, remove
            DataRow[] check = Globals.CitsInWorld.Select("Name = '" + Name + "'");
            int rows = check.Count();
            if (rows != 0)
            {
                foreach (DataRow z in check)
                {
                    Globals.CitsInWorld.Rows.Remove(z);
                }
            }

            

            // Add to the CitsInWorld, return whether or not is registered
            string Registered = CitTableAdd(Name, Sess, Citnum);

            // Log the event in the status window, and draw HUD if the citizen is registered
            if (Registered == "yes")
            {
                Stat(1, "Simplayer Enters", "Registered Simplayer " + Name + " has joined the world.", "blue");
                DrawHUD(sender.Attributes.AvatarSession);
            }
            else
            {
                Stat(1, "Citizen Enters", "Non-Simplayer " + Name + " has joined the world.", "blue");
            }


            // If Captain, announce entry
            if (Citnum == Globals.sCaptain)
            {
                Stat(1, Globals.sBotName, "Captain " + Name + " on deck.", "black");
                Chat(1, Globals.sBotName, "Captain " + Name + " on deck.", "black");
                _instance.Say("Captain " + Name + " on deck.");
            }
        }

        private void OnEventAvatarDelete(IInstance sender)
        {
            
            string Name = sender.Attributes.AvatarName;
            //string Citnum = sender.Attributes.AvatarCitizen.ToString();

            // Remove the leaving citizen from the CitsInWorld
            CitTableRemove(Name);

            // If Captain, announce leaving, if not, just log the fact
            if (Name == Globals.sCaptain)
            {
                Stat(1, Globals.sBotName, "Captain " + Name + " has left the deck.", "black");
                Chat(1, Globals.sBotName, "Captain " + Name + " has left the deck.", "black");
                _instance.Say("Captain " + Name + " has left the deck.");
            }
            else
            {
                if (CheckRegistered(Name) == true)
                {
                    Stat(1, "Simplayer Exits", "Registered Simplayer " + Name + " has left the world.", "blue");
                }
                else
                {
                    Stat(1, "Citizen Exits", "Non-Simplayer " + Name + " has left the world.", "blue");
                }
            }


        }

        private void OnEventHUDClick(IInstance sender)
        {
            int Sess = sender.Attributes.HudElementSession;
            int id = sender.Attributes.HudElementId;
            var x = sender.Attributes.HudElementClickX;
            var y = sender.Attributes.HudElementClickY;
            var z = sender.Attributes.HudElementClickZ;


            if (id == 1)
            {
                // Clicked inventory
                ButtonInv(Sess);
            }

            if (id == 2)
            {
                Stat(1, "HUD CMD", "Simplayer session " + Sess + " clicked on 'Attack' -- at x" + x + ", y" + y + ", z" + z + ".", "black");
                ConsolePrint(Sess, Globals.ColorInv, true, false, "Attack:\tYou clicked 'Attack' (to be developed)");
            }

            if (id == 3)
            {
                Stat(1, "HUD CMD", "Simplayer session " + Sess + " clicked on 'Settings' -- at x" + x + ", y" + y + ", z" + z + ".", "black");
                ConsolePrint(Sess, Globals.ColorInv, true, false, "Attack:\tYou clicked 'Settings' (to be developed)");
            }

            if (id == 4)
            {
                Stat(1, "HUD CMD", "Simplayer session " + Sess + " clicked on 'Trade' -- at x" + x + ", y" + y + ", z" + z + ".", "black");
                ConsolePrint(Sess, Globals.ColorInv, true, false, "Attack:\tYou clicked 'Trade' (to be developed)");
            }

            if (id == 5)
            {
                Stat(1, "HUD CMD", "Simplayer session " + Sess + " clicked on 'Extra1' -- at x" + x + ", y" + y + ", z" + z + ".", "black");
                ConsolePrint(Sess, Globals.ColorInv, true, false, "Attack:\tYou clicked 'Extra BUtton1' (to be developed)");
            }

            if (id == 6)
            {
                Stat(1, "HUD CMD", "Simplayer session " + Sess + " clicked on 'Extra2' -- at x" + x + ", y" + y + ", z" + z + ".", "black");
                ConsolePrint(Sess, Globals.ColorInv, true, false, "Attack:\tYou clicked 'Extra BUtton2' (to be developed)");
            }

            if (id == 7)
            {
                Stat(1, "HUD CMD", "Simplayer session " + Sess + " clicked on 'Extra3' -- at x" + x + ", y" + y + ", z" + z + ".", "black");
                ConsolePrint(Sess, Globals.ColorInv, true, false, "Attack:\tYou clicked 'Extra BUtton3' (to be developed)");
            }


            //Stat(1, "HUD Click", "Simplayer session " + Sess + " clicked on HUD ID " + id + " -- at x" + x + ", y" + y + ", z" + z + ".", "black");
            //Chat(1, "HUD Click", "Simplayer " + Sess + " clicked on HUD ID " + id + " -- at x" + x + ", y" + y + ", z" + z + ".", "black");
            //_instance.Say("Simplayer " + Sess + " clicked on HUD ID " + id + " -- at x" + x + ", y" + y + ", z" + z + ".");
        }

    }
}
