using MineNET.Utils.Config;
using System;
using System.IO;

namespace MineNET.Reports
{
    public class CrashReport : IReport
    {
        public string Title { get; }
        public string Description { get; }

        public Exception Cause { get; }

        public DateTime Time { get; }

        public string OSData { get; }

        private CrashReport(string title, Exception cause, DateTime time)
        {
            this.Title = title;

            this.Cause = cause;
            this.Time = time;

            this.OSData = Environment.OSVersion.ToString();
        }

        private CrashReport(string title, string cause, DateTime time)
        {
            this.Title = title;
            this.Description = cause;

            this.Time = time;

            this.OSData = Environment.OSVersion.ToString();
        }

        public static CrashReport ExportReport(string title, Exception cause, bool debugReport = false)
        {
            DateTime time = DateTime.Now;
            string path = Server.ExecutePath + "/reports/crash";
            string file = path + "/report-" + time.ToString("yyyy_M_d H_mm_ss") + ".report";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            YamlConfig report = YamlConfig.Load(file);
            CrashReport data = null;
            if (debugReport)
            {
                data = new CrashReport(title, cause, time);
            }
            else
            {
                data = new CrashReport(title, cause.ToString(), time);
            }

            report.Set("reportData", data);
            report.Save();

            return data;
        }

        public static CrashReport ExportReport(string title, string cause)
        {
            DateTime time = DateTime.Now;
            string path = Server.ExecutePath + "/reports/crash";
            string file = path + "/report-" + time.ToString("yyyy_M_d H_mm_ss") + ".report";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            CrashReport data = new CrashReport(title, cause, time);

            YamlConfig report = YamlConfig.Load(file);
            report.Set("reportData", data);
            report.Save();

            return data;
        }

        public void SendReport()
        {
            throw new NotImplementedException();
        }
    }
}