<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReproductorAnidado.aspx.cs" Inherits="LiveStreamMusic.Web.Users.ReproductorAnidado" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Repeater runat="server" ID="rptListasReproduccion">
        <ItemTemplate>
            <asp:LinkButton ID="btnSleccionaLista" runat="server" Text='<%# Eval("Descripcion") %>' CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-success " OnClick="btnSleccionaLista_OnClick" Style="text-decoration: none">
                            <span class="glyphicon glyphicon-music"><%# Eval("Descripcion") %></span>
            </asp:LinkButton>
        </ItemTemplate>
    </asp:Repeater>
    <section id="player" data-autoplay="0">
        <section id="playlist">
            <section id="tracks">
                <asp:Repeater runat="server" ID="rptDetalleLista">
                    <ItemTemplate>
                        <article class="track trackPlaying" data-source='FileCS.ashx?id=<%# Eval("Cancion.Id") %>'>
                            <span><%# Eval("Cancion.Descripcion") %></span>
                        </article>
                    </ItemTemplate>
                </asp:Repeater>
            </section>
            <section id="playlistControl">
                <span>&#x25B2;</span>
            </section>
        </section>
        <section id="controls">
            <section id="songTitle">
                <span>Titulo de la canción</span>
                <div id="timeStatus">
                    <time id="played">00:00</time>
                    <span>/</span>
                    <time id="totalTime">00:00</time>
                </div>
            </section>
            <section id="playertrols">
                <div id="plauseStop">
                    <div id="ctlprev"></div>
                    <div id="plause"></div>
                    <div id="ctlnext"></div>
                    <div id="stop"></div>
                </div>
                <div id="progressBar">
                    <div id="timeLoaded"></div>
                    <div id="timePlayed"></div>
                </div>
                <section id="volumTime" class="range-slider">
                    <input type="range" class="range-slider__range" min="0" max="1" step="0.01" value="0.5" />

                </section>

            </section>
        </section>
    </section>
    <div id="currentTime">
        <time>00:00</time>
    </div>
    <span id="rangeNote" class="range-slider__value">0</span>
</asp:Content>
