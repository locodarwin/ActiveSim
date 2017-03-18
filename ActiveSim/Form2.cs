using System;
using System.Windows.Forms;
using AW;
using System.Data.SQLite;


namespace ActiveSim
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            // Get the default login table from the database
            string sql = "select * from LoginProfiles where ProfileName = 'Default'";
            SQLiteCommand cmd = new SQLiteCommand(sql, Form1.Globals.m_db);
            SQLiteDataReader reader = cmd.ExecuteReader();
            
            // Read login data from database and save into global login variables
            while(reader.Read())
            {
                Form1.Globals.sUnivLogin = reader["LoginUniv"].ToString();
                Form1.Globals.iPort = Convert.ToInt32(reader["LoginPort"]);
                Form1.Globals.sBotName = reader["LoginName"].ToString();
                Form1.Globals.iCitNum = Convert.ToInt32(reader["LoginCitnum"]);
                Form1.Globals.sPassword = reader["LoginPassword"].ToString();
                Form1.Globals.sWorld = reader["LoginWorld"].ToString();

                Form1.Globals.iXPos = Convert.ToInt32(reader["LoginX"]);
                Form1.Globals.iYPos = Convert.ToInt32(reader["LoginY"]);
                Form1.Globals.iZPos = Convert.ToInt32(reader["LoginZ"]);
                Form1.Globals.iAV = Convert.ToInt32(reader["LoginAV"]);
                Form1.Globals.iYaw = Convert.ToInt32(reader["LoginYaw"]);

            }

            // Populate form textboxes with login data pulled from database
            txtHost.Text = Form1.Globals.sUnivLogin;
            txtPort.Text = Convert.ToString(Form1.Globals.iPort);
            txtBotname.Text = Form1.Globals.sBotName;
            txtCitnum.Text = Convert.ToString(Form1.Globals.iCitNum);
            txtPassword.Text = Form1.Globals.sPassword;
            txtWorld.Text = Form1.Globals.sWorld;
            txtX.Text = Convert.ToString(Form1.Globals.iXPos);
            txtY.Text = Convert.ToString(Form1.Globals.iYPos);
            txtZ.Text = Convert.ToString(Form1.Globals.iZPos);
            txtYaw.Text = Convert.ToString(Form1.Globals.iYaw);
            txtAV.Text = Convert.ToString(Form1.Globals.iAV);

        }

        private void butLogin_Click(object sender, EventArgs e)
        {
            // Populate login globals with form contents
            Form1.Globals.sUnivLogin = txtHost.Text;
            Form1.Globals.iPort = Convert.ToInt32(txtPort.Text);
            Form1.Globals.sBotName = txtBotname.Text;
            Form1.Globals.iCitNum = Convert.ToInt32(txtCitnum.Text);
            Form1.Globals.sPassword = txtPassword.Text;
            Form1.Globals.sWorld = txtWorld.Text;
            Form1.Globals.iXPos = Convert.ToInt32(txtX.Text);
            Form1.Globals.iYPos = Convert.ToInt32(txtY.Text);
            Form1.Globals.iZPos = Convert.ToInt32(txtZ.Text);
            Form1.Globals.iYaw = Convert.ToInt32(txtYaw.Text);
            Form1.Globals.iAV = Convert.ToInt32(txtAV.Text);

            // Then close the form so form1 can perform the login
            this.Close();

        }
    }
}
