
namespace Zp.Forms
{
    partial class Settings
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
            System.Security.SecureString secureString1 = new System.Security.SecureString();
            this.label_Email = new System.Windows.Forms.Label();
            this.label_EmailPass = new System.Windows.Forms.Label();
            this.label_SmtpAdd = new System.Windows.Forms.Label();
            this.textBox_SmtpAdd = new System.Windows.Forms.TextBox();
            this.textBox_SmtpPort = new System.Windows.Forms.TextBox();
            this.label_SmtpPort = new System.Windows.Forms.Label();
            this.textBox_Email = new System.Windows.Forms.TextBox();
            this.label_EmailSubject = new System.Windows.Forms.Label();
            this.textBox_EmailSubject = new System.Windows.Forms.TextBox();
            this.groupBox_MailSettings = new System.Windows.Forms.GroupBox();
            this.groupBox_MainSettings = new System.Windows.Forms.GroupBox();
            this.label_EmailList = new System.Windows.Forms.Label();
            this.textBox_EmailList = new System.Windows.Forms.TextBox();
            this.textBox_path_SaveDir = new System.Windows.Forms.TextBox();
            this.label_SaveDir = new System.Windows.Forms.Label();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.textBox_EmailMsg = new System.Windows.Forms.TextBox();
            this.label_EmailMsg = new System.Windows.Forms.Label();
            this.securityTextBox_EmailPassword = new Zp.Forms.SecurityTextBox();
            this.groupBox_MailSettings.SuspendLayout();
            this.groupBox_MainSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_Email
            // 
            this.label_Email.AutoSize = true;
            this.label_Email.Location = new System.Drawing.Point(6, 85);
            this.label_Email.Name = "label_Email";
            this.label_Email.Size = new System.Drawing.Size(32, 13);
            this.label_Email.TabIndex = 2;
            this.label_Email.Text = "Email";
            // 
            // label_EmailPass
            // 
            this.label_EmailPass.AutoSize = true;
            this.label_EmailPass.Location = new System.Drawing.Point(180, 85);
            this.label_EmailPass.Name = "label_EmailPass";
            this.label_EmailPass.Size = new System.Drawing.Size(80, 13);
            this.label_EmailPass.TabIndex = 3;
            this.label_EmailPass.Text = "Email password";
            // 
            // label_SmtpAdd
            // 
            this.label_SmtpAdd.AutoSize = true;
            this.label_SmtpAdd.Location = new System.Drawing.Point(6, 36);
            this.label_SmtpAdd.Name = "label_SmtpAdd";
            this.label_SmtpAdd.Size = new System.Drawing.Size(71, 13);
            this.label_SmtpAdd.TabIndex = 4;
            this.label_SmtpAdd.Text = "Smtp address";
            // 
            // textBox_SmtpAdd
            // 
            this.textBox_SmtpAdd.Location = new System.Drawing.Point(6, 57);
            this.textBox_SmtpAdd.Name = "textBox_SmtpAdd";
            this.textBox_SmtpAdd.Size = new System.Drawing.Size(150, 20);
            this.textBox_SmtpAdd.TabIndex = 5;
            // 
            // textBox_SmtpPort
            // 
            this.textBox_SmtpPort.Location = new System.Drawing.Point(182, 57);
            this.textBox_SmtpPort.Name = "textBox_SmtpPort";
            this.textBox_SmtpPort.Size = new System.Drawing.Size(150, 20);
            this.textBox_SmtpPort.TabIndex = 6;
            // 
            // label_SmtpPort
            // 
            this.label_SmtpPort.AutoSize = true;
            this.label_SmtpPort.Location = new System.Drawing.Point(179, 36);
            this.label_SmtpPort.Name = "label_SmtpPort";
            this.label_SmtpPort.Size = new System.Drawing.Size(81, 13);
            this.label_SmtpPort.TabIndex = 7;
            this.label_SmtpPort.Text = "Smtp port (SSL)";
            // 
            // textBox_Email
            // 
            this.textBox_Email.Location = new System.Drawing.Point(6, 106);
            this.textBox_Email.Name = "textBox_Email";
            this.textBox_Email.Size = new System.Drawing.Size(150, 20);
            this.textBox_Email.TabIndex = 8;
            // 
            // label_EmailSubject
            // 
            this.label_EmailSubject.AutoSize = true;
            this.label_EmailSubject.Location = new System.Drawing.Point(6, 134);
            this.label_EmailSubject.Name = "label_EmailSubject";
            this.label_EmailSubject.Size = new System.Drawing.Size(69, 13);
            this.label_EmailSubject.TabIndex = 9;
            this.label_EmailSubject.Text = "Email subject";
            // 
            // textBox_EmailSubject
            // 
            this.textBox_EmailSubject.Location = new System.Drawing.Point(6, 155);
            this.textBox_EmailSubject.Name = "textBox_EmailSubject";
            this.textBox_EmailSubject.Size = new System.Drawing.Size(150, 20);
            this.textBox_EmailSubject.TabIndex = 10;
            // 
            // groupBox_MailSettings
            // 
            this.groupBox_MailSettings.Controls.Add(this.label_EmailMsg);
            this.groupBox_MailSettings.Controls.Add(this.textBox_EmailMsg);
            this.groupBox_MailSettings.Controls.Add(this.securityTextBox_EmailPassword);
            this.groupBox_MailSettings.Controls.Add(this.textBox_SmtpAdd);
            this.groupBox_MailSettings.Controls.Add(this.label_EmailPass);
            this.groupBox_MailSettings.Controls.Add(this.textBox_SmtpPort);
            this.groupBox_MailSettings.Controls.Add(this.label_SmtpPort);
            this.groupBox_MailSettings.Controls.Add(this.textBox_EmailSubject);
            this.groupBox_MailSettings.Controls.Add(this.label_Email);
            this.groupBox_MailSettings.Controls.Add(this.label_EmailSubject);
            this.groupBox_MailSettings.Controls.Add(this.label_SmtpAdd);
            this.groupBox_MailSettings.Controls.Add(this.textBox_Email);
            this.groupBox_MailSettings.Location = new System.Drawing.Point(16, 172);
            this.groupBox_MailSettings.Name = "groupBox_MailSettings";
            this.groupBox_MailSettings.Size = new System.Drawing.Size(343, 195);
            this.groupBox_MailSettings.TabIndex = 11;
            this.groupBox_MailSettings.TabStop = false;
            this.groupBox_MailSettings.Text = "Email settings";
            // 
            // groupBox_MainSettings
            // 
            this.groupBox_MainSettings.Controls.Add(this.label_EmailList);
            this.groupBox_MainSettings.Controls.Add(this.textBox_EmailList);
            this.groupBox_MainSettings.Controls.Add(this.textBox_path_SaveDir);
            this.groupBox_MainSettings.Controls.Add(this.label_SaveDir);
            this.groupBox_MainSettings.Location = new System.Drawing.Point(16, 12);
            this.groupBox_MainSettings.Name = "groupBox_MainSettings";
            this.groupBox_MainSettings.Size = new System.Drawing.Size(343, 154);
            this.groupBox_MainSettings.TabIndex = 12;
            this.groupBox_MainSettings.TabStop = false;
            this.groupBox_MainSettings.Text = "Main settings";
            // 
            // label_EmailList
            // 
            this.label_EmailList.AutoSize = true;
            this.label_EmailList.Location = new System.Drawing.Point(12, 75);
            this.label_EmailList.Name = "label_EmailList";
            this.label_EmailList.Size = new System.Drawing.Size(61, 13);
            this.label_EmailList.TabIndex = 3;
            this.label_EmailList.Text = "Email-ID list";
            // 
            // textBox_EmailList
            // 
            this.textBox_EmailList.Location = new System.Drawing.Point(6, 96);
            this.textBox_EmailList.Name = "textBox_EmailList";
            this.textBox_EmailList.Size = new System.Drawing.Size(326, 20);
            this.textBox_EmailList.TabIndex = 2;
            // 
            // textBox_path_SaveDir
            // 
            this.textBox_path_SaveDir.Location = new System.Drawing.Point(6, 44);
            this.textBox_path_SaveDir.Name = "textBox_path_SaveDir";
            this.textBox_path_SaveDir.Size = new System.Drawing.Size(326, 20);
            this.textBox_path_SaveDir.TabIndex = 1;
            // 
            // label_SaveDir
            // 
            this.label_SaveDir.AutoSize = true;
            this.label_SaveDir.Location = new System.Drawing.Point(9, 23);
            this.label_SaveDir.Name = "label_SaveDir";
            this.label_SaveDir.Size = new System.Drawing.Size(75, 13);
            this.label_SaveDir.TabIndex = 0;
            this.label_SaveDir.Text = "Save directory";
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(120, 399);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(75, 23);
            this.button_Save.TabIndex = 13;
            this.button_Save.Text = "Save";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.Button_Save_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(202, 399);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 14;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // textBox_EmailMsg
            // 
            this.textBox_EmailMsg.Location = new System.Drawing.Point(182, 155);
            this.textBox_EmailMsg.Name = "textBox_EmailMsg";
            this.textBox_EmailMsg.Size = new System.Drawing.Size(150, 20);
            this.textBox_EmailMsg.TabIndex = 12;
            // 
            // label_EmailMsg
            // 
            this.label_EmailMsg.AutoSize = true;
            this.label_EmailMsg.Location = new System.Drawing.Point(180, 134);
            this.label_EmailMsg.Name = "label_EmailMsg";
            this.label_EmailMsg.Size = new System.Drawing.Size(77, 13);
            this.label_EmailMsg.TabIndex = 13;
            this.label_EmailMsg.Text = "Email message";
            // 
            // securityTextBox_EmailPassword
            // 
            this.securityTextBox_EmailPassword.IsPasswordChanged = false;
            this.securityTextBox_EmailPassword.Location = new System.Drawing.Point(182, 106);
            this.securityTextBox_EmailPassword.Name = "securityTextBox_EmailPassword";
            this.securityTextBox_EmailPassword.PasswordChar = '●';
            this.securityTextBox_EmailPassword.SecureString = secureString1;
            this.securityTextBox_EmailPassword.Size = new System.Drawing.Size(150, 20);
            this.securityTextBox_EmailPassword.TabIndex = 11;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 438);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.groupBox_MailSettings);
            this.Controls.Add(this.groupBox_MainSettings);
            this.Name = "Settings";
            this.Text = "Settings";
            this.groupBox_MailSettings.ResumeLayout(false);
            this.groupBox_MailSettings.PerformLayout();
            this.groupBox_MainSettings.ResumeLayout(false);
            this.groupBox_MainSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label_Email;
        private System.Windows.Forms.Label label_EmailPass;
        private System.Windows.Forms.Label label_SmtpAdd;
        private System.Windows.Forms.TextBox textBox_SmtpAdd;
        private System.Windows.Forms.TextBox textBox_SmtpPort;
        private System.Windows.Forms.Label label_SmtpPort;
        private System.Windows.Forms.TextBox textBox_Email;
        private System.Windows.Forms.Label label_EmailSubject;
        private System.Windows.Forms.TextBox textBox_EmailSubject;
        private System.Windows.Forms.GroupBox groupBox_MailSettings;
        private System.Windows.Forms.GroupBox groupBox_MainSettings;
        private System.Windows.Forms.TextBox textBox_path_SaveDir;
        private System.Windows.Forms.Label label_SaveDir;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_Cancel;
        private SecurityTextBox securityTextBox_EmailPassword;
        private System.Windows.Forms.Label label_EmailList;
        private System.Windows.Forms.TextBox textBox_EmailList;
        private System.Windows.Forms.Label label_EmailMsg;
        private System.Windows.Forms.TextBox textBox_EmailMsg;
    }
}