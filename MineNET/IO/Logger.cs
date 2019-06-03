using MineNET.Text;
using NLog;
using NLog.Config;
using System;
using System.IO;
using System.Text;
using NLog.Layouts;
using NLog.Targets;

namespace MineNET.IO
{
    public class Logger : IServerLogger
    {
        public ILogger OutputLogger { get; }
        public InputInterface InputLogger { get; }

        public Logger()
        {
            LoggingConfiguration conf = new LoggingConfiguration();

            this.InputLogger = new Input();

            try
            {
                Console.Title = "MineNET";
                conf.AddTarget("console", new MineNetConsoleTarget((Input) this.InputLogger)
                {
                    Layout = new SimpleLayout(
                        "[${longdate}] [${threadname} /${uppercase:${level:padding=5}}] ${message}")
                });
                conf.AddRule(LogLevel.Debug, LogLevel.Fatal, "console");

                Console.CancelKeyPress += ConsoleOnCancelKeyPress;
            }
            catch (IOException)
            {
            }

            this.SetLogFileConfig(conf);
            LogManager.Configuration = conf;

            this.OutputLogger = LogManager.GetCurrentClassLogger();
        }

        public static void Debug(object text)
        {
            Server.Instance?.Logger.OutputLogger.Debug(CheckLocalizationFormat(text, null));
        }

        public static void Debug(object text, params object[] args)
        {
            Server.Instance?.Logger.OutputLogger.Debug(CheckLocalizationFormat(text, args));
        }

        public static void Debug(ITextContainer textContainer)
        {
            Server.Instance?.Logger.OutputLogger.Debug(textContainer.GetText());
        }

        public static void Info(object text)
        {
            Server.Instance?.Logger.OutputLogger.Info(CheckLocalizationFormat(text, null));
        }

        public static void Info(object text, params object[] args)
        {
            Server.Instance?.Logger.OutputLogger.Info(CheckLocalizationFormat(text, args));
        }

        public static void Info(ITextContainer textContainer)
        {
            Server.Instance?.Logger.OutputLogger.Info(textContainer.GetText());
        }

        public static void Trace(object text)
        {
            if (!Server.Instance.Config.TraceLogDisable)
                Server.Instance?.Logger.OutputLogger.Trace(CheckLocalizationFormat(text, null));
        }

        public static void Trace(object text, params object[] args)
        {
            if (!Server.Instance.Config.TraceLogDisable)
                Server.Instance?.Logger.OutputLogger.Trace(CheckLocalizationFormat(text, args));
        }

        public static void Trace(ITextContainer textContainer)
        {
            if (!Server.Instance.Config.TraceLogDisable)
                Server.Instance?.Logger.OutputLogger.Trace(textContainer.GetText());
        }

        public static void Warn(object text)
        {
            Server.Instance?.Logger.OutputLogger.Warn(CheckLocalizationFormat(text, null));
        }

        public static void Warn(object text, params object[] args)
        {
            Server.Instance?.Logger.OutputLogger.Warn(CheckLocalizationFormat(text, args));
        }

        public static void Warn(ITextContainer textContainer)
        {
            Server.Instance?.Logger.OutputLogger.Warn(textContainer.GetText());
        }

        public static void Error(object text)
        {
            Server.Instance?.Logger.OutputLogger.Error(CheckLocalizationFormat(text, null));
        }

        public static void Error(object text, params object[] args)
        {
            Server.Instance?.Logger.OutputLogger.Error(CheckLocalizationFormat(text, args));
        }

        public static void Error(ITextContainer textContainer)
        {
            Server.Instance?.Logger.OutputLogger.Error(textContainer.GetText());
        }

        public static void Fatal(object text)
        {
            Server.Instance?.Logger.OutputLogger.Fatal(CheckLocalizationFormat(text, null));
        }

        public static void Fatal(object text, params object[] args)
        {
            Server.Instance?.Logger.OutputLogger.Fatal(CheckLocalizationFormat(text, args));
        }

        public static void Fatal(ITextContainer textContainer)
        {
            Server.Instance?.Logger.OutputLogger.Fatal(textContainer.GetText());
        }

        internal static string CheckLocalizationFormat(object text, object[] args)
        {
            string str = text?.ToString();
            if (str?.Length > 0)
            {
                char f = str[0];
                if (f == '%')
                {
                    CultureTextContainer textContainer = new CultureTextContainer(str.Remove(0, 1), args);
                    return textContainer.GetText();
                }

                return str;
            }

            return "Null";
        }

        public void Dispose()
        {
            this.InputLogger.Dispose();
        }

        private void SetLogFileConfig(LoggingConfiguration conf)
        {
            conf.AddTarget("event", new FileTarget()
            {
                Encoding = Encoding.UTF8,
                LineEnding = LineEndingMode.LF,
                Layout = new SimpleLayout("[${longdate}] [${threadname} /${uppercase:${level:padding=5}}] ${message}"),
                FileName = new SimpleLayout("${basedir}/logs/event.log"),
                ArchiveNumbering = ArchiveNumberingMode.Date,
                ArchiveFileName = new SimpleLayout("${basedir}/logs/{#}-event.log"),
                ArchiveEvery = FileArchivePeriod.Day,
                ArchiveDateFormat = "yyyyMMdd",
                MaxArchiveFiles = 8
            });
            conf.AddRule(LogLevel.Debug, LogLevel.Fatal, "event");

            conf.AddTarget("error", new FileTarget()
            {
                Encoding = Encoding.UTF8,
                LineEnding = LineEndingMode.LF,
                Layout = new SimpleLayout("[${longdate}] [${threadname} /${uppercase:${level:padding=5}}] ${message}"),
                FileName = new SimpleLayout("${basedir}/logs/error.log"),
                ArchiveNumbering = ArchiveNumberingMode.Date,
                ArchiveFileName = new SimpleLayout("${basedir}/logs/{#}-error.log"),
                ArchiveEvery = FileArchivePeriod.Day,
                ArchiveDateFormat = "yyyyMMdd"
            });
            conf.AddRule(LogLevel.Error, LogLevel.Fatal, "error");
        }

        private void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            if (Server.Instance.Status == ServerStatus.Running)
            {
                Server.Instance.Stop();
            }
        }
    }
}