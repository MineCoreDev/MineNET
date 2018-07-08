using MineNET.GUI.Data;
using MineNET.GUI.UI.Forms;
using MineNET.GUI.UI.Modals;
using MineNET.Utils.Config;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace MineNET.GUI
{
    public class App
    {
        #region Static
        public static App Instance { get; private set; }
        public static void CreateInstance(MainForm form)
        {
            if (App.Instance == null)
            {
                App.Instance = new App(form);
            }
        }

        public static string ExecutePath { get; } = Environment.CurrentDirectory;
        public static Language[] SupportLanguage { get; } = new Language[]
        {
            new Language("日本語", "ja_JP")
        };
        #endregion

        #region Property & Field
        public MainForm MainForm { get; }

        public MineNETGUIConfig Config { get; private set; }
        #endregion

        #region Ctor
        private App(MainForm form)
        {
            this.MainForm = form;
        }
        #endregion

        public void LoadLanguage()
        {
            string file = $"{ExecutePath}\\MineNET.yml";
            MineNETConfig config = null;
            if (!File.Exists(file))
            {
                config = YamlStaticConfig.Load<MineNETConfig>(file);
                LanguageSelectModal modal = new LanguageSelectModal();
                modal.ShowDialog();
                if (modal.DialogResult == DialogResult.OK)
                {
                    config.Language = modal.SelectResult.Code;
                    config.Save<MineNETConfig>();
                }
            }
            else
            {
                config = YamlStaticConfig.Load<MineNETConfig>(file);
            }
            string language = config.Language;
            LanguageService.LangCode = language;
        }

        public void LoadConfig()
        {
            string file = $"{ExecutePath}\\MineNET_GUI.yml";
            this.Config = YamlStaticConfig.Load<MineNETGUIConfig>(file);
        }

        public async void CheckVersion()
        {
            WebClient client = new WebClient();
            try
            {
                string newVersion = await client.DownloadStringTaskAsync("https://raw.githubusercontent.com/MineNETDevelopmentGroup/MineNET/master/MineNET.GUI/VERSION");
                string version = this.GetType().Assembly.GetName().Version.ToString();
                if (version != newVersion)
                {
                    MessageBox.Show(LanguageService.GetString("app.loadForm.statusLabel.checkVersion.newVersion"));
                    Process.Start("https://github.com/MineNETDevelopmentGroup/MineNET/releases");
                }
            }
            catch (WebException e)
            {
                MessageBox.Show(e.Message + Environment.NewLine + LanguageService.GetString("app.loadForm.statusLabel.checkVersion.error"));
            }
        }
    }
}
