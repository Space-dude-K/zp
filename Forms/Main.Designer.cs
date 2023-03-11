
namespace Zp
{
    partial class Main
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
            this.menuStrip_Main = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_AppSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_AppExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView_Main = new System.Windows.Forms.DataGridView();
            this.Column_PersonalNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_FIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_TotalSumm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Image = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column_PersAcc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_Start = new System.Windows.Forms.Button();
            this.label_SmtpStatus = new System.Windows.Forms.Label();
            this.progressBar_Main = new System.Windows.Forms.ProgressBar();
            this.button_FilePicker = new System.Windows.Forms.Button();
            this.label_LoadFileName = new System.Windows.Forms.Label();
            this.button_Send = new System.Windows.Forms.Button();
            this.label_EmailListStatus = new System.Windows.Forms.Label();
            this.pictureBox_EmailListStatus = new System.Windows.Forms.PictureBox();
            this.pictureBox_SMTP = new System.Windows.Forms.PictureBox();
            this.button_ReloadEmails = new System.Windows.Forms.Button();
            this.label_TotalEmails = new System.Windows.Forms.Label();
            this.label_TotalEmailsValue = new System.Windows.Forms.Label();
            this.label_ParsedDocs = new System.Windows.Forms.Label();
            this.label_ParsedDocsValue = new System.Windows.Forms.Label();
            this.label_TotalSend = new System.Windows.Forms.Label();
            this.label_TotalSendValue = new System.Windows.Forms.Label();
            this.button_OpenSaveFolder = new System.Windows.Forms.Button();
            this.menuStrip_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_EmailListStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SMTP)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip_Main
            // 
            this.menuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_AppSettings,
            this.toolStripMenuItem_AppExit,
            this.toolStripMenuItem_Help,
            this.toolStripMenuItem_About});
            this.menuStrip_Main.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_Main.Name = "menuStrip_Main";
            this.menuStrip_Main.Size = new System.Drawing.Size(904, 24);
            this.menuStrip_Main.TabIndex = 1;
            // 
            // toolStripMenuItem_AppSettings
            // 
            this.toolStripMenuItem_AppSettings.Name = "toolStripMenuItem_AppSettings";
            this.toolStripMenuItem_AppSettings.Size = new System.Drawing.Size(79, 20);
            this.toolStripMenuItem_AppSettings.Text = "Настройки";
            this.toolStripMenuItem_AppSettings.Click += new System.EventHandler(this.ToolStripMenuItem_AppSettings_Click);
            // 
            // toolStripMenuItem_AppExit
            // 
            this.toolStripMenuItem_AppExit.Name = "toolStripMenuItem_AppExit";
            this.toolStripMenuItem_AppExit.Size = new System.Drawing.Size(54, 20);
            this.toolStripMenuItem_AppExit.Text = "Выход";
            this.toolStripMenuItem_AppExit.Click += new System.EventHandler(this.ToolStripMenuItem_AppExit_Click);
            // 
            // toolStripMenuItem_Help
            // 
            this.toolStripMenuItem_Help.Name = "toolStripMenuItem_Help";
            this.toolStripMenuItem_Help.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItem_Help.Text = "Help";
            this.toolStripMenuItem_Help.Click += new System.EventHandler(this.ToolStripMenuItem_Help_Click);
            // 
            // toolStripMenuItem_About
            // 
            this.toolStripMenuItem_About.Name = "toolStripMenuItem_About";
            this.toolStripMenuItem_About.Size = new System.Drawing.Size(52, 20);
            this.toolStripMenuItem_About.Text = "About";
            this.toolStripMenuItem_About.Click += new System.EventHandler(this.ToolStripMenuItem_About_Click);
            // 
            // dataGridView_Main
            // 
            this.dataGridView_Main.AllowUserToAddRows = false;
            this.dataGridView_Main.AllowUserToDeleteRows = false;
            this.dataGridView_Main.AllowUserToResizeRows = false;
            this.dataGridView_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_Main.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Main.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_PersonalNumber,
            this.Column_Email,
            this.Column_FIO,
            this.Column_TotalSumm,
            this.Column_Image,
            this.Column_PersAcc,
            this.Column_Date});
            this.dataGridView_Main.Location = new System.Drawing.Point(15, 73);
            this.dataGridView_Main.Name = "dataGridView_Main";
            this.dataGridView_Main.Size = new System.Drawing.Size(877, 300);
            this.dataGridView_Main.TabIndex = 2;
            // 
            // Column_PersonalNumber
            // 
            this.Column_PersonalNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column_PersonalNumber.HeaderText = "Личный номер";
            this.Column_PersonalNumber.MinimumWidth = 110;
            this.Column_PersonalNumber.Name = "Column_PersonalNumber";
            this.Column_PersonalNumber.Width = 110;
            // 
            // Column_Email
            // 
            this.Column_Email.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column_Email.HeaderText = "Email";
            this.Column_Email.Name = "Column_Email";
            this.Column_Email.Width = 57;
            // 
            // Column_FIO
            // 
            this.Column_FIO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column_FIO.HeaderText = "ФИО";
            this.Column_FIO.Name = "Column_FIO";
            this.Column_FIO.Width = 59;
            // 
            // Column_TotalSumm
            // 
            this.Column_TotalSumm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column_TotalSumm.HeaderText = "К выплате";
            this.Column_TotalSumm.MinimumWidth = 85;
            this.Column_TotalSumm.Name = "Column_TotalSumm";
            this.Column_TotalSumm.Width = 85;
            // 
            // Column_Image
            // 
            this.Column_Image.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column_Image.HeaderText = "Image";
            this.Column_Image.Name = "Column_Image";
            this.Column_Image.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column_Image.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column_Image.Width = 61;
            // 
            // Column_PersAcc
            // 
            this.Column_PersAcc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_PersAcc.HeaderText = "Лицевой счет";
            this.Column_PersAcc.Name = "Column_PersAcc";
            // 
            // Column_Date
            // 
            this.Column_Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column_Date.HeaderText = "Дата";
            this.Column_Date.Name = "Column_Date";
            this.Column_Date.Width = 58;
            // 
            // button_Start
            // 
            this.button_Start.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_Start.Enabled = false;
            this.button_Start.Location = new System.Drawing.Point(371, 379);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(167, 23);
            this.button_Start.TabIndex = 4;
            this.button_Start.Text = "Загрузить расчётник";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.Button_Start_Click);
            // 
            // label_SmtpStatus
            // 
            this.label_SmtpStatus.AutoSize = true;
            this.label_SmtpStatus.Location = new System.Drawing.Point(12, 38);
            this.label_SmtpStatus.Name = "label_SmtpStatus";
            this.label_SmtpStatus.Size = new System.Drawing.Size(77, 13);
            this.label_SmtpStatus.TabIndex = 5;
            this.label_SmtpStatus.Text = "Статус SMTP:";
            // 
            // progressBar_Main
            // 
            this.progressBar_Main.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_Main.Location = new System.Drawing.Point(337, 31);
            this.progressBar_Main.Name = "progressBar_Main";
            this.progressBar_Main.Size = new System.Drawing.Size(555, 39);
            this.progressBar_Main.TabIndex = 7;
            // 
            // button_FilePicker
            // 
            this.button_FilePicker.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_FilePicker.Enabled = false;
            this.button_FilePicker.Location = new System.Drawing.Point(197, 379);
            this.button_FilePicker.Name = "button_FilePicker";
            this.button_FilePicker.Size = new System.Drawing.Size(167, 23);
            this.button_FilePicker.TabIndex = 9;
            this.button_FilePicker.Text = "Выбрать файл";
            this.button_FilePicker.UseVisualStyleBackColor = true;
            this.button_FilePicker.Click += new System.EventHandler(this.Button_FilePicker_Click);
            // 
            // label_LoadFileName
            // 
            this.label_LoadFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_LoadFileName.BackColor = System.Drawing.Color.Transparent;
            this.label_LoadFileName.Location = new System.Drawing.Point(337, 10);
            this.label_LoadFileName.Name = "label_LoadFileName";
            this.label_LoadFileName.Size = new System.Drawing.Size(555, 13);
            this.label_LoadFileName.TabIndex = 10;
            this.label_LoadFileName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_Send
            // 
            this.button_Send.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_Send.Enabled = false;
            this.button_Send.Location = new System.Drawing.Point(545, 379);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(167, 23);
            this.button_Send.TabIndex = 11;
            this.button_Send.Text = "Отправить";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.Button_Send_Click);
            // 
            // label_EmailListStatus
            // 
            this.label_EmailListStatus.AutoSize = true;
            this.label_EmailListStatus.Location = new System.Drawing.Point(127, 38);
            this.label_EmailListStatus.Name = "label_EmailListStatus";
            this.label_EmailListStatus.Size = new System.Drawing.Size(61, 13);
            this.label_EmailListStatus.TabIndex = 12;
            this.label_EmailListStatus.Text = "Список ID:";
            // 
            // pictureBox_EmailListStatus
            // 
            this.pictureBox_EmailListStatus.Location = new System.Drawing.Point(196, 36);
            this.pictureBox_EmailListStatus.Name = "pictureBox_EmailListStatus";
            this.pictureBox_EmailListStatus.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_EmailListStatus.TabIndex = 13;
            this.pictureBox_EmailListStatus.TabStop = false;
            // 
            // pictureBox_SMTP
            // 
            this.pictureBox_SMTP.Location = new System.Drawing.Point(97, 36);
            this.pictureBox_SMTP.Name = "pictureBox_SMTP";
            this.pictureBox_SMTP.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_SMTP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_SMTP.TabIndex = 8;
            this.pictureBox_SMTP.TabStop = false;
            // 
            // button_ReloadEmails
            // 
            this.button_ReloadEmails.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_ReloadEmails.Location = new System.Drawing.Point(23, 379);
            this.button_ReloadEmails.Name = "button_ReloadEmails";
            this.button_ReloadEmails.Size = new System.Drawing.Size(167, 23);
            this.button_ReloadEmails.TabIndex = 14;
            this.button_ReloadEmails.Text = "Загрузить список ID";
            this.button_ReloadEmails.UseVisualStyleBackColor = true;
            this.button_ReloadEmails.Click += new System.EventHandler(this.Button_ReloadEmails_Click);
            // 
            // label_TotalEmails
            // 
            this.label_TotalEmails.AutoSize = true;
            this.label_TotalEmails.Location = new System.Drawing.Point(221, 31);
            this.label_TotalEmails.Name = "label_TotalEmails";
            this.label_TotalEmails.Size = new System.Drawing.Size(40, 13);
            this.label_TotalEmails.TabIndex = 15;
            this.label_TotalEmails.Text = "Emails:";
            // 
            // label_TotalEmailsValue
            // 
            this.label_TotalEmailsValue.AutoSize = true;
            this.label_TotalEmailsValue.Location = new System.Drawing.Point(302, 31);
            this.label_TotalEmailsValue.Name = "label_TotalEmailsValue";
            this.label_TotalEmailsValue.Size = new System.Drawing.Size(13, 13);
            this.label_TotalEmailsValue.TabIndex = 16;
            this.label_TotalEmailsValue.Text = "0";
            // 
            // label_ParsedDocs
            // 
            this.label_ParsedDocs.AutoSize = true;
            this.label_ParsedDocs.Location = new System.Drawing.Point(221, 44);
            this.label_ParsedDocs.Name = "label_ParsedDocs";
            this.label_ParsedDocs.Size = new System.Drawing.Size(75, 13);
            this.label_ParsedDocs.TabIndex = 17;
            this.label_ParsedDocs.Text = "Расчётников:";
            // 
            // label_ParsedDocsValue
            // 
            this.label_ParsedDocsValue.AutoSize = true;
            this.label_ParsedDocsValue.Location = new System.Drawing.Point(302, 44);
            this.label_ParsedDocsValue.Name = "label_ParsedDocsValue";
            this.label_ParsedDocsValue.Size = new System.Drawing.Size(13, 13);
            this.label_ParsedDocsValue.TabIndex = 18;
            this.label_ParsedDocsValue.Text = "0";
            // 
            // label_TotalSend
            // 
            this.label_TotalSend.AutoSize = true;
            this.label_TotalSend.Location = new System.Drawing.Point(221, 57);
            this.label_TotalSend.Name = "label_TotalSend";
            this.label_TotalSend.Size = new System.Drawing.Size(71, 13);
            this.label_TotalSend.TabIndex = 19;
            this.label_TotalSend.Text = "Отправлено:";
            // 
            // label_TotalSendValue
            // 
            this.label_TotalSendValue.AutoSize = true;
            this.label_TotalSendValue.Location = new System.Drawing.Point(302, 57);
            this.label_TotalSendValue.Name = "label_TotalSendValue";
            this.label_TotalSendValue.Size = new System.Drawing.Size(13, 13);
            this.label_TotalSendValue.TabIndex = 20;
            this.label_TotalSendValue.Text = "0";
            // 
            // button_OpenSaveFolder
            // 
            this.button_OpenSaveFolder.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_OpenSaveFolder.Location = new System.Drawing.Point(714, 379);
            this.button_OpenSaveFolder.Name = "button_OpenSaveFolder";
            this.button_OpenSaveFolder.Size = new System.Drawing.Size(167, 23);
            this.button_OpenSaveFolder.TabIndex = 21;
            this.button_OpenSaveFolder.Text = "Открыть архив";
            this.button_OpenSaveFolder.UseVisualStyleBackColor = true;
            this.button_OpenSaveFolder.Click += new System.EventHandler(this.Button_OpenSaveFolder_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 407);
            this.Controls.Add(this.button_OpenSaveFolder);
            this.Controls.Add(this.label_TotalSendValue);
            this.Controls.Add(this.label_TotalSend);
            this.Controls.Add(this.label_ParsedDocsValue);
            this.Controls.Add(this.label_ParsedDocs);
            this.Controls.Add(this.label_TotalEmailsValue);
            this.Controls.Add(this.label_TotalEmails);
            this.Controls.Add(this.button_ReloadEmails);
            this.Controls.Add(this.pictureBox_EmailListStatus);
            this.Controls.Add(this.label_EmailListStatus);
            this.Controls.Add(this.button_Send);
            this.Controls.Add(this.label_LoadFileName);
            this.Controls.Add(this.button_FilePicker);
            this.Controls.Add(this.pictureBox_SMTP);
            this.Controls.Add(this.progressBar_Main);
            this.Controls.Add(this.label_SmtpStatus);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.dataGridView_Main);
            this.Controls.Add(this.menuStrip_Main);
            this.MainMenuStrip = this.menuStrip_Main;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Zp";
            this.menuStrip_Main.ResumeLayout(false);
            this.menuStrip_Main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Main)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_EmailListStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SMTP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip_Main;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_AppSettings;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_AppExit;
        private System.Windows.Forms.DataGridView dataGridView_Main;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Label label_SmtpStatus;
        private System.Windows.Forms.ProgressBar progressBar_Main;
        private System.Windows.Forms.PictureBox pictureBox_SMTP;
        private System.Windows.Forms.Button button_FilePicker;
        private System.Windows.Forms.Label label_LoadFileName;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.Label label_EmailListStatus;
        private System.Windows.Forms.PictureBox pictureBox_EmailListStatus;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_About;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_PersonalNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_FIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_TotalSumm;
        private System.Windows.Forms.DataGridViewButtonColumn Column_Image;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_PersAcc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Date;
        private System.Windows.Forms.Button button_ReloadEmails;
        private System.Windows.Forms.Label label_TotalEmails;
        private System.Windows.Forms.Label label_TotalEmailsValue;
        private System.Windows.Forms.Label label_ParsedDocs;
        private System.Windows.Forms.Label label_ParsedDocsValue;
        private System.Windows.Forms.Label label_TotalSend;
        private System.Windows.Forms.Label label_TotalSendValue;
        private System.Windows.Forms.Button button_OpenSaveFolder;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Help;
    }
}

