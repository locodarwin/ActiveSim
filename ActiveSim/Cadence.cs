using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ActiveSim
{


    public partial class Form1
    {
        public void Cadence(object sender, EventArgs e)
        //public void Cadence(object sender)
        {

            //Globals.Debug = true;
            

            // Fire off the background Cadence thread
            CadenceWorker.RunWorkerAsync();



            // Do day/night
            DayNight dn = new DayNight();
            dn.SimProfile = "Default";


            // Disable object/HUD event handlers temporarily
            //_instance.EventHudClick += null;
            //_instance.EventObjectClick += null;

            // Update all WorldFarmItems
            //foreach (WorldFarmItem d in Globals.WorldFarmItemList)
            //{
                //string q = "Updating object " + d.ObjectID.ToString() + 
             //   d.Update();

            //}

            // Re-enable event handlers
            //_instance.EventHudClick += OnEventHUDClick;
            //_instance.EventObjectClick += OnEventObjectClick;


            
            
        }


        private void CadenceThread(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgWorker = (BackgroundWorker)sender;

            Stopwatch sw = Stopwatch.StartNew();
            if (Globals.Debug == true)
            {
                Stat(1, "Cadence", "Cadence event initiated", "black");
                _instance.Say("Cadence event initiated.");
            }





            foreach (WorldFarmItem d in Globals.WorldFarmItemList)
            {
                //string q = "Updating object " + d.ObjectID.ToString() + 
                d.Update();

            }


            sw.Stop();
            if(Globals.Debug == true)
            {
                Stat(1, "Cadence", "Cadence event completed", "black");
                _instance.Say("Cadence event completed - Elapsed time: " + sw.ElapsedMilliseconds + "ms. Total items parsed: " + Globals.WorldFarmItemList.Count());
            }
        }


        private void CadenceProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //textBox1.Text = e.ProgressPercentage.ToString();
        }

        void CadenceRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //button1.Enabled = true;
        }


    }
}
