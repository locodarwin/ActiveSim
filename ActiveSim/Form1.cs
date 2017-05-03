using System;
using System.Windows.Forms;
using System.Collections.Generic;
using AW;
using System.Data.SQLite;
using System.Data;
using System.Timers;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;

namespace ActiveSim
{
    public partial class Form1 : Form
    {

        // Timer identifiers created out here in public
        public static IInstance _instance;
        public System.Windows.Forms.Timer aTimer;
        //public System.Timers.Timer aCadence;
        // public System.Threading.Timer aCadence;
        public System.Windows.Forms.Timer aCadence;

        // Cadence 
        private readonly BackgroundWorker CadenceWorker;

        // Delegates
        public delegate void StatDelegate();

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

            // Prepare Cadence as a background worker
            CadenceWorker = new BackgroundWorker();
            CadenceWorker.WorkerReportsProgress = true;
            CadenceWorker.DoWork += CadenceThread;
            CadenceWorker.ProgressChanged += CadenceProgressChanged;
            CadenceWorker.RunWorkerCompleted += CadenceRunWorkerCompleted;

            Stat(1, "Startup", Globals.sAppName + " " + Globals.sVersion + " - " + Globals.sByline + ".", "black");
            Stat(1, "Startup", "Bot started and ready to log in.", "black");

            this.FormClosing += Form1_Closing;

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
            public static int iYPos = 0;
            public static int iZPos = 0;
            public static int iYaw = 0;
            public static int iAV = 1;

            public static bool iInUniv = false;
            public static bool iInWorld = false;
            public static bool iSimRun = false;
            public static bool iCadenceOn = false;

            public static string sSimProfile = "Default";
            public static string sCurrencyName = "gold";
            public static int iCurrency = 0;
            public static string sCarryName = "pound";
            public static int iCarryCap = 0;

            // This is a test of multithreading the "Stat" status update feature
            //(int icon, string action, string message, string color)
            public static int StatIcon;
            public static string StatAction, StatMessage, StatColor;

            // Debugging
            public static bool Debug = false;
            public static string Error = "";

            // Console colors (for RGB, switch places the first byte (i.e. ff) with last byte, so 0x336699 = 0x996633
            public static int ColorInv, ColorPresentList, ColorRegList;

            // Generic counters
            public static int iCount;

            // World user list
            public static DataTable CitTable = new DataTable();
            
            // World farm objects & related
            public static List<WorldFarmItem> WorldFarmItemList = new List<WorldFarmItem>();
            public static string TilledSoil = "plantingdirtsq.rwx";

            // Permissions dictionaries
            public static Dictionary<string, string> CitnumPermLevel = new Dictionary<string, string>();
            public static DataTable CMDPermLevel = new DataTable();

            // Sim rules
            public static string sCaptain = "318855";

            // SQLITE connection
            public static SQLiteConnection m_db;

        }

        // The form's starting point
        private void Form1_Load(object sender, EventArgs e)
        {

            // Initialize the AW.Wait() timer
            aTimer = new System.Windows.Forms.Timer();
            aTimer.Tick += new EventHandler(aTimer_Tick);
            aTimer.Interval = 100;
            //aTimer.Start();

            // Initialize the Cadence timer
            aCadence = new System.Windows.Forms.Timer();
            aCadence.Tick += new EventHandler(Cadence);
            aCadence.Interval = 60000;
            aCadence.Enabled = true;

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
            _instance.EventAvatarAdd += OnEventAvatarAdd;
            _instance.EventAvatarDelete += OnEventAvatarDelete;
            _instance.EventChat += OnEventChat;
            _instance.EventHudClick += OnEventHUDClick;
            _instance.EventObjectClick += OnEventObjectClick;

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
                Stat(1, "Universe Login", "Universe entry successful. (" + rc + ")", "black");
                Globals.iInUniv = true;
                butLoginWorld.Enabled = true;
                butLogOut.Enabled = true;
            }

            // Clear and then add columns to CitTable
            Globals.CitTable.Clear();
            Globals.CitTable.Columns.Clear();
            Globals.CitTable.Columns.Add("Name", typeof(string));
            Globals.CitTable.Columns.Add("Session", typeof(int));
            Globals.CitTable.Columns.Add("Registered", typeof(string));
            Globals.CitTable.Columns.Add("PermLevel", typeof(string));
            Globals.CitTable.Columns.Add("Citnum", typeof(string));
            Globals.CitTable.Columns.Add("PlantFlag", typeof(string));

            // Initialize and start the timer
            aTimer = new System.Windows.Forms.Timer();
            aTimer.Tick += new EventHandler(aTimer_Tick);
            aTimer.Interval = 100;
            aTimer.Start();
        }

        private void OnCallbackEnter(IInstance sender, Result result)
        {
            throw new NotImplementedException();
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

            // Prepare caretakermode
            _instance.Attributes.EnterGlobal = true;

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

            // Turn off & kill timer (if it's on)
            aTimer.Stop();

            // Turn off & kill Cadence (if running)
            if (Globals.iCadenceOn == true)
            {
                aCadence.Stop();
                //aCadence.Change(Timeout.Infinite, Timeout.Infinite);
                Stat(1, "Cadence", "Cadence turned off", "black");
                Globals.iCadenceOn = false;
            }
            

            // Dispose of the API instance, reset all flags
            _instance.HudClear(0);
            _instance.Dispose();
            Utility.Wait(0);
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
            // Call StartSim()
            SimStart();
        }

        private void butSimStop_Click(object sender, EventArgs e)
        {
            // Call StopSim();
            SimStop();
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

            // Turn off & kill Cadence (if running)
            if (Globals.iCadenceOn == true)
            {
                aCadence.Stop();
                //aCadence.Change(Timeout.Infinite, Timeout.Infinite);
                Stat(1, "Cadence", "Cadence turned off", "black");
                Globals.iCadenceOn = false;
            }

            // turn off HUD
            //_instance.HudClear(0);c
            Stat(1, "Logout", "Logged out.", "black");
            Globals.iInUniv = false;
            Globals.m_db.Close();
            //_instance.Dispose();
            //Utility.Wait(0);

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

            Globals.StatIcon = icon;
            Globals.StatAction = action;
            Globals.StatMessage = message;
            Globals.StatColor = color;

            if (ControlInvokeRequired(StatMon, () => ExecStat())) return;

            DateTime now = DateTime.Now;
            string dt = String.Format("{0:M/d/yyyy - HH:mm:ss}", now);
            ListViewItem item = new ListViewItem(dt, 0);
            item.UseItemStyleForSubItems = false;
            item.SubItems.Add(Globals.StatAction);
            item.SubItems.Add(Globals.StatMessage);

            item.SubItems[1].Font = new System.Drawing.Font(ChatMon.Font, System.Drawing.FontStyle.Bold);
            item.SubItems[1].ForeColor = System.Drawing.Color.FromName(Globals.StatColor);
            item.SubItems[2].ForeColor = System.Drawing.Color.FromName(Globals.StatColor);

            StatMon.Items.Add(item);
            StatMon.EnsureVisible(StatMon.Items.Count - 1);
            //Console.WriteLine("I'm NOT in the threadpool, fool!");

        }

        private void ExecStat()
        {
            DateTime now = DateTime.Now;
            string dt = String.Format("{0:M/d/yyyy - HH:mm:ss}", now);
            ListViewItem item = new ListViewItem(dt, 0);
            item.UseItemStyleForSubItems = false;
            item.SubItems.Add(Globals.StatAction);
            item.SubItems.Add(Globals.StatMessage);

            item.SubItems[1].Font = new System.Drawing.Font(ChatMon.Font, System.Drawing.FontStyle.Bold);
            item.SubItems[1].ForeColor = System.Drawing.Color.FromName(Globals.StatColor);
            item.SubItems[2].ForeColor = System.Drawing.Color.FromName(Globals.StatColor);

            StatMon.Items.Add(item);
            StatMon.EnsureVisible(StatMon.Items.Count - 1);
            //Console.WriteLine("I'm in the threadpool, fool!");
        }

        /// <summary>
        /// Helper method to determin if invoke required, if so will rerun method on correct thread.
        /// if not do nothing.
        /// </summary>
        /// <param name="c">Control that might require invoking</param>
        /// <param name="a">action to preform on control thread if so.</param>
        /// <returns>true if invoke required</returns>
        public bool ControlInvokeRequired(Control c, Action a)
        {
            if (c.InvokeRequired) c.Invoke(new MethodInvoker(delegate { a(); }));
            else return false;

            return true;
        }


    }
}
