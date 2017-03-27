namespace ActiveSim
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.butLoginUniv = new System.Windows.Forms.Button();
            this.butLoginWorld = new System.Windows.Forms.Button();
            this.butLogOut = new System.Windows.Forms.Button();
            this.butMove2Coords = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtSendChat = new System.Windows.Forms.TextBox();
            this.butSendChat = new System.Windows.Forms.Button();
            this.ChatMon = new System.Windows.Forms.ListView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.butSimStatus = new System.Windows.Forms.Button();
            this.butSimConfig = new System.Windows.Forms.Button();
            this.butSimStop = new System.Windows.Forms.Button();
            this.butSimStart = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.StatMon = new System.Windows.Forms.ListView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // butLoginUniv
            // 
            this.butLoginUniv.Location = new System.Drawing.Point(16, 20);
            this.butLoginUniv.Name = "butLoginUniv";
            this.butLoginUniv.Size = new System.Drawing.Size(109, 42);
            this.butLoginUniv.TabIndex = 0;
            this.butLoginUniv.Text = "Log Into Universe";
            this.butLoginUniv.UseVisualStyleBackColor = true;
            this.butLoginUniv.Click += new System.EventHandler(this.butLoginUniv_Click);
            // 
            // butLoginWorld
            // 
            this.butLoginWorld.Location = new System.Drawing.Point(131, 20);
            this.butLoginWorld.Name = "butLoginWorld";
            this.butLoginWorld.Size = new System.Drawing.Size(107, 42);
            this.butLoginWorld.TabIndex = 1;
            this.butLoginWorld.Text = "Log Into World";
            this.butLoginWorld.UseVisualStyleBackColor = true;
            this.butLoginWorld.Click += new System.EventHandler(this.butLoginWorld_Click);
            // 
            // butLogOut
            // 
            this.butLogOut.Location = new System.Drawing.Point(16, 68);
            this.butLogOut.Name = "butLogOut";
            this.butLogOut.Size = new System.Drawing.Size(109, 41);
            this.butLogOut.TabIndex = 2;
            this.butLogOut.Text = "Log Out";
            this.butLogOut.UseVisualStyleBackColor = true;
            this.butLogOut.Click += new System.EventHandler(this.butLogOut_Click);
            // 
            // butMove2Coords
            // 
            this.butMove2Coords.Location = new System.Drawing.Point(131, 68);
            this.butMove2Coords.Name = "butMove2Coords";
            this.butMove2Coords.Size = new System.Drawing.Size(107, 41);
            this.butMove2Coords.TabIndex = 3;
            this.butMove2Coords.Text = "Move To Coords";
            this.butMove2Coords.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(4, 462);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(234, 149);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Status";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtSendChat);
            this.groupBox3.Controls.Add(this.butSendChat);
            this.groupBox3.Controls.Add(this.ChatMon);
            this.groupBox3.Location = new System.Drawing.Point(248, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(749, 309);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "In-World Chatlog";
            // 
            // txtSendChat
            // 
            this.txtSendChat.Location = new System.Drawing.Point(114, 272);
            this.txtSendChat.Name = "txtSendChat";
            this.txtSendChat.Size = new System.Drawing.Size(629, 22);
            this.txtSendChat.TabIndex = 2;
            // 
            // butSendChat
            // 
            this.butSendChat.Location = new System.Drawing.Point(12, 267);
            this.butSendChat.Name = "butSendChat";
            this.butSendChat.Size = new System.Drawing.Size(96, 30);
            this.butSendChat.TabIndex = 1;
            this.butSendChat.Text = "Send To Chat";
            this.butSendChat.UseVisualStyleBackColor = true;
            this.butSendChat.Click += new System.EventHandler(this.butSendChat_Click);
            // 
            // ChatMon
            // 
            this.ChatMon.BackColor = System.Drawing.Color.PowderBlue;
            this.ChatMon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ChatMon.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ChatMon.Location = new System.Drawing.Point(12, 21);
            this.ChatMon.Name = "ChatMon";
            this.ChatMon.Size = new System.Drawing.Size(731, 239);
            this.ChatMon.TabIndex = 0;
            this.ChatMon.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.butSimStatus);
            this.groupBox4.Controls.Add(this.butSimConfig);
            this.groupBox4.Controls.Add(this.butSimStop);
            this.groupBox4.Controls.Add(this.butSimStart);
            this.groupBox4.Location = new System.Drawing.Point(4, 334);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(234, 121);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "The Simulator";
            // 
            // butSimStatus
            // 
            this.butSimStatus.Location = new System.Drawing.Point(121, 71);
            this.butSimStatus.Name = "butSimStatus";
            this.butSimStatus.Size = new System.Drawing.Size(107, 39);
            this.butSimStatus.TabIndex = 3;
            this.butSimStatus.Text = "Detailed Status";
            this.butSimStatus.UseVisualStyleBackColor = true;
            // 
            // butSimConfig
            // 
            this.butSimConfig.Location = new System.Drawing.Point(7, 71);
            this.butSimConfig.Name = "butSimConfig";
            this.butSimConfig.Size = new System.Drawing.Size(108, 39);
            this.butSimConfig.TabIndex = 2;
            this.butSimConfig.Text = "Simulator Config";
            this.butSimConfig.UseVisualStyleBackColor = true;
            // 
            // butSimStop
            // 
            this.butSimStop.Location = new System.Drawing.Point(121, 21);
            this.butSimStop.Name = "butSimStop";
            this.butSimStop.Size = new System.Drawing.Size(107, 41);
            this.butSimStop.TabIndex = 1;
            this.butSimStop.Text = "Stop The Simulator";
            this.butSimStop.UseVisualStyleBackColor = true;
            this.butSimStop.Click += new System.EventHandler(this.butSimStop_Click);
            // 
            // butSimStart
            // 
            this.butSimStart.Location = new System.Drawing.Point(6, 21);
            this.butSimStart.Name = "butSimStart";
            this.butSimStart.Size = new System.Drawing.Size(109, 41);
            this.butSimStart.TabIndex = 0;
            this.butSimStart.Text = "Start The Simulator";
            this.butSimStart.UseVisualStyleBackColor = true;
            this.butSimStart.Click += new System.EventHandler(this.butSimStart_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.StatMon);
            this.groupBox1.Location = new System.Drawing.Point(248, 334);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(749, 278);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status Log";
            // 
            // StatMon
            // 
            this.StatMon.BackColor = System.Drawing.Color.PowderBlue;
            this.StatMon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StatMon.Location = new System.Drawing.Point(12, 21);
            this.StatMon.Name = "StatMon";
            this.StatMon.Size = new System.Drawing.Size(731, 245);
            this.StatMon.TabIndex = 0;
            this.StatMon.UseCompatibleStateImageBehavior = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.Location = new System.Drawing.Point(0, 619);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1005, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statStrip";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1005, 641);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.butMove2Coords);
            this.Controls.Add(this.butLogOut);
            this.Controls.Add(this.butLoginWorld);
            this.Controls.Add(this.butLoginUniv);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ActiveSim";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butLoginUniv;
        private System.Windows.Forms.Button butLoginWorld;
        private System.Windows.Forms.Button butLogOut;
        private System.Windows.Forms.Button butMove2Coords;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView ChatMon;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button butSimStatus;
        private System.Windows.Forms.Button butSimConfig;
        private System.Windows.Forms.Button butSimStop;
        private System.Windows.Forms.Button butSimStart;
        private System.Windows.Forms.TextBox txtSendChat;
        private System.Windows.Forms.Button butSendChat;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView StatMon;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}

