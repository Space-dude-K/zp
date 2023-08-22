using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Zp
{
    public static class Validator
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private static bool CheckPath(string pathName, string path)
        {
            bool isPathValid = false;

            if (string.IsNullOrEmpty(path))
            {
                logger.Error($"[Validator] {pathName} {path} is null or empty!");
            }
            else if (!Directory.Exists(path))
            {
                logger.Error($"[Validator] {pathName} {path} didnt exist!");
            }
            else if (!IsDirectoryWritable(path))
            {
                logger.Error($"[Validator] {pathName} {path} write access error, check privileges!");
            }
            else
            {
                isPathValid = true;
            }

            return isPathValid;
        }
        private static bool IsDirectoryWritable(string dirPath, bool throwIfFails = false)
        {
            try
            {
                using (FileStream fs = File.Create(Path.Combine(dirPath, Path.GetRandomFileName()), 1, FileOptions.DeleteOnClose))
                { }

                return true;
            }
            catch
            {
                if (throwIfFails)
                {
                    logger.Error("[Validator] Write access exception for: " + dirPath);
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }
        public static bool ValidateTextBoxInputs(System.Windows.Forms.Control.ControlCollection controls)
        {
            List<bool> isAllSettingsValid = new List<bool>();

            foreach (Control x in controls)
            {
                if (x is GroupBox)
                {
                    isAllSettingsValid.Add(ValidateTextBoxInputs(x.Controls));
                }
                else if (x.Enabled && x is TextBox && x.Name.Contains("textBox"))
                {
                    isAllSettingsValid.Add(CheckSetting(x));
                }
            }

            return !isAllSettingsValid.Any(s => !s);
        }
        private static bool CheckSetting(Control x)
        {
            bool iScurrentSettingValid = false;

            string textBoxStr = x.Text;

            if (string.IsNullOrWhiteSpace(textBoxStr))
            {
                EventHandler eh = new EventHandler((sender, e) => ShowToolTip((Control)sender, "Empty inputs!", 100, 8));

                x.BackColor = Color.Red;
                x.MouseEnter += eh;
                x.TextChanged += new EventHandler((sender, e) => ResetToolTip((Control)sender, eh));
                x.EnabledChanged += new EventHandler((sender, e) => ResetToolTip((Control)sender, eh));

                logger.Error($"[Validator] {textBoxStr} is null or empty!");
            }
            else if (x.Name.Contains("path"))
            {
                if (!Directory.Exists(textBoxStr))
                {
                    EventHandler eh = new EventHandler((sender, e) => ShowToolTip((Control)sender, textBoxStr + " didnt exist!", 100, 8));

                    x.BackColor = Color.Red;
                    x.MouseEnter += eh;
                    x.TextChanged += new EventHandler((sender, e) => ResetToolTip((Control)sender, eh));
                    x.EnabledChanged += new EventHandler((sender, e) => ResetToolTip((Control)sender, eh));

                    logger.Error($"[Validator] {x.Name} {textBoxStr} didnt exist!");
                }
                else if (!IsDirectoryWritable(textBoxStr))
                {
                    EventHandler eh = new EventHandler((sender, e) => ShowToolTip((Control)sender, 
                        textBoxStr + " write access error, check privileges!", 100, 8));

                    x.BackColor = Color.Red;
                    x.MouseEnter += eh;
                    x.TextChanged += new EventHandler((sender, e) => ResetToolTip((Control)sender, eh));
                    x.EnabledChanged += new EventHandler((sender, e) => ResetToolTip((Control)sender, eh));

                    logger.Error($"[Validator] {x.Name} {textBoxStr} write access error, check privileges!");
                }
                else
                {
                    iScurrentSettingValid = true;
                }
            }
            else
            {
                iScurrentSettingValid = true;
            }

            return iScurrentSettingValid;
        }
        private static void ShowToolTip(Control control, string msg, int pointOffSetX, int pointOffSetY)
        {
            MethodInvoker methodInvokerDelegate = delegate ()
            {
                Point point = new Point(control.PointToScreen(Point.Empty).X + pointOffSetX, control.PointToScreen(Point.Empty).Y - pointOffSetY);

                Help.ShowPopup(control, msg, point);
            };

            control.Invoke(methodInvokerDelegate);
        }
        private static void ResetToolTip(Control control, EventHandler eh)
        {
            control.MouseEnter -= eh;
            control.BackColor = Color.White;
        }
    }
}