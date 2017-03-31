using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AW;

namespace ActiveSim
{
    class HUD
    {

        private string Type { get; set; } = "image";
        private string Text { get; set; } = "attack-no.jpg";
        private int ID { get; set; }
        private int Session { get; set; }
        private int Origin { get; set; } = 1;
        private float Opacity { get; set; } = 1.0f;
        private int X { get; set; } = 0;
        private int Y { get; set; } = 0;
        private int Z { get; set; } = 0;
        private int Flags { get; set; } = 0;
        private int Color { get; set; } = 0xFFFFFF;
        private int SizeX { get; set; } = 32;
        private int SizeY { get; set; } = 32;


        public HUD(int iSession, int iID)
        {
            this.Session = iSession;
            this.ID = iID;
        }

        /*
        public bool Create()
        {
            if (Type == "image")
            {
                Form1._instance.Attributes.HudElementType = HudType.Image;
            }
            else
            {
                Form1._instance.Attributes.HudElementType = HudType.Text;
            }

            Form1._instance.Attributes.HudElementText = Text;







            return true;
        }

        */


            
        /*
         *  _instance.Attributes.HudElementType = AW.HudType.Image;
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
         * 
         */


            
        public string Element(string type)
        {
            return type + "dude";
        }
    }
}


