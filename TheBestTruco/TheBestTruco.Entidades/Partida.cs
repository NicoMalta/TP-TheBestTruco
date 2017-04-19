using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBestTruco.Entidades
{
    public class Partida
    {

        public Mazo GenerarMazo() //Genera las cartas sin 8 y 9 y las agrega al mazo y lo retorna
        {
            Mazo mazo = new Mazo(); //Lo hacemos vacio

            for (int i = 1; i < 13; i++)
            {
                if ((i != 8) && (i != 9))
                {
                    Carta carta = new Carta(Palo.Espada, i);
                    mazo.AgregarCartaAlMazo(carta, mazo);
                }
            }

            for (int i = 1; i < 13; i++)
            {
                if ((i != 8) && (i != 9))
                {
                    Carta carta = new Carta(Palo.Oro, i);
                    mazo.AgregarCartaAlMazo(carta, mazo);
                }
            }

            for (int i = 1; i < 13; i++)
            {
                if ((i != 8) && (i != 9))
                {
                    Carta carta = new Carta(Palo.Copa, i);
                    mazo.AgregarCartaAlMazo(carta, mazo);
                }
            }

            for (int i = 1; i < 13; i++)
            {
                if ((i != 8) && (i != 9))
                {
                    Carta carta = new Carta(Palo.Basto, i);
                    mazo.AgregarCartaAlMazo(carta, mazo);
                }
            }

            return mazo;
        }

        public void RepartirCartas(Jugador Jugador1 ,Jugador Jugador2, Mazo mazo)
        {
            for (int i = 0; i < 3; i++)
            {
                int numero = 0;
                Random random = new Random();
                numero = random.Next(0, mazo.ListaCartas.Count());

                Jugador1.Mano.Add(mazo.ListaCartas[numero]); //Hacer esto en un solo paso
                mazo.ListaCartas.RemoveAt(numero);

            }

            for (int i = 0; i < 3; i++)
            {
                int numero = 0;
                Random random = new Random();
                numero = random.Next(0, mazo.ListaCartas.Count());

                Jugador2.Mano.Add(mazo.ListaCartas[numero]); //Hacer esto en un solo paso
                mazo.ListaCartas.RemoveAt(numero);

            }
        }

        public void VolverCartasMazo(Jugador Jugador1, Jugador Jugador2, Mazo mazo)
        {
            foreach (var item in Jugador1.Mano)
            {
                mazo.AgregarCartaAlMazo(item,mazo);
            }

            foreach (var item in Jugador2.Mano)
            {
                mazo.AgregarCartaAlMazo(item, mazo);
            }
        }
    }
}
