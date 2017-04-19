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


    }
}
