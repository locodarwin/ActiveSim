using System;
using System.Windows.Forms;
using AW;
using System.Data.SQLite;

namespace ActiveSim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Set up the columns in the listviews (ChatMon and StatMon)
            ChatMon.View = View.Details;
            ChatMon.Columns.Add("Time (Local)", 120, HorizontalAlignment.Left);
            ChatMon.Columns.Add("Speaker", 120, HorizontalAlignment.Left);
            ChatMon.Columns.Add("Message", 500, HorizontalAlignment.Left);

            StatMon.View = View.Details;
            StatMon.Columns.Add("Time (Local)", 120, HorizontalAlignment.Left);
            StatMon.Columns.Add("Action", 120, HorizontalAlignment.Left);
            StatMon.Columns.Add("Message", 500, HorizontalAlignment.Left);

            
            Globals.m_db = new SQLiteConnection("data source=activesim.db;version=3");
            Globals.m_db.Open();


        }

        // Timer identifiers out here in public
        public IInstance _instance;
        public Timer aTimer;

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

            // Load database in here somewhere?


            // Status("Ready to log into universe.");
            // txtHost.Text = Globals.sUnivLogin;
            // txtPort.Text = Convert.ToString(Globals.iPort);
            // txtName.Text = Globals.sBotName;
            // txtDesc.Text = Globals.sBotDesc;
            //  txtCitNum.Text = Convert.ToString(Globals.iCitNum);
            //  txtPassword.Text = Globals.sPassword;
            // txtWorld.Text = Globals.sWorld;
            // txtXPos.Text = Convert.ToString(Globals.iXPos);
            // txtYPos.Text = Convert.ToString(Globals.iYPos);
            //txtZPos.Text = Convert.ToString(Globals.iZPos);
            //txtYaw.Text = Convert.ToString(Globals.iYaw);
            //txtAV.Text = Convert.ToString(Globals.iAV);

            // Initialize and start the timer
            aTimer = new Timer();
            aTimer.Tick += new EventHandler(aTimer_Tick);
            aTimer.Interval = 100;
            aTimer.Start();
        }









        private void butLoginUniv_Click(object sender, EventArgs e)
        {
            //Chat(1, "Speaker", "Message", "green");
            Stat(1, "Log In", "Opening the login manager", "color");

            Form2 frm = new Form2();
            frm.Show();

            // do a test query of the database
            //string sql = "select * from LoginProfiles where ProfileName = 'Default'";
            //SQLiteCommand cmd = new SQLiteCommand(sql, Globals.m_db);
            //SQLiteDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //    Console.WriteLine("Name: " + reader["LoginUniv"] + "\tScore: " + reader["LoginName"]);

        }




        // Timer function for the AW Wait function
        private void aTimer_Tick(object source, EventArgs e)
        {
            if (Globals.iInWorld)
            {
                Utility.Wait(0);
            }
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
        }
    }
}
