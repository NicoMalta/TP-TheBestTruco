using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBestTruco.Entidades
{

    public class Mazo
    {
        public List<Carta> ListaCartas { get; set; }

        public  Mazo()
        {
            ListaCartas = new List<Carta>();

            for (int i = 1; i < 13; i++)
            {
                if ((i != 8) && (i != 9))
                {
                    Carta carta = new Carta(Palo.Espada, i);
                    AgregarCartaAlMazo(carta, this.ListaCartas);
                }
            }

            for (int i = 1; i < 13; i++)
            {
                if ((i != 8) && (i != 9))
                {
                    Carta carta = new Carta(Palo.Oro, i);
                    AgregarCartaAlMazo(carta, this.ListaCartas);
                }
            }

            for (int i = 1; i < 13; i++)
            {
                if ((i != 8) && (i != 9))
                {
                    Carta carta = new Carta(Palo.Copa, i);
                    AgregarCartaAlMazo(carta, this.ListaCartas);
                }
            }

            for (int i = 1; i < 13; i++)
            {
                if ((i != 8) && (i != 9))
                {
                    Carta carta = new Carta(Palo.Basto, i);
                    AgregarCartaAlMazo(carta, this.ListaCartas);
                }
            }

        }

        public Mazo MezclarMazo() //Genera las cartas sin 8 y 9 y las agrega al mazo y lo retorna
        {
            Mazo mazo = new Mazo();

            for (int i = 0; i < 2; i++)
            {
                List<Carta> Aux = new List<Carta>();
                Random randNum = new Random();
                while (mazo.ListaCartas.Count > 0)
                {
                    int val = randNum.Next(0, mazo.ListaCartas.Count - 1);
                    Aux.Add(mazo.ListaCartas[val]);
                    mazo.ListaCartas.RemoveAt(val);
                }
                mazo.ListaCartas = Aux;
            }

            return mazo;
        }

        public void AgregarCartaAlMazo(Carta carta, List<Carta> mazo)
        {
            var encontrado = false;
            foreach (var item in mazo )
            {
                if (item.Numero == carta.Numero && item.Palos == carta.Palos )
                {
                    encontrado = true;
                }
            }
            if (!encontrado)
            {
                mazo.Add(carta);
            }
        } 
    }
}
