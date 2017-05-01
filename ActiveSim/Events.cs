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

            // If the entity is already in the CitTable, remove
            DataRow[] check = Globals.CitTable.Select("Name = '" + Name + "'");
            int rows = check.Count();
            if (rows != 0)
            {
                foreach (DataRow z in check)
                {
                    Globals.CitTable.Rows.Remove(z);
                }
            }

            // Add to the CitTable, return whether or not is registered
            string Registered = CitTableAdd(Name, Sess, Citnum);

            // Check to see if registered - if so, and Sim is started, draw HUD
            if (Registered == "yes")
            {
                Stat(1, "Simplayer Enters", "Registered Simplayer " + Name + " has joined the world.", "blue");
                if (Globals.iSimRun == true)
                {
                    DrawHUD(sender.Attributes.AvatarSession);
                }
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

            // Remove the leaving citizen from the CitTable
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
                StatusHUD(Sess, "Clicked 'Attack' (TBD)");
            }

            if (id == 3)
            {
                Stat(1, "HUD CMD", "Simplayer session " + Sess + " clicked on 'Settings' -- at x" + x + ", y" + y + ", z" + z + ".", "black");
                ConsolePrint(Sess, Globals.ColorInv, true, false, "Attack:\tYou clicked 'Settings' (to be developed)");
                StatusHUD(Sess, "Clicked 'Settings' (TBD)");
            }

            if (id == 4)
            {
                //Stat(1, "HUD CMD", "Simplayer session " + Sess + " clicked on 'Plant' -- at x" + x + ", y" + y + ", z" + z + ".", "black");
                //ConsolePrint(Sess, Globals.ColorInv, true, false, "Attack:\tYou clicked 'Plant' (to be developed)");
                //StatusHUD(Sess, "Clicked 'Plant' (TBD)");

                ButtonPlant(Sess);



            }

            if (id == 5)
            {
                Stat(1, "HUD CMD", "Simplayer session " + Sess + " clicked on 'Extra1' -- at x" + x + ", y" + y + ", z" + z + ".", "black");
                ConsolePrint(Sess, Globals.ColorInv, true, false, "Attack:\tYou clicked 'Extra Button1' (to be developed)");
                StatusHUD(Sess, "Clicked 'Button1' (TBD)");
            }

            if (id == 6)
            {
                Stat(1, "HUD CMD", "Simplayer session " + Sess + " clicked on 'Extra2' -- at x" + x + ", y" + y + ", z" + z + ".", "black");
                ConsolePrint(Sess, Globals.ColorInv, true, false, "Attack:\tYou clicked 'Extra Button2' (to be developed)");
                StatusHUD(Sess, "Clicked 'Button2' (TBD)");
            }

            if (id == 7)
            {
                Stat(1, "HUD CMD", "Simplayer session " + Sess + " clicked on 'Extra3' -- at x" + x + ", y" + y + ", z" + z + ".", "black");
                ConsolePrint(Sess, Globals.ColorInv, true, false, "Attack:\tYou clicked 'Extra Button3' (to be developed)");
                StatusHUD(Sess, "Clicked 'Button3' (TBD)");
            }


            //Stat(1, "HUD Click", "Simplayer session " + Sess + " clicked on HUD ID " + id + " -- at x" + x + ", y" + y + ", z" + z + ".", "black");
            //Chat(1, "HUD Click", "Simplayer " + Sess + " clicked on HUD ID " + id + " -- at x" + x + ", y" + y + ", z" + z + ".", "black");
            //_instance.Say("Simplayer " + Sess + " clicked on HUD ID " + id + " -- at x" + x + ", y" + y + ", z" + z + ".");
        }

        private void OnEventObjectClick(IInstance sender)
        {
            string Name = sender.Attributes.AvatarName;
            int ObjID = sender.Attributes.ObjectId;
            int iSess = sender.Attributes.AvatarSession;

            // Check to see if the user (session) has the plant flag on
            if (GetPlantFlag(iSess) == true)
            {
                // Check to see if the user clicked tilled soil
                string tModel = "";
                Form1._instance.Attributes.ObjectNumber = 0;
                Form1._instance.Attributes.ObjectId = ObjID;
                Form1._instance.ObjectQuery();
                tModel = Form1._instance.Attributes.ObjectModel;

                if (tModel == Globals.TilledSoil)
                {
                    WorldFarmItem d = new WorldFarmItem();
                    bool rc = d.Init(1, ObjID);
                    if (rc != true)
                    {
                        _instance.Say(Globals.Error);
                    }

                    Globals.WorldFarmItemList.Add(d);
                    StatusHUD(iSess, "Planted Wheat");

                }
                else
                {
                    StatusHUD(iSess, "Aborted: not tilled soil");
                   
                }

                // Turm plant HUD blink off
                _instance.Attributes.HudElementType = AW.HudType.Image;
                _instance.Attributes.HudElementText = "/hud/plant-yes.jpg";
                _instance.Attributes.HudElementId = 4;
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
                PlantFlagUpdate(iSess, "no");
                return;
            }




            /*
            StringBuilder str = new StringBuilder();
            str.Append("Avatar ");
            str.Append(Name);
            str.Append(" clicked object #");
            str.Append(ObjID);
            string str1 = str.ToString();
            _instance.Say(str1);
            */


            StatusHUD(iSess, "Clicked object #" + ObjID.ToString());

        }

    }
}
