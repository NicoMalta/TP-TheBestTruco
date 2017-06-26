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
        public bool BuenasEquipo1 { get; set; }
        public bool BuenasEquipo2 { get; set; }
        public List<Ronda> Rondas { get; set; }

        public Partida()
        {
            BuenasEquipo1 = false;
            BuenasEquipo2 = false;
            Rondas = new List<Ronda>();
            Jugadores = new List<Jugador>();
            Mazo = new Mazo();
            Puntaje1 = 0;
            Puntaje2 = 0;
        }

        public void RevisarBuenas()
        {
            if (Puntaje1 > 15)
            {
                if (BuenasEquipo1 == false)
                {
                    this.Puntaje1 = 15 - this.Puntaje1;
                }
                this.BuenasEquipo1 = true;
            }
            if (Puntaje2 > 15)
            {
                if (BuenasEquipo2 == false)
                {
                    this.Puntaje2 = 15 - this.Puntaje2;
                }
                this.BuenasEquipo2 = true;
            }
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
                this.Puntaje1++;
            }
            if ((jugador.Equipo == Equipos.Equipo2) && (CantoAlgo == false))
            {
                this.Puntaje2++;
                this.Puntaje2++;
            }
            RevisarBuenas();
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

        public Equipos GanadorEnvido(List<int> Puntos , List<Jugador> Jugadores)
        {
            var ganador = Puntos.Max();

            for (int i = 3; i < -1; i--)
            {
                if (Jugadores[i].ContadorEnvido(Jugadores[i].Mano) == ganador)
                {
                    return Jugadores[i].Equipo;
                }
            }
            return Equipos.Equipo1;
        }
    }
}
