using Microsoft.Extensions.Configuration;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Zp.Properties;
using static Zp.ParsedDoc;

namespace Zp
{
    public partial class Main : Form, IMain
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IConfigurationRoot cfg;
        private string filePath;
        private List<Tuple<string, string>> emails;
        private List<ParsedDoc> parsedDocs;
        private int timerCounter;
        public Main()
        {
            InitializeComponent();
            cfg = GetConfig();
            InitMainForm();
            InitEmailList();
            InitDataGrid();
        }
        private void InitMainForm()
        {
            logger.Info("[MAIN] Init main form.");
            this.Icon = Resources.money_bag;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.StartPosition = FormStartPosition.CenterScreen;
            CheckSMTP();
            this.pictureBox_EmailListStatus.Image = Resources.red_c;

            this.MaximizeBox = true;
            this.MinimizeBox = true;
        }
        private void InitDataGrid()
        {
            this.dataGridView_Main.CellContentClick += DataGridView_Main_CellContentClick;
        }
        public void InitEmailList()
        {
            Task.Run(() => RunParserForEmails());
        }
        private IConfigurationRoot GetConfig()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

            return config; 
        }
        private void ClearRows(bool clearParsedDocs = true)
        {
            try
            {
                if (this.dataGridView_Main.DataSource != null)
                {
                    this.dataGridView_Main.ThreadSafeControlInvoke(new Action(() => this.dataGridView_Main.DataSource = null));
                }
                else
                {
                    this.dataGridView_Main.ThreadSafeControlInvoke(new Action(() => this.dataGridView_Main.Rows.Clear()));
                    this.dataGridView_Main.ThreadSafeControlInvoke(new Action(() => this.dataGridView_Main.Refresh()));
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }

            if(clearParsedDocs)
                parsedDocs = null;
        }
        private void RunParserForEmails()
        {
            bool isEmailsValid = false;
            string emailsPath = cfg.GetSection("mainSettings")["emailList"];

            if (string.IsNullOrEmpty(emailsPath))
            {
                logger.Info("[MAIN] EmailList setting was empty!");
            }
            else if(!File.Exists(emailsPath))
            {
                logger.Info("[MAIN] File " + emailsPath + " did not exist.");
            }
            else
            {
                var progress = new Progress<int>(percent =>
                {
                    this.progressBar_Main.ThreadSafeControlInvoke(new Action(() => this.progressBar_Main.Value = percent));
                });

                this.label_LoadFileName.ThreadSafeControlInvoke(new Action(() => this.label_LoadFileName.Text = "Загружаем email's ..."));

                ExcelParser ep = new ExcelParser();
                emails = ep.ParseEmails(emailsPath, progress);

                if (emails.Count == 0)
                {
                    logger.Info("[MAIN] Email collection was empty!");
                    this.label_LoadFileName.ThreadSafeControlInvoke(new Action(() => this.label_LoadFileName.Text = "Список с email пуст!"));
                }
                else
                {
                    UpdateStatusLabels();

                    logger.Info("[MAIN] Loaded email elements: " + emails.Count);

                    this.label_LoadFileName.ThreadSafeControlInvoke(
                        new Action(() => this.label_LoadFileName.Text = "Загружено элементов с ID: " + emails.Count));
                    isEmailsValid = true;
                }
            }

            if(isEmailsValid)
            {
                this.pictureBox_EmailListStatus.ThreadSafeControlInvoke(new Action(() => this.pictureBox_EmailListStatus.Image = Resources.green_c));
            }
            else
            {
                this.pictureBox_EmailListStatus.ThreadSafeControlInvoke(new Action(() => this.pictureBox_EmailListStatus.Image = Resources.red_c));
            }

            this.button_FilePicker.ThreadSafeControlInvoke(new Action(() => this.button_FilePicker.Enabled = true));
        }
        private void RunParser()
        {
            try
            {
                if (CheckSettings())
                {
                    logger.Info("[MAIN] CheckSettings -> OK.");

                    ExcelParser ep = new ExcelParser();

                    if (this.dataGridView_Main.Rows.Count > 1)
                    {
                        logger.Info("[MAIN] Clear rows.");
                        ClearRows();
                    }

                    var progress = new Progress<int>(percent =>
                    {
                        this.progressBar_Main.ThreadSafeControlInvoke(new Action(() => this.progressBar_Main.Value = percent));
                    });

                    /*
                    var eF = !string.IsNullOrEmpty(cfg.GetSection("mainSettings")["exportFormat"]) ?
                        (ExportFormat)Enum.Parse(typeof(ExportFormat), cfg.GetSection("mainSettings")["exportFormat"], true) : ExportFormat.Png;
                    */
                    var eF = ExportFormat.Png;

                    logger.Info("[MAIN] Selected export format -> " + eF.ToString());

                    // Block buttons until file is processed.
                    UpdateButtons(false);

                    parsedDocs = ep.ParseDoc(filePath, cfg.GetSection("mainSettings")["saveDir"], 
                        progress, eF, int.Parse(cfg.GetSection("mainSettings")["exportRetryAttempts"]));

                    // Re-enable buttons.
                    UpdateButtons(true);

                    UpdateStatusLabels();

                    FillGrid(parsedDocs);
                }
                else
                {
                    logger.Info("[MAIN] CheckSettings -> FALSE.");
                    MessageBox.Show("Ошибка в заданных настройках.");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            } 
        }
        private void UpdateButtons(bool isButtonsDisabled)
        {
            this.button_Start.ThreadSafeControlInvoke(new Action(() => this.button_Start.Enabled = isButtonsDisabled));
            this.button_FilePicker.ThreadSafeControlInvoke(new Action(() => this.button_FilePicker.Enabled = isButtonsDisabled));
            this.button_ReloadEmails.ThreadSafeControlInvoke(new Action(() => this.button_ReloadEmails.Enabled = isButtonsDisabled));
        }
        private void UpdateStatusLabels(bool updateEmails = true, bool updateDocs = true)
        {
            // Update email label.
            if(updateEmails)
            {
                int parsedEmails = emails == null || emails.Count == 0 ? 0 : emails.Where(e => !string.IsNullOrEmpty(e.Item2)).Count();
                int totalEmailElements = emails == null || emails.Count == 0 ? 0 : emails.Count;
                this.label_TotalEmailsValue.ThreadSafeControlInvoke(new Action(() => this.label_TotalEmailsValue.Text = parsedEmails + "/" + totalEmailElements));
            }

            // Update doc label.
            if(updateDocs)
            {
                int parsedDocsWithEmail = parsedDocs == null || parsedDocs.Count == 0 ? 0 :
                                        parsedDocs.Where(pd => emails.Any(em => !string.IsNullOrEmpty(em.Item2) && em.Item1 == pd.PersonalNumber)).Count();
                int totalParsedDocs = parsedDocs == null || parsedDocs.Count == 0 ? 0 : parsedDocs.Count();

                this.label_ParsedDocsValue.ThreadSafeControlInvoke(
                            new Action(() => this.label_ParsedDocsValue.Text = parsedDocsWithEmail + "/" + totalParsedDocs));
            }
        }
        private void UpdateSendLabel(int totalEmailsSendedWithoutErrors, int totalEmails)
        {
            this.label_TotalSendValue.ThreadSafeControlInvoke(
                            new Action(() => this.label_TotalSendValue.Text = totalEmailsSendedWithoutErrors + "/" + totalEmails));
        }
        private void FillGrid(List<ParsedDoc> parsedDocs)
        {
            foreach (var pd in parsedDocs)
            {
                string email = emails.Where(k => k.Item1.Equals(pd.PersonalNumber)).DefaultIfEmpty(Tuple.Create("Email not found!", "")).First().Item2;
                pd.Email = email;

                this.dataGridView_Main.ThreadSafeControlInvoke(
                    new Action(() => this.dataGridView_Main.Rows.Add(pd.PersonalNumber, email, pd.Fio, pd.TotalSummPayout, "Show", pd.PersonalAccount, pd.Date)));

                int lastRowIndex = this.dataGridView_Main.Rows.GetLastRow(DataGridViewElementStates.None);

                if (string.IsNullOrEmpty(email))
                    this.dataGridView_Main.ThreadSafeControlInvoke(new Action(() => this.dataGridView_Main[1, lastRowIndex].Style.BackColor = Color.Red));
            }

            if (parsedDocs.Count != 0 && parsedDocs.Any(e => !string.IsNullOrWhiteSpace(e.Email)))
            {
                this.button_Send.ThreadSafeControlInvoke(new Action(() => this.button_Send.Enabled = true));
            }
            else if (parsedDocs.All(e => string.IsNullOrWhiteSpace(e.Email)))
            {
                logger.Info("[MAIN] Collection doesn't contains any emails.");
                this.button_Send.ThreadSafeControlInvoke(new Action(() => this.button_Send.Enabled = false));
                MessageBox.Show("Отсутствуют привязки с email. Проверьте соответствие личных номеров в ID листе и в оригинальном расчётнике.");
            }
            else
            {
                logger.Info("[MAIN] Excel collection was empty.");
                this.button_Send.ThreadSafeControlInvoke(new Action(() => this.button_Send.Enabled = false));
                MessageBox.Show("Расчётный лист пуст.");
            }
        }
        #region Util
        private bool CheckSettings()
        {
            Dictionary<string, bool> settingStatuses = new Dictionary<string, bool>();

            settingStatuses.Add("saveDir", string.IsNullOrEmpty(cfg.GetSection("mainSettings")["saveDir"]));
            settingStatuses.Add("emailList", string.IsNullOrEmpty(cfg.GetSection("mainSettings")["emailList"]));
            settingStatuses.Add("delayMinInSeconds", string.IsNullOrEmpty(cfg.GetSection("mainSettings")["delayMinInSeconds"]));
            settingStatuses.Add("delayMaxInSeconds", string.IsNullOrEmpty(cfg.GetSection("mainSettings")["delayMaxInSeconds"]));
            settingStatuses.Add("exportFormat", string.IsNullOrEmpty(cfg.GetSection("mainSettings")["exportFormat"]));

            settingStatuses.Add("smtpAddress", string.IsNullOrEmpty(cfg.GetSection("emailSettings")["smtpAddress"]));
            settingStatuses.Add("smtpPort", string.IsNullOrEmpty(cfg.GetSection("emailSettings")["smtpPort"]));
            settingStatuses.Add("email", string.IsNullOrEmpty(cfg.GetSection("emailSettings")["email"]));

            if (settingStatuses.Any(ss => ss.Value == true))
            {
                StringBuilder sb = new StringBuilder();

                var badSettings = settingStatuses.Where(ss => ss.Value == true);

                for (int i = 0; i < badSettings.Count(); i++)
                {
                    if (i != badSettings.Count() - 1)
                    {
                        sb.Append(badSettings.ElementAt(i).Key + ", ");
                    }
                    else
                    {
                        sb.Append(badSettings.ElementAt(i).Key);
                    }
                }

                logger.Error("Settings is null: " + sb.ToString() + ".");
                MessageBox.Show("Некорректные настройки в AppSettings: " + sb.ToString() + ".");
            }

            return settingStatuses.All(ss => ss.Value == false);
        }
        public bool CheckSMTP()
        {
            string host = cfg.GetSection("emailSettings")["smtpAddress"];

            if (ConnectionChecker.CheckConnection(host))
            {
                this.pictureBox_SMTP.Image = Resources.green_c;
                return true;
            }
            else
            {
                logger.Info("[MAIN] Unable connect to " + host);
                this.pictureBox_SMTP.Image = Resources.red_c;
                return false;
            }
        }
        private void TimerEventProcessor(object sender, EventArgs e, int i, int docsCount, int updateInterval)
        {
            timerCounter = timerCounter - updateInterval;
            Debug.WriteLine(timerCounter);
            this.label_LoadFileName.ThreadSafeControlInvoke(new Action(() => this.label_LoadFileName.Text =
                        "Отправка " + (i + 1) + "/" + docsCount + ". До следующей отправки " + timerCounter + " ..."));
        }
        #endregion
        private void DataGridView_Main_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                var imgPath = parsedDocs.Single(pd => pd.PersonalNumber.Equals((string)this.dataGridView_Main[0, e.RowIndex].Value)).ImageLocation;

                if (!string.IsNullOrEmpty(imgPath) && File.Exists(imgPath))
                    Process.Start(imgPath);
            }
        }
        #region Menu items
        private void ToolStripMenuItem_AppExit_Click(object sender, EventArgs e)
        {
            logger.Info("[MAIN] ToolStripMenuItem_AppExit_Click.");
            Application.Exit();
        }
        private void ToolStripMenuItem_About_Click(object sender, EventArgs e)
        {
            logger.Info("[MAIN] ToolStripMenuItem_About_Click.");
            MessageBox.Show("Zp 0.5 @ Skoibeda K.S. 2021");
        }
        private void ToolStripMenuItem_Help_Click(object sender, EventArgs e)
        {
            logger.Info("[MAIN] ToolStripMenuItem_Help_Click.");

            var fileHelpPath = Path.Combine(AppContext.BaseDirectory, @"Help\help.chm");

            if(File.Exists(fileHelpPath))
            {
                Process.Start(fileHelpPath);
            }
            else
            {
                logger.Info("[MAIN] Help file not found -> " + fileHelpPath);
                MessageBox.Show("Не могу загрузить справку, подробности в лог-файле.");
            }
        }
        private void ToolStripMenuItem_AppSettings_Click(object sender, EventArgs e)
        {
            logger.Info("[MAIN] ToolStripMenuItem_AppSettings_Click.");
            Forms.Settings sForm = new Forms.Settings(cfg, this);
            sForm.Show();
        }
        #endregion
        #region Buttons
        private async void Button_ReloadEmails_Click(object sender, EventArgs e)
        {
            logger.Info("[MAIN] Button_ReloadEmails_Click.");

            await Task.Run(() => RunParserForEmails());

            if (parsedDocs != null && parsedDocs.Count > 0)
            {
                ClearRows(false);
                FillGrid(parsedDocs);
            }
            else
            {
                logger.Info("[MAIN] ParsedDocs collection was empty!");
            }
        }
        private void Button_FilePicker_Click(object sender, EventArgs e)
        {
            logger.Info("[MAIN] Button_FilePicker_Click.");

            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                FileName = "Select a excel file",
                Filter = "Excel files: (*.xls;*.xlsx)|*.xls;*.xlsx",
                Title = "Open excel file."
            };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    filePath = fileDialog.FileName;
                    this.button_Start.Enabled = true;

                    using (Stream str = fileDialog.OpenFile())
                    {
                        this.label_LoadFileName.Text = filePath;
                        this.label_LoadFileName.TextAlign = ContentAlignment.MiddleCenter;
                    }
                }
                catch (SecurityException ex)
                {
                    logger.Error(ex);
                }
            }
        }
        private void Button_Start_Click(object sender, EventArgs e)
        {
            logger.Info("[MAIN] Button_Start_Click.");
            Extensions.StartSTATask(() => RunParser());
        }
        private void Button_OpenSaveFolder_Click(object sender, EventArgs e)
        {
            logger.Info("[MAIN] Button_OpenSaveFolder_Click.");

            var folder = cfg.GetSection("mainSettings")["saveDir"];

            if (!string.IsNullOrEmpty(folder))
                Process.Start(folder);
        }
        private async void Button_Send_Click(object sender, EventArgs e)
        {
            logger.Info("[MAIN] Button_Send_Click.");

            string smtpAdd = cfg.GetSection("emailSettings")["smtpAddress"];
            string smtpPort = cfg.GetSection("emailSettings")["smtpPort"];
            string senderEmail = cfg.GetSection("emailSettings")["email"];
            string emailSubject = cfg.GetSection("emailSettings")["emailSubject"];
            string emailText = cfg.GetSection("emailSettings")["emailText"];

            if (CheckSMTP())
            {
                MailSender ms = new MailSender(smtpAdd, smtpPort, senderEmail, emailText, cfg);

                var docsToSend = parsedDocs.Where(pd => !string.IsNullOrWhiteSpace(pd.Email)).ToList();
                UpdateSendLabel(0, docsToSend.Count);

                logger.Info("[MAIN] Docs to send: " + docsToSend.Count);

                var result = docsToSend.Select(o => o.Email).Count();

                DialogResult dialogResult = MessageBox.Show("Количество подготовленных email: " + result + ". Отправить?", "Отправка писем", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    IProgress<int> progress = new Progress<int>(percent =>
                    {
                        this.progressBar_Main.ThreadSafeControlInvoke(new Action(() => this.progressBar_Main.Value = percent));
                    });

                    Random rnd = new Random();
                    progress.Report(0);

                    int minDelay = Int32.TryParse(cfg.GetSection("mainSettings")["delayMinInSeconds"], out var tempMin) ? tempMin : 10;
                    int maxDelay = Int32.TryParse(cfg.GetSection("mainSettings")["delayMaxInSeconds"], out var tempMax) ? tempMax : 30;

                    logger.Info("[MAIN] Settings for delay: " + minDelay + " - " + maxDelay);

                    List<Tuple<string, bool>> errorStatusesForMails = new List<Tuple<string, bool>>();

                    // Block send button.
                    this.button_Send.ThreadSafeControlInvoke(new Action(() => this.button_Send.Enabled = false));

                    for (int i = 0; i < docsToSend.Count; i++)
                    {
                        int progressPercent = Convert.ToInt32((i + 1) / (decimal)docsToSend.Count * 100);
                        int delay = rnd.Next(minDelay * 1000, maxDelay * 1000);

                        emailSubject = emailSubject.Replace("%parsedDate%", docsToSend[i].Date);

                        Task.Run(
                            () => errorStatusesForMails.Add(
                                Tuple.Create(docsToSend[i].Email, ms.SendEmail(docsToSend[i].ImageLocation, docsToSend[i].Email, emailSubject)))).Wait();

                        System.Timers.Timer timer = null;

                        // Check if email is the last.
                        if (i != docsToSend.Count - 1)
                        {
                            logger.Info("[MAIN] Waiting " + delay + " for next send event.");

                            timer = new System.Timers.Timer();

                            timerCounter = (delay / 1000);
                            logger.Info("timerCounter -> " + timerCounter);
                            timer.Interval = 1000;
                            timer.Elapsed += (object s, ElapsedEventArgs a) => TimerEventProcessor(s, a, i, docsToSend.Count, (int)timer.Interval / 1000);
                            timer.Start();

                            await Task.Delay(delay);
                        }
                        
                        if(timer != null)
                            timer.Stop();

                        progress.Report(progressPercent);
                    }

                    // Re-enable send button.
                    this.button_Send.ThreadSafeControlInvoke(new Action(() => this.button_Send.Enabled = true));

                    if (errorStatusesForMails.Any(em => em.Item2))
                    {
                        int errorMsgCount = errorStatusesForMails.Where(em => em.Item2).ToList().Count;

                        UpdateSendLabel(errorStatusesForMails.Where(em => !em.Item2).ToList().Count, docsToSend.Count);

                        string errorMsg = "Возникли ошибки при отправке " + errorMsgCount + " " 
                            + errorMsgCount.GetDeclension("письма", "писем", "писем") + ". Подробности в лог-файле.";

                        this.label_LoadFileName.ThreadSafeControlInvoke(
                            new Action(() => this.label_LoadFileName.Text = errorMsg));
                    }
                    else
                    {
                        UpdateSendLabel(errorStatusesForMails.Where(em => !em.Item2).ToList().Count, docsToSend.Count);
                        this.label_LoadFileName.ThreadSafeControlInvoke(new Action(() => this.label_LoadFileName.Text = "Отправка завершена."));
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    logger.Info("[MAIN] Button_Send_Click skip.");
                }
            }
            else
            {
                MessageBox.Show("Unable connect to " + smtpAdd + ". Check SMTP settings and connection.");
            }
        }
        #endregion
    }
    public interface IMain
    {
        void InitEmailList();
        bool CheckSMTP();
    }
}