using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheBestTruco.Entidades;

namespace TestTruco
{
   
    [TestClass]
    public class TestEnvido
    {

        [TestMethod]
        public void TestMethod1()
        {
            Jugador jugador = new Jugador();

            jugador.PosiblesRespuestas(RespuestasEnvido.RealEnvido);

        }
    }
}
