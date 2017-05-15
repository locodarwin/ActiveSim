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

        /// <summary>
        /// Cadence main entry method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Cadence(object sender, EventArgs e)
        //public void Cadence(object sender)
        {
            //Globals.Debug = true;

            // Fire off the background Cadence thread
            CadenceWorker.RunWorkerAsync();

        }

        /// <summary>
        /// The Cadence background worker thread method, where the work occurs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CadenceThread(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgWorker = (BackgroundWorker)sender;

            // Disable object/HUD event handlers temporarily
            //_instance.EventHudClick += null;
            //_instance.EventObjectClick += null;

            // Start the debug stopwatch
            Stopwatch sw = Stopwatch.StartNew();

            // If debug on
            if (Globals.Debug == true)
            {
                Stat(1, "Cadence", "Cadence event initiated", "black");
                m_bot.Say("Cadence event initiated.");
            }

            // Do day/night



            // Update all of the Farming objects
            try
            {
                foreach (WorldFarmItem d in Globals.WorldFarmItemList)
                {
                    d.Update();
                }
            }
            catch
            {

            }
            

            // Stop the stopwatch
            sw.Stop();

            // If debug
            if(Globals.Debug == true)
            {
                Stat(1, "Cadence", "Cadence event completed", "black");
                m_bot.Say("Cadence event completed - Elapsed time: " + sw.ElapsedMilliseconds + "ms. Total items parsed: " + Globals.WorldFarmItemList.Count());
            }

            // Re-enable event handlers
            //_instance.EventHudClick += OnEventHUDClick;
            //_instance.EventObjectClick += OnEventObjectClick;

        }


        private void CadenceProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //textBox1.Text = e.ProgressPercentage.ToString();
        }

        void CadenceRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Console.WriteLine(e.Error.Message);
            }
        }


    }
}
