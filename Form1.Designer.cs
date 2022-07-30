
namespace ArduinoLCD16x2
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnWrite = new System.Windows.Forms.Button();
            this.cmbPorts = new System.Windows.Forms.ComboBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.textSerial = new System.Windows.Forms.RichTextBox();
            this.getCpuInfo = new System.Windows.Forms.Timer(this.components);
            this.cpuInfo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.cbAutomatic = new System.Windows.Forms.CheckBox();
            this.panelManual = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelManual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnWrite
            // 
            this.btnWrite.BackColor = System.Drawing.SystemColors.GrayText;
            this.btnWrite.FlatAppearance.BorderSize = 0;
            this.btnWrite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWrite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWrite.Location = new System.Drawing.Point(299, 9);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(64, 23);
            this.btnWrite.TabIndex = 0;
            this.btnWrite.Text = "Start";
            this.btnWrite.UseVisualStyleBackColor = false;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // cmbPorts
            // 
            this.cmbPorts.FormattingEnabled = true;
            this.cmbPorts.Location = new System.Drawing.Point(0, 9);
            this.cmbPorts.Name = "cmbPorts";
            this.cmbPorts.Size = new System.Drawing.Size(293, 21);
            this.cmbPorts.TabIndex = 1;
            this.cmbPorts.SelectedIndexChanged += new System.EventHandler(this.cmbPorts_SelectedIndexChanged);
            // 
            // serialPort1
            // 
            this.serialPort1.WriteBufferSize = 4096;
            this.serialPort1.WriteTimeout = 5000;
            // 
            // textSerial
            // 
            this.textSerial.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.textSerial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textSerial.Cursor = System.Windows.Forms.Cursors.Default;
            this.textSerial.ForeColor = System.Drawing.Color.PaleGreen;
            this.textSerial.Location = new System.Drawing.Point(12, 86);
            this.textSerial.Name = "textSerial";
            this.textSerial.ReadOnly = true;
            this.textSerial.Size = new System.Drawing.Size(363, 128);
            this.textSerial.TabIndex = 2;
            this.textSerial.Text = "";
            this.textSerial.MouseMove += new System.Windows.Forms.MouseEventHandler(this.textSerial_MouseMove);
            // 
            // getCpuInfo
            // 
            this.getCpuInfo.Interval = 2000;
            this.getCpuInfo.Tick += new System.EventHandler(this.getCpuInfo_Tick);
            // 
            // cpuInfo
            // 
            this.cpuInfo.AutoSize = true;
            this.cpuInfo.Location = new System.Drawing.Point(310, 13);
            this.cpuInfo.Name = "cpuInfo";
            this.cpuInfo.Size = new System.Drawing.Size(0, 13);
            this.cpuInfo.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.label1.Location = new System.Drawing.Point(12, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ⓒ Jardin Recto  @ 2021";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(48, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Hardware i2c Display";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.GrayText;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(346, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(29, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "x";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.SystemColors.GrayText;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimize.Location = new System.Drawing.Point(311, 9);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(29, 23);
            this.btnMinimize.TabIndex = 6;
            this.btnMinimize.Text = "-";
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "i2cDisplay";
            this.notifyIcon1.BalloonTipTitle = "i2cDisplay";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "i2c Display(16x2)";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // cbAutomatic
            // 
            this.cbAutomatic.AutoSize = true;
            this.cbAutomatic.Location = new System.Drawing.Point(302, 223);
            this.cbAutomatic.Name = "cbAutomatic";
            this.cbAutomatic.Size = new System.Drawing.Size(73, 17);
            this.cbAutomatic.TabIndex = 8;
            this.cbAutomatic.Text = "Automatic";
            this.cbAutomatic.UseVisualStyleBackColor = true;
            this.cbAutomatic.CheckedChanged += new System.EventHandler(this.cbAutomatic_CheckedChanged);
            // 
            // panelManual
            // 
            this.panelManual.Controls.Add(this.cmbPorts);
            this.panelManual.Controls.Add(this.btnWrite);
            this.panelManual.Location = new System.Drawing.Point(12, 41);
            this.panelManual.Name = "panelManual";
            this.panelManual.Size = new System.Drawing.Size(363, 39);
            this.panelManual.TabIndex = 9;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::ArduinoLCD16x2.Properties.Resources.jr_logo_1_3;
            this.pictureBox1.Location = new System.Drawing.Point(12, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(387, 246);
            this.Controls.Add(this.panelManual);
            this.Controls.Add(this.cbAutomatic);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cpuInfo);
            this.Controls.Add(this.textSerial);
            this.ForeColor = System.Drawing.Color.LimeGreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "MainProcessor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panelManual.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.ComboBox cmbPorts;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.RichTextBox textSerial;
        private System.Windows.Forms.Timer getCpuInfo;
        private System.Windows.Forms.Label cpuInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.CheckBox cbAutomatic;
        private System.Windows.Forms.Panel panelManual;
    }
}

