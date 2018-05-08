<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcSinglePlayer.ascx.cs" Inherits="LiveStreamMusic.Web.UserControls.Reproductor.UcSinglePlayer" %>
<link href="../MiReproductor/singlePlayer.css" rel="stylesheet" />
<script type="text/javascript" src="https://code.jquery.com/jquery-3.1.1.min.js"></script>
<script src="http://cdn.jquerytools.org/1.2.6/full/jquery.tools.min.js"></script>
<script src="../MiReproductor/singlePlayer.js"></script>
<section class="singlePlayer">
    <section class="controlsSinglePlayer">
        <div class="singlePlayerControls">
            <div class="play" data-source='Users/FileCS.ashx?id=1' onclick="Init(this)"></div>
            <a class="downloadSong" href='Users/FileCS.ashx?id=1'></a>
        </div>
    </section>
</section>

<section class="singlePlayer">
    <section class="controlsSinglePlayer">
        <div class="singlePlayerControls">
            <div class="play" data-source='Users/FileCS.ashx?id=2' onclick="Init(this)"></div>
            <a class="downloadSong" href='Users/FileCS.ashx?id=1'></a>
        </div>
    </section>
</section>