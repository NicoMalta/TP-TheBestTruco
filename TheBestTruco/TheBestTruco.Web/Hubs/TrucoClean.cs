using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheBestTruco.Entidades;
using TheBestTruco.Entidades.Properties;

namespace Truco.Web.Hubs
{
    [HubName("truco")]
    public class Truco : Hub
    {
        public static Partida juego = new Partida();

        public Ronda ronda = Ronda.Instancia();
        public void Conectarse()
        {
            foreach (var j in juego.Jugadores)
            {
                Clients.Caller.mostrarnombre(j);
               
            }
            
        }

        

        public void AgregarJugador(string nombre)
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
                    Numero = juego.Jugadores.Count()+1
               };

                juego.Jugadores.Add(jugador);

                Clients.All.mostrarnombre(jugador);

                // Si es el ultimo jugador...
                juego.RevisarCantidadJugadores();

                if (juego.EstaCompleto == true)
                {
                   
                    //Clients.All.mostrarpuntos("Ellos", 0);
                    //Clients.All.mostrarpuntos("Nosotros", 0);
                   

                    Repartir();

                    //ronda.JugarRonda();
                }
            }
        }

        //public void cantar(string accion) 
        //{
        //    Clients.Others.mostrarmensaje("Jugador X canto ACCION");
        //    Clients.Caller.mostrarmensaje("Yo cante ACCION");

        //    Clients.Client(jugador.IdConexion).deshabilitarMovimientos();

        //    // Si el juego termino...
        //    Clients.Client(jugador.IdConexion).mostrarMensajeFinal(true); // GANADOR
        //    Clients.Client(jugador.IdConexion).mostrarMensajeFinal(false); // PERDEDOR
        //    Clients.All.deshabilitarMovimientos();

        //    // Sino
        //    Clients.All.limpiarpuntos();

        //    // Y mostrar puntos y repartir.


        //    switch (accion) 
        //    {   
        //        case "me voy al mazo":            
        //            break;
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
        //    }
        //}

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

        public void HacerSeñas()
        {
            var j = juego.Jugadores.Single(x => x.IdConexion == Context.ConnectionId);

            Clients.All.MostrarSeñas("hola",j.Numero);
        }

        public void JugarCarta(string codigoCarta)
        {
            var j = juego.Jugadores.Single(x => x.IdConexion == Context.ConnectionId);
            var c = j.Mano.Single(x => x.Codigo == codigoCarta);

            Clients.All.mostrarCarta(c, j.NombreInterno, ronda.Manos);
            ronda.CartasMesa[ronda.Manos - 1, j.Numero - 1]= c;
            j.Mano.Remove(c);
            Clients.Client(j.IdConexion).deshabilitarMovimientos();
            ronda.Turno++;

            if (ronda.Turno == 5)
            {
                ronda.Turno = 1;
                Clients.Client(ronda.GanaMano(juego.Jugadores).IdConexion).habilitarMovimientos();
                ronda.Manos++;
            }
            else
            {
                if (j.Numero + 1 == 5)
                {
                    //Clients.Client(juego.Jugadores[0].IdConexion).habilitarMovimientos();
                    //Clients.Client(juego.Jugadores[0].IdConexion).showRealEnvidoBotton();
                    //Clients.Client(juego.Jugadores[0].IdConexion).showFaltaEnvidoBotton();
                    //Clients.Client(juego.Jugadores[0].IdConexion).showEnvidoBotton();
                    //Clients.Client(juego.Jugadores[0].IdConexion).showTrucoBotton();
                }
                else
                {
                    var jugadorturno = juego.Jugadores.Single(x => x.Numero == j.Numero + 1);
                    //Clients.Client(jugadorturno.IdConexion).habilitarMovimientos();
                    //Clients.Client(jugadorturno.IdConexion).habilitarMovimientos();
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
                ronda.Manos = 1;
                juego.EsMano++;
                if (juego.EsMano == 5)
                {
                    juego.EsMano = 1;
                }
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
            Clients.All.limpiarTablero();

            // Por cada jugador y cada carta que maneja...
            juego.RepartirCartas(juego.Jugadores, juego.Mazo);

            foreach (var jugadores in juego.Jugadores)
            {
                Clients.Client(jugadores.IdConexion).mostrarCartas(jugadores.Mano, jugadores.Numero);
                Clients.All.MostrarManoPorTurno(ronda.Manos);
                Clients.Client(jugadores.IdConexion).MostrarSeñas();
            }

            var jugadorturno = juego.Jugadores.Single(x => x.Numero == juego.EsMano);

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