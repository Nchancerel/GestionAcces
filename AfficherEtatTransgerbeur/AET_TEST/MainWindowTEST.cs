using AfficherEtatTransgerbeur;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace AET_TEST
{
    [TestClass]
    public class MainWindowTEST
    {
        [TestMethod]
        public void TestMethod1()
        {

            double expected = ""; 
            
            Assert.AreEqual(expected, actual, 0.001, "RFID Tag is OK"); 
        }
    }
}
