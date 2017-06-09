using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBestTruco.Entidades
{
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

        public Ronda()
        {
            CartasMesa = new Carta[3, 4];
            Turno = 1;
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

        //public void Truco(int Turno, RespuestasTruco tipotruco, QuiereoNo respuesta, Equipos equipo)
        //{
        //    bool primeravez = false;
        //    int valor = 0;
        //    int Jugador = 0;
        //    for (int i = 0; i < 4; i++)
        //    {
        //        if (primeravez == false)
        //        {
        //            primeravez = true;
        //            valor = CartasMesa[Turno, i].Valor;
        //            Jugador = i;
        //        }
        //        else if (valor < CartasMesa[Turno, i].Valor)
        //        {
        //            valor = CartasMesa[Turno, i].Valor;
        //            Jugador = i;
        //        }
        //    }
        //    Equipos equipoganador = Jugadores[Jugador].Equipo;
        //    switch (tipotruco)
        //    {
        //        case RespuestasTruco.Truco:
        //            if (respuesta == QuiereoNo.NoQuiero)
        //            {
        //                if (equipo == Equipos.Equipo1)
        //                {
        //                    Puntaje1 += 1;
        //                }
        //                else
        //                {
        //                    Puntaje2 += 1;
        //                }
        //            }
        //            else
        //            {
        //                if (equipoganador == Equipos.Equipo1)
        //                {
        //                    Puntaje1 += 2;
        //                }
        //                else
        //                {
        //                    Puntaje2 += 2;
        //                }
        //            }
        //            break;
        //        case RespuestasTruco.Retruco:
        //            if (respuesta == QuiereoNo.NoQuiero)
        //            {
        //                if (equipo == Equipos.Equipo1)
        //                {
        //                    Puntaje1 += 2;
        //                }
        //                else
        //                {
        //                    Puntaje2 += 2;
        //                }
        //            }
        //            else
        //            {
        //                if (equipoganador == Equipos.Equipo1)
        //                {
        //                    Puntaje1 += 3;
        //                }
        //                else
        //                {
        //                    Puntaje2 += 3;
        //                }
        //            }
        //            break;
        //        case RespuestasTruco.QuieroVale4:
        //            if (respuesta == QuiereoNo.NoQuiero)
        //            {
        //                if (equipo == Equipos.Equipo1)
        //                {
        //                    Puntaje1 += 3;
        //                }
        //                else
        //                {
        //                    Puntaje2 += 3;
        //                }
        //            }
        //            else
        //            {
        //                if (equipoganador == Equipos.Equipo1)
        //                {
        //                    Puntaje1 += 4;
        //                }
        //                else
        //                {
        //                    Puntaje2 += 4;
        //                }
        //            }
        //            break;
        //    }


        //}

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

            return this.Puntaje1.ToString()+":"+this.Puntaje2.ToString(); //retorna una cadena (PUNTEQUIPO1+PUNTEQUIPO2)
        }

        public void MeVoyAlMazo()
        {
            this.FinPartida = true;
        }
    }
}
