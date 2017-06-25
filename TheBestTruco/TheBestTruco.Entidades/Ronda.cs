using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBestTruco.Entidades
{
    public class Mayor
    {
        public int Valor { get; set; }
        public int Posicion { get; set; }
    }
    public class Ronda
    {
        public RespuestasEnvido EstadoEnvido { get; set; }
        public Carta[,] CartasMesa { get; set; }
        public int Turno { get; set; }
        public bool TrucoActivo { get; set; }
        public bool PardaActivo { get; set; }
        public bool FinPartida { get; set; }
        public int Puntaje1 { get; set; }
        public int Puntaje2 { get; set; }
        public int Manos { get; set; }
        public bool CantoAlgo { get; set; }
        public int Envido { get; set; }
        public Equipos EquipoCantoEnvido { get; set; }
        public Ronda()
        {
            CartasMesa = new Carta[3, 4];
            Turno = 1;
            Manos = 1;
            CantoAlgo = false;
            Envido = 0;
        }
        public Equipos CantarEnvido(Equipos EquipoQeCanta)
        {
            this.EquipoCantoEnvido = EquipoQeCanta;
            if (EquipoQeCanta == Equipos.Equipo1)
            {
                return Equipos.Equipo2;
            }
            if (EquipoQeCanta == Equipos.Equipo2)
            {
                return Equipos.Equipo1;
            }
            return Equipos.Equipo1;
        }
        private Mayor CicloMayor(int Fila)
        {
            int valor = 0;
            bool primeravez = false;
            int posicion = 0;
            var Mayor = new Mayor();
            for (int i = 0; i < 4; i++)
            {
                if (primeravez == false)
                {
                    primeravez = true;
                    if (CartasMesa[Manos - 1, i] != null)
                    {
                        valor = CartasMesa[Manos - 1, i].Valor;
                        posicion = i + 1;
                    }

                }
                else if (valor < CartasMesa[Manos - 1, i].Valor)
                {
                    if (CartasMesa[Manos - 1, i] != null)
                    {
                        valor = CartasMesa[Manos - 1, i].Valor;
                        posicion = i + 1;
                    }
                }
            }
            Mayor.Valor = valor;
            Mayor.Posicion = posicion;
            return Mayor;
        }

        public int EmpiezaParda(int ronda, List<Carta> movimientos, List<Jugador> jugadores)
        {
           var Aux =  CicloMayor(0);
            int comienza = 0;
            for (int i = 0; i < 3; i++)
            {
                if (movimientos[i].Valor == Aux.Valor)
                {
                    foreach (var item in jugadores)
                    {
                        if (item.Mano[ronda].Valor == Aux.Valor)
                        {
                            comienza = item.Numero - 1;
                            return comienza;
                        }
                    }
                }
                
            }
            return comienza;
        } 
        public Jugador GanaMano(List<Jugador> jugadores)
        {
            var jugadorGanador = jugadores.Single(x => x.Numero == CicloMayor(Manos - 1).Posicion);
            return jugadorGanador;
        }

        public bool RevisarPardas(int ronda)
        {
            this.PardaActivo = false;
            int band = 0;
            for (int i = 0; i < 4; i++)
            {
                if (CicloMayor(ronda).Valor == CartasMesa[ronda, i].Valor)
                {
                    band++;
                    if (band == 2)
                    {
                        return this.PardaActivo = true;

                    }
                   
                }
            }

            return this.PardaActivo;
        }

        public void VolverCartasMazo(List<Jugador> jugadores, Mazo mazo)
        {
            foreach (var item in jugadores)
            {
                foreach (var item2 in item.Mano)
                {
                    mazo.AgregarCartaAlMazo(item2, mazo.ListaCartas);
                    item.Mano.Remove(item2);
                }
            }

        }

        public void Truco(List<Jugador> Jugadores, int Turno, RespuestasTruco tipotruco, QuiereoNo respuesta, Equipos equipo)
        {

            Equipos equipoganador = Jugadores[CicloMayor(Manos - 1).Posicion].Equipo;
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

        public Equipos GanoPrimera(List<Jugador> Jugadores)
        {
            Jugador jugadorGano = Jugadores.Single(x => x.Numero == CicloMayor(Manos - 1).Posicion);
            return jugadorGano.Equipo;
        }

        public void sumarPuntosRonda(List<Jugador> jugadores)
        {
            List<Equipos> Ganadores = new List<Equipos>();
            this.Manos = 1;
            Ganadores.Add(GanoPrimera(jugadores));
            this.Manos = 2;
            Ganadores.Add(GanaMano(jugadores).Equipo);
            this.Manos = 3;
            Ganadores.Add(GanaMano(jugadores).Equipo);
            this.Manos = 1;

            if (this.PardaActivo != true)
            {
                int Equipo1 = Ganadores.Where(x => x == Equipos.Equipo1).Count();
                int Equipo2 = Ganadores.Where(x => x == Equipos.Equipo2).Count();
                if (Equipo2 > Equipo1)
                {
                    this.Puntaje2++;
                }
                else
                {
                    this.Puntaje1++;
                }
            }
            //CUANDO ES PARDA FALTA!!
        }
        public string JugarRonda()
        {
            while (this.FinPartida == false)
            {
                if (Turno == 3 && CartasMesa.Length == 12)
                {
                    this.FinPartida = true;
                }

                if (TrucoActivo == true)
                {
                    this.FinPartida = true;
                }

                this.Puntaje1 = 1;
                this.Puntaje2 = 1;
            }

            return this.Puntaje1.ToString() + ":" + this.Puntaje2.ToString(); //retorna una cadena (PUNTEQUIPO1+PUNTEQUIPO2)
        }

        public void MeVoyAlMazo()
        {
            this.FinPartida = true;
        }
    }
}
