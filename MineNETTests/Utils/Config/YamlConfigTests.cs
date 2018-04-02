using System;
using System.Collections.Generic;
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
            List<User> users = new List<User>();
            try
            {
                users.Add(new User() { Name = "tom", Password = "1234" });
                users.Add(new User() { Name = "hiroki", Password = "0625" });
                conf.Root.Add("key", "value");
                conf.Root.Add("int", 10000);
                conf.Root.Add("users", users);
                conf.Save();
            }
            catch
            {

            }
        }
    }


    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}