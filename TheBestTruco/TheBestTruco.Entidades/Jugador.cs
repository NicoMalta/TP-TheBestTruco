using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBestTruco.Entidades
{
    public enum RespuestasEnvido
    {
        Envido = 1, RealEnvido = 2, FaltaEnvido = 3
    }

    public enum RespuestasTruco
    {
        Truco = 1,Retruco = 2, QuieroVale4 = 3
    }

    public enum QuiereoNo
    {
        Quiero, NoQuiero
    }
    public class Jugador
    {

        public string Nombre { get; set; }
        public string NombreInterno { get; set; }
        public string IdConexion { get; set; }
        public List<Carta> Mano { get; set; }
        public Equipos Equipo { get; set; }
        public int Numero { get; set; }
        public string DireccionAvatar { get; set; }
        public bool Activo { get; set; }
        public Jugador() { Mano = new List<Carta>(); }



        //public void SolicitarEnvido(int Equipo, List<Jugador> jugadores, Puntuacion puntaje, RespuestasEnvido valor)
        //{
        //    RespuestasEnvido anterior = RespuestasEnvido.Envido;
        //    int identificador = 0;

        //    while ((valor != RespuestasEnvido.Quiero) && (valor != RespuestasEnvido.NoQuiero))
        //    {
        //        List<RespuestasEnvido> Respuestas = PosiblesRespuestas(ElegirValor(PosiblesRespuestas(valor)));

        //        identificador++;

        //        foreach (Jugador item in jugadores)
        //        {
        //            if (Equipo != item.Equipo)
        //            {
        //                anterior = valor;
        //                valor = ElegirValor(Respuestas);
        //            }
        //        }

        //        Equipo++;
        //        if (Equipo == 3)
        //        {
        //            Equipo = 1;
        //        }
        //    }

        //    int PuntosGanador = 0;

        //    if (valor == RespuestasEnvido.Quiero)
        //    {
        //        foreach (var item in jugadores)
        //        {
        //            if (jugadores.IndexOf(item) == 0)
        //            {
        //                PuntosGanador = item.ContadorEnvido(item.Mano);
        //                Equipo = item.Equipo;
        //            }
        //            else if(PuntosGanador < item.ContadorEnvido(item.Mano))
        //            {
        //                PuntosGanador = item.ContadorEnvido(item.Mano);
        //                Equipo = item.Equipo;
        //            }
        //        }

        //        if (Equipo == 1)
        //        {
        //            puntaje.Equipo1 = 1000;
        //        }
        //        else
        //        {
        //            puntaje.Equipo2 = 1000;
        //        }
        //    }
        //    else
        //    {
        //        switch (anterior)
        //        {
        //            case RespuestasEnvido.Envido:
        //                break;
        //            case RespuestasEnvido.RealEnvido:
        //                break;
        //            case RespuestasEnvido.FaltaEnvido:
        //                break;
        //        }

        //    }

        //} 

        //bool bandera = false;

        //public RespuestasEnvido ElegirValor(List<RespuestasEnvido> PosiblesRespuestas)
        //{
        //    int cantidad = 5 - (PosiblesRespuestas.Count);

        //    int aleatorio = 0;

        //    do
        //    {
        //        if (bandera == true)
        //        {
        //            aleatorio++;
        //        }

        //        if (PosiblesRespuestas[0] == RespuestasEnvido.Envido)
        //        {
        //            bandera = true;
        //        }

        //        switch (aleatorio)
        //        {
        //            case 1:
        //                return RespuestasEnvido.Envido;
        //            case 2:
        //                return RespuestasEnvido.RealEnvido;
        //            case 3:
        //                return RespuestasEnvido.FaltaEnvido;
        //            case 4:
        //                return RespuestasEnvido.NoQuiero;
        //            case 5:
        //                return RespuestasEnvido.Quiero;
        //        }
        //        return RespuestasEnvido.NoQuiero;

        //    } while ((aleatorio >= cantidad) && (aleatorio < 6));

        //}

        //bool bander = false;

        //public List<RespuestasEnvido> PosiblesRespuestas(RespuestasEnvido valor)
        //{
        //    int c = 0;
        //    List<RespuestasEnvido> respuesta = new List<RespuestasEnvido>();

        //    foreach (RespuestasEnvido item in Enum.GetValues(typeof(RespuestasEnvido)))
        //    {
        //        c++;
        //        if (valor == RespuestasEnvido.Envido && bander == false)
        //        {
        //            respuesta.Add(RespuestasEnvido.Envido);
        //            bander = true;
        //        }

        //        if ((int)valor < c)
        //        {
        //            respuesta.Add(item);
        //        }
        //    }

        //    return respuesta;
        //}

        public void SolicitarTruco(int Equipo, List<Jugador> jugadores, int turno)
        {

        }



        public int ContadorEnvido(List<Carta> mano) //SE LE PASA UNA MANO Y DEVUELVE LA CANTIDAD DE PUNTOS DE ENVIDO QUE TIENE      
        {
            int envido1 = 0;
            int envido2 = 0;
            int envido3 = 0;

            if (mano[0].Palos == mano[1].Palos)
            {
                envido1 = TraductorDeCartas(mano[0], mano[1]);
            }

            if (mano[0].Palos == mano[2].Palos)
            {
                envido1 = TraductorDeCartas(mano[0], mano[2]);
            }

            if (mano[1].Palos == mano[2].Palos)
            {
                envido1 = TraductorDeCartas(mano[1], mano[2]);
            }

            if (envido1 >= envido2)
            {
                if (envido1 >= envido3)
                {
                    return envido1;
                }
                else
                {
                    return envido3;
                }
            }
            else
            {
                if (envido2 >= envido3)
                {
                    return envido2;
                }
                else
                {
                    return envido3;
                }
            }
        }

        public int TraductorDeCartas(Carta carta1, Carta carta2)
        {
            int valor1 = 0;
            int valor2 = 0;

            bool normal1 = true;
            bool normal2 = true;

            switch (carta1.Numero)
            {
                case 12:
                    valor1 = 20;
                    normal1 = false;
                    break;
                case 11:
                    valor1 = 20;
                    normal1 = false;
                    break;
                case 10:
                    valor1 = 20;
                    normal1 = false;
                    break;
            }

            if (normal1 == true)
            {
                valor1 = carta1.Numero;
            }


            switch (carta2.Numero)
            {
                case 12:
                    valor2 = 20;
                    normal2 = false;
                    break;
                case 11:
                    valor2 = 20;
                    normal2 = false;
                    break;
                case 10:
                    valor2 = 20;
                    normal2 = false;
                    break;
            }


            if (normal2 == true)
            {
                valor2 = carta2.Numero;
            }

            if (normal1 == false && normal2 == false)
            {
                return 20;
            }

            if (normal1 == true && normal2 == true)
            {
                return valor1 + valor2 + 20;
            }


            if ((normal2 == false && normal1 == true) || (normal1 == true && normal2 == false))
            {
                return valor1 + valor2;
            }


            return 0;


        } // A LOS 10 - 11 - 12 LOS CUENTA COMO 20 PUNTOS EN EL ENVIDO Y A LAS DEMAS CARTAS NORMAL POR SU NUMERO 


    }
}
