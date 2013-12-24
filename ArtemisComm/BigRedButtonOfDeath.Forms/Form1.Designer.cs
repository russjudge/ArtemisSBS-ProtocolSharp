namespace BigRedButtonOfDeath.Forms
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
            this.btnSelfDestruct = new System.Windows.Forms.Button();
            this.connectBox = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpStandby = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.grpShip = new System.Windows.Forms.GroupBox();
            this.lstShips = new System.Windows.Forms.ListBox();
            this.btnShip = new System.Windows.Forms.Button();
            this.grpSimulationEnded = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.connectBox.SuspendLayout();
            this.grpStandby.SuspendLayout();
            this.grpShip.SuspendLayout();
            this.grpSimulationEnded.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelfDestruct
            // 
            this.btnSelfDestruct.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSelfDestruct.BackColor = System.Drawing.Color.Red;
            this.btnSelfDestruct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelfDestruct.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelfDestruct.Location = new System.Drawing.Point(0, 0);
            this.btnSelfDestruct.Name = "btnSelfDestruct";
            this.btnSelfDestruct.Size = new System.Drawing.Size(334, 269);
            this.btnSelfDestruct.TabIndex = 0;
            this.btnSelfDestruct.Text = "Push for Self-Destruct";
            this.btnSelfDestruct.UseVisualStyleBackColor = false;
            this.btnSelfDestruct.Visible = false;
            this.btnSelfDestruct.Click += new System.EventHandler(this.OnSelfDestruct);
            // 
            // connectBox
            // 
            this.connectBox.BackColor = System.Drawing.Color.Tomato;
            this.connectBox.Controls.Add(this.btnConnect);
            this.connectBox.Controls.Add(this.txtPort);
            this.connectBox.Controls.Add(this.txtHost);
            this.connectBox.Controls.Add(this.label2);
            this.connectBox.Controls.Add(this.label1);
            this.connectBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectBox.Location = new System.Drawing.Point(13, 13);
            this.connectBox.Name = "connectBox";
            this.connectBox.Size = new System.Drawing.Size(206, 103);
            this.connectBox.TabIndex = 1;
            this.connectBox.TabStop = false;
            this.connectBox.Text = "Set Server";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(125, 69);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.OnConnect);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(100, 43);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "2010";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(100, 17);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(100, 20);
            this.txtHost.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Artemis Server:";
            // 
            // grpStandby
            // 
            this.grpStandby.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpStandby.BackColor = System.Drawing.Color.LightGreen;
            this.grpStandby.Controls.Add(this.label4);
            this.grpStandby.Controls.Add(this.label3);
            this.grpStandby.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpStandby.Location = new System.Drawing.Point(161, 148);
            this.grpStandby.Name = "grpStandby";
            this.grpStandby.Size = new System.Drawing.Size(247, 56);
            this.grpStandby.TabIndex = 2;
            this.grpStandby.TabStop = false;
            this.grpStandby.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "Please Stand by...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(227, 25);
            this.label4.TabIndex = 1;
            this.label4.Text = "Waiting for game to start.";
            // 
            // grpShip
            // 
            this.grpShip.BackColor = System.Drawing.Color.LightSalmon;
            this.grpShip.Controls.Add(this.btnShip);
            this.grpShip.Controls.Add(this.lstShips);
            this.grpShip.Location = new System.Drawing.Point(286, 12);
            this.grpShip.Name = "grpShip";
            this.grpShip.Size = new System.Drawing.Size(122, 77);
            this.grpShip.TabIndex = 3;
            this.grpShip.TabStop = false;
            this.grpShip.Text = "Select Ship";
            this.grpShip.Visible = false;
            // 
            // lstShips
            // 
            this.lstShips.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstShips.FormattingEnabled = true;
            this.lstShips.Location = new System.Drawing.Point(7, 17);
            this.lstShips.Name = "lstShips";
            this.lstShips.Size = new System.Drawing.Size(109, 17);
            this.lstShips.TabIndex = 0;
            // 
            // btnShip
            // 
            this.btnShip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShip.Location = new System.Drawing.Point(102, 43);
            this.btnShip.Name = "btnShip";
            this.btnShip.Size = new System.Drawing.Size(0, 23);
            this.btnShip.TabIndex = 1;
            this.btnShip.Text = "Select";
            this.btnShip.UseVisualStyleBackColor = true;
            this.btnShip.Click += new System.EventHandler(this.OnSelectShip);
            // 
            // grpSimulationEnded
            // 
            this.grpSimulationEnded.BackColor = System.Drawing.Color.DarkKhaki;
            this.grpSimulationEnded.Controls.Add(this.label5);
            this.grpSimulationEnded.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSimulationEnded.Location = new System.Drawing.Point(23, 134);
            this.grpSimulationEnded.Name = "grpSimulationEnded";
            this.grpSimulationEnded.Size = new System.Drawing.Size(200, 100);
            this.grpSimulationEnded.TabIndex = 4;
            this.grpSimulationEnded.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Simulation Ended";
            // 
            // Form1
            // 
            this.AcceptButton = this.btnConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(334, 269);
            this.Controls.Add(this.grpSimulationEnded);
            this.Controls.Add(this.grpShip);
            this.Controls.Add(this.grpStandby);
            this.Controls.Add(this.connectBox);
            this.Controls.Add(this.btnSelfDestruct);
            this.Name = "Form1";
            this.Text = "The Big Red Button of Death";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClosed);
            this.connectBox.ResumeLayout(false);
            this.connectBox.PerformLayout();
            this.grpStandby.ResumeLayout(false);
            this.grpStandby.PerformLayout();
            this.grpShip.ResumeLayout(false);
            this.grpSimulationEnded.ResumeLayout(false);
            this.grpSimulationEnded.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelfDestruct;
        private System.Windows.Forms.GroupBox connectBox;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpStandby;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grpShip;
        private System.Windows.Forms.Button btnShip;
        private System.Windows.Forms.ListBox lstShips;
        private System.Windows.Forms.GroupBox grpSimulationEnded;
        private System.Windows.Forms.Label label5;
    }
}

