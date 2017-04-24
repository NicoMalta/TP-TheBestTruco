using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBestTruco.Entidades
{
    public class Carta
    {
        public Palo Palos { get;  }
        public int Numero { get; }
        public int Valor { get; }

        public Carta(Palo palo , int numero)
        {
            Numero = numero;
            Palos = palo;
            Valor = ValorCarta(this.Palos, this.Numero);
        }

        private int ValorCarta(Palo palo , int numero)
        {

            switch (palo )
            {
                default:
                    break;
            }

            return Valor;

        } 
    }
}
