using System;
using System.Windows.Forms;
using System.Collections.Generic;
using AW;
using System.Data.SQLite;
using System.Data;

namespace ActiveSim
{
    public partial class Form1 : Form
    {

        // Timer identifiers created out here in public
        public IInstance _instance;
        public Timer aTimer;

        public Form1()
        {
            InitializeComponent();

            // Set up the columns in the listviews (ChatMon and StatMon)
            ChatMon.View = View.Details;
            ChatMon.Columns.Add("Time (Local)", 120, HorizontalAlignment.Left);
            ChatMon.Columns.Add("Speaker", 110, HorizontalAlignment.Left);
            ChatMon.Columns.Add("Message", 480, HorizontalAlignment.Left);

            StatMon.View = View.Details;
            StatMon.Columns.Add("Time (Local)", 120, HorizontalAlignment.Left);
            StatMon.Columns.Add("Action", 110, HorizontalAlignment.Left);
            StatMon.Columns.Add("Message", 480, HorizontalAlignment.Left);

            // Disable all buttons except universe login
            butLoginUniv.Enabled = true;
            butLoginWorld.Enabled = false;
            butLogOut.Enabled = false;
            butMove2Coords.Enabled = false;
            butSendChat.Enabled = false;
            butSimConfig.Enabled = false;
            butSimStart.Enabled = false;
            butSimStatus.Enabled = false;
            butSimStop.Enabled = false;

            // Open the database
            Globals.m_db = new SQLiteConnection("data source=activesim.db;version=3");
            Globals.m_db.Open();

            Stat(1, "Startup", Globals.sAppName + " " + Globals.sVersion + " - " + Globals.sByline + ".", "black");
            Stat(1, "Startup", "Bot started and ready to log in.", "black");

        }

        // Class for the public global variables
        public static class Globals
        {
            public static string sAppName = "ActiveSim";
            public static string sVersion = "v1.0";
            public static string sByline = "Copyright © 2017 by Locodarwin";

            public static string sUnivLogin = "auth.activeworlds.com";
            public static int iPort = 6670;
            public static string sBotName = "ActiveSim";
            public static string sBotDesc = "ActiveSim";
            public static int iCitNum = 101010;
            public static string sPassword = "hidden";
            public static string sWorld = "simulator";
            public static int iXPos = 0;
            public static int iYPos = 690;
            public static int iZPos = 500;
            public static int iYaw = 0;
            public static int iAV = 1;

            public static bool iInUniv = false;
            public static bool iInWorld = false;
            public static bool iSimRun = false;

            public static string sSimProfile = "Default";
            public static string sCurrencyName = "gold";
            public static int iCurrency = 0;
            public static string sCarryName = "pound";
            public static int iCarryCap = 0;

            // Permissions dictionaries
            public static Dictionary<string, string> CitnumPermLevel = new Dictionary<string, string>();
            public static DataTable CMDPermLevel = new DataTable();

            // SQLITE connection
            public static SQLiteConnection m_db;

        }


        // The form's starting point
        private void Form1_Load(object sender, EventArgs e)
        {

            // Initialize and start the timer
            aTimer = new Timer();
            aTimer.Tick += new EventHandler(aTimer_Tick);
            aTimer.Interval = 100;
            aTimer.Start();
        }





        private void butLoginUniv_Click(object sender, EventArgs e)
        {

            // Are we already logged in? Abort if so
            if (Globals.iInUniv == true)
            {
                Stat(1, "Error", "Already logged in!", "red");
                return;
            }

            // Disable universe login button
            butLoginUniv.Enabled = false;

            // Open the login manager form
            Stat(1, "Login Manager", "Opening the login manager.", "black");
            Form2 frm = new Form2();
            frm.ShowDialog();

            // Init the AW API
            Stat(1, "API Init", "Initializing the AW API.", "black");
            _instance = new Instance();

            // Install events & callbacks
            Stat(1, "API Init", "Installing events and callbacks.", "black");
            //_instance.EventAvatarAdd += OnEventAvatarAdd;
            _instance.EventChat += OnEventChat;

            // Set universe login parameters
            _instance.Attributes.LoginName = Globals.sBotName;
            _instance.Attributes.LoginOwner = Globals.iCitNum;
            _instance.Attributes.LoginPrivilegePassword = Globals.sPassword;
            _instance.Attributes.LoginApplication = Globals.sBotDesc;

            // Log into universe
            Stat(1, "Universe Login", "Entering universe.", "black");
            var rc = _instance.Login();
            if (rc != Result.Success)
            {
                Stat(1, "Error", "Failed to log in to universe (reason: " + rc + ").", "red");
                return;
            }
            else
            {
                Stat(1, "Universe Login", "Universe entry successful.", "black");
                Globals.iInUniv = true;
                butLoginWorld.Enabled = true;
                butLogOut.Enabled = true;
            }

            // Initialize and start the timer
            aTimer = new Timer();
            aTimer.Tick += new EventHandler(aTimer_Tick);
            aTimer.Interval = 100;
            aTimer.Start();
        }

        private void butLoginWorld_Click(object sender, EventArgs e)
        {

            // Check universe login state and abort if we're not already logged in
            if (Globals.iInUniv == false)
            {
                Stat(1, "Error", "Not logged into universe! Aborting.", "red");
                return;
            }

            // Check world login state and abort if we're already logged in
            if (Globals.iInWorld == true)
            {
                Stat(1, "Error", "Already logged into world! Aborting.", "red");
                return;
            }

            // Disable world login button (and logout button, temporarily)
            butLoginWorld.Enabled = false;
            butLogOut.Enabled = false;

            // Enter world
            Stat(1, "World Login", "Logging into world " + Globals.sWorld + ".", "black");
            var rc = _instance.Enter(Globals.sWorld);
            if (rc != Result.Success)
            {
                Stat(1, "Error", "Failed to log into world " + Globals.sWorld + " (reason:" + rc + ").", "red");
                butLogOut.Enabled = true;
                return;
            }
            else
            {
                Stat(1, "World Login", "World entry successful.", "black");
                Globals.iInWorld = true;
            }

            // Commit the positioning and become visible
            Stat(1, "World Pos", "Changing position in world.", "black");
            _instance.Attributes.MyX = Globals.iXPos;
            _instance.Attributes.MyY = Globals.iYPos;
            _instance.Attributes.MyZ = Globals.iZPos;
            _instance.Attributes.MyYaw = Globals.iYaw;
            _instance.Attributes.MyType = Globals.iAV;

            rc = _instance.StateChange();
            if (rc == Result.Success)
            {
                Stat(1, "World Pos", "Movement successful.", "black");
            }
            else
            {
                Stat(1, "World Pos", "Movement aborted (reason: " + rc + ").", "red");
            }

            // Enable all the appropriate buttons
            butLogOut.Enabled = true;
            butMove2Coords.Enabled = true;
            butSimStart.Enabled = true;
            butSendChat.Enabled = true;
            butSimConfig.Enabled = true;

        }


        private void butLogOut_Click(object sender, EventArgs e)
        {
            // Check for login state
            if (Globals.iInUniv == false)
            {
                Stat(1, "Error", "Not in universe. Aborted.", "red");
                return;
            }

            // Dispose of the API instance, reset all flags
            _instance.Dispose();
            Stat(1, "Logout", "Logged out.", "black");
            Globals.iInUniv = false;
            Globals.iInWorld = false;
            Globals.iSimRun = false;

            // Disable all buttons except universe login
            butLoginUniv.Enabled = true;
            butLoginWorld.Enabled = false;
            butLogOut.Enabled = false;
            butMove2Coords.Enabled = false;
            butSendChat.Enabled = false;
            butSimConfig.Enabled = false;
            butSimStart.Enabled = false;
            butSimStatus.Enabled = false;
            butSimStop.Enabled = false;
        }

        private void butSendChat_Click(object sender, EventArgs e)
        {
            // Read the input box
            string send = txtSendChat.Text;
            _instance.Say(send);
            Stat(1, "Chat", "Sent chat text to chat window", "black");
            Chat(1, _instance.Attributes.LoginName, send, "black");
        }

        private void butSimStart_Click(object sender, EventArgs e)
        {
            // Disabled the buttons for this and sim config, enable the button for "stop sim"
            butSimStart.Enabled = false;
            butSimConfig.Enabled = false;
            butSimStop.Enabled = true;


            Stat(1, "Sim Start", "Started Active Simulator profile '" + Globals.sSimProfile + "'", "black");

            // Load Sim Data
            SimDataLoad();

            // Load permissions dictionaries
            LoadPerms();
            Globals.iSimRun = true;
        }

        private void butSimStop_Click(object sender, EventArgs e)
        {
            // Disabled the buttons for this and enable sim config & start sim
            butSimStart.Enabled = true;
            butSimConfig.Enabled = true;
            butSimStop.Enabled = false;

            Stat(1, "Sim Start", "Stopped Active Simulator profile '" + Globals.sSimProfile + "'", "black");
            Globals.iSimRun = false;
        }

        // timer function for the AW Wait function
        private void aTimer_Tick(object source, EventArgs e)
        {
            if (Globals.iInUniv)
            {
                Utility.Wait(0);
            }
        }

        // Form1 is closing; let's do a clean log out of the universe first
        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _instance.Dispose();
            Stat(1, "Logout", "Logged out.", "black");
            Globals.iInUniv = false;
            Globals.m_db.Close();
        }



        private void Chat(int icon, string speaker, string message, string color)
        {
            DateTime now = DateTime.Now;
            string dt = String.Format("{0:M/d/yyyy - HH:mm:ss}", now);
            ListViewItem temp = new ListViewItem(dt, 0);
            temp.UseItemStyleForSubItems = false;
            temp.SubItems.Add(speaker);
            temp.SubItems.Add(message);
            if (color == "blue")
            {
                temp.SubItems[2].Font = new System.Drawing.Font(ChatMon.Font, System.Drawing.FontStyle.Italic);
                temp.SubItems[2].ForeColor = System.Drawing.Color.FromName(color);
            }
            else
            {
                temp.SubItems[2].Font = new System.Drawing.Font(ChatMon.Font, System.Drawing.FontStyle.Regular);
                temp.SubItems[2].ForeColor = System.Drawing.Color.FromName("black");
            }


            ChatMon.Items.Add(temp);
            ChatMon.EnsureVisible(ChatMon.Items.Count - 1);
        }

        private void Stat(int icon, string action, string message, string color)
        {
            DateTime now = DateTime.Now;
            string dt = String.Format("{0:M/d/yyyy - HH:mm:ss}", now);
            ListViewItem item = new ListViewItem(dt, 0);
            item.UseItemStyleForSubItems = false;
            item.SubItems.Add(action);
            item.SubItems.Add(message);

            item.SubItems[1].Font = new System.Drawing.Font(ChatMon.Font, System.Drawing.FontStyle.Bold);
            item.SubItems[1].ForeColor = System.Drawing.Color.FromName(color);
            item.SubItems[2].ForeColor = System.Drawing.Color.FromName(color);
            StatMon.Items.Add(item);
            StatMon.EnsureVisible(StatMon.Items.Count - 1);

        }

        private void OnEventChat(IInstance sender)
        {
            
            // Echo the chat (or whisper) to the chat window
            if (sender.Attributes.ChatType == ChatTypes.Whisper)
            {
                Chat(1, sender.Attributes.AvatarName, "(whispered): " + sender.Attributes.ChatMessage, "blue");
            }
            else
            {
                Chat(1, sender.Attributes.AvatarName, sender.Attributes.ChatMessage, "black");
            }

            // If the simulation is not started, go no further
            if (Globals.iSimRun == false)
            {
                return;
            }

            
            // If command is whispered rather than stated aloud in chat
            int iType;
            if (sender.Attributes.ChatType == ChatTypes.Whisper)
            {
                iType = 2;
            }
            else
            {
                iType = 1;
            }

            // Send the chat to the parser with session ID, speaker, message, chattype, etc.
            Commands(sender.Attributes.AvatarName, iType, sender.Attributes.ChatSession, sender.Attributes.ChatMessage);

        }


    }
}
