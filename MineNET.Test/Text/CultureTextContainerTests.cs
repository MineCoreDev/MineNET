using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineNET.Text;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Text.Tests
{
    [TestClass()]
    public class CultureTextContainerTests
    {
        [TestMethod()]
        public void GetTextTest()
        {
            CultureTextContainer container = new CultureTextContainer("commands_effect_description");
            Console.WriteLine(">" + container.GetText());
        }
    }
}