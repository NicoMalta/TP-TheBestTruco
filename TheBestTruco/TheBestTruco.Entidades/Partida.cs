using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBestTruco.Entidades
{
    public enum Equipos
    {
        Equipo1, Equipo2
    }
    public class Partida
    {
        public List<Jugador> Jugadores { get; set; }
        public Mazo Mazo { get; set; }
        public bool EstaCompleto { get; set; }
        public int Puntaje1 { get; set; }
        public int Puntaje2 { get; set; }
        public int EsMano { get; set; }

        public Partida()
        {
            Jugadores = new List<Jugador>();
            Mazo = new Mazo();
            Puntaje1 = 0;
            Puntaje2 = 0;
            EsMano = 2;
        }

        public void RevisarCantidadJugadores()
        {
            if (Jugadores.Count == 4)
            {
                this.EstaCompleto = true;
            }

        }

        public void RepartirCartas(List<Jugador> jugadores, Mazo mazo)
        {
            mazo.MezclarMazo(new Mazo());
            int CartasRepartidas = 0, indice = 0;
            int ultima = 39;

            while (CartasRepartidas != (jugadores.Count * 3))
            {
                if (indice == jugadores.Count)
                {
                    indice = 0;
                }

                jugadores[indice].Mano.Add(mazo.ListaCartas[ultima]);
                indice++;
                ultima--;
                CartasRepartidas++;

            }
        }

       

        //public void JugarCarta(int jugador, Carta cartaSeleccionada)
        //{
        //    foreach (var item in Jugadores)
        //    {
        //        if (item.Numero == jugador)
        //        {
        //            CartasMesa[jugador, Turno] = cartaSeleccionada;
        //            item.Mano.Remove(cartaSeleccionada);

        //        }
        //    }

        //}
    }
}
