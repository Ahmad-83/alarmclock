namespace AlarmClock
{
    partial class FormAlarmClock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAlarmClock));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panelClock = new System.Windows.Forms.Panel();
            this.textBoxEndTime = new System.Windows.Forms.TextBox();
            this.labelCurrentTime = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.clockTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.panelClock.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notyfyIcon";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(93, 26);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.progressBar.Location = new System.Drawing.Point(0, 0);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(327, 107);
            this.progressBar.TabIndex = 1;
            // 
            // panelClock
            // 
            this.panelClock.BackColor = System.Drawing.Color.White;
            this.panelClock.Controls.Add(this.textBoxEndTime);
            this.panelClock.Controls.Add(this.labelCurrentTime);
            this.panelClock.Controls.Add(this.buttonStart);
            this.panelClock.Location = new System.Drawing.Point(15, 13);
            this.panelClock.Name = "panelClock";
            this.panelClock.Size = new System.Drawing.Size(299, 82);
            this.panelClock.TabIndex = 2;
            // 
            // textBoxEndTime
            // 
            this.textBoxEndTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxEndTime.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxEndTime.ForeColor = System.Drawing.Color.DarkGray;
            this.textBoxEndTime.Location = new System.Drawing.Point(2, 3);
            this.textBoxEndTime.Name = "textBoxEndTime";
            this.textBoxEndTime.Size = new System.Drawing.Size(293, 18);
            this.textBoxEndTime.TabIndex = 2;
            this.textBoxEndTime.TabStop = false;
            this.textBoxEndTime.Text = "Click to enter time";
            this.textBoxEndTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxEndTime.Click += new System.EventHandler(this.textBoxEndTime_Click);
            this.textBoxEndTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxEndTime_KeyPress);
            // 
            // labelCurrentTime
            // 
            this.labelCurrentTime.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrentTime.ForeColor = System.Drawing.Color.DarkGray;
            this.labelCurrentTime.Location = new System.Drawing.Point(2, 28);
            this.labelCurrentTime.Name = "labelCurrentTime";
            this.labelCurrentTime.Size = new System.Drawing.Size(293, 28);
            this.labelCurrentTime.TabIndex = 1;
            this.labelCurrentTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonStart
            // 
            this.buttonStart.FlatAppearance.BorderSize = 0;
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.ForeColor = System.Drawing.Color.RoyalBlue;
            this.buttonStart.Location = new System.Drawing.Point(124, 59);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(0);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(48, 22);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            this.buttonStart.MouseEnter += new System.EventHandler(this.buttonStart_MouseEnter);
            this.buttonStart.MouseLeave += new System.EventHandler(this.buttonStart_MouseLeave);
            // 
            // clockTimer
            // 
            this.clockTimer.Tick += new System.EventHandler(this.clockTimer_Tick);
            // 
            // FormAlarmClock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(327, 107);
            this.Controls.Add(this.panelClock);
            this.Controls.Add(this.progressBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAlarmClock";
            this.Text = "AlarmClock";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAlarmClock_FormClosing);
            this.Load += new System.EventHandler(this.FormAlarmClock_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panelClock.ResumeLayout(false);
            this.panelClock.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel panelClock;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Timer clockTimer;
        private System.Windows.Forms.Label labelCurrentTime;
        private System.Windows.Forms.TextBox textBoxEndTime;
    }
}

