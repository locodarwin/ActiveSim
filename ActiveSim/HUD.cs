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

            _instance.Attributes.HudElementType = AW.HudType.Image;
            _instance.Attributes.HudElementText = "/hud/inventory-yes.jpg";
            _instance.Attributes.HudElementId = 1;
            _instance.Attributes.HudElementSession = iSess;
            _instance.Attributes.HudElementOrigin = AW.HudOrigin.Left;
            _instance.Attributes.HudElementOpacity = 1.0f;
            _instance.Attributes.HudElementX = 0;
            _instance.Attributes.HudElementY = -64;
            _instance.Attributes.HudElementZ = 2;
            _instance.Attributes.HudElementFlags = AW.HudElementFlag.Clicks;
            _instance.Attributes.HudElementColor = 0xFFFFFF;
            _instance.Attributes.HudElementSizeX = 64;
            _instance.Attributes.HudElementSizeY = 64;

            _instance.HudCreate();

            _instance.Attributes.HudElementType = AW.HudType.Image;
            _instance.Attributes.HudElementText = "/hud/attack-yes.jpg";
            _instance.Attributes.HudElementId = 2;
            _instance.Attributes.HudElementSession = iSess;
            _instance.Attributes.HudElementOrigin = AW.HudOrigin.Left;
            _instance.Attributes.HudElementOpacity = 1.0f;
            _instance.Attributes.HudElementX = 64;
            _instance.Attributes.HudElementY = -64;
            _instance.Attributes.HudElementZ = 2;
            _instance.Attributes.HudElementFlags = AW.HudElementFlag.Clicks;
            _instance.Attributes.HudElementColor = 0xFFFFFF;
            _instance.Attributes.HudElementSizeX = 64;
            _instance.Attributes.HudElementSizeY = 64;

            _instance.HudCreate();


            _instance.Attributes.HudElementType = AW.HudType.Image;
            _instance.Attributes.HudElementText = "/hud/settings.jpg";
            _instance.Attributes.HudElementId = 3;
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


            _instance.Attributes.HudElementType = AW.HudType.Image;
            _instance.Attributes.HudElementText = "/hud/ntdibut.jpg";
            _instance.Attributes.HudElementId = 5;
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
            _instance.Attributes.HudElementId = 6;
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
            _instance.Attributes.HudElementId = 7;
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


            _instance.Attributes.HudElementType = AW.HudType.Image;
            _instance.Attributes.HudElementText = "/hud/ntdibut.jpg";
            _instance.Attributes.HudElementId = 8;
            _instance.Attributes.HudElementSession = iSess;
            _instance.Attributes.HudElementOrigin = AW.HudOrigin.Left;
            _instance.Attributes.HudElementOpacity = 1.0f;
            _instance.Attributes.HudElementX = 0;
            _instance.Attributes.HudElementY = -96;
            _instance.Attributes.HudElementZ = 2;
            _instance.Attributes.HudElementFlags = AW.HudElementFlag.Clicks;
            _instance.Attributes.HudElementColor = 0xFFFFFF;
            _instance.Attributes.HudElementSizeX = 128;
            _instance.Attributes.HudElementSizeY = 32;

            _instance.HudCreate();

            _instance.Attributes.HudElementType = AW.HudType.Text;
            _instance.Attributes.HudElementText = "HUD";
            _instance.Attributes.HudElementId = 9;
            _instance.Attributes.HudElementSession = iSess;
            _instance.Attributes.HudElementOrigin = AW.HudOrigin.Left;
            _instance.Attributes.HudElementOpacity = 1.0f;
            _instance.Attributes.HudElementX = 0;
            _instance.Attributes.HudElementY = -96;
            _instance.Attributes.HudElementZ = 1;
            _instance.Attributes.HudElementFlags = AW.HudElementFlag.Clicks;
            _instance.Attributes.HudElementColor = 0xFFFFFF;
            _instance.Attributes.HudElementSizeX = 128;
            _instance.Attributes.HudElementSizeY = 32;

            _instance.HudCreate();

            _instance.Attributes.HudElementType = AW.HudType.Image;
            _instance.Attributes.HudElementText = "/hud/ntdibut.jpg";
            _instance.Attributes.HudElementId = 10;
            _instance.Attributes.HudElementSession = iSess;
            _instance.Attributes.HudElementOrigin = AW.HudOrigin.BottomLeft;
            _instance.Attributes.HudElementOpacity = 1.0f;
            _instance.Attributes.HudElementX = 0;
            _instance.Attributes.HudElementY = -32;
            _instance.Attributes.HudElementZ = 2;
            _instance.Attributes.HudElementFlags = AW.HudElementFlag.Stretch;
            _instance.Attributes.HudElementColor = 0xFFFFFF;
            _instance.Attributes.HudElementSizeX = 256;
            _instance.Attributes.HudElementSizeY = 32;

            _instance.HudCreate();

            _instance.Attributes.HudElementType = AW.HudType.Text;
            _instance.Attributes.HudElementText = "Status";
            _instance.Attributes.HudElementId = 11;
            _instance.Attributes.HudElementSession = iSess;
            _instance.Attributes.HudElementOrigin = AW.HudOrigin.BottomLeft;
            _instance.Attributes.HudElementOpacity = 1.0f;
            _instance.Attributes.HudElementX = 0;
            _instance.Attributes.HudElementY = -32;
            _instance.Attributes.HudElementZ = 1;
            _instance.Attributes.HudElementFlags = AW.HudElementFlag.Stretch;
            _instance.Attributes.HudElementColor = 0xFFFFFF;
            _instance.Attributes.HudElementSizeX = 256;
            _instance.Attributes.HudElementSizeY = 32;

            _instance.HudCreate();
        }

        private void StatusHUD(int iSess, string Msg)
        {
            _instance.Attributes.HudElementType = AW.HudType.Text;
            _instance.Attributes.HudElementText = Msg;
            _instance.Attributes.HudElementId = 11;
            _instance.Attributes.HudElementSession = iSess;
            _instance.Attributes.HudElementOrigin = AW.HudOrigin.BottomLeft;
            _instance.Attributes.HudElementOpacity = 1.0f;
            _instance.Attributes.HudElementX = 0;
            _instance.Attributes.HudElementY = -32;
            _instance.Attributes.HudElementZ = 1;
            _instance.Attributes.HudElementFlags = AW.HudElementFlag.Stretch;
            _instance.Attributes.HudElementColor = 0xFFFFFF;
            _instance.Attributes.HudElementSizeX = 256;
            _instance.Attributes.HudElementSizeY = 32;

            _instance.HudCreate();
        }

        private void EraseHUD(int iSess)
        {
            _instance.HudClear(iSess);
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
            _instance.Attributes.HudElementType = AW.HudType.Image;
            _instance.Attributes.HudElementText = "/hud/plant-yes.jpg";
            _instance.Attributes.HudElementId = 4;
            _instance.Attributes.HudElementSession = iSess;
            _instance.Attributes.HudElementOrigin = AW.HudOrigin.Left;
            _instance.Attributes.HudElementOpacity = 1.0f;
            _instance.Attributes.HudElementX = 64;
            _instance.Attributes.HudElementY = 0;
            _instance.Attributes.HudElementZ = 2;
            _instance.Attributes.HudElementFlags = AW.HudElementFlag.Clicks|AW.HudElementFlag.Highlight;
            _instance.Attributes.HudElementColor = 0xFFFFFF;
            _instance.Attributes.HudElementSizeX = 64;
            _instance.Attributes.HudElementSizeY = 64;

            _instance.HudCreate();

            // Set plant flag for this user
            PlantFlagUpdate(iSess, "yes");

        }


    }
}
