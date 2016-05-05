namespace KP.GmailApi.WinFormsSample
{
    partial class MainForm
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
            this.gbEmail = new System.Windows.Forms.GroupBox();
            this.btnLabels = new System.Windows.Forms.Button();
            this.btnInboxMail = new System.Windows.Forms.Button();
            this.picBoxStatus = new System.Windows.Forms.PictureBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtData = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnReset = new System.Windows.Forms.Button();
            this.gbEmail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxStatus)).BeginInit();
            this.gbLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbEmail
            // 
            this.gbEmail.Controls.Add(this.btnReset);
            this.gbEmail.Controls.Add(this.btnLabels);
            this.gbEmail.Controls.Add(this.btnInboxMail);
            this.gbEmail.Controls.Add(this.picBoxStatus);
            this.gbEmail.Controls.Add(this.btnLogin);
            this.gbEmail.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbEmail.Location = new System.Drawing.Point(0, 0);
            this.gbEmail.Name = "gbEmail";
            this.gbEmail.Size = new System.Drawing.Size(438, 196);
            this.gbEmail.TabIndex = 0;
            this.gbEmail.TabStop = false;
            this.gbEmail.Text = "Email";
            // 
            // btnLabels
            // 
            this.btnLabels.Location = new System.Drawing.Point(6, 75);
            this.btnLabels.Name = "btnLabels";
            this.btnLabels.Size = new System.Drawing.Size(75, 50);
            this.btnLabels.TabIndex = 4;
            this.btnLabels.Text = "Labels";
            this.btnLabels.UseVisualStyleBackColor = true;
            this.btnLabels.Click += new System.EventHandler(this.OnBtnLabelsClick);
            // 
            // btnInboxMail
            // 
            this.btnInboxMail.Location = new System.Drawing.Point(6, 19);
            this.btnInboxMail.Name = "btnInboxMail";
            this.btnInboxMail.Size = new System.Drawing.Size(75, 50);
            this.btnInboxMail.TabIndex = 3;
            this.btnInboxMail.Text = "Mail";
            this.btnInboxMail.UseVisualStyleBackColor = true;
            this.btnInboxMail.Click += new System.EventHandler(this.OnBtnInboxMailClick);
            // 
            // picBoxStatus
            // 
            this.picBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picBoxStatus.Location = new System.Drawing.Point(357, 19);
            this.picBoxStatus.Name = "picBoxStatus";
            this.picBoxStatus.Size = new System.Drawing.Size(75, 61);
            this.picBoxStatus.TabIndex = 2;
            this.picBoxStatus.TabStop = false;
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogin.Location = new System.Drawing.Point(357, 140);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 50);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.OnBtnLoginClicked);
            // 
            // txtData
            // 
            this.txtData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtData.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtData.Location = new System.Drawing.Point(0, 0);
            this.txtData.Multiline = true;
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(566, 729);
            this.txtData.TabIndex = 1;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(3, 16);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(432, 514);
            this.txtLog.TabIndex = 2;
            // 
            // gbLog
            // 
            this.gbLog.Controls.Add(this.txtLog);
            this.gbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbLog.Location = new System.Drawing.Point(0, 196);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new System.Drawing.Size(438, 533);
            this.gbLog.TabIndex = 3;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "Log";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtData);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbLog);
            this.splitContainer1.Panel2.Controls.Add(this.gbEmail);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 729);
            this.splitContainer1.SplitterDistance = 566;
            this.splitContainer1.TabIndex = 4;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(276, 140);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 50);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.OnBtnResetClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "KP.GmailAPI Demo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.gbEmail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxStatus)).EndInit();
            this.gbLog.ResumeLayout(false);
            this.gbLog.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbEmail;
        private System.Windows.Forms.PictureBox picBoxStatus;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnLabels;
        private System.Windows.Forms.Button btnInboxMail;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnReset;
    }
}

