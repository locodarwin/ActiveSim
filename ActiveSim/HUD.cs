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
            _instance.Attributes.HudElementText = "/hud/trade-yes.jpg";
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
            Stat(1, "HUD CMD", "HUD Command: inv (requested by " + sName + " (" + iCitnum.ToString() + ")", "black");


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
                while (reader.Read())
                {

                    string item = reader["Name"].ToString();
                    string value = reader["Value"].ToString();
                    string weight = reader["Weight"].ToString();
                    string type = reader["Type"].ToString();

                    string sOut = item + " [v: " + value + "] [w: " + weight + "] [t: " + type + "]";

                    ConsolePrint(iSess, Globals.ColorInv, true, false, "Inventory:\t" + sOut);

                }
            }
            reader.Close();
        }




    }
}
