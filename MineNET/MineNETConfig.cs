using System;
using System.IO;

using YamlDotNet.Core;
using YamlDotNet.Serialization;

using MineNET.Utils.Config;

namespace MineNET
{
    public class MineNETConfig : YamlStaticConfig
    {
        private string useAutoUpdate = "false";
        public string UseAutoUpdate
        {
            get
            {
                return useAutoUpdate.ToString();
            }

            set
            {
                this.useAutoUpdate = value;
            }
        }

        private string versionFileURL = "";
        public string VersionFileURL
        {
            get
            {
                return this.versionFileURL;
            }

            set
            {
                this.versionFileURL = value;
            }
        }

        private int loggerBufferSize = 500;
        public int LoggerBufferSize
        {
            get
            {
                return this.loggerBufferSize;
            }

            set
            {
                this.loggerBufferSize = value;
            }
        }
    }
}
