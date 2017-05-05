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

        private void DrawHUD(int iSess)
        {

            m_bot.Attributes.HudElementType = AW.HudType.Image;
            m_bot.Attributes.HudElementText = "/hud/inventory-yes.jpg";
            m_bot.Attributes.HudElementId = 1;
            m_bot.Attributes.HudElementSession = iSess;
            m_bot.Attributes.HudElementOrigin = AW.HudOrigin.Left;
            m_bot.Attributes.HudElementOpacity = 1.0f;
            m_bot.Attributes.HudElementX = 0;
            m_bot.Attributes.HudElementY = -64;
            m_bot.Attributes.HudElementZ = 2;
            m_bot.Attributes.HudElementFlags = AW.HudElementFlag.Clicks;
            m_bot.Attributes.HudElementColor = 0xFFFFFF;
            m_bot.Attributes.HudElementSizeX = 64;
            m_bot.Attributes.HudElementSizeY = 64;

            m_bot.HudCreate();

            m_bot.Attributes.HudElementType = AW.HudType.Image;
            m_bot.Attributes.HudElementText = "/hud/attack-yes.jpg";
            m_bot.Attributes.HudElementId = 2;
            m_bot.Attributes.HudElementSession = iSess;
            m_bot.Attributes.HudElementOrigin = AW.HudOrigin.Left;
            m_bot.Attributes.HudElementOpacity = 1.0f;
            m_bot.Attributes.HudElementX = 64;
            m_bot.Attributes.HudElementY = -64;
            m_bot.Attributes.HudElementZ = 2;
            m_bot.Attributes.HudElementFlags = AW.HudElementFlag.Clicks;
            m_bot.Attributes.HudElementColor = 0xFFFFFF;
            m_bot.Attributes.HudElementSizeX = 64;
            m_bot.Attributes.HudElementSizeY = 64;

            m_bot.HudCreate();


            m_bot.Attributes.HudElementType = AW.HudType.Image;
            m_bot.Attributes.HudElementText = "/hud/settings.jpg";
            m_bot.Attributes.HudElementId = 3;
            m_bot.Attributes.HudElementSession = iSess;
            m_bot.Attributes.HudElementOrigin = AW.HudOrigin.Left;
            m_bot.Attributes.HudElementOpacity = 1.0f;
            m_bot.Attributes.HudElementX = 0;
            m_bot.Attributes.HudElementY = 0;
            m_bot.Attributes.HudElementZ = 2;
            m_bot.Attributes.HudElementFlags = AW.HudElementFlag.Clicks;
            m_bot.Attributes.HudElementColor = 0xFFFFFF;
            m_bot.Attributes.HudElementSizeX = 64;
            m_bot.Attributes.HudElementSizeY = 64;

            m_bot.HudCreate();

            m_bot.Attributes.HudElementType = AW.HudType.Image;
            m_bot.Attributes.HudElementText = "/hud/plant-yes.jpg";
            m_bot.Attributes.HudElementId = 4;
            m_bot.Attributes.HudElementSession = iSess;
            m_bot.Attributes.HudElementOrigin = AW.HudOrigin.Left;
            m_bot.Attributes.HudElementOpacity = 1.0f;
            m_bot.Attributes.HudElementX = 64;
            m_bot.Attributes.HudElementY = 0;
            m_bot.Attributes.HudElementZ = 2;
            m_bot.Attributes.HudElementFlags = AW.HudElementFlag.Clicks;
            m_bot.Attributes.HudElementColor = 0xFFFFFF;
            m_bot.Attributes.HudElementSizeX = 64;
            m_bot.Attributes.HudElementSizeY = 64;

            m_bot.HudCreate();


            m_bot.Attributes.HudElementType = AW.HudType.Image;
            m_bot.Attributes.HudElementText = "/hud/ntdibut.jpg";
            m_bot.Attributes.HudElementId = 5;
            m_bot.Attributes.HudElementSession = iSess;
            m_bot.Attributes.HudElementOrigin = AW.HudOrigin.Left;
            m_bot.Attributes.HudElementOpacity = 1.0f;
            m_bot.Attributes.HudElementX = 0;
            m_bot.Attributes.HudElementY = 64;
            m_bot.Attributes.HudElementZ = 2;
            m_bot.Attributes.HudElementFlags = AW.HudElementFlag.Clicks;
            m_bot.Attributes.HudElementColor = 0xFFFFFF;
            m_bot.Attributes.HudElementSizeX = 128;
            m_bot.Attributes.HudElementSizeY = 32;

            m_bot.HudCreate();

            m_bot.Attributes.HudElementType = AW.HudType.Image;
            m_bot.Attributes.HudElementText = "/hud/ntdibut.jpg";
            m_bot.Attributes.HudElementId = 6;
            m_bot.Attributes.HudElementSession = iSess;
            m_bot.Attributes.HudElementOrigin = AW.HudOrigin.Left;
            m_bot.Attributes.HudElementOpacity = 1.0f;
            m_bot.Attributes.HudElementX = 0;
            m_bot.Attributes.HudElementY = 96;
            m_bot.Attributes.HudElementZ = 2;
            m_bot.Attributes.HudElementFlags = AW.HudElementFlag.Clicks;
            m_bot.Attributes.HudElementColor = 0xFFFFFF;
            m_bot.Attributes.HudElementSizeX = 128;
            m_bot.Attributes.HudElementSizeY = 32;

            m_bot.HudCreate();

            m_bot.Attributes.HudElementType = AW.HudType.Image;
            m_bot.Attributes.HudElementText = "/hud/ntdibut.jpg";
            m_bot.Attributes.HudElementId = 7;
            m_bot.Attributes.HudElementSession = iSess;
            m_bot.Attributes.HudElementOrigin = AW.HudOrigin.Left;
            m_bot.Attributes.HudElementOpacity = 1.0f;
            m_bot.Attributes.HudElementX = 0;
            m_bot.Attributes.HudElementY = 128;
            m_bot.Attributes.HudElementZ = 2;
            m_bot.Attributes.HudElementFlags = AW.HudElementFlag.Clicks;
            m_bot.Attributes.HudElementColor = 0xFFFFFF;
            m_bot.Attributes.HudElementSizeX = 128;
            m_bot.Attributes.HudElementSizeY = 32;

            m_bot.HudCreate();


            m_bot.Attributes.HudElementType = AW.HudType.Image;
            m_bot.Attributes.HudElementText = "/hud/ntdibut.jpg";
            m_bot.Attributes.HudElementId = 8;
            m_bot.Attributes.HudElementSession = iSess;
            m_bot.Attributes.HudElementOrigin = AW.HudOrigin.Left;
            m_bot.Attributes.HudElementOpacity = 1.0f;
            m_bot.Attributes.HudElementX = 0;
            m_bot.Attributes.HudElementY = -96;
            m_bot.Attributes.HudElementZ = 2;
            m_bot.Attributes.HudElementFlags = AW.HudElementFlag.Clicks;
            m_bot.Attributes.HudElementColor = 0xFFFFFF;
            m_bot.Attributes.HudElementSizeX = 128;
            m_bot.Attributes.HudElementSizeY = 32;

            m_bot.HudCreate();

            m_bot.Attributes.HudElementType = AW.HudType.Text;
            m_bot.Attributes.HudElementText = "HUD";
            m_bot.Attributes.HudElementId = 9;
            m_bot.Attributes.HudElementSession = iSess;
            m_bot.Attributes.HudElementOrigin = AW.HudOrigin.Left;
            m_bot.Attributes.HudElementOpacity = 1.0f;
            m_bot.Attributes.HudElementX = 0;
            m_bot.Attributes.HudElementY = -96;
            m_bot.Attributes.HudElementZ = 1;
            m_bot.Attributes.HudElementFlags = AW.HudElementFlag.Clicks;
            m_bot.Attributes.HudElementColor = 0xFFFFFF;
            m_bot.Attributes.HudElementSizeX = 128;
            m_bot.Attributes.HudElementSizeY = 32;

            m_bot.HudCreate();

            m_bot.Attributes.HudElementType = AW.HudType.Image;
            m_bot.Attributes.HudElementText = "/hud/ntdibut.jpg";
            m_bot.Attributes.HudElementId = 10;
            m_bot.Attributes.HudElementSession = iSess;
            m_bot.Attributes.HudElementOrigin = AW.HudOrigin.BottomLeft;
            m_bot.Attributes.HudElementOpacity = 1.0f;
            m_bot.Attributes.HudElementX = 0;
            m_bot.Attributes.HudElementY = -32;
            m_bot.Attributes.HudElementZ = 2;
            m_bot.Attributes.HudElementFlags = AW.HudElementFlag.Stretch;
            m_bot.Attributes.HudElementColor = 0xFFFFFF;
            m_bot.Attributes.HudElementSizeX = 256;
            m_bot.Attributes.HudElementSizeY = 32;

            m_bot.HudCreate();

            m_bot.Attributes.HudElementType = AW.HudType.Text;
            m_bot.Attributes.HudElementText = "Status";
            m_bot.Attributes.HudElementId = 11;
            m_bot.Attributes.HudElementSession = iSess;
            m_bot.Attributes.HudElementOrigin = AW.HudOrigin.BottomLeft;
            m_bot.Attributes.HudElementOpacity = 1.0f;
            m_bot.Attributes.HudElementX = 0;
            m_bot.Attributes.HudElementY = -32;
            m_bot.Attributes.HudElementZ = 1;
            m_bot.Attributes.HudElementFlags = AW.HudElementFlag.Stretch;
            m_bot.Attributes.HudElementColor = 0xFFFFFF;
            m_bot.Attributes.HudElementSizeX = 256;
            m_bot.Attributes.HudElementSizeY = 32;

            m_bot.HudCreate();
        }

        private void StatusHUD(int iSess, string Msg)
        {
            m_bot.Attributes.HudElementType = AW.HudType.Text;
            m_bot.Attributes.HudElementText = Msg;
            m_bot.Attributes.HudElementId = 11;
            m_bot.Attributes.HudElementSession = iSess;
            m_bot.Attributes.HudElementOrigin = AW.HudOrigin.BottomLeft;
            m_bot.Attributes.HudElementOpacity = 1.0f;
            m_bot.Attributes.HudElementX = 0;
            m_bot.Attributes.HudElementY = -32;
            m_bot.Attributes.HudElementZ = 1;
            m_bot.Attributes.HudElementFlags = AW.HudElementFlag.Stretch;
            m_bot.Attributes.HudElementColor = 0xFFFFFF;
            m_bot.Attributes.HudElementSizeX = 256;
            m_bot.Attributes.HudElementSizeY = 32;

            m_bot.HudCreate();
        }

        private void EraseHUD(int iSess)
        {
            m_bot.HudClear(iSess);
        }

        private void ButtonInv(int iSess)
        {
            // Get information about the session (name, citnum) and report the command
            int iCitnum = CitGetCitnum(iSess);
            string sName = CitGetName(iCitnum.ToString());
            StatusHUD(iSess, "Clicked 'Inventory'");
            Stat(1, "HUD CMD", "HUD Command: INVENTORY (requested by " + sName + " (" + iCitnum.ToString() + ")", "black");


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
                string dOut = "Inventory:\tName\t\t\tValue\tWt\tType\t\tQuantity";
                ConsolePrint(iSess, Globals.ColorInv, true, false, dOut);

                while (reader.Read())
                {

                    string item = reader["Name"].ToString();
                    string value = reader["Value"].ToString();
                    string weight = reader["Weight"].ToString();
                    string type = reader["Type"].ToString();
                    string quantity = reader["Quantity"].ToString();

                    //string sOut = item + " \t[v: " + value + "] \t[w: " + weight + "] \t[t: " + type + "] \t[q: " + quantity + "]";
                    string sOut = item + "\t\t" + value + "\t" + weight + "\t" + type + "\t\t" + quantity;

                    ConsolePrint(iSess, Globals.ColorInv, false, false, "\t" + sOut);

                }
            }
            reader.Close();
        }

        private void ButtonPlant(int iSess)
        {
            // Get information about the session (name, citnum) and report the command
            int iCitnum = CitGetCitnum(iSess);
            string sName = CitGetName(iCitnum.ToString());
            StatusHUD(iSess, "Plant: click tilled soil...");
            Stat(1, "HUD CMD", "HUD Command: PLANT (requested by " + sName + " (" + iCitnum.ToString() + ")", "black");

            // Make the HUD element blink
            m_bot.Attributes.HudElementType = AW.HudType.Image;
            m_bot.Attributes.HudElementText = "/hud/plant-yes.jpg";
            m_bot.Attributes.HudElementId = 4;
            m_bot.Attributes.HudElementSession = iSess;
            m_bot.Attributes.HudElementOrigin = AW.HudOrigin.Left;
            m_bot.Attributes.HudElementOpacity = 1.0f;
            m_bot.Attributes.HudElementX = 64;
            m_bot.Attributes.HudElementY = 0;
            m_bot.Attributes.HudElementZ = 2;
            m_bot.Attributes.HudElementFlags = AW.HudElementFlag.Clicks|AW.HudElementFlag.Highlight;
            m_bot.Attributes.HudElementColor = 0xFFFFFF;
            m_bot.Attributes.HudElementSizeX = 64;
            m_bot.Attributes.HudElementSizeY = 64;

            m_bot.HudCreate();

            // Set plant flag for this user
            PlantFlagUpdate(iSess, "yes");

        }


    }
}
