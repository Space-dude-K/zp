using System;
using System.Windows.Forms;
using Zp.Crypto;
using Microsoft.Extensions.Configuration;
using NLog;
using System.Diagnostics;
using Zp.Properties;

namespace Zp.Forms
{
    public partial class Settings : Form
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IConfiguration cfg;
        private readonly IMain main;

        public Settings(IConfiguration cfg, IMain main)
        {
            this.cfg = cfg;
            this.main = main;
            InitializeComponent();
            InitSettingsForm();
        }
        private void InitSettingsForm()
        {
            this.Icon = Resources.money_bag;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadCfgValues();
            InitSecurityTextBox();
            this.FormClosing += Settings_FormClosing;
        }
        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            main.CheckSMTP();
        }
        private void InitSecurityTextBox()
        {
            this.securityTextBox_EmailPassword.Pb.MouseUp += Pb_MouseUp;
            this.securityTextBox_EmailPassword.Pb.MouseDown += Pb_MouseDown;
            this.securityTextBox_EmailPassword.SetPasswordDummy(0, true); 
        }

        private void Pb_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.securityTextBox_EmailPassword.SecureString.Length != 0)
            {
                this.securityTextBox_EmailPassword.SetPasswordDummy(this.securityTextBox_EmailPassword.SecureString.Length);
            }
            else
            {
                Debug.WriteLine("Up");
            }
        }
        private void Pb_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.securityTextBox_EmailPassword.SecureString.Length != 0)
            {
                Debug.WriteLine("Down - " + this.securityTextBox_EmailPassword.SecureString.Length);
                this.securityTextBox_EmailPassword.Tb.Text = Encryptor.ToInsecureString(this.securityTextBox_EmailPassword.SecureString);
            }
            else
            {
                Debug.WriteLine("Down");
            }
        }
        private void LoadCfgValues()
        {
            this.textBox_path_SaveDir.Text = cfg.GetSection("mainSettings")["saveDir"];
            this.textBox_EmailList.Text = cfg.GetSection("mainSettings")["emailList"];

            this.textBox_SmtpAdd.Text = cfg.GetSection("emailSettings")["smtpAddress"];
            this.textBox_SmtpPort.Text = cfg.GetSection("emailSettings")["smtpPort"];
            this.textBox_Email.Text = cfg.GetSection("emailSettings")["email"];

            if(!string.IsNullOrEmpty(cfg.GetSection("emailSettings")["emailPassword"]))
            {
                foreach (char ch in Encryptor.ToInsecureString(Encryptor.DecryptString(
                cfg.GetSection("emailSettings")["emailPassword"], cfg.GetSection("emailSettings")["emailPasswordSalt"])))
                {
                    this.securityTextBox_EmailPassword.SecureString.AppendChar(ch);
                }
            }

            this.textBox_EmailSubject.Text = cfg.GetSection("emailSettings")["emailSubject"];
            this.textBox_EmailMsg.Text = cfg.GetSection("emailSettings")["emailText"];
        }
        private void Button_Save_Click(object sender, EventArgs e)
        {
            if(Validator.ValidateTextBoxInputs(this.Controls))
            {
                logger.Info("[SETTINGS] Settings is valid!");

                // Save settings.
                var salt = Encryptor.GetSalt(64);

                SettingsHelpers.SetAppSettingValue("mainSettings", "saveDir", this.textBox_path_SaveDir.Text);
                SettingsHelpers.SetAppSettingValue("mainSettings", "emailList", this.textBox_EmailList.Text);

                SettingsHelpers.SetAppSettingValue("emailSettings", "smtpAddress", this.textBox_SmtpAdd.Text);
                SettingsHelpers.SetAppSettingValue("emailSettings", "smtpPort", this.textBox_SmtpPort.Text);
                SettingsHelpers.SetAppSettingValue("emailSettings", "email", this.textBox_Email.Text);
                SettingsHelpers.SetAppSettingValue("emailSettings", "emailPassword", 
                    Encryptor.EncryptString(this.securityTextBox_EmailPassword.SecureString, salt));
                SettingsHelpers.SetAppSettingValue("emailSettings", "emailPasswordSalt", Convert.ToBase64String(salt));
                SettingsHelpers.SetAppSettingValue("emailSettings", "emailSubject", this.textBox_EmailSubject.Text);
                SettingsHelpers.SetAppSettingValue("emailSettings", "emailText", this.textBox_EmailMsg.Text);
            }
            else
            {
                logger.Info("[SETTINGS] Settings is not valid!");
            }
        }
        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}