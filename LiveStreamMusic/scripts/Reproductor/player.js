var a;
var previous;
var aSource;
var aTitle;
var autoplay;
var totalTracks;
var previousTrackNum;
var loop;

//variables donde almacenaremos los selectores de los elementos html
var currentTimeInfo;
var plause;
var next;
var back;
var playList;
var playlistControl;
var stop;
var downloadPlayList;
var song;
var songTitle;
var timeLoader;
var timePlayed;
var timeLoaded;
var totalTime;
var trackSection;
var playing = false;
var trackSelector;
var rangenote;
var lt;
var started = false;
var trackNum = 0;
var vol = 0.5;
var maxLenght = 65;
var listaEnReproduccion;

function Init() {
    if (a === undefined) {
        a = new Audio();
        a.id = "audioPrincipal";
    }
    a.autoplay = 'false';



    currentTimeInfo = $("#currentTime");
    plause = $('#plause');
    back = $('#ctlprev');
    next = $('#ctlnext');
    playList = $('#playlist');
    playlistControl = $('#playlistControl');
    stop = $('#stop');
    downloadPlayList = $('downloadPlayList');
    songTitle = $('#songTitle');
    timeLoaded = $('#timeLoaded');
    timePlayed = $('#timePlayed');
    totalTime = $('#totalTime');
    trackSection = $('#tracks');
    rangenote = $('#rangeNote');
    autoplay = $('#player').attr('data-autoplay');
    loop = $('#player').attr('data-loop');
    LimpiaEventosReproductor();
    playList.hide();
    playlistControl.removeClass('glyphicon glyphicon-music');
    playlistControl.addClass('glyphicon glyphicon-list');
};
function LimpiaEventosReproductor() {
    a.removeEventListener('timeupdate', updateTime);
    a.removeEventListener('ended', endSong);
    a.removeEventListener('loadedmetadata', metadata);
    a.removeEventListener('error', error);

    back.unbind("click");
    back.unbind("hover");
    plause.unbind("click");
    plause.unbind("hover");
    next.unbind("click");
    next.unbind("hover");
    stop.unbind("click");
    stop.unbind("hover");
    downloadPlayList.unbind("click");
    playlistControl.unbind("click");
}
function CargarLista(idLista) {
    debugger;
    listaEnReproduccion = idLista;
    var params = '{id:' + idLista + '}';
    $.ajax({

        type: 'POST',
        url: 'WebMethods.aspx/ObtenerDetalleLista',
        data: params,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: OnSuccess
    });
}

function OnSuccess(response) {
    Init();
    trackSection.empty();
    var canciones = response.d;
    $(canciones).each(function (index, actuaSong) {
        trackSection.append("<article class='track' data-source='FileCS.ashx?id=" + actuaSong.Id + "'><span>" + actuaSong.Descripcion + "</span></article>");
    });
    start();
}

function AgregarCancionAlista(lista, cancion) {
    debugger;
    if (trackSelector === undefined)
        return;
    if (lista === listaEnReproduccion) {
        trackSection.append("<article class='track' data-source='FileCS.ashx?id=" + cancion.Id + "'><span>" + cancion.Descripcion + "</span></article>");
        totalTracks++;
        trackSelector = $('.track');
        trackSelector.unbind("click");
        trackSelector.click(function () {
            var trackIndex = trackSelector.index(this);
            aSource = trackSelector.eq(trackIndex).attr('data-source');

            if ((previous === aSource && playing == false) || previous !== aSource) {
                previousTrackNum = trackNum;
                trackNum = trackIndex;
                beforePlay();
            }
        });
    }
}

function start() {
    trackNum = 0;
    trackSelector = $('.track');
    trackSelector.unbind("click");
    totalTracks = trackSelector.size();
    if (totalTracks <= 0) {
        a.removeEventListener('canplay', letsPlay);
        a.pause();
        a.currentTime = 0;
        aSource = null;
        LimpiaEventosReproductor();
        return;
    }
    a.addEventListener('timeupdate', updateTime);
    a.addEventListener('ended', endSong);
    a.addEventListener('loadedmetadata', metadata);
    a.addEventListener('error', error);
    renameTrackItems();
    aSource = trackSelector.eq(trackNum).attr('data-source');
    a.volume = 1;

    if (autoplay == 1) {
        previousTrackNum = trackNum;
        beforePlay();
    }

    back.click(function () {
        if (window.frames[0].frameElement.contentWindow.isPlayingSingle()) {
            stopSinglePlayer();
        }
        if (trackNum == (totalTracks - 1) && loop == 1) {
            a.pause();
            playing = false;
            a.removeEventListener('canplay', letsPlay);
            previousTrackNum = trackNum;
            if (trackNum < (totalTracks - 1))
                trackNum--;
            else
                trackNum = 0;


            aSource = trackSelector.eq(trackNum).attr('data-source');
            beforePlay();
        }
        else if (trackNum > 0) {
            a.pause();
            playing = false;

            a.removeEventListener('canplay', letsPlay);
            previousTrackNum = trackNum;
            trackNum--;
            aSource = trackSelector.eq(trackNum).attr('data-source');
            beforePlay();
        }
    });
    back.hover(function () {
        back.addClass('transition');

    },
    function () {
        back.removeClass('transition');
    });

    plause.click(function () {
        if (window.frames[0].frameElement.contentWindow.isPlayingSingle()) {
            window.frames[0].frameElement.contentWindow.stopSinglePlayer();
        }
        if (playing) {
            a.pause();
            playing = false;
            plause.css('background', 'url(../Images/audio/play.png)');
        } else {
            if (!started) {
                beforePlay();
            } else {
                a.play();
                playing = true;
                plause.css('background', 'url(../Images/audio/pause.png)');
            }
        }
    });
    plause.hover(function () {
        if (playing)
            plause.css('background', 'url(../Images/audio/pause_on.png)');
        else
            plause.css('background', 'url(../Images/audio/play_on.png)');
        plause.addClass('transition');

    },
    function () {
        if (playing)
            plause.css('background', 'url(../Images/audio/pause.png)');
        else
            plause.css('background', 'url(../Images/audio/play.png)');
        plause.removeClass('transition');
    });

    next.click(function () {

        if (trackNum == (totalTracks - 1) && loop == 1) {
            a.pause();
            playing = false;

            a.removeEventListener('canplay', letsPlay);
            previousTrackNum = trackNum;
            if (trackNum < (totalTracks - 1))
                trackNum++;
            else
                trackNum = 0;

            aSource = trackSelector.eq(trackNum).attr('data-source');
            beforePlay();
        }
        else if (trackNum < (totalTracks - 1)) {
            a.pause();
            playing = false;

            a.removeEventListener('canplay', letsPlay);
            previousTrackNum = trackNum;
            trackNum++;
            aSource = trackSelector.eq(trackNum).attr('data-source');
            beforePlay();
        }
    });
    next.hover(function () {
        next.addClass('transition');

    },
    function () {
        next.removeClass('transition');
    });



    stop.click(function () {
        //for (var i = 0; i < totalTracks; i++) {
        //    window.location.href = trackSelector.eq(i).attr('data-source');
        //}
        //trackSelector.each(function (index, Can))
        window.location.href = trackSelector.eq(trackNum).attr('data-source');
        //a.stop();
        //a.pause();
        //a.currentTime = 0;
        //playing = false;
        //a.removeEventListener('canplay', letsPlay);
        //timePlayed.css('width', '0%');
        //plause.css('background', 'url(../Images/audio/play.png)');
    });
    stop.hover(function () {
        stop.addClass('transition');
        stop.css('background', 'url(../Images/audio/download_on.png)');
    },
    function () {
        stop.removeClass('transition');
        stop.css('background', 'url(../Images/audio/download.png)');
    });

    downloadPlayList.click(function () {
        for (var song = 0; song < totalTracks; song++) {
            window.location.href = trackSelector.eq(song).attr('data-source');
        }
    });

    trackSelector.click(function () {
        var trackIndex = trackSelector.index(this);
        aSource = trackSelector.eq(trackIndex).attr('data-source');

        if ((previous === aSource && playing == false) || previous !== aSource) {
            previousTrackNum = trackNum;
            trackNum = trackIndex;
            beforePlay();
        }

    });


    playlistControl.click(function () {
        playList.toggle();
        if ($(this).hasClass("glyphicon glyphicon-list")) {
            $(this).removeClass('glyphicon glyphicon-list');
            $(this).addClass('glyphicon glyphicon-music');
        } else {
            $(this).removeClass('glyphicon glyphicon-music');
            $(this).addClass('glyphicon glyphicon-list');
        }
    });
};

function beforePlay() {
    if (window.frames[0].frameElement.contentWindow.isPlayingSingle()) {
        window.frames[0].frameElement.contentWindow.stopSinglePlayer();
    }
    if (playing) {
        a.pause();
        playing = false;
        plause.css('background', 'url(../Images/audio/pause.png)');
        timeLoaded.css('width', '0%');
        timePlayed.css('width', '0%');
    }
    aTitle = trackSelector.eq(trackNum).text().trim();
    document.title = aTitle;
    songTitle.text('Loading..');
    trackSelector.eq(previousTrackNum).removeClass('trackPlaying');
    trackSelector.eq(trackNum).addClass('trackPlaying');

    started = true;
    a.src = aSource;
    a.load();

    a.addEventListener('canplay', letsPlay);
}

function renameTrackItems() {
    $('.track span').each(function () {
        var st = $(this).text().trim();
        if (st.length > maxLenght) {
            st = st.substring(0, maxLenght);
            $(this).text(st + "...");
        }
    });
}

function letsPlay() {
    songTitle.text(aTitle);
    a.play();
    previous = aSource;
    playing = true;
    plause.css('background', 'url(../Images/audio/pause.png)');
}

function updateTime() {
    if (isNaN(a.duration))
        return;
    var total = a.duration;
    var current = a.currentTime;
    var currentPercentage = (current * 100) / total;
    timePlayed.css('width', currentPercentage + '%');

    var ctText = formatTime(current);

    $('#played').text(ctText);

}

function endSong() {
    a.pause();
    playing = false;

    a.removeEventListener('canplay', letsPlay);

    timePlayed.css('width', '0%');
    plause.css('background', 'url(../Images/audio/play.png)');

    previousTrackNum = trackNum;
    if (trackNum == (totalTracks - 1) && loop == 1) {
        if (trackNum < (totalTracks - 1))
            trackNum++;
        else
            trackNum = 0;

        aSource = trackSelector.eq(trackNum).attr('data-source');
        beforePlay();
    }
    else if (trackNum < (totalTracks - 1)) {
        trackNum++;
        aSource = trackSelector.eq(trackNum).attr('data-source');
        beforePlay();
    }
}

function metadata() {
    var total = formatTime(a.duration);
    totalTime.text(total);
}

function error() {
    if (a.error.code == 4) {
        errorString = 'Codec Error';
    }
    songTitle.text('Error loading File: ' + errorString);
}

function formatTime(time) {
    var s = Math.floor(time % 60);
    var min = Math.floor(time / 60);

    var timetext;
    if (s < 10)
        s = '0' + s;
    if (min < 10)
        min = '0' + min;

    timetext = min + ':' + s;
    return timetext;

}


function listPlayerContinue() {
    plause.css('background', 'url(../Images/audio/pause.png)');
    a.play();
    playing = true;
}

function listPlayerPause() {
    plause.css('background', 'url(../Images/audio/play.png)');
    a.pause();
    playing = false;
}

function playingPrincipal() {
    return playing;
}

