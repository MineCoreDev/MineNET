using System;

namespace MineNET.Plugins
{
    public abstract class PluginBase : IPlugin
    {
        public string Directory
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string FileName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool Loaded
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Init(PluginAttribute plugin)
        {
            throw new NotImplementedException();
        }

        public void OnDisable()
        {
            throw new NotImplementedException();
        }

        public void OnEnable()
        {
            throw new NotImplementedException();
        }

        public void OnLoad()
        {
            throw new NotImplementedException();
        }

        public void OnUnLoad()
        {
            throw new NotImplementedException();
        }
    }
}
