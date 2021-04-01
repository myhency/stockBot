using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBot.Tests
{
    [TestClass()]
    public class BotTests
    {
        [TestMethod()]
        public void BotTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void printTestTest()
        {
            //given
            string str = "abc";
            var bot = new Bot();
            string expected = bot.printTest();
            Assert.AreEqual(expected, str);
        }
    }
}