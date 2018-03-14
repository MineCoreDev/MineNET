using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MineNET.Utils.Config.Tests
{
    [TestClass()]
    public class YamlConfigTests
    {
        [TestMethod()]
        public void LoadSaveTest()
        {
            string path = Environment.CurrentDirectory + "\\TestConfig.yml";
            YamlConfig conf = YamlConfig.Load(Environment.CurrentDirectory + "\\TestConfig.yml");
            if (!File.Exists(path))
            {
                List<User> users = new List<User>();
                users.Add(new User() { Name = "tom", Password = "1234" });
                users.Add(new User() { Name = "hiroki", Password = "0625" });
                conf.Datas.Add("key", "value");
                conf.Datas.Add("int", 10000);
                conf.Datas.Add("users", users);
                conf.Save();
            }
        }
    }

    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}