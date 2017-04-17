using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBestTruco.Entidades
{
    public class Carta
    {
        public Palo Palos { get; set; }
        public int Numero { get; set; }
        public int Valor { get; set; }

        public Carta(Palo palo , int numero)
        {
            Numero = numero;
            Palos = palo;
        }
    }
}
