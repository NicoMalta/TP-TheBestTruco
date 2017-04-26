using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBestTruco.Entidades
{
    public class Carta
    {
        public Carta(Palo palo, int numero)
        {
            Numero = numero;
            Palos = palo;
            Valor = ValorCarta(this.Palos, this.Numero);
        }

        public Palo Palos { get; }
        public int Numero { get; }
        public int Valor { get; }

        private int ValorCarta(Palo palo, int numero)
        {

            bool normal = false;
         
            if (numero == 1 && palo == Palo.Espada)
            {
                normal = true;
                return 100;
            }
            if (numero == 1 && palo == Palo.Basto)
            {
                normal = true;
                return 90;
            }
            if (numero == 7 && palo == Palo.Espada)
            {
                normal = true;
                return 80;
            }
            if (numero == 7 && palo == Palo.Oro)
            {
                normal = true;
                return 70;
            }
            if (numero == 3)
            {
                normal = true;
                return 60;
            }
            if (numero == 2)
            {
                normal = true;
                return 50;
            }
            if ((numero == 1 && palo == Palo.Copa) || (numero == 1 && palo == Palo.Oro))
            {
                normal = true;
                return 40;
            }

            if (normal == false)
            {
                return numero;
            }
            else
            {
                return 0;
            }

        }
    }
}
