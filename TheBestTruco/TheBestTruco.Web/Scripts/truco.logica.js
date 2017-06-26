$.urlParam = function (name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    if (results == null) {
        return null;
    }
    else {
        return decodeURI(results[1]) || 0;
    }
}


var trucoHub = {};
var idIntervalo = {};
var delay = {};


function TirarReyes() {
    trucoHub.server.empezarJuego();
}

function OcultarBotonReyes() {
    $(".BotonTirarReyes").hide();
    $("#FondoReyes").hide();
    $(".container").show();
    $(".chat").show();
    $(".PanelInferior").show();
    $("#Señas").show();
}

function MostrarBotonReyes() {
    $(".BotonTirarReyes").show();
    $("#FondoReyes").show();
}

function ReproducirSonido(audio) {
    var objeto = document.getElementById('audio')
    objeto.src = "Sonidos/" + audio + ".mp3"
    objeto.play();
}

function Seña(señaid) {
    trucoHub.server.hacerSeñas(señaid, $.urlParam('username')); // Le pasa el id de la seña
};

// Muestra quien es mano.
function MostrarMano() {
    $(".Mano").show();
}

// Oculta la mano
function OcultarMano() {
    $(".Mano").hide();
}

// Oculta los emogins de las señas
function OcultarSeña(numero) {
    $("#emogin_" + numero).attr("src", "");
    clearTimeout(delay);
}

// Oculta las acciones del envido.
function ocultarSeccionesEnvido() {
    $("#envidoRegion").hide();
    $("#envidoEnvidoRegion").hide();
    $("#faltaEnvidoRegion").hide();
    $("#quieroEnvidoRegion").hide();
    $("#realEnvidoRegion").hide();
}

// Oculta las acciones del truco.
function ocultarSeccionesTruco() {
    $("#trucoRegion").hide();
    $("#retrucoRegion").hide();
    $("#vale4Region").hide();
}

// Oculta todas las acciones.
function ocultarMovimientos() {
    $("#movements").hide();
}

// Visualiza todas las acciones.
function mostrarMovimientos() {
    $("#movements").show();
}

// Limpia el tablero.
function Limpiar() {
    // Ocultamos las acciones.
    ocultarSeccionesEnvido();
    ocultarSeccionesTruco();
    OcultarMano();

    // Limpiamos las cartas del jugador.
    $("#cards").html("");

    // Volteamos las cartas de la mesa.
    $("[id*=user][id*=card]").attr("src", "Images/BACK.JPG");
}

// Inicializamos los componentes.
function inicializarComponentes() {
    //$("#cards").hide();
    //$("#movements").hide();
    $("#rightPanel").niceScroll();
    $("#FondoReyes").hide();
    OcultarBotonReyes();
    ocultarSeccionesEnvido();
    ocultarSeccionesTruco();
}

$(function () {

    // Inicializo los componentes.
    inicializarComponentes();

    // Creamos el hub.
    trucoHub = $.connection.truco;



    // Evento click del boton "ingresar jugador".
    $("#botonAgregarJugador").click(function () {
        var userName = $("#userName").val();
        $("#userRegion").hide()


        // Habilitar para el trabajo práctico.
        // trucoHub.server.agregarJugador(userName, $("#imagen").attr("src"));
        document.title = (userName);
    });


    $("#bottonMazo").click(function () {
        trucoHub.server.cantar("me voy al mazo");
    });


    trucoHub.client.OcultarElementos = function (boolean) {
        if (boolean == true) {

            MostrarBotonReyes();
        }
        else {

            OcultarBotonReyes();
        }
    };
    // Visualizar nombre en el tablero.
    trucoHub.client.mostrarNombre = function (data) {
        var selector = "#" + data.NombreInterno;
        $(selector).html(data.Nombre);
        var barra = "#barra_carga" + data.Numero;
        $(barra).hide()
        $("#avatar_" + data.Numero).attr("src", data.DireccionAvatar)
    }

    trucoHub.client.mostrarPuntosEnvido = function (jugador, puntos) {
        $("#emogin_" + jugador).attr("src", "/Images/PuntosEnvido/" + puntos + ".PNG")
        $("#emogin_" + jugador).width(30);
    };

    trucoHub.client.mostrarPuntos = function (equipo, puntos) {
        $("#Equipo" + equipo).attr("src", "/Images/Puntos/" + puntos + ".PNG")
    };

    trucoHub.client.MostrarSeñas = function (idSeña, numero) {
        $("#emogin_" + numero).attr("src", $("#" + idSeña).attr("src"));
        delay = setTimeout(function () { OcultarSeña(numero) }, 1000);
    };

    trucoHub.client.MostrarManoPorTurno = function (turno) {
        OcultarMano();
        $("#mano_" + turno).show();
    };

    // Visualizar nombre en el chat.
    trucoHub.client.mostrarnuevousuario = function (data) {
        $("#discussion").prepend("<p>" + data + " se ha unido al juego!</p>");
    };

    trucoHub.client.mostrarMensajeFinal = function (data) {
        if (data == true) {
           alert("GANASTE!");
        } else {
            alert("PERDISTE!");
        }
    };
    $("#bottonEnvido").click(function () {
        $("#bottonEnvido").hide();
        trucoHub.server.cantar("envido");
    });

    $("#bottonEnvidoEnvido").click(function () {
        $("#bottonEnvidoEnvido").hide();
        trucoHub.server.cantar("envidoenvido");
    });

    $("#bottonFaltaEnvido").click(function () {
        $("#bottonFaltaEnvido").hide();
        trucoHub.server.cantar("faltaenvido");
    });

    $("#bottonRealEnvido").click(function () {
        $("#bottonRealEnvido").hide();
        trucoHub.server.cantar("realenvido");
    });

    $("#bottonQuieroEnvido").click(function () {
        $("#bottonQuieroEnvido").hide();
        trucoHub.server.ejecutarAccion("Envido", true);
    });

    $("#bottonNoQuieroEnvido").click(function () {
        $("#bottonNoQuieroEnvido").hide();
        trucoHub.server.ejecutarAccion("Envido", false);
    });

    $("#bottonQuieroEnvidoEnvido").click(function () {
        $("#bottonQuieroEnvidoEnvido").hide();
        trucoHub.server.ejecutarAccion("EnvidoEnvido", true);
    });

    $("#bottonNoQuieroEnvidoEnvido").click(function () {
        $("#bottonNoQuieroEnvidoEnvido").hide();
        trucoHub.server.ejecutarAccion("EnvidoEnvido", false);
    });


    $("#bottonQuieroFaltaEnvido").click(function () {
        $("#bottonQuieroFaltaEnvido").hide();
        trucoHub.server.ejecutarAccion("FaltaEnvido", true);
    });

    $("#bottonNoQuieroFaltaEnvido").click(function () {
        $("#bottonNoQuieroFaltaEnvido").hide();
        trucoHub.server.ejecutarAccion("FaltaEnvido", false);
    });

    $("#bottonQuieroRealEnvido").click(function () {
        $("#bottonQuieroRealEnvido").hide();
        trucoHub.server.ejecutarAccion("RealEnvido", true);
    });

    $("#bottonNoQuieroRealEnvido").click(function () {
        $("#bottonNoQuieroRealEnvido").hide();
        trucoHub.server.ejecutarAccion("RealEnvido", false);
    });

    $("#bottonCantarPuntosEnvido").click(function () {
        trucoHub.server.cantarPuntos(lastAction, $("#envidoPoints").val());
    });

    $("#bottonTruco").click(function () {
        $("#bottonTruco").hide();
        trucoHub.server.cantar("truco");
    });

    $("#bottonReTruco").click(function () {
        $("#bottonReTruco").hide();
        trucoHub.server.cantar("retruco");
    });

    $("#bottonVale4").click(function () {
        $("#bottonVale4").hide();
        trucoHub.server.cantar("vale4");
    });

    $("#bottonQuieroTruco").click(function () {
        $("#bottonQuieroTruco").hide();
        trucoHub.server.ejecutarAccion("Truco", true);
    });

    $("#bottonNoQuieroTruco").click(function () {
        $("#bottonNoQuieroTruco").hide();
        trucoHub.server.ejecutarAccion("Truco", false);
    });

    $("#bottonQuieroReTruco").click(function () {
        $("#bottonQuieroReTruco").hide();
        trucoHub.server.ejecutarAccion("ReTruco", true);
    });

    $("#bottonNoQuieroReTruco").click(function () {
        $("#bottonNoQuieroReTruco").hide();
        trucoHub.server.ejecutarAccion("ReTruco", false);
    });

    $("#bottonQuieroVale4").click(function () {
        $("#bottonQuieroVale4").hide();
        trucoHub.server.ejecutarAccion("Vale4", true);
    });

    $("#bottonNoQuieroVale4").click(function () {
        $("#bottonNoQuieroVale4").hide();
        trucoHub.server.ejecutarAccion("Vale4", false);
    });
    trucoHub.client.mostrarmensaje = function (data) {
        $("#discussion").prepend("<p>" + data + "</p>");
    };

    trucoHub.client.mostrarpuntos = function (equipo, puntos) {
        $("#punt").prepend("<p>" + puntos + "</p>");
        $("#punt").prepend("<p>" + equipo + "</p>");
    };

    trucoHub.client.limpiarpuntos = function () {
        $("#punt").html("");
    };

    trucoHub.client.mostrarCarta = function (carta, nombreinterno, cartaelegida) {
        var selector = "#" + nombreinterno + "_card" + cartaelegida;
        $(selector).attr("src", carta.Imagen);


    };

    trucoHub.client.mostrarCartas = function (data, numero) {


        var c = 1;
        $.each(data,
            function (index, value) {
                console.log("mostrarCarta:");
                console.log(value);

                //var img = "<img width='100' 'id'=" + value.Codigo + " 'src'=" + value.Imagen + " 'alt'=" + value.Codigo + " 'cc'=" + value.Codigo + "/>";

                //$(img).appendTo($("#cards"));


                //var selector = "#user" + numero + "_card" + c;
                //$(selector).attr("src", data[c - 1].Imagen);
                //c = c + 1;
                //if (c == 4) {
                //    c = 1;
                //}
                var img = $("<img width='130'/>").attr({
                    'id': value.Codigo,
                    'src': value.Imagen,
                    'alt': value.Codigo,
                    'cc': value.Codigo
                }).appendTo($("#cards"));
            });

        $("#cards").show();
        $("#movements").show();
    };

    trucoHub.client.limpiarTablero = function () {
        Limpiar();
    };

    trucoHub.client.ocultarBottonTruco = function () {
        ocbtTruco();
    };

    trucoHub.client.habilitarMovimientos = function (data) {

        console.log("habilitarMovimientos");

        $("[cc]").bind("click",
            function (evt) {
                console.log("jugar carta");
                $(this).hide();
                trucoHub.server.jugarCarta($(this).attr("cc")); //
            });
    };

    trucoHub.client.deshabilitarMovimientos = function (data) {

        console.log("deshabilitarMovimientos");
        $("[cc]").unbind("click");
    };

    trucoHub.client.showFaltaEnvidoOptions = function (data) {
        $("#faltaEnvidoRegion").show();
    };

    trucoHub.client.hideFaltaEnvidoOptions = function (data) {
        $("#faltaEnvidoRegion").hide();
    };

    trucoHub.client.showFaltaEnvidoBotton = function (data) {
        $("#bottonFaltaEnvido").show();
    };

    trucoHub.client.showEnvidoEnvidoOptions = function (data) {
        $("#envidoEnvidoRegion").show();
        $("#bottonRealEnvido").show();
        $("#bottonFaltaEnvido").show();
    };

    trucoHub.client.hideEnvidoEnvidoOptions = function (data) {
        $("#envidoEnvidoRegion").hide();
        $("#bottonRealEnvido").hide();
        $("#bottonFaltaEnvido").hide();
    };

    trucoHub.client.hideEnvidoBotton = function (data) {
        $("#bottonEnvido").hide();
    };
    trucoHub.client.hideRealEnvidoBotton = function (data) {
        $("#bottonRealEnvido").hide();
    };
    trucoHub.client.showRealEnvidoBotton = function (data) {
        $("#bottonRealEnvido").show();
    };
    trucoHub.client.showEnvidoBotton = function (data) {
        $("#bottonEnvido").show();
    };
    trucoHub.client.showEnvidoEnvidoBotton = function (data) {
        $("#bottonEnvidoEnvido").show();
    };
    trucoHub.client.showFaltaEnvidoBotton = function (data) {
        $("#bottonFaltaEnvido").show();
    };
    trucoHub.client.hideEnvidoEnvidoBotton = function (data) {
        $("#bottonEnvidoEnvido").hide();
    };
    trucoHub.client.hideEnvidoOptions = function (data) {
        $("#envidoRegion").hide();
        $("#bottonEnvido").hide();
        $("#bottonEnvidoEnvido").hide();
        $("#bottonFaltaEnvido").hide();
        $("#bottonRealEnvido").hide();
    };
    trucoHub.client.hideRealEnvidoOptions = function (data) {
        $("#realEnvidoRegion").hide()
        $("#bottonFaltaEnvido").hide();
    };
    trucoHub.client.showRealEnvidoOptions = function (data) {
        $("#realEnvidoRegion").show()
        $("#bottonFaltaEnvido").show();
    };
    trucoHub.client.showEnvidoOptions = function (data) {
        $("#envidoRegion").show()
        $("#bottonEnvido").hide();
        $("#bottonEnvidoEnvido").show();
        $("#bottonFaltaEnvido").show();
        $("#bottonRealEnvido").show();
    };

    trucoHub.client.showQuieroEnvido = function (data) {
        lastAction = data;
        $("#envidoRegion").hide()
        $("#quieroEnvidoRegion").show();
    };

    trucoHub.client.hidemazo = function (data) {
        $("#bottonMazo").hide();
    };
    trucoHub.client.showmazo = function (data) {
        $("#bottonMazo").show();
    };
    trucoHub.client.hideTrucoBotton = function (data) {
        $("#bottonTruco").hide();
    };

    trucoHub.client.hideReTrucoBotton = function (data) {
        $("#bottonReTruco").hide();
    };

    trucoHub.client.hideVale4Botton = function (data) {
        $("#bottonVale4").hide();
    };
    trucoHub.client.hideFaltaEnvidoBotton = function (data) {
        $("#bottonFaltaEnvido").hide();
    };

    trucoHub.client.showTrucoBotton = function (data) {
        $("#bottonTruco").show();
    };

    trucoHub.client.showReTrucoBotton = function (data) {
        $("#bottonReTruco").show();
    };

    trucoHub.client.showVale4Botton = function (data) {
        $("#bottonVale4").show();
    };
    trucoHub.client.showTrucoOptions = function (data) {
        $("#trucoRegion").show();
        $("#bottonReTruco").show();
    };

    trucoHub.client.hideTrucoOptions = function (data) {
        $("#trucoRegion").hide();
        $("#bottonReTruco").hide();
    };
    trucoHub.client.hideTrucoRegion = function (data) {
        $("#trucoRegion").hide();
    };
    trucoHub.client.showReTrucoOptions = function (data) {
        $("#retrucoRegion").show();
        $("#bottonVale4").show();
    };
    trucoHub.client.hideReTrucoOptions = function (data) {
        $("#retrucoRegion").hide();
        $("#bottonVale4").hide();
    };
    trucoHub.client.hideReTrucoRegion = function (data) {
        $("#retrucoRegion").hide();
    };
    trucoHub.client.showVale4Options = function (data) {
        $("#vale4Region").show();
    };
    trucoHub.client.hideVale4Options = function (data) {
        $("#vale4Region").hide();
    };

    $.connection.hub.start(function () {
        trucoHub.server.conectarse();


        //preguntar si tenes parametro username y si lo tenes llamas a la funcion crear jugador.
        var userName = $.urlParam('username');
        var Avatar = $.urlParam('avatar');
        if (userName != null) {
            $("#userRegion").hide()

            // Habilitar para el trabajo práctico.
            trucoHub.server.agregarJugador(userName, Avatar);
            $(".container").hide();
            $(".chat").hide();
            $(".PanelInferior").hide();
            $("#Señas").hide();
            document.title = userName;
        }
    });
});