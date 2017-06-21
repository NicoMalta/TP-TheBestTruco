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
        public List<Ronda> Rondas { get; set; }

        public Partida()
        {
            Rondas = new List<Ronda>();
            Jugadores = new List<Jugador>();
            Mazo = new Mazo();
            Puntaje1 = 0;
            Puntaje2 = 0;
        }

        public void RevisarCantidadJugadores()
        {
            if (Jugadores.Count == 4)
            {
                this.EstaCompleto = true;
            }

        }

        public void MeVoyAlMazo(Jugador jugador, bool CantoAlgo)
        {
            if ((jugador.Equipo == Equipos.Equipo1) && (CantoAlgo == true))
            {
                this.Puntaje1++;
            }
            if ((jugador.Equipo == Equipos.Equipo2) && (CantoAlgo == true))
            {
                this.Puntaje2++;
            }
            if ((jugador.Equipo == Equipos.Equipo1) && (CantoAlgo == false))
            {
                this.Puntaje1++;
            }
            if ((jugador.Equipo == Equipos.Equipo2) && (CantoAlgo == false))
            {
                this.Puntaje2++;
            }
        }


        public void RepartirCartas(List<Jugador> jugadores, Mazo mazo)
        {
            foreach(var x in jugadores)
            {
                foreach(var c in x.Mano)
                {
                    mazo.ListaCartas.Add(c);                    
                }

                x.Mano.Clear();
            }

            mazo.MezclarMazo(mazo);
            int CartasRepartidas = 0, indice = 0;
            int ultima = 39;

            while (CartasRepartidas != (jugadores.Count * 3))
            {
                if (indice == jugadores.Count)
                {
                    indice = 0;
                }

                jugadores[indice].Mano.Add(mazo.ListaCartas[ultima]);
                this.Mazo.ListaCartas.Remove(mazo.ListaCartas[ultima]);
                indice++;
                ultima--;
                CartasRepartidas++;

            }
        }

       
        public int QuienEmpieza(int rondas)
        {
            if ((rondas + 1) % 4 == 0)
            {
                return 4;
            }
            return (rondas + 1) % 4;
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
