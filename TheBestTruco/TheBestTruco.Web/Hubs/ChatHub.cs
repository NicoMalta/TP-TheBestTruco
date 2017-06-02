//using Microsoft.AspNet.SignalR;
//using Microsoft.Owin;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;


//namespace TheBestTruco.Web.Hubs
//{
//    public class ChatHub : Hub
//    {

//        public void enviarMensaje(string nombre, string texto)
//        {
//            //  Context.ConnectionId    //Identificador
//            Clients.All.mostrarMensaje(nombre, texto);
//        }

//        public void mensajePrivado(string nombre, string texto)
//        {
//            Clients.Caller.mostrarMensaje(nombre, texto);
//        }

//    }
//}