using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MineNET.Items.Tests
{
    [TestClass()]
    public class ItemTests
    {
        [TestMethod()]
        public void GetTest()
        {
            Item i1 = Item.Get("minecraft:stone");
            Item i2 = Item.Get("minecraft:stone:1");
            Item i3 = Item.Get("minecraft:arrow");
            Item i4 = Item.Get("minecraft:arrow:1");
            Item i5 = Item.Get("minecraft:null");
            Item i6 = Item.Get("minecraft:null:1");
            Item i7 = Item.Get("5");
            Item i8 = Item.Get("5:1");
            Item i9 = Item.Get("301");
            Item i10 = Item.Get("301:1");
            Item i11 = Item.Get("600");
            Item i12 = Item.Get("600:1");

            Console.WriteLine(i1.ItemID + ":" + i1.Damage);//1:0
            Console.WriteLine(i2.ItemID + ":" + i2.Damage);//1:1
            Console.WriteLine(i3.ItemID + ":" + i3.Damage);//6:0?
            Console.WriteLine(i4.ItemID + ":" + i4.Damage);//6:1?
            Console.WriteLine(i5.ItemID + ":" + i5.Damage);//0:0
            Console.WriteLine(i6.ItemID + ":" + i6.Damage);//0:1
            Console.WriteLine(i7.ItemID + ":" + i7.Damage);//5:0
            Console.WriteLine(i8.ItemID + ":" + i8.Damage);//5:1
            Console.WriteLine(i9.ItemID + ":" + i9.Damage);//45:0?
            Console.WriteLine(i10.ItemID + ":" + i10.Damage);//45:1?
            Console.WriteLine(i11.ItemID + ":" + i11.Damage);//88:0?
            Console.WriteLine(i12.ItemID + ":" + i12.Damage);//88:1?

        }
    }
}