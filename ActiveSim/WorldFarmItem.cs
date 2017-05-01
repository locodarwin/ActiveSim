﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using AW;
using System.Text;
using System.Threading.Tasks;

namespace ActiveSim
{
    public class WorldFarmItem
    {
        public string SimProfile { get; set; }
        public string CropName { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int PosZ { get; set; }
        public int PosYaw { get; set; }
        public int PosTilt { get; set; }
        public int PosRoll { get; set; }
        public string Model { get; set; }
        public int Stages { get; set; }                     // number of stages for crop
        public List<string> StageModels { get; set; }       // list of models for the stages
        public List<string> StageTime { get; set; }          // list of stage durations for the stages
        public string Fertilizer { get; set; }
        public string YieldProduct { get; set; }
        public int YieldProductAmount { get; set; }
        public string YieldSeed { get; set; }
        public int YieldSeedAmount { get; set; }
        private string pGUID;
        private int pCurrentStage;
        private int pCadenceCounter;
        private int pObjectID;
        private List<int> pStageTime = new List<int>();

        // Constructor
        public WorldFarmItem()
        {
            // Create GUID
            pGUID = Guid.NewGuid().ToString();

        }



        public bool Init(int crop, int objid)
        {

                       
            // Pull the farm item info out of the CropTable in the database
            string sql = "select * from CropTable where SimProfile = '" + Form1.Globals.sSimProfile + "' and CropID = '" + crop + "'";
            SQLiteCommand sqlcmd = new SQLiteCommand(sql, Form1.Globals.m_db);
            SQLiteDataReader reader = sqlcmd.ExecuteReader();

            // Any items in the reader? If not, return empty handed
            if (reader.HasRows == false)
            {
                Form1.Globals.Error = "FarmItem Error: No such CropID in database.";
                return false;
            }

            // Set internal object ID
            pObjectID = objid;
            //Console.WriteLine("ObjectID: " + pObjectID);

            // Pull the data for this new farm item from the CropTable query results
            string tStageModels = "", tStageTime = "";
            while (reader.Read())
            {
                CropName = reader["CropName"].ToString();
                Stages = Convert.ToInt32(reader["Stages"]);
                tStageModels = reader["StageModels"].ToString();
                tStageTime = reader["StageTime"].ToString();
                Fertilizer = reader["Fertilizer"].ToString();
                YieldProduct = reader["YieldProduct"].ToString();
                YieldProductAmount = Convert.ToInt32(reader["YieldProductAmount"]);
                YieldSeed = reader["YieldSeed"].ToString();
                YieldSeedAmount = Convert.ToInt32(reader["YieldSeedAmount"]);
            }
            reader.Close();

            StageModels = tStageModels.Split(',').ToList();
            StageTime = tStageTime.Split(',').ToList();

            // query for and get the x, y, z, yaw, tilt, roll of the object in question
            Form1._instance.Attributes.ObjectNumber = 0;
            Form1._instance.Attributes.ObjectId = pObjectID;
            var rc = Form1._instance.ObjectQuery();
            if (rc != Result.Success)
            {
                Form1.Globals.Error = "FarmItem Error: Unable to query object.";
                return false;
            }
            PosX = Form1._instance.Attributes.ObjectX;
            PosY = Form1._instance.Attributes.ObjectY;
            PosZ = Form1._instance.Attributes.ObjectZ;
            PosYaw = Form1._instance.Attributes.ObjectYaw;
            PosTilt = Form1._instance.Attributes.ObjectTilt;
            PosRoll = Form1._instance.Attributes.ObjectRoll;
            Model = Form1._instance.Attributes.ObjectModel;

            // Create a new model at the same location which is the 1st stage of the crop
            // Don't remove the old one, and keep its model number as well for later comparisons
            //Form1._instance.Attributes.ObjectNumber = 0;
            //Form1._instance.Attributes.ObjectId = pObjectID;
            Form1._instance.Attributes.ObjectModel = StageModels.ElementAt(0);
            Form1._instance.Attributes.ObjectDescription = CropName;
            Form1._instance.ObjectAdd();

            // Store new objectID
            //Form1._instance.Attributes.ObjectNumber = 0;
            //Form1._instance.Attributes.ObjectId = pObjectID;
            //Form1._instance.Attributes.ObjectModel = StageModels.ElementAt(0);
            //Form1._instance.ObjectChange();
            pObjectID = Form1._instance.Attributes.ObjectId;

            // Set stage and decide the true values for the stages
            pCurrentStage = 1;
            pCadenceCounter = 0;
            var rnd = new Random(DateTime.Now.Millisecond);
            foreach (string e in StageTime)
            {
                string[] times = e.Split('-');
                int tick = rnd.Next(Convert.ToInt32(times[0]), Convert.ToInt32(times[1]) + 1);
                //Console.WriteLine("Stage time: " + tick);
                pStageTime.Add(tick);
            }


            return true;
        }

        public bool Update()
        {
            pCadenceCounter = pCadenceCounter + 1;
            // Are we at the last stage of growth? If so, do nothing and exit the update stage
            if (pCurrentStage == Stages)
            {
                return true;
            }

            

            // If it's time to update the crop to the next stage, do so
            if (pCadenceCounter >= pStageTime[pCurrentStage - 1])
            {

                //Form1._instance.Say("Updating object " + pObjectID + " for stage " + (pCurrentStage + 1) + " of " + Stages);
                // Query the object to make sure we're looking at the right one
                Form1._instance.Attributes.ObjectNumber = 0;
                Form1._instance.Attributes.ObjectId = pObjectID;
                Form1._instance.ObjectQuery();


                // Change the model of the object to first stage
                Form1._instance.Attributes.ObjectNumber = 0;
                Form1._instance.Attributes.ObjectId = pObjectID;
                Form1._instance.Attributes.ObjectModel = StageModels.ElementAt(pCurrentStage);
                Form1._instance.Attributes.ObjectX = PosX;
                Form1._instance.Attributes.ObjectY = PosY;
                Form1._instance.Attributes.ObjectZ = PosZ;
                Form1._instance.Attributes.ObjectYaw = PosYaw;
                Form1._instance.Attributes.ObjectTilt = PosTilt;
                Form1._instance.Attributes.ObjectRoll = PosRoll;
                Form1._instance.ObjectChange();

                pCurrentStage = pCurrentStage + 1;

            }


            return true;
        }




    }
}