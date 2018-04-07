using System;
using System.Resources;
using System.Windows.Forms;

namespace MineNET.GUI
{
    public static class LangManager
    {
        public static string Language { get; set; } = "ja_JP";

        static ResourceManager manager;
        public static ResourceManager Manager
        {
            get
            {
                if (manager == null)
                {
                    try
                    {
                        manager = new ResourceManager("MineNET.GUI.Resources.Lang." + Language, typeof(LangManager).Assembly);
                    }
                    catch
                    {
                        manager = new ResourceManager("MineNET.GUI.Resources.Lang.ja_JP", typeof(LangManager).Assembly);
                    }
                }

                return manager;
            }

            set
            {
                manager = value;
            }
        }

        public static string GetString(string key)
        {
            string msg = null;
            try
            {
                msg = Manager.GetString(key);
                if (msg != null)
                {
                    return msg;
                }
                throw new NullReferenceException(string.Format(Manager.GetString("app_language_key_error"), key));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), manager.BaseName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                manager = new ResourceManager("MineNET.GUI.Resources.Lang.ja_JP", typeof(LangManager).Assembly);
            }

            msg = Manager.GetString(key);
            if (msg != null)
            {
                return msg;
            }

            return string.Format(Manager.GetString("app_language_key_error"), key);
        }
    }
}
