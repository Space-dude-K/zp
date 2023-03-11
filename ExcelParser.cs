using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;
using static Zp.ParsedDoc;
using Excel = Microsoft.Office.Interop.Excel;

namespace Zp
{
    class ExcelParser
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        
        public List<ParsedDoc> ParseDoc(string excelPath, string saveFolder, IProgress<int> progress, ExportFormat ef, int maxiumRetryAttempts)
        {
            List<ParsedDoc> pdL = null;
            int hWnd;

            Excel.Application rApp;
            Excel.Workbook rWb;
            Excel.Worksheet rSheet;
            Excel.Range range;

            rApp = new Excel.Application();

            hWnd = rApp.Hwnd;

            rWb = rApp.Workbooks.Open(excelPath, ReadOnly: true);
            rSheet = (Excel.Worksheet)rWb.Sheets[1];
            range = (Excel.Range)rSheet.UsedRange;

            logger.Info("[EXCEL-P] Total rows: " + range.Rows.Count);

            try
            {
                pdL = new List<ParsedDoc>();
                ParsedDoc pd = null;

                string A1topLeftCornerCoordinate = string.Empty;
                string A1botRightCornerCoordinate = string.Empty;

                string saveDir = Path.Combine(saveFolder, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());

                logger.Info("[EXCEL-P] Check dir -> " + saveDir);

                if (!Directory.Exists(saveDir))
                {
                    Directory.CreateDirectory(saveDir);
                }
                else
                {
                    Directory.Delete(saveDir, true);
                    Directory.CreateDirectory(saveDir);
                }

                if(!Directory.Exists(saveDir))
                {
                    logger.Error("[EXCEL-P] IO Error, check directory permissions for -> " + saveDir);
                }    

                progress.Report(0);

                logger.Info("[EXCEL-P] Parsing ...");

                for (int r = 1; r < range.Rows.Count; r++)
                {
                    int progressPercent = Convert.ToInt32(r / (decimal)range.Rows.Count * 100);

                    progress.Report(progressPercent);

                    for(int c = 1; c < range.Columns.Count; c++)
                    {
                        string str = Convert.ToString(((Excel.Range)range.Rows.Cells[r, c]).Value2);

                        if(!string.IsNullOrEmpty(str))
                        {
                            switch (str)
                            {
                                case string a when a.Contains("Расчетный листок за"):
                                    pd = new ParsedDoc();
                                    pd.Date = !string.IsNullOrEmpty(str) ? str.Substring(str.IndexOf("Расчетный листок за") + "Расчетный листок за".Length).Trim() : "Null";
                                    A1topLeftCornerCoordinate = ((Excel.Range)range.Rows.Cells[r, c]).Address;
                                    pd.TopLeftCoordinate = ((Excel.Range)range.Rows.Cells[r, c]).Address[true, true, Excel.XlReferenceStyle.xlR1C1, false];
                                    break;
                                case string f when f.Contains("Лицевой"):
                                    pd.PersonalAccount = !string.IsNullOrEmpty(str) ? str.Substring(str.IndexOf(':') + 1).Trim() : "Null";
                                    break;
                                case string b when b.Contains("Работник"):
                                    pd.Fio = !string.IsNullOrEmpty(str) ? str.Substring(str.IndexOf(':') + 1).Trim() : "Null";
                                    break;
                                case string b when b.Contains("Личный"):
                                    pd.PersonalNumber = !string.IsNullOrEmpty(str) ? str.Substring(str.IndexOf(':') + 1).Trim() : "Null";
                                    break;
                                case string d when d.Contains("Всего выплат"):
                                    str = Convert.ToString(((Excel.Range)range.Rows.Cells[r, c + 6]).Value2);
                                    pd.TotalSummPayout = !string.IsNullOrEmpty(str) ? str.Trim() : "Null";
                                    break;
                                case string d when d.Contains("Полагается к выплате"):
                                    str = Convert.ToString(((Excel.Range)range.Rows.Cells[r, c + 6]).Value2);
                                    pd.TotalSummOrganizationDebt = !string.IsNullOrEmpty(str) ? str.Trim() : "Null";
                                    pdL.Add(pd);
                                    pd.BotRightCoordinate = ((Excel.Range)range.Rows.Cells[r, c + 6]).Address[true, true, Excel.XlReferenceStyle.xlR1C1, false];
                                    A1botRightCornerCoordinate = ((Excel.Range)range.Rows.Cells[r + 1, c + 7]).Address;

                                    switch (ef)
                                    {
                                        case ExportFormat.Png:
                                            ExportRangeToPngHighResolutionWithRetryLogic(rSheet.Range[A1topLeftCornerCoordinate, A1botRightCornerCoordinate], rSheet, saveDir, pd, maxiumRetryAttempts);
                                            break;
                                        case ExportFormat.Pdf:
                                            ExportRangeToPdfWithRetryLogic(rSheet.Range[A1topLeftCornerCoordinate, A1botRightCornerCoordinate], rSheet, saveDir, pd, maxiumRetryAttempts);
                                            break;
                                        default:
                                            ExportRangeToPngHighResolutionWithRetryLogic(rSheet.Range[A1topLeftCornerCoordinate, A1botRightCornerCoordinate], rSheet, saveDir, pd, maxiumRetryAttempts);
                                            break;
                                    }

                                    break;
                            }
                        }
                    }   
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, "[EXCEL-P] ParseDoc error!");
            }
            finally
            {
                Dispose(rApp, rWb, rSheet, hWnd);
                progress.Report(100);
            }
 
            return pdL;
        }
        public List<Tuple<string, string>> ParseEmails(string emailsPath, IProgress<int> progress)
        {
            List<Tuple<string, string>> pEm = null;
            int hWnd = 0;

            Excel.Application rApp = null;
            Excel.Workbook rWb = null;
            Excel.Worksheet rSheet = null;
            Excel.Range range;

            try
            {
                rApp = new Excel.Application();

                hWnd = rApp.Hwnd;

                rWb = rApp.Workbooks.Open(emailsPath, ReadOnly: true);

                rSheet = (Excel.Worksheet)rWb.Sheets[1];
                range = (Excel.Range)rSheet.UsedRange;

                logger.Info("[EXCEL-P] Total email rows: " + range.Rows.Count);

                pEm = new List<Tuple<string, string>>();

                progress.Report(0);

                for (int r = 2; r < range.Rows.Count + 1; r++)
                {
                    int progressPercent = Convert.ToInt32(r / (decimal)(range.Rows.Count) * 100);

                    progress.Report(progressPercent);

                    string fio = Convert.ToString(((Excel.Range)range.Rows.Cells[r, 1]).Value2);
                    string email = Convert.ToString(((Excel.Range)range.Rows.Cells[r, 2]).Value2);
                    string id = Convert.ToString(((Excel.Range)range.Rows.Cells[r, 3]).Value2);
                    
                    logger.Info("[EXCEL-P] Parse email for: " + fio);

                    if (!string.IsNullOrEmpty(id))
                    {
                        pEm.Add(Tuple.Create(id, email));
                    }
                    else
                    {
                        logger.Info("[EXCEL-P] Id was empty for email: " + email);
                    } 
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "[EXCEL-P] ParseEmails error!");
            }
            finally
            {
                Dispose(rApp, rWb, rSheet, hWnd);
                progress.Report(100);
            }

            return pEm;
        }
        private void ExportRangeToPng(Excel.Range range, Excel.Worksheet sheet, string saveFolder, ParsedDoc pd)
        {
            try
            {
                string saveDir = Path.Combine(saveFolder, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());
                string savePath = Path.Combine(saveDir, pd.Fio.Replace(' ', '_') + "_" + pd.PersonalNumber + ".png");

                pd.ImageLocation = savePath;

                logger.Info("[EXCEL-P] Export -> " + savePath);

                if (!Directory.Exists(saveDir))
                    Directory.CreateDirectory(saveDir);
                
                range.CopyPicture(Excel.XlPictureAppearance.xlScreen, Excel.XlCopyPictureFormat.xlPicture);
                Excel.ChartObject chartObj = sheet.ChartObjects().Add(range.Left, range.Top, range.Width, range.Height);

                chartObj.Activate();  // Don't Forget!

                Excel.Chart chart = chartObj.Chart;
                chart.Paste();
                chart.Export(Path.Combine(saveFolder, savePath));

                chartObj.Delete();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "[EXCEL-P] ExportRangeToPng error!");
            }
        }
        private void ExportRangeToPngHighResolution(Excel.Range range, Excel.Worksheet sheet, string saveDir, ParsedDoc pd)
        {
            try
            {
                string savePath = Path.Combine(saveDir, pd.Fio.Replace(' ', '_') +  ".png");
                string safeFullPath = Path.Combine(saveDir, savePath).GetSafePathName();

                pd.ImageLocation = safeFullPath;

                logger.Info("[EXCEL-P] Export -> " + savePath);

                FormatRange(range);

                range.CopyPicture(Excel.XlPictureAppearance.xlScreen, Excel.XlCopyPictureFormat.xlBitmap);
                Excel.ChartObject chartObj = sheet.ChartObjects().Add(range.Left, range.Top, range.Width, range.Height);
                chartObj.Activate();

                Excel.Chart chart = chartObj.Chart;
                sheet.Paste();

                sheet.Shapes.Item(sheet.Shapes.Count).Copy();

                // Save the image.
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(Clipboard.GetImage()));

                using (MemoryStream outStream = new MemoryStream())
                {
                    enc.Save(outStream);

                    Image pic = new Bitmap(outStream);

                    pic = ResizeBitmap(new Bitmap(outStream), (int)(pic.Width * 1.2), (int)(pic.Height * 1.2));
                    pic.Save(safeFullPath, ImageFormat.Png);
                }

                chartObj.Delete();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "[EXCEL-P] ExportRangeToPng error!");
            }
        }
        private void ExportRangeToPngHighResolutionWithRetryLogic(Excel.Range range, Excel.Worksheet sheet, string saveDir, ParsedDoc pd, int maxiumRetryAttempts)
        {
            int maxiumAttempts = maxiumRetryAttempts > 100 ? 100 : maxiumRetryAttempts;
            var errorCounter = 0;
            var copyDone = false;

            while(!copyDone && errorCounter <= maxiumAttempts)
            {
                try
                {
                    string savePath = Path.Combine(saveDir, pd.Fio.Replace(' ', '_') + ".png");
                    string safeFullPath = Path.Combine(saveDir, savePath).GetSafePathName();

                    pd.ImageLocation = safeFullPath;

                    logger.Info("[EXCEL-P] Export -> " + savePath);

                    FormatRange(range);

                    range.CopyPicture(Excel.XlPictureAppearance.xlScreen, Excel.XlCopyPictureFormat.xlBitmap);
                    Excel.ChartObject chartObj = sheet.ChartObjects().Add(range.Left, range.Top, range.Width, range.Height);
                    chartObj.Activate();

                    Excel.Chart chart = chartObj.Chart;
                    sheet.Paste();

                    sheet.Shapes.Item(sheet.Shapes.Count).Copy();

                    // Save the image.
                    BitmapEncoder enc = new BmpBitmapEncoder();
                    enc.Frames.Add(BitmapFrame.Create(Clipboard.GetImage()));

                    using (MemoryStream outStream = new MemoryStream())
                    {
                        enc.Save(outStream);

                        Image pic = new Bitmap(outStream);

                        pic = ResizeBitmap(new Bitmap(outStream), (int)(pic.Width * 1.2), (int)(pic.Height * 1.2));
                        pic.Save(safeFullPath, ImageFormat.Png);
                    }

                    chartObj.Delete();

                    copyDone = true;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "[EXCEL-P] ExportRangeToPng error!");
                }
            }

            if (errorCounter == 100)
                throw new ApplicationException("[EXCEL-P] Unable to copy the selected range for png export. Достигнут предел попыток, попробуйте закрыть все приложения Excel и повторите попытку.");
        }
        private void ExportRangeToPdf(Excel.Range range, Excel.Worksheet sheet, string saveDir, ParsedDoc pd)
        {
            try
            {
                string savePath = Path.Combine(saveDir, pd.Fio.Replace(' ', '_') + ".pdf");
                string safeFullPath = Path.Combine(saveDir, savePath).GetSafePathName();

                pd.ImageLocation = safeFullPath;

                logger.Info("[EXCEL-P] Export -> " + savePath);

                FormatRange(range);

                range.CopyPicture(Excel.XlPictureAppearance.xlScreen, Excel.XlCopyPictureFormat.xlBitmap);

                Excel.ChartObject chartObj = sheet.ChartObjects().Add(range.Left, range.Top, range.Width, range.Height);
                chartObj.Activate();

                Excel.Chart chart = chartObj.Chart;
                chart.Paste();

                sheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

                chart.ExportAsFixedFormat(
                    Excel.XlFixedFormatType.xlTypePDF,
                    pd.ImageLocation,
                    Excel.XlFixedFormatQuality.xlQualityStandard,
                    true,
                    true,
                    1,
                    1,
                    false);

                chartObj.Delete();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "[EXCEL-P] ExportRangeToPdf error!");
            }
        }
        private void ExportRangeToPdfWithRetryLogic(Excel.Range range, Excel.Worksheet sheet, string saveDir, ParsedDoc pd, int maxiumRetryAttempts)
        {
            int maxiumAttempts = maxiumRetryAttempts > 100 ? 100 : maxiumRetryAttempts;
            var errorCounter = 0;
            var copyDone = false;

            while (!copyDone && errorCounter <= maxiumAttempts)
            {
                try
                {
                    string savePath = Path.Combine(saveDir, pd.Fio.Replace(' ', '_') + ".pdf");
                    string safeFullPath = Path.Combine(saveDir, savePath).GetSafePathName();

                    pd.ImageLocation = safeFullPath;

                    logger.Info("[EXCEL-P] Export -> " + savePath);

                    FormatRange(range);

                    range.CopyPicture(Excel.XlPictureAppearance.xlScreen, Excel.XlCopyPictureFormat.xlBitmap);

                    Excel.ChartObject chartObj = sheet.ChartObjects().Add(range.Left, range.Top, range.Width, range.Height);
                    chartObj.Activate();

                    Excel.Chart chart = chartObj.Chart;
                    chart.Paste();

                    sheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

                    chart.ExportAsFixedFormat(
                        Excel.XlFixedFormatType.xlTypePDF,
                        pd.ImageLocation,
                        Excel.XlFixedFormatQuality.xlQualityStandard,
                        true,
                        true,
                        1,
                        1,
                        false);

                    chartObj.Delete();

                    copyDone = true;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "[EXCEL-P] ExportRangeToPdf error!");
                }
            }

            if (errorCounter == 100)
                throw new ApplicationException("[EXCEL-P] Unable to copy the selected range for pdf export. Достигнут предел попыток, попробуйте закрыть все приложения Excel и повторите попытку.");
        }
        // Change font size.
        private void FormatRange(Excel.Range range)
        {
            // Change font size.
            foreach (Excel.Range cell in range.Cells)
            {
                if (cell.Font.Size == 6)
                {
                    cell.Font.Size = 9;
                }
            }

            range.Columns.AutoFit();
            range.Rows.AutoFit();
        }
        // Resize output image.
        private Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(sourceBMP, 0, 0, width, height);
            }
            return result;
        }
        private void Dispose(Excel.Application iApp, Excel.Workbook iWb, Excel.Worksheet nSheet, int hWnd)
        {
            logger.Info("[EXCEL-P] Disposing ...");

            iWb.Close(false);
            iApp.Quit();

            Marshal.ReleaseComObject(nSheet);
            Marshal.ReleaseComObject(iWb);
            Marshal.ReleaseComObject(iApp);

            iApp = null;
            iWb = null;
            nSheet = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();

            TryKillProcessByMainWindowHwnd(hWnd);

            logger.Info("[EXCEL-P] Done.");
        }
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        /// <summary> Tries to find and kill process by hWnd to the main window of the process.</summary>
        /// <param name="hWnd">Handle to the main window of the process.</param>
        /// <returns>True if process was found and killed. False if process was not found by hWnd or if it could not be killed.</returns>
        public static bool TryKillProcessByMainWindowHwnd(int hWnd)
        {
            uint processID;
            GetWindowThreadProcessId((IntPtr)hWnd, out processID);
            if (processID == 0) return false;
            try
            {
                Process.GetProcessById((int)processID).Kill();
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (Win32Exception)
            {
                return false;
            }
            catch (NotSupportedException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            return true;
        }
        /// <summary> Finds and kills process by hWnd to the main window of the process.</summary>
        /// <param name="hWnd">Handle to the main window of the process.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when process is not found by the hWnd parameter (the process is not running). 
        /// The identifier of the process might be expired.
        /// </exception>
        /// <exception cref="Win32Exception">See Process.Kill() exceptions documentation.</exception>
        /// <exception cref="NotSupportedException">See Process.Kill() exceptions documentation.</exception>
        /// <exception cref="InvalidOperationException">See Process.Kill() exceptions documentation.</exception>
        public static void KillProcessByMainWindowHwnd(int hWnd)
        {
            uint processID;
            GetWindowThreadProcessId((IntPtr)hWnd, out processID);
            if (processID == 0)
                throw new ArgumentException("Process has not been found by the given main window handle.", "hWnd");
            Process.GetProcessById((int)processID).Kill();
        }
    }
}