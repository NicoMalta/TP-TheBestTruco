using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheBestTruco.Entidades;
using System.Collections.Generic;

namespace TestTruco
{
    [TestClass]
    public class TMazo
    {
        [TestMethod]
        public void TestMethod1()
        {
            Partida partida = new Partida();

            var total = partida.GenerarMazo();
            Console.WriteLine(total);
            //Assert.AreEqual(total);
        }
    }
}
