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

        public Mazo()
        {
            ListaCartas = new List<Carta>();
        }

     
        
        public void AgregarCartaAlMazo(Carta carta, Mazo mazo)
        {
            int n = 0;
            foreach (var item in mazo.ListaCartas )
            {
                if (item.Numero == carta.Numero && item.Palos == carta.Palos )
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
