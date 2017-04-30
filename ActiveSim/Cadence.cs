using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveSim
{


    public partial class Form1
    {
        public void Cadence(object sender, EventArgs e)
        {

            if (Globals.Debug == true)
            {
                Stat(1, "Cadence", "Cadence event initiated", "black");
                _instance.Say("Cadence event initiated...");
            }


            // Update all WorldFarmItems
            foreach (WorldFarmItem d in Globals.WorldFarmItemList)
            {
                //string q = "Updating object " + d.ObjectID.ToString() + 

                d.Update();

                
            }




        }
    }
}
