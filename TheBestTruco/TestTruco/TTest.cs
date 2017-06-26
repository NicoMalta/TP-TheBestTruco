using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheBestTruco.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestTruco
{
    [TestClass]
    public class TTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Partida partida = new Partida();

            //Jugador jug = new Jugador();
            //jug.Equipo = 1;

            //Jugador juge = new Jugador();
            //juge.Equipo = 2;

            //partida.Jugadores.Add(jug);
            //partida.Jugadores.Add(juge);

            //partida.RepartirCartas(partida.Jugadores, partida.Mazo);

            //Puntuacion puntuacion = new Puntuacion();
            //jug.SolicitarEnvido(1, partida.Jugadores, puntuacion, jug.ElegirValor(jug.PosiblesRespuestas(RespuestasEnvido.Envido)));

        }

        [TestMethod]
        public void TestMethod2()
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
            partida.RepartirCartas(partida.Jugadores, partida.Mazo);

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
            partida.RepartirCartas(partida.Jugadores, partida.Mazo);

            var CantPuntos = 0;

            CantPuntos = partida.Jugadores[0].ContadorEnvido(partida.Jugadores[0].Mano);


            if (CantPuntos > 0)
            {
                CantPuntos = -20;
            }

            Assert.AreEqual(-20, CantPuntos);
        }

        [TestMethod]
        public void Quienempiezaprimero()
        {
            var partida = new Partida();

            int x = partida.QuienEmpieza(4);

            Assert.AreEqual(0, x);
        }
        [TestMethod]

        public void IrAlMazo()
        {
            var partida = new Partida();
            partida.Jugadores.Add(new Jugador { Nombre = "jugador1", Equipo = Equipos.Equipo1 });
            partida.MeVoyAlMazo(partida.Jugadores[0], true);

            Assert.AreEqual(1, partida.Puntaje1);
        }

        [TestMethod]

        public void RevisarCantidadJugadores()
        {
            var partida = new Partida();
            partida.Jugadores.Add(new Jugador { Nombre = "jugador1" });
            partida.Jugadores.Add(new Jugador { Nombre = "jugador2" });
            partida.Jugadores.Add(new Jugador { Nombre = "jugador3" });
            partida.Jugadores.Add(new Jugador { Nombre = "jugador4" });
            partida.RevisarCantidadJugadores();

            Assert.AreEqual(4, partida.Jugadores.Count);
        }

        [TestMethod]

        public void GanaMano()
        {
            var ronda = new Ronda();
            var partida = new Partida();

            partida.Jugadores.Add(new Jugador { Nombre = "jugador1" });
            partida.Jugadores.Add(new Jugador { Nombre = "jugador2" });
            partida.Jugadores.Add(new Jugador { Nombre = "jugador3" });
            partida.Jugadores.Add(new Jugador { Nombre = "jugador4" });
            partida.RepartirCartas(partida.Jugadores, new Mazo());
            ronda.GanaMano(partida.Jugadores);

        }

        [TestMethod]

        public void RevisarParda()
        {
            var ronda = new Ronda();
            var partida = new Partida();

            partida.Jugadores.Add(new Jugador { Nombre = "jugador1" });
            partida.Jugadores.Add(new Jugador { Nombre = "jugador2" });
            partida.Jugadores.Add(new Jugador { Nombre = "jugador3" });
            partida.Jugadores.Add(new Jugador { Nombre = "jugador4" });
            partida.RepartirCartas(partida.Jugadores, new Mazo());
            foreach (var item in partida.Jugadores)
            {
                ronda.CartasMesa[0, item.Numero - 1] = item.Mano[0];
            }

            ronda.RevisarPardas(0);

        }
    }
}
