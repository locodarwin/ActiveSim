using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveSim
{
    public class DayNight
    {
        // Properties
        public string SimProfile { get; set; }
        public string CropName { get; set; }
        public int SunPosX { get; set; }
        public int SunPosY { get; set; }
        public int SunPosZ { get; set; }

        // Privates


        //Constructor
        public DayNight()
        {

        }


        // Save the status of the day/night 
        public void SaveToDB()
        {

        }


        // Load day/night status from DB


    }
}
