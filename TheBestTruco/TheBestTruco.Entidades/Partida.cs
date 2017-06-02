using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBestTruco.Entidades
{
    public class Partida
    {
        public List<Jugador> Jugadores { get; set; }
        public RespuestasEnvido EstadoEnvido { get; set; }
        public int[,] CartasMesa { get; set; }
        public int Turno { get; set; }
        public Mazo  Mazo { get; set; }
        public bool EstaCompleto { get; set; }

        public Partida()
        {
            Jugadores = new List<Jugador>();
            CartasMesa = new int[3, 4];
            Mazo = new Mazo();
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

        public void VolverCartasMazo(List<Jugador> jugadores, Mazo mazo)
        {
            foreach (var item in jugadores)
            {
                foreach (var item2 in item.Mano)
                {
                    mazo.AgregarCartaAlMazo(item2,mazo.ListaCartas);
                    item.Mano.Remove(item2);
                }
            }
           
        }

        public void JugarCarta(int jugador, Carta cartaSeleccionada)
        {
            foreach (var item in Jugadores)
            {
                if (item.Numero == jugador)
                {
                    CartasMesa[jugador, Turno] = cartaSeleccionada.Valor;
                    item.Mano.Remove(cartaSeleccionada);

                }
            }
           
        }
    }
}
