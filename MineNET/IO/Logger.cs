using System;
using System.Threading;

namespace MineNET.IO
{
    public class Logger : ILogger
    {
        #region Property & Field
        public bool IsRunConsole { get; private set; }

        public InputInterface Input { get; }
        public OutputInterface Output { get; }
        #endregion

        #region Ctor
        public Logger()
        {
            this.Input = new Input();
            this.Output = new Output();
        }
        #endregion

        #region Output Method
        public void Error(object text)
        {
            string str = text.ToString();
            if (!string.IsNullOrEmpty(str))
            {
                string time = this.CreateTime();
                if (str[0] == '%')
                {
                    string key = str.Remove(0, 1);
                    str = LanguageService.GetString(key);
                }
                str = $"§c[{time}][{this.GetInvokeThread()}/Error] {str}";

                this.AddOueue(str, LoggerLevel.Error);
            }
        }

        public void Error(object text, params object[] args)
        {
            string str = text.ToString();
            if (!string.IsNullOrEmpty(str))
            {
                if (str[0] == '%')
                {
                    string key = str.Remove(0, 1);
                    str = LanguageService.GetString(key);
                }
                str = string.Format(str, args);

                this.Error(str);
            }
        }

        public void Fatal(object text)
        {
            string str = text.ToString();
            if (!string.IsNullOrEmpty(str))
            {
                string time = this.CreateTime();
                if (str[0] == '%')
                {
                    string key = str.Remove(0, 1);
                    str = LanguageService.GetString(key);
                }
                str = $"§4[{time}][{this.GetInvokeThread()}/Fatal] {str}";

                this.AddOueue(str, LoggerLevel.Fatal);
            }
        }

        public void Fatal(object text, params object[] args)
        {
            string str = text.ToString();
            if (!string.IsNullOrEmpty(str))
            {
                if (str[0] == '%')
                {
                    string key = str.Remove(0, 1);
                    str = LanguageService.GetString(key);
                }
                str = string.Format(str, args);

                this.Fatal(str);
            }
        }

        public void Info(object text)
        {
            string str = text.ToString();
            if (!string.IsNullOrEmpty(str))
            {
                string time = this.CreateTime();
                if (str[0] == '%')
                {
                    string key = str.Remove(0, 1);
                    str = LanguageService.GetString(key);
                }
                str = $"§f[{time}][{this.GetInvokeThread()}/Info] {str}";

                this.AddOueue(str, LoggerLevel.Info);
            }
        }

        public void Info(object text, params object[] args)
        {
            string str = text.ToString();
            if (!string.IsNullOrEmpty(str))
            {
                if (str[0] == '%')
                {
                    string key = str.Remove(0, 1);
                    str = LanguageService.GetString(key);
                }
                str = string.Format(str, args);

                this.Info(str);
            }
        }

        public void Log(object text)
        {
            string str = text.ToString();
            if (!string.IsNullOrEmpty(str))
            {
                string time = this.CreateTime();
                if (str[0] == '%')
                {
                    string key = str.Remove(0, 1);
                    str = LanguageService.GetString(key);
                }
                str = $"§7[{time}][{this.GetInvokeThread()}/Log] {str}";

                this.AddOueue(str, LoggerLevel.Log);
            }
        }

        public void Log(object text, params object[] args)
        {
            string str = text.ToString();
            if (!string.IsNullOrEmpty(str))
            {
                if (str[0] == '%')
                {
                    string key = str.Remove(0, 1);
                    str = LanguageService.GetString(key);
                }
                str = string.Format(str, args);

                this.Log(str);
            }
        }

        public void Notice(object text)
        {
            string str = text.ToString();
            if (!string.IsNullOrEmpty(str))
            {
                string time = this.CreateTime();
                if (str[0] == '%')
                {
                    string key = str.Remove(0, 1);
                    str = LanguageService.GetString(key);
                }
                str = $"§b[{time}][{this.GetInvokeThread()}/Notice] {str}";

                this.AddOueue(str, LoggerLevel.Notice);
            }
        }

        public void Notice(object text, params object[] args)
        {
            string str = text.ToString();
            if (!string.IsNullOrEmpty(str))
            {
                if (str[0] == '%')
                {
                    string key = str.Remove(0, 1);
                    str = LanguageService.GetString(key);
                }
                str = string.Format(str, args);

                this.Notice(str);
            }
        }

        public void Warning(object text)
        {
            string str = text.ToString();
            if (!string.IsNullOrEmpty(str))
            {
                string time = this.CreateTime();
                if (str[0] == '%')
                {
                    string key = str.Remove(0, 1);
                    str = LanguageService.GetString(key);
                }
                str = $"§e[{time}][{this.GetInvokeThread()}/Warning] {str}";

                this.AddOueue(str, LoggerLevel.Warning);
            }
        }

        public void Warning(object text, params object[] args)
        {
            string str = text.ToString();
            if (!string.IsNullOrEmpty(str))
            {
                if (str[0] == '%')
                {
                    string key = str.Remove(0, 1);
                    str = LanguageService.GetString(key);
                }
                str = string.Format(str, args);

                this.Warning(str);
            }
        }
        #endregion

        #region Add Oueue Method
        public void AddOueue(string text, LoggerLevel level)
        {
            LoggerData data = new LoggerData();
            data.Level = level;
            data.Text = text;
            this.Output.AddOutputQueue(data);
        }
        #endregion

        #region Time Method
        private string CreateTime()
        {
            return DateTime.Now.ToString("yyyy/M/d H:mm:ss");
        }

        private string CreateDay()
        {
            return DateTime.Now.ToString("yyyy-M-d");
        }
        #endregion

        #region Get Invoke Thread Method
        private string GetInvokeThread()
        {
            return Thread.CurrentThread.Name;
        }
        #endregion

        #region Dispose Method
        public void Dispose()
        {
            this.Input.Dispose();
            this.Output.Dispose();
        }
        #endregion
    }
}
