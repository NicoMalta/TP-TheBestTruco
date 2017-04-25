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

            bool normal = false;

            if (numero == 1 && palo == Palo.Espada)
            {
                return 100;
                normal = true;
            }

            if (numero == 1 && palo == Palo.Basto)
            {
                return 90;
                normal = true;
            }

            if (numero == 7 && palo == Palo.Espada)
            {
                return 80;
                normal = true;
            }

            if (numero == 7 && palo == Palo.Oro)
            {
                return 70;
                normal = true;
            }

            if (numero == 3)
            {
                return 60;
                normal = true;
            }

            if (numero == 2)
            {
                return 50;
                normal = true;
            }

            if ((numero == 1 && palo == Palo.Copa) || (numero == 1 && palo == Palo.Oro)) ;
            {
                return 40;
                normal = true;
            }

            if (normal == false)
            {
                return numero;
            }


        } 
    }
}
