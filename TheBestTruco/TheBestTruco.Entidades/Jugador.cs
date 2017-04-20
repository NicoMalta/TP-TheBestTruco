using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBestTruco.Entidades
{
    public class Jugador
    {
        public string Nombre { get; set; }
        public List<Carta> Mano { get; set; }

        public Jugador() { Mano = new List<Carta>(); }

    }
}
