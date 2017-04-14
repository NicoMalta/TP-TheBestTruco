using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBestTruco.Entidades
{
    public class Carta
    {
        public string Palo { get; set; }
        public int Numero { get; set; }
        public int Valor { get; set; }

        public Carta(string palo , int numero)
        {
            Numero = numero;
            Palo = palo;
        }
    }
}
