<%@ Page Title="" Language="C#" MasterPageFile="~/WebSite.Master" AutoEventWireup="true" CodeBehind="Musica.aspx.cs" Inherits="LiveStreamMusic.Web.Users.Musica" %>

<%@ Register Src="~/UserControls/Altas/UcAltaIncidencia.ascx" TagPrefix="uc1" TagName="UcAltaIncidencia" %>
<%@ Register Src="~/UserControls/Altas/UcAgregarCancionLista.ascx" TagPrefix="uc1" TagName="UcAgregarCancionLista" %>


<asp:Content ID="Content1" ContentPlaceHolderID="webSiteHead" runat="server">
    <title>Biblioteca</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="webSiteContent" runat="server">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <section class="module">
                <div class="row">
                    <div class="module-inner">
                        <div class="row col-xs-12 col-sm-12 col-md-12 col-lg-12 ">
                            <div class="module-heading">
                                <h3 class="module-title">
                                    <asp:Label runat="server" Text="Bibliotecas" /></h3>
                            </div>
                            <p>
                                Encuentra aqui toda tu musica si no la encuentras puedes pedirlas!!!. 
                            </p>
                            <hr />
                        </div>
                        <div class="row col-xs-12 col-sm-12 col-md-12 col-lg-12" runat="server" id="divConsulta">
                            <div class="form col-lg-6 separador-vertical-derecho">
                                <div class="form-group">
                                    <label class="col-xs-12 col-sm-12 col-md-12 col-lg-12 no-padding-left no-margin-left">Buscar:</label>
                                    <div class="col-xs-10 col-sm-10 col-md-10 col-lg-10 no-padding-left no-margin-left">
                                        <asp:TextBox runat="server" CssClass="form-control no-padding-left no-margin-left" ID="txtFilter" onkeydown="return (event.keyCode!=13 && event.keyCode!=27);;" autofocus="autofocus" />
                                    </div>
                                    <div class="col-xs-2 col-sm-2 col-md-2 col-lg-2 margin-top-3">
                                        <asp:LinkButton runat="server" ID="btnBuscar" CssClass="btn btn-primary btn-single-icon" OnClick="btnBuscar_OnClick"><i class="fa fa-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="module-content collapse in" id="content-4">
                                <div class="module-content-inner no-padding-bottom">
                                    <asp:UpdatePanel runat="server" style="height: 100%">
                                        <ContentTemplate>
                                            <div class="form-inline">
                                                <div class="form-group col-sm-12">
                                                    <div class="col-sm-3">
                                                    </div>

                                                </div>
                                                <div class="clearfix clear-fix clearfix"></div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <section class="module module-headings">
                <div class="module-inner">

                    <div class="module-content collapse in" id="content-1">
                        <div class="module-content-inner no-padding-bottom">
                            <div class="table-responsive">
                                <asp:Repeater runat="server" ID="rptMusica">
                                    <ItemTemplate>
                                        <div style="float: left; width: calc(100% / 1); padding-left: 5px">
                                            <div class="row musicResultMobile">
                                                <div class="controlSinglePlayrSearchDatos">
                                                    <asp:Label runat="server" Text='<%# "Genero: " + Eval("Genero") %>' /><br />
                                                    <asp:Label runat="server" Text='<%# "Artista: " + Eval("Artista") %>' /><br />
                                                    <asp:Label runat="server" Text='<%# "Album: " + Eval("Album") %>' /><br />
                                                    <asp:Label runat="server" Text='<%# "Cancion: <strong>" + Eval("Cancion") + "</strong>" %>' /><br />
                                                </div>

                                                <div class="controlSinglePlayrSearch">
                                                    <section class="singlePlayer">
                                                        <section class="controlsSinglePlayer">
                                                            <div class="singlePlayerControls">
                                                                <div class="play" data-source='FileCS.ashx?id=<%# Eval("IdCancion") %>' onclick="ReproducirCancion(this)"></div>
                                                                <a class="downloadSong" href='FileCS.ashx?id=<%# Eval("IdCancion") %>'></a>
                                                            </div>
                                                        </section>
                                                    </section>
                                                    <asp:Button runat="server" ID="btnAddToList" Text="Agregar..." CssClass="btn btn-sm btn-success" OnClick="btnAddToList_OnClick" CommandArgument='<%# Eval("IdCancion") %>' />
                                                    <asp:Button runat="server" Enabled='<%#!(bool) Eval("TieneReporte")%>' Text='<%#(bool) Eval("TieneReporte") ? "Reportada" : "Reportar" %>' CommandArgument='<%# Eval("IdCancion") %>' CssClass='<%# (bool) Eval("TieneReporte") ? "btn btn-sm btn-danger" : "btn btn-sm btn-success" %>' ID="btnReportar" OnClick="btnReportar_OnClick" />
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <div class="modal fade" id="modalAgregarCancion" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <uc1:UcAgregarCancionLista runat="server" ID="ucAgregarCancionLista" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="modal fade" id="modalAltaincidencia" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <uc1:UcAltaIncidencia runat="server" ID="ucAltaIncidencia" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
