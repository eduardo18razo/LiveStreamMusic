var reproductor;
var playingSingle = false;
var previuosSong;
var startedSingle = false;
var ContinuaPrincipal = false;
var reproduccionActual;

function ReproducirCancion(object) {
    debugger;
    object = $(object);

    if (reproductor === undefined) {
        reproductor = new Audio();
        reproductor.id = "reproductorBiblioteca";
        reproductor.autoplay = 'false';
    }
    if (window.parent.playingPrincipal()) {
        ContinuaPrincipal = true;
        window.parent.listPlayerPause();
    }
    if (startedSingle) {
        if (playingSingle) {
            if (object.attr('class') === 'pause') {
                reproductor.pause();
                object.removeClass('pause');
                object.addClass('play');
                object.parent().parent().parent().parent().parent().removeClass('musicResultMobilePlayer');
                previuosSong = object.attr('data-source');
                playingSingle = false;
                if (ContinuaPrincipal) {
                    window.parent.listPlayerContinue();
                    ContinuaPrincipal = false;
                }
                return;
            } else if (object.attr('class') === 'play') {
                reproduccionActual = $(".pause");
                reproduccionActual.removeClass('pause');
                reproduccionActual.parent().parent().parent().parent().parent().removeClass('musicResultMobilePlayer');
                reproduccionActual.addClass('play');
                object.removeClass('play');
                object.addClass('pause');
                object.parent().parent().parent().parent().parent().addClass('musicResultMobilePlayer');
                reproductor.src = object.attr('data-source');
                previuosSong = object.attr('data-source');
                reproductor.load();
                reproductor.play();
            }
        } else if (previuosSong === object.attr('data-source')) {
            reproductor.play();
            object.removeClass('play');
            object.addClass('pause');
            object.parent().parent().parent().parent().parent().addClass('musicResultMobilePlayer');
            previuosSong = object.attr('data-source');
            playingSingle = true;
            return;
        } else if (object.attr('class') === 'play' && previuosSong === object.attr('data-source')) {
            reproductor.play();
        }
        else if (object.attr('class') === 'play' && previuosSong !== object.attr('data-source')) {
            object.removeClass('play');
            object.addClass('pause');
            object.parent().parent().parent().parent().parent().addClass('musicResultMobilePlayer');
            reproductor.src = object.attr('data-source');
            previuosSong = object.attr('data-source');
            reproductor.load();
            reproductor.play();
        }
    } else {
        object.removeClass('play');
        object.addClass('pause');
        object.parent().parent().parent().parent().parent().addClass('musicResultMobilePlayer');
        reproductor.src = object.attr('data-source');
        previuosSong = object.attr('data-source');
        reproductor.load();
        reproductor.play();
        playingSingle = true;
        startedSingle = true;
    }
};
function isPlayingSingle() {

    return playingSingle;
}
function stopSinglePlayer() {
    reproduccionActual = $(".pause");
    reproduccionActual.removeClass('pause');
    reproduccionActual.addClass('play');
    reproductor.pause(); // Stop playing
    reproductor.currentTime = 0;
}
