using NLog;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zp
{
    public static class Extensions
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        // Thread safe invoke.
        static public void ThreadSafeControlInvoke(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
        static public string GetSafePathName(this string fullFilePath, int fileRepeatIndex = 1)
        {
            string safeFileName = string.Empty;

            if(fileRepeatIndex < 10)
            {
                if (File.Exists(fullFilePath))
                {
                    string filePath = Path.GetFullPath(Path.GetDirectoryName(fullFilePath));
                    string fileName = Path.GetFileNameWithoutExtension(fullFilePath);
                    string fileExt = Path.GetExtension(fullFilePath);
                    string newFileName = string.Empty;
                    string newFilePath;

                    if (fileRepeatIndex > 1)
                    {
                        newFileName = fileName.Substring(0, fileName.Length - fileRepeatIndex.ToString().Length) + fileRepeatIndex + fileExt;
                        logger.Info("[EXT] fileRepeatIndex name -> " + newFileName);
                    }
                    else
                    {
                        newFileName = fileName + "_" + fileRepeatIndex + fileExt;
                    }

                    newFilePath = Path.Combine(filePath, newFileName);

                    if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                        throw new Exception("Invalid file name -> " + fileName);

                    return GetSafePathName(newFilePath, fileRepeatIndex + 1);
                }
                else
                {
                    safeFileName = fullFilePath;
                }
            }
            else
            {
                logger.Info("[EXT] File repeat was reached for " + fullFilePath);
                safeFileName = fullFilePath;
            }

            logger.Info("[EXT] Safe file name -> " + safeFileName);

            return safeFileName;
        }
        public static Task<T> StartSTATask<T>(Func<T> func)
        {
            var tcs = new TaskCompletionSource<T>();
            Thread thread = new Thread(() =>
            {
                try
                {
                    tcs.SetResult(func());
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }
        public static Task StartSTATask(Action func)
        {
            var tcs = new TaskCompletionSource<object>();
            var thread = new Thread(() =>
            {
                try
                {
                    func();
                    tcs.SetResult(null);
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }
        /// <summary>
        /// Возвращает слова в падеже, зависимом от заданного числа 
        /// </summary>
        /// <param name="number">Число от которого зависит выбранное слово</param>
        /// <param name="nominativ">Именительный падеж слова. Например "день"</param>
        /// <param name="genetiv">Родительный падеж слова. Например "дня"</param>
        /// <param name="plural">Множественное число слова. Например "дней"</param>
        /// <returns></returns>
        public static string GetDeclension(this int number, string nominativ, string genetiv, string plural)
        {
            number = number % 100;

            if (number >= 11 && number <= 19)
            {
                return plural;
            }

            var i = number % 10;

            switch (i)
            {
                case 1:
                    return nominativ;
                case 2:
                case 3:
                case 4:
                    return genetiv;
                default:
                    return plural;
            }

        }
    }
}