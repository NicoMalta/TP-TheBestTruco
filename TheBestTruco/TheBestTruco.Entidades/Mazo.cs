using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBestTruco.Entidades
{
    class Mazo
    {
        private List<Carta> ListaCartas { get; set; }

        Mazo()
        {
            new List<Carta>();
        }

        public Mazo GenerarMazo() //Genera las cartas sin 8 y 9 y las agrega al mazo y lo retorna
        {
            Mazo mazo = new Mazo(); //Lo hacemos vacio

            for (int i = 0; i < 12; i++)
            {
                if (i != 8 || i != 9)
                {
                    Carta carta = new Carta("Espada", i);
                    mazo.AgregarCartaAlMazo(carta,mazo);
                }
            }

            for (int i = 0; i < 12; i++)
            {
                if (i != 8 || i != 9)
                {
                    Carta carta = new Carta("Oro", i);
                    mazo.AgregarCartaAlMazo(carta, mazo);
                }
            }

            for (int i = 0; i < 12; i++)
            {
                if (i != 8 || i != 9)
                {
                    Carta carta = new Carta("Copa", i);
                    mazo.AgregarCartaAlMazo(carta, mazo);
                }
            }

            for (int i = 0; i < 12; i++)
            {
                if (i != 8 || i != 9)
                {
                    Carta carta = new Carta("Basto", i);
                    mazo.AgregarCartaAlMazo(carta, mazo);
                }
            }
            return mazo;
        }
        
        public void AgregarCartaAlMazo(Carta carta, Mazo mazo)
        {
            int n = 0;
            foreach (var item in mazo.ListaCartas )
            {
                if (item.Numero == carta.Numero && item.Palo == carta.Palo )
                {
                    n = 1;
                }
            }
            if (n == 0)
            {
                mazo.ListaCartas.Add(carta);
            }
        } 
    }
}
