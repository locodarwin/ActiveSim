using System;
using System.Windows.Forms;
using AW;
using System.Data.SQLite;

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
            ChatMon.Columns.Add("Speaker", 120, HorizontalAlignment.Left);
            ChatMon.Columns.Add("Message", 480, HorizontalAlignment.Left);

            StatMon.View = View.Details;
            StatMon.Columns.Add("Time (Local)", 120, HorizontalAlignment.Left);
            StatMon.Columns.Add("Action", 120, HorizontalAlignment.Left);
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


        }

        // Class for the public global variables
        public static class Globals
        {
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
                Stat(1, "Error", "Already logged in!", "color");
                return;
            }

            // Disable universe login button
            butLoginUniv.Enabled = false;

            // Open the login manager form
            Stat(1, "Log In", "Opening the login manager", "color");
            Form2 frm = new Form2();
            frm.ShowDialog();

            // Init the AW API
            Stat(1, "Init", "Initializing the AW API", "color");
            _instance = new Instance();

            // Install events & callbacks
            Stat(1, "Init", "Installing events and callbacks.", "color");
            //_instance.EventAvatarAdd += OnEventAvatarAdd;
            _instance.EventChat += OnEventChat;

            // Set universe login parameters
            _instance.Attributes.LoginName = Globals.sBotName;
            _instance.Attributes.LoginOwner = Globals.iCitNum;
            _instance.Attributes.LoginPrivilegePassword = Globals.sPassword;
            _instance.Attributes.LoginApplication = Globals.sBotDesc;

            // Log into universe
            Stat(1, "Log In", "Entering universe", "color");
            var rc = _instance.Login();
            if (rc != Result.Success)
            {
                Stat(1, "Log In", "Failed to log in to universe (reason:" + rc + ").", "color");
                return;
            }
            else
            {
                Stat(1, "Log In", "Universe entry successfull", "color");
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
                Stat(1, "Error", "Not logged into universe! Aborting.", "color");
                return;
            }

            // Check world login state and abort if we're already logged in
            if (Globals.iInWorld == true)
            {
                Stat(1, "Error", "Already logged into world! Aborting.", "color");
                return;
            }

            // Disable world login button (and logout button, temporarily)
            butLoginWorld.Enabled = false;
            butLogOut.Enabled = false;

            // Enter world
            Stat(1, "World Login", "Logging into world " + Globals.sWorld + ".", "color");
            var rc = _instance.Enter(Globals.sWorld);
            if (rc != Result.Success)
            {
                Stat(1, "Error", "Failed to log into world" + Globals.sWorld + " (reason:" + rc + ").", "color");
                return;
            }
            else
            {
                Stat(1, "World Login", "World entry successful.", "color");
                Globals.iInWorld = true;
            }

            // Commit the positioning and become visible
            Stat(1, "World Pos", "Changing position in world.", "color");
            _instance.Attributes.MyX = Globals.iXPos;
            _instance.Attributes.MyY = Globals.iYPos;
            _instance.Attributes.MyZ = Globals.iZPos;
            _instance.Attributes.MyYaw = Globals.iYaw;
            _instance.Attributes.MyType = Globals.iAV;

            rc = _instance.StateChange();
            if (rc == Result.Success)
            {
                Stat(1, "World Pos", "Movement successful.", "color");
            }
            else
            {
                Stat(1, "World Pos", "Movement aborted (reason: " + rc + ").", "color");
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
                Stat(1, "Error", "Not in universe. Aborted.", "color");
                return;
            }

            // Dispose of the API instance, reset all flags
            _instance.Dispose();
            Stat(1, "Logout", "Logged out.", "color");
            Globals.iInUniv = false;
            Globals.iInWorld = false;

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
            if (Globals.iInUniv == false)
            {
                return;
            }
            _instance.Dispose();
            Stat(1, "Logout", "Logged out.", "color");
            Globals.iInUniv = false;
        }

      

        private void Chat(int icon, string speaker, string message, string color)
        {
            DateTime now = DateTime.Now;
            string dt = String.Format("{0:d/M/yyyy - HH:mm:ss}", now);
            ListViewItem temp = new ListViewItem(dt, 0);
            temp.SubItems.Add(speaker);
            temp.SubItems.Add(message);
            //temp.SubItems.Add("Hello, cruel world!", System.Drawing.Color.Red, System.Drawing.Color.Black);  //fucked
            ChatMon.Items.Add(temp);
            ChatMon.EnsureVisible(ChatMon.Items.Count - 1);
        }

        private void Stat(int icon, string action, string message, string color)
        {
            DateTime now = DateTime.Now;
            string dt = String.Format("{0:d/M/yyyy - HH:mm:ss}", now);
            ListViewItem temp = new ListViewItem(dt, 0);
            temp.SubItems.Add(action);
            temp.SubItems.Add(message);
            //temp.SubItems.Add("Hello, cruel world!", System.Drawing.Color.Red, System.Drawing.Color.Black);  //fucked
            StatMon.Items.Add(temp);
            StatMon.EnsureVisible(StatMon.Items.Count - 1);

        }

        private void OnEventChat(IInstance sender)
        {
            Chat(1, sender.Attributes.AvatarName, sender.Attributes.ChatMessage, "color");
        }

        private void butSendChat_Click(object sender, EventArgs e)
        {
            // Read the input box
            string send = txtSendChat.Text;
            _instance.Say(send);
            Stat(1, "Chat", "Sent chat text to chat window", "color");
            Chat(1, _instance.Attributes.LoginName, send, "color");
        }
    }
}
