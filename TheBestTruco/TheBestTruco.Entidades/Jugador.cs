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
        public int Equipo { get; set; }

        public Jugador() { Mano = new List<Carta>(); }

        public void SolicitarEnvido(List<Jugador> Jugadores,Puntuacion puntaje)
        {
            if (true)
            {

            }
            else
            {
                if (this.Equipo == 1)
                {
                    puntaje.Equipo1++;
                }
                else
                {
                    puntaje.Equipo2++;
                }
            }
        }

        public void SolicitarTruco()
        {

        }
    }
}
