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
            EsMano = 1;
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

        public string TirarReyes(Mazo mazo)
        {
            bool bandera = true;

            Carta[,] CartasReyes = new Carta[4, 15];

            int x = 1;

            int b = 1;

            int c = 0;

            string palabra = "";

            while (bandera == true)
            {
                if (x == 5)
                {
                    x = 1;
                    b++;
                }

                Random aleatorio = new Random(mazo.ListaCartas.Count);

                CartasReyes[x,b] = mazo.ListaCartas[Convert.ToInt32(aleatorio)];

                mazo.ListaCartas.Remove(mazo.ListaCartas[Convert.ToInt32(aleatorio)]);

                if (mazo.ListaCartas[Convert.ToInt32(aleatorio)].Numero == 12)
                {
                    c++;
                }


                for (int i = 1; i < b; i++)
                {
                    if (CartasReyes[x,i].Numero == 12)
                    {
                        palabra = palabra + "Equipo1:Jugador(" + x + ")";
                        x++;
                        break;
                    }
                }

                if (x == 5)
                {
                    x = 1;
                    b++;
                }

                if (c == 2)
                {
                    bandera = false;
                }

                x++;
            }

            int salir = 1;

            while (salir == 4)
            {
                for (int i = 1; i < 16; i++)
                {
                    if (CartasReyes[salir, i] != null)
                    {
                        mazo.ListaCartas.Add(CartasReyes[salir, i]);
                    }
                }
                salir++;
            }
            return palabra;
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
