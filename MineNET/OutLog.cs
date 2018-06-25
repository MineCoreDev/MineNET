namespace MineNET
{
    public static class OutLog
    {
        public static void Log(object text)
        {
            Server.Instance?.Logger?.Log(text);
        }

        public static void Log(object text, params object[] args)
        {
            Server.Instance?.Logger?.Log(text, args);
        }

        public static void Info(object text)
        {
            Server.Instance?.Logger?.Info(text);
        }

        public static void Info(object text, params object[] args)
        {
            Server.Instance?.Logger?.Info(text, args);
        }

        public static void Notice(object text)
        {
            Server.Instance?.Logger?.Notice(text);
        }

        public static void Notice(object text, params object[] args)
        {
            Server.Instance?.Logger?.Notice(text, args);
        }

        public static void Warning(object text)
        {
            Server.Instance?.Logger?.Warning(text);
        }

        public static void Warning(object text, params object[] args)
        {
            Server.Instance?.Logger?.Warning(text, args);
        }

        public static void Error(object text)
        {
            Server.Instance?.Logger?.Error(text);
        }

        public static void Error(object text, params object[] args)
        {
            Server.Instance?.Logger?.Error(text, args);
        }

        public static void Fatal(object text)
        {
            Server.Instance?.Logger?.Fatal(text);
        }

        public static void Fatal(object text, params object[] args)
        {
            Server.Instance?.Logger?.Fatal(text, args);
        }
    }
}
