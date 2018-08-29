using MineNET.Text;
using NLog;

namespace MineNET
{
    public static class Logger
    {
        public static ILogger CurrentLogger { get; } = LogManager.GetCurrentClassLogger();

        public static void Debug(object text)
        {
            CurrentLogger.Debug(text);
        }

        public static void Debug(object text, params object[] args)
        {
            CultureTextContainer textContainer = new CultureTextContainer(text.ToString(), args);
            CurrentLogger.Debug(textContainer.GetText());
        }

        public static void Debug(ITextContainer textContainer)
        {
            CurrentLogger.Debug(textContainer.GetText());
        }

        public static void Info(object text)
        {
            CurrentLogger.Info(text);
        }

        public static void Info(object text, params object[] args)
        {
            CultureTextContainer textContainer = new CultureTextContainer(text.ToString(), args);
            CurrentLogger.Info(textContainer.GetText());
        }

        public static void Info(ITextContainer textContainer)
        {
            CurrentLogger.Info(textContainer.GetText());
        }

        public static void Trace(object text)
        {
            CurrentLogger.Trace(text);
        }

        public static void Trace(object text, params object[] args)
        {
            CultureTextContainer textContainer = new CultureTextContainer(text.ToString(), args);
            CurrentLogger.Trace(textContainer.GetText());
        }

        public static void Trace(ITextContainer textContainer)
        {
            Server.Instance?.Logger?.Trace(textContainer.GetText());
        }

        public static void Warn(object text)
        {
            CurrentLogger.Warn(text);
        }

        public static void Warn(object text, params object[] args)
        {
            CultureTextContainer textContainer = new CultureTextContainer(text.ToString(), args);
            CurrentLogger.Warn(textContainer.GetText());
        }

        public static void Warn(ITextContainer textContainer)
        {
            Server.Instance?.Logger?.Warn(textContainer.GetText());
        }

        public static void Error(object text)
        {
            CurrentLogger.Error(text);
        }

        public static void Error(object text, params object[] args)
        {
            CultureTextContainer textContainer = new CultureTextContainer(text.ToString(), args);
            CurrentLogger.Error(textContainer.GetText());
        }

        public static void Error(ITextContainer textContainer)
        {
            Server.Instance?.Logger?.Error(textContainer.GetText());
        }

        public static void Fatal(object text)
        {
            CurrentLogger.Fatal(text);
        }

        public static void Fatal(object text, params object[] args)
        {
            CultureTextContainer textContainer = new CultureTextContainer(text.ToString(), args);
            CurrentLogger.Fatal(textContainer.GetText());
        }

        public static void Fatal(ITextContainer textContainer)
        {
            Server.Instance?.Logger?.Fatal(textContainer.GetText());
        }
    }
}
