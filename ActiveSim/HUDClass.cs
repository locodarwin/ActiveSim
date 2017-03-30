using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveSim
{
    class HUD
    {

        private string ElementType;
        private string ElementText;
        private int ElementID;
        private int ElementSession;
        private int ElementOrigin;
        private float ElementOpacity;





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
