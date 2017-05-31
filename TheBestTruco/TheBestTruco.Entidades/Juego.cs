using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TheBestTruco.Entidades
{
    public enum Palo
    {
        Espada, Basto, Copa, Oro
    }

    class Juego
    {
        public List<Jugador> ListaJugadores { get; set; }
        public bool Completo { get; set; }

        public void CargarJugador()
        {

        }

    }
}
