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

            Mazo mazo = new Mazo();
           
         
            //Console.WriteLine(total);
            //Assert.AreEqual(total);
        }

        [TestMethod]
        public void Probarcartas()
        {
            Partida partida = new Partida();

          //  partida.RepartirCartas(new Jugador { Nombre = "Nico" }, new Jugador { Nombre = "Martin" }, partida.GenerarMazo());
            //Console.WriteLine("ola");

            //Assert.AreEqual(3, total);
        }

        [TestMethod]
        public void RepartirCartas()
        {
            Partida partida = new Partida();
            Mazo mazo = new Mazo();
            partida.Jugadores.Add(new Jugador { Nombre = "jugador1" });
            partida.Jugadores.Add(new Jugador { Nombre = "jugador2" });
            partida.RepartirCartas(partida.Jugadores, mazo.MezclarMazo());

            var CantCartas = 0;

            foreach (var item in partida.Jugadores)
            {
                CantCartas = CantCartas + item.Mano.Count;
            }
            

            Assert.AreEqual(6, CantCartas);
        }

        [TestMethod]
        public void ProvarEnvido()
        {
            Partida partida = new Partida();
            Mazo mazo = new Mazo();
            partida.Jugadores.Add(new Jugador { Nombre = "jugador1" });
            partida.RepartirCartas(partida.Jugadores, mazo.MezclarMazo());

            var CantPuntos = 0;

            CantPuntos = partida.Jugadores[0].ContadorEnvido(partida.Jugadores[0].Mano);


            if (CantPuntos > 0)
            {
                CantPuntos = -20;
            }

            Assert.AreEqual(-20, CantPuntos);
        }
    }
}
