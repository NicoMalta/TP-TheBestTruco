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

        public Partida()
        {
            Jugadores = new List<Jugador>();
        }
        public void RepartirCartas(List<Jugador> jugadores, Mazo mazo)//MODIFICAR
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
    }
}
