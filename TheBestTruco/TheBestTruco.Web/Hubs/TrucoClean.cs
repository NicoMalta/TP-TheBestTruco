using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheBestTruco.Entidades;
using TheBestTruco.Entidades.Properties;
using System.Timers;
using System.Threading.Tasks;

namespace Truco.Web.Hubs
{
    [HubName("truco")]
    public class Truco : Hub
    {
        public static Partida juego = new Partida();
        public static bool puerta = false;

        public void Conectarse()
        {
            //foreach (var j in juego.Jugadores)
            //{
            //    Clients.Caller.mostrarnombre(j);
                
            //}
        }

        public void enviarMensaje(string texto)
        {
            var j = juego.Jugadores.Single(x => x.IdConexion == Context.ConnectionId);
            Clients.All.mensajeChat(texto, j.Nombre);
        }

        public void mensajePrivado(string texto)
        {
            Clients.Caller.mostrarMensaje(texto);
        }
        public void AgregarJugador(string nombre, string avatar)
        {
            juego.RevisarCantidadJugadores();

            if (juego.EstaCompleto)
            {
                // Si el juego esta completo...
                Clients.Caller.mostrarmensaje("El juego ya está completo!");
            }
            else
            {
                Clients.Others.mostrarnuevousuario(nombre);
                // crear j
                var jugador = new Jugador()
                {
                    Nombre = nombre,
                    IdConexion = Context.ConnectionId,
                    NombreInterno = $"user{juego.Jugadores.Count() + 1}",
                    Numero = juego.Jugadores.Count() + 1,
                    DireccionAvatar = avatar

                };
               
                juego.Jugadores.Add(jugador);

                //Clients.All.mostrarnombre(jugador);
                Clients.Client(jugador.IdConexion).OcultarElementos(true);
                // Si es el ultimo jugador...
                juego.RevisarCantidadJugadores();

                if (juego.EstaCompleto == true)
                {
                   // Clients.All.OcultarElementos(false);
                    Clients.All.mostrarpuntos("EQUIPO 1 ", 0);
                    Clients.All.mostrarpuntos("EQUIPO 2 ", 0);

                    //ronda.JugarRonda();
                }
            }
        }

        public void cantar(string accion) 
        {
            var j = juego.Jugadores.Where(x => x.IdConexion == Context.ConnectionId).Single();
            Clients.Others.mostrarmensaje("Jugador " + j.Nombre + " canto " + accion);
            Clients.Caller.mostrarmensaje("Yo cante " + accion);

        //    Clients.Client(jugador.IdConexion).deshabilitarMovimientos();

        //    // Si el juego termino...
        //    Clients.Client(jugador.IdConexion).mostrarMensajeFinal(true); // GANADOR
        //    Clients.Client(jugador.IdConexion).mostrarMensajeFinal(false); // PERDEDOR
        //    Clients.All.deshabilitarMovimientos();

        //    // Sino
        //    Clients.All.limpiarpuntos();

        //    // Y mostrar puntos y repartir.


            switch (accion) 
            {   
                case "me voy al mazo":
                    // TODO > llevar a la lògica del juego.


                    Repartir();
                    break;
        //        case "envido":
        //            Clients.All.hidemazo();
        //            break;
        //        case "envidoenvido":
        //            Clients.All.hidemazo();
        //            break;
        //        case "faltaenvido":
        //            Clients.All.hidemazo();
        //            break;
        //        case "realenvido":
        //            Clients.All.hidemazo();
        //            break;
        //        case "truco":
        //            break;
        //        case "retruco":
        //            break;
        //        case "vale4":
        //            break;
            }
        }

        //public void EjecutarAccion(string accion, bool confirmacion)
        //{
        //    // confirmacion == true => Acepto la acción.
        //    Clients.All.mostrarmensaje("Jugador X acepto/rechazo la ACCION");

        //    switch (accion)
        //    {
        //        case "Envido":
        //            Clients.All.showmazo();            
        //            Clients.Client(jugador.IdConexion).habilitarMovimientos();
        //            break;
        //        case "EnvidoEnvido":
        //            Clients.All.showmazo();
        //            Clients.Client(jugador.IdConexion).habilitarMovimientos();
        //            break;
        //        case "RealEnvido":
        //            Clients.All.showmazo();
        //            Clients.Client(jugador.IdConexion).habilitarMovimientos();
        //            break;
        //        case "FaltaEnvido":
        //            Clients.All.showmazo();
        //            Clients.Client(jugador.IdConexion).habilitarMovimientos();
        //            break;
        //        case "Truco":
        //            break;
        //        case "ReTruco":
        //            break;
        //        case "Vale4":
        //            break;
        //    }
        //}

        public void EmpezarJuego()
        {
            if (juego.EstaCompleto)
            {
                TirarReyes(juego.Mazo, juego);
                OrdenarJugadores(juego.Jugadores);
                Repartir();
            }
        }

        public void hacerSeñas(string idSeña, string nombre)
        {
            var j = juego.Jugadores.Single(x => x.Nombre == nombre);

            Clients.All.MostrarSeñas(idSeña, j.Numero);
        }


        public void TirarReyes(Mazo mazo, Partida partida)
        {

            bool bandera = true;

            List<Carta> Lista1 = new List<Carta>();

            List<Carta> Lista2 = new List<Carta>();

            List<Carta> Lista3 = new List<Carta>();

            List<Carta> Lista4 = new List<Carta>();

            int x = 1;


            int uno = 0, dos =  0, tres = 0, cuatro = 0;

            bool a = false;

            Random aleatorio = new Random();

            int numero = 0;

            while (bandera == true)
            {
                if (mazo.ListaCartas.Count() == 0)
                {
                    bandera = false;
                }

                numero = aleatorio.Next(0, mazo.ListaCartas.Count());

                bool seguir = false;

                bool aux = false;

                while (seguir == false)
                {
                    if (x == 5)
                    {
                        x = 1;
                    }
                    switch (x)
                    {
                        case 1:
                            aux = Lista1.Exists(p => p.Numero == 12);
                            if (aux == false)
                            {
                                Lista1.Add(mazo.ListaCartas[numero]);
                                Clients.All.mostrarCarta(Lista1[Lista1.Count - 1], juego.Jugadores[x - 1].NombreInterno, 2);
                                seguir = true;
                            }
                            x++;
                            break;
                        case 2:
                            aux = Lista2.Exists(p => p.Numero == 12);
                            if (aux == false)
                            {
                                Lista2.Add(mazo.ListaCartas[numero]);
                                Clients.All.mostrarCarta(Lista2[Lista2.Count - 1], juego.Jugadores[x - 1].NombreInterno, 2);
                                seguir = true;
                            }
                            x++;
                            break;
                        case 3:
                            aux = Lista3.Exists(p => p.Numero == 12);
                            if (aux == false)
                            {
                                Lista3.Add(mazo.ListaCartas[numero]);
                                Clients.All.mostrarCarta(Lista3[Lista3.Count - 1], juego.Jugadores[x - 1].NombreInterno, 2);
                                seguir = true;
                            }
                            x++;
                            break;
                        case 4:
                            aux = Lista4.Exists(p => p.Numero == 12);
                            if (aux == false)
                            {
                                Lista4.Add(mazo.ListaCartas[numero]);
                                Clients.All.mostrarCarta(Lista4[Lista4.Count - 1], juego.Jugadores[x - 1].NombreInterno, 2);
                                seguir = true;
                            }
                            x++;
                            break;
                    }
                }
                mazo.ListaCartas.Remove(mazo.ListaCartas[numero]);

                a = Lista1.Exists(p => p.Numero == 12);
                if (a == true)
                {
                    uno = 1;
                    a = false;
                }

                a = Lista2.Exists(p => p.Numero == 12);
                if (a == true)
                {
                    dos = 1;
                    a = false;
                }

                a = Lista3.Exists(p => p.Numero == 12);
                if (a == true)
                {
                    tres = 1;
                    a = false;
                }

                a = Lista4.Exists(p => p.Numero == 12);
                if (a == true)
                {
                    cuatro = 1;
                    a = false;
                }
                
                if ((uno + dos + tres + cuatro) == 2)
                {
                    bandera = false;
                }

            }
            if (uno == 1)
            {
                partida.Jugadores[0].Equipo = Equipos.Equipo1;
            }
            else
            {
                partida.Jugadores[0].Equipo = Equipos.Equipo2;
            }

            if (dos == 1)
            {
                partida.Jugadores[1].Equipo = Equipos.Equipo1;
            }
            else
            {
                partida.Jugadores[1].Equipo = Equipos.Equipo2;
            }

            if (tres == 1)
            {
                partida.Jugadores[2].Equipo = Equipos.Equipo1;
            }
            else
            {
                partida.Jugadores[2].Equipo = Equipos.Equipo2;
            }

            if (cuatro == 1)
            {
                partida.Jugadores[3].Equipo = Equipos.Equipo1;
            }
            else
            {
                partida.Jugadores[3].Equipo = Equipos.Equipo2;
            }

            foreach (var item in Lista1)
            {
                mazo.AgregarCartaAlMazo(item, mazo.ListaCartas);
            }

            foreach (var item in Lista2)
            {
                mazo.AgregarCartaAlMazo(item, mazo.ListaCartas);
            }

            foreach (var item in Lista3)
            {
                mazo.AgregarCartaAlMazo(item, mazo.ListaCartas);
            }

            foreach (var item in Lista4)
            {
                mazo.AgregarCartaAlMazo(item, mazo.ListaCartas);
            }


        }

        public void OrdenarJugadores(List<Jugador> jugadores)
        {
            int ant = 0;
            int sig = 0;

            for (int i = 0; i < 4; i++)
            {
                sig = i + 1;

                if (sig == 4 )
                {
                    sig = 0;
                }

                if (jugadores[i].Equipo == jugadores[sig].Equipo)
                {
                    ant = i - 1;
                    if (ant == -1)
                    {
                        ant = 3;
                    }

                    Jugador ayudante = new Jugador();

                    ayudante = jugadores[ant];

                    jugadores[ant] = jugadores[i];
                    jugadores[ant].Numero = ant + 1;
                    jugadores[ant].NombreInterno = $"user{ant + 1}";

                    jugadores[i] = ayudante;
                    jugadores[i].Numero = i + 1;
                    jugadores[i].NombreInterno = $"user{i + 1}";
                }
            }
        }

        public void JugarCarta(string codigoCarta)
        {
            var j = juego.Jugadores.Single(x => x.IdConexion == Context.ConnectionId);
            var c = j.Mano.Single(x => x.Codigo == codigoCarta);
            var ronda = juego.Rondas[juego.Rondas.Count - 1];
            Clients.All.mostrarCarta(c, j.NombreInterno, ronda.Manos);
            ronda.CartasMesa[ronda.Manos - 1, j.Numero - 1] = c;
            juego.Mazo.ListaCartas.Add(c);
            j.Mano.Remove(c);

            Clients.Client(j.IdConexion).deshabilitarMovimientos();
            ronda.Turno++;

            if (ronda.Turno == 5)
            {

                //  ronda.RevisarPardas();
                ronda.Turno = 1;
                Clients.Client(ronda.GanaMano(juego.Jugadores).IdConexion).habilitarMovimientos();
                if (ronda.Manos == 2)
                {
                    ronda.Manos = 1;
                    var Equipo = ronda.GanoPrimera(juego.Jugadores);
                    ronda.Manos = 2;
                    if (Equipo == ronda.GanaMano(juego.Jugadores).Equipo)
                    {
                        if (Equipo == Equipos.Equipo1)
                        {
                            ronda.Puntaje1++;
                        }
                        else
                        {
                            ronda.Puntaje2++;
                        }
                        Clients.All.limpiarpuntos();
                        Clients.All.mostrarpuntos("EQUIPO 1", ronda.Puntaje1);
                        Clients.All.mostrarpuntos("EQUIPO 2", ronda.Puntaje2);
                        ronda.Manos = 1;
                        foreach (var item in juego.Jugadores)
                        {
                            juego.Mazo.ListaCartas.Add(item.Mano[0]);
                            item.Mano.RemoveAt(0);
                        }
                        Repartir();

                    }
                    else
                    {
                        ronda.Manos++;
                    }
                }
                else
                {
                    ronda.Manos++;
                }
            }
            else
            {
                if (j.Numero + 1 == 5)
                {
                    Clients.Client(juego.Jugadores[0].IdConexion).habilitarMovimientos();
                    //Clients.Client(juego.Jugadores[0].IdConexion).showRealEnvidoBotton();
                    //Clients.Client(juego.Jugadores[0].IdConexion).showFaltaEnvidoBotton();
                    //Clients.Client(juego.Jugadores[0].IdConexion).showEnvidoBotton();
                    //Clients.Client(juego.Jugadores[0].IdConexion).showTrucoBotton();
                }
                else
                {
                    var jugadorturno = juego.Jugadores.Single(x => x.Numero == j.Numero + 1);
                    Clients.Client(jugadorturno.IdConexion).habilitarMovimientos();
                    //Clients.Client(jugadorturno.IdConexion).showRealEnvidoBotton();
                    //Clients.Client(jugadorturno.IdConexion).showFaltaEnvidoBotton();
                    //Clients.Client(jugadorturno.IdConexion).showEnvidoBotton();
                    //Clients.Client(jugadorturno.IdConexion).showTrucoBotton();
                }

            }
            if (ronda.Manos > 1)
            {
                Clients.All.hideRealEnvidoBotton();
                Clients.All.hideFaltaEnvidoBotton();
                Clients.All.hideEnvidoBotton();
                Clients.All.hideEnvidoEnvidoBotton();
            }
            if (ronda.Manos == 4)
            {
                ronda.sumarPuntosRonda(juego.Jugadores);
                Clients.All.limpiarpuntos();
                Clients.All.mostrarpuntos("EQUIPO 1", ronda.Puntaje1);
                Clients.All.mostrarpuntos("EQUIPO 2", ronda.Puntaje2);
                Repartir();

            }

        }

        public string ConseguirNumeroJugador()
        {
            var j = juego.Jugadores.Single(x => x.IdConexion == Context.ConnectionId);
            return j.Numero.ToString();
        }

        public void Repartir()
        {
            Ronda ronda = new Ronda();
            juego.Rondas.Add(ronda);

            Clients.All.limpiarTablero();
            Clients.All.OcultarElementos(false);


            // Por cada jugador y cada carta que maneja...
            juego.RepartirCartas(juego.Jugadores, juego.Mazo);

            foreach (var jugadores in juego.Jugadores)
            {
                Clients.All.mostrarnombre(jugadores);
            }

            foreach (var jugadores in juego.Jugadores)
            {
                Clients.Client(jugadores.IdConexion).mostrarCartas(jugadores.Mano, jugadores.Numero);
                Clients.Client(jugadores.IdConexion).MostrarSeñas();
            }

            var jugadorturno = juego.Jugadores.Single(x => x.Numero == juego.QuienEmpieza(juego.Rondas.Count));//AGREGAR PARA VER QUIEN JUEGA
            Clients.All.MostrarManoPorTurno(jugadorturno.Numero == 1 ? 3 : jugadorturno.Numero == 2 ? 4 : jugadorturno.Numero - 2);

            Clients.Client(jugadorturno.IdConexion).habilitarMovimientos();

            /*
             * Propiedades de la Carta:
             * Codigo
             * Imagen                       
             */

            Clients.All.hideEnvidoEnvidoBotton();
            Clients.All.hideVale4Botton();
            Clients.All.hideReTrucoBotton();
            Clients.AllExcept(jugadorturno.IdConexion).hideEnvidoBotton();
            Clients.AllExcept(jugadorturno.IdConexion).hideTrucoBotton();
            Clients.AllExcept(jugadorturno.IdConexion).hideRealEnvidoBotton();
            Clients.AllExcept(jugadorturno.IdConexion).hideFaltaEnvidoBotton();
        }
    }
}