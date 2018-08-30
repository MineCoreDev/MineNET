using MineNET.Text;
using NLog;
using NLog.Config;
using System;

namespace MineNET.IO
{
    public class Logger : IServerLogger
    {
        public static Logger CurrentLogger { get; private set; }

        public ILogger OutputLogger { get; }
        public InputInterface InputLogger { get; }

        public Logger()
        {
            LogManager.Configuration = new XmlLoggingConfiguration(Environment.CurrentDirectory + "\\NLog.config");
            this.OutputLogger = LogManager.GetCurrentClassLogger();
            this.InputLogger = new Input();
            CurrentLogger = this;
            try
            {
                Console.Title = "MineNET";
                Console.OpenStandardInput();
            }
            catch (InvalidOperationException)
            {

            }
        }

        public static void Debug(object text)
        {
            CurrentLogger.OutputLogger.Debug(CheckLocalizationFormat(text, null));
        }

        public static void Debug(object text, params object[] args)
        {
            CurrentLogger.OutputLogger.Debug(CheckLocalizationFormat(text, args));
        }

        public static void Debug(ITextContainer textContainer)
        {
            CurrentLogger.OutputLogger.Debug(textContainer.GetText());
        }

        public static void Info(object text)
        {
            CurrentLogger.OutputLogger.Info(CheckLocalizationFormat(text, null));
        }

        public static void Info(object text, params object[] args)
        {
            CurrentLogger.OutputLogger.Info(CheckLocalizationFormat(text, args));
        }

        public static void Info(ITextContainer textContainer)
        {
            CurrentLogger.OutputLogger.Info(textContainer.GetText());
        }

        public static void Trace(object text)
        {
            CurrentLogger.OutputLogger.Trace(CheckLocalizationFormat(text, null));
        }

        public static void Trace(object text, params object[] args)
        {
            CurrentLogger.OutputLogger.Trace(CheckLocalizationFormat(text, args));
        }

        public static void Trace(ITextContainer textContainer)
        {
            CurrentLogger.OutputLogger.Trace(textContainer.GetText());
        }

        public static void Warn(object text)
        {
            CurrentLogger.OutputLogger.Warn(CheckLocalizationFormat(text, null));
        }

        public static void Warn(object text, params object[] args)
        {
            CurrentLogger.OutputLogger.Warn(CheckLocalizationFormat(text, args));
        }

        public static void Warn(ITextContainer textContainer)
        {
            CurrentLogger.OutputLogger.Warn(textContainer.GetText());
        }

        public static void Error(object text)
        {
            CurrentLogger.OutputLogger.Error(CheckLocalizationFormat(text, null));
        }

        public static void Error(object text, params object[] args)
        {
            CurrentLogger.OutputLogger.Error(CheckLocalizationFormat(text, args));
        }

        public static void Error(ITextContainer textContainer)
        {
            CurrentLogger.OutputLogger.Error(textContainer.GetText());
        }

        public static void Fatal(object text)
        {
            CurrentLogger.OutputLogger.Fatal(CheckLocalizationFormat(text, null));
        }

        public static void Fatal(object text, params object[] args)
        {
            CurrentLogger.OutputLogger.Fatal(CheckLocalizationFormat(text, args));
        }

        public static void Fatal(ITextContainer textContainer)
        {
            CurrentLogger.OutputLogger.Fatal(textContainer.GetText());
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
    }
}
