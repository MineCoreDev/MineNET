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

            Console.WriteLine(i1.ID + ":" + i1.Damage);//1:0
            Console.WriteLine(i2.ID + ":" + i2.Damage);//1:1
            Console.WriteLine(i3.ID + ":" + i3.Damage);//6:0?
            Console.WriteLine(i4.ID + ":" + i4.Damage);//6:1?
            Console.WriteLine(i5.ID + ":" + i5.Damage);//0:0
            Console.WriteLine(i6.ID + ":" + i6.Damage);//0:1
            Console.WriteLine(i7.ID + ":" + i7.Damage);//5:0
            Console.WriteLine(i8.ID + ":" + i8.Damage);//5:1
            Console.WriteLine(i9.ID + ":" + i9.Damage);//45:0?
            Console.WriteLine(i10.ID + ":" + i10.Damage);//45:1?
            Console.WriteLine(i11.ID + ":" + i11.Damage);//88:0?
            Console.WriteLine(i12.ID + ":" + i12.Damage);//88:1?

        }
    }
}