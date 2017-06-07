using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBestTruco.Entidades
{
    public enum Equipos
    {
        Equipo1 , Equipo2
    }
    public class Partida
    {
        public List<Jugador> Jugadores { get; set; }
        public RespuestasEnvido EstadoEnvido { get; set; }
        public Carta[,] CartasMesa { get; set; }
        public int Turno { get; set; }
        public Mazo  Mazo { get; set; }
        public bool EstaCompleto { get; set; }
        public bool TrucoActivo { get; set; }
        public bool PardaActivo { get; set; }
        public int Puntaje1 { get; set; }
        public int Puntaje2 { get; set; }

        public Partida()
        {
            Jugadores = new List<Jugador>();
            CartasMesa = new Carta[3, 4];
            Mazo = new Mazo();
            Puntaje1 = 0;
            Puntaje2 = 0;
            Turno = 1;
        }

        public void RevisarCantidadJugadores()
        {
            if (Jugadores.Count == 4)
            {
                this.EstaCompleto = true;
            }
            
        }

        public void RepartirCartas(List<Jugador> jugadores, Mazo mazo)
        {
            int CartasRepartidas = 0, indice = 0;
            int ultima = 39;

            while (CartasRepartidas != (jugadores.Count * 3))
            {
                if (indice == jugadores.Count)
                {
                    indice = 0;
                }

                jugadores[indice].Mano.Add(mazo.ListaCartas[ultima]);
                indice++;
                ultima--;
                CartasRepartidas++;

            }
        }

        public void Truco(int Turno, RespuestasTruco tipotruco, QuiereoNo respuesta, Equipos equipo)
        {
            bool primeravez = false;
            int valor = 0;
            int Jugador = 0;
            for (int i = 0; i < 4; i++)
            {
                if (primeravez == false)
                {
                    primeravez = true;
                    valor = CartasMesa[Turno, i].Valor;
                    Jugador = i;
                }
                else if (valor < CartasMesa[Turno, i].Valor)
                {
                    valor = CartasMesa[Turno, i].Valor;
                    Jugador = i;
                }
            }
            Equipos equipoganador = Jugadores[Jugador].Equipo;
            switch (tipotruco)
            {
                case RespuestasTruco.Truco:
                    if (respuesta == QuiereoNo.NoQuiero)
                    {
                        if (equipo == Equipos.Equipo1)
                        {
                            Puntaje1 += 1;
                        }
                        else
                        {
                            Puntaje2 += 1;
                        }
                    }
                    else
                    {
                        if (equipoganador == Equipos.Equipo1)
                        {
                            Puntaje1 += 2;
                        }
                        else
                        {
                            Puntaje2 += 2;
                        }
                    }
                    break;
                case RespuestasTruco.Retruco:
                    if (respuesta == QuiereoNo.NoQuiero)
                    {
                        if (equipo == Equipos.Equipo1)
                        {
                            Puntaje1 += 2;
                        }
                        else
                        {
                            Puntaje2 += 2;
                        }
                    }
                    else
                    {
                        if (equipoganador == Equipos.Equipo1)
                        {
                            Puntaje1 += 3;
                        }
                        else
                        {
                            Puntaje2 += 3;
                        }
                    }
                    break;
                case RespuestasTruco.QuieroVale4:
                    if (respuesta == QuiereoNo.NoQuiero)
                    {
                        if (equipo == Equipos.Equipo1)
                        {
                            Puntaje1 += 3;
                        }
                        else
                        {
                            Puntaje2 += 3;
                        }
                    }
                    else
                    {
                        if (equipoganador == Equipos.Equipo1)
                        {
                            Puntaje1 += 4;
                        }
                        else
                        {
                            Puntaje2 += 4;
                        }
                    }
                    break;
            }


        }
        public void VolverCartasMazo(List<Jugador> jugadores, Mazo mazo)
        {
            foreach (var item in jugadores)
            {
                foreach (var item2 in item.Mano)
                {
                    mazo.AgregarCartaAlMazo(item2,mazo.ListaCartas);
                    item.Mano.Remove(item2);
                }
            }
           
        }

        public bool RevisarPardas()
        {
            this.PardaActivo = false;
            bool primeravez = false;
            int valor = 0;
            for (int i = 0; i < 4; i++)
            {
                if (primeravez == false)
                {
                    primeravez = true;
                    valor = CartasMesa[Turno, i].Valor;
                }
                else if (valor < CartasMesa[Turno, i].Valor)
                {
                    valor = CartasMesa[Turno, i].Valor;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (valor == CartasMesa[Turno, i].Valor)
                {
                    this.PardaActivo = true;
                    break;
                }
            }

           return this.PardaActivo;
        }

        public void JugarCarta(int jugador, Carta cartaSeleccionada)
        {
            foreach (var item in Jugadores)
            {
                if (item.Numero == jugador)
                {
                    CartasMesa[jugador, Turno] = cartaSeleccionada;
                    item.Mano.Remove(cartaSeleccionada);

                }
            }
           
        }
    }
}
