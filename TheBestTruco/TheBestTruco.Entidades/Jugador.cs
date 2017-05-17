using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBestTruco.Entidades
{
    public enum RespuestasEnvido
    {
        Envido, EnvidoEnvido, RealEnvido, FaltaEnvido, Quiero, NoQuiero
    }

    public class Jugador
    {

        public string Nombre { get; set; }
        public List<Carta> Mano { get; set; }
        public int Equipo { get; set; }
        public int Numero { get; set; }

        public Jugador() { Mano = new List<Carta>(); }

        public void SolicitarEnvido(int Equipo, List<Jugador> jugadores, Puntuacion puntaje, RespuestasEnvido valor)
        {
            while ((valor != RespuestasEnvido.Quiero) || (valor != RespuestasEnvido.NoQuiero))
            {
                foreach (Jugador item in jugadores)
                {
                    if (Equipo != item.Equipo)
                    {
                        PosiblesRespuestas(valor);
                    }
                }
            }

            if (valor == RespuestasEnvido.Quiero)
            {
                foreach (var item in jugadores)
                {
                    item.ContadorEnvido(item.Mano);
                } // HACER UN CICLO DEL MAYOR Y AVERIGUAR QUIEN ES EL GANADOR DEL ENVIDO Y MOFICAR LOS PUNTOS DE LA PUNTUACION
            }
            else
            {
                //VER A QUIEN HAY Q SUMARLE LOS PUNTOS


            }



        }

        public RespuestasEnvido PosiblesRespuestas(RespuestasEnvido valor)
        {
            int mostrar = (int)valor;

            for (int i = 0; i < 6; i++)
            {
                if (mostrar < i)
                {
                    Console.WriteLine(Convert.ToString((RespuestasEnvido)i));

                    //Console.WriteLine(Convert.ToString((RespuestasEnvido)i));
                    //numero = readkey;
                    //valor = (RespuestasEnvido)numero);

                }
            }

            return valor;
        }

        public void SolicitarTruco()
        {

        }

        public int Truco(List<Jugador> jugadores, int turno)
        {
            throw new NotImplementedException();
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
