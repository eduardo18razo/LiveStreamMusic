<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcPlayer.ascx.cs" Inherits="LiveStreamMusic.Web.UserControls.Reproductor.UcPlayer" %>
<ul class="nav navbar-nav">
    <li class="dropdown">
        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">MisListas <span class="caret"></span></a>
        <ul class="dropdown-menu">
            <asp:Repeater runat="server" ID="rptListasReproduccion" OnItemCreated="rptListasReproduccion_OnItemCreated">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <li style="margin: 2px">
                        <asp:LinkButton ID="btnSleccionaLista" runat="server" OnClientClick='<%# "CargarLista(" + Eval("Id") + ")" %> ' Text='<%# Eval("Descripcion") %>' CssClass="btn btn-default" CommandArgument='<%# Eval("Id") %>'
                            Style="text-decoration: none">
                            <span class="glyphicon glyphicon-music"><%# Eval("Descripcion") %></span>
                        </asp:LinkButton>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </ul>
    </li>

    <li style="margin-left: 2em">
        <section id="player" data-autoplay="1">
            <section id="controls">
                <section id="songTitle">
                    <span>Titulo de la canción</span>
                    <div id="timeStatus">
                        <time id="played"></time>
                        <span></span>
                        <time id="totalTime"></time>
                    </div>
                </section>
                <section id="playertrols">
                    <div id="plauseStop">
                        <div id="ctlprev"></div>
                        <div id="plause"></div>
                        <div id="ctlnext"></div>
                        <div id="stop"></div>
                    </div>
                    <section id="playlistControl">
                        <span></span>
                    </section>
                </section>
            </section>
            <section id="playlist" style="display: none">
                <section id="tracks">
                </section>
            </section>
        </section>
        <div id="currentTime" style="display: none">
            <time>00:00</time>
        </div>
    </li>

</ul>


