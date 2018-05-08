<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Mobile.Master" AutoEventWireup="true" CodeBehind="Musica.aspx.cs" Inherits="LiveStreamMusic.Web.Users.Mobile.Musica" %>

<%@ Register Src="~/UserControls/Altas/UcAltaIncidencia.ascx" TagPrefix="uc1" TagName="UcAltaIncidencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMobile" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentMobile" runat="server">

    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:UpdatePanel runat="server" style="height: 100%">
                <ContentTemplate>
                    <div class="panel panel-warning" style="-ms-opacity: .8; opacity: .8">
                        <div class="panel-heading">
                            <h1>Biblioteca</h1>
                        </div>
                        <div class="panel-body">
                            <header id="panelAlerta" runat="server" visible="False">
                                <div class="alert alert-danger">
                                    <asp:Repeater runat="server" ID="rptErrorGeneral">
                                        <ItemTemplate>
                                            <div style="color: #ff0000">
                                                <%# Eval("Detalle")  %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </header>
                            <br />
                            <div class="form-inline">
                                <div class="form-group col-sm-12">
                                    <asp:Label runat="server" Text="Buscar" CssClass="control-label col-sm-1"></asp:Label>
                                    <div class="col-sm-3">
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtFilter" onkeydown="return (event.keyCode!=13);" autofocus="autofocus" Width="100%" />
                                    </div>
                                    <asp:Button runat="server" ID="btnBuscar" Text="Buscar" CssClass="btn btn-success col-sm-1" OnClick="btnBuscar_OnClick" />
                                </div>
                                <div class="cleaner clear-fix clearfix"></div>
                            </div>

                            <br />
                            <asp:Repeater runat="server" ID="rptMusica">
                                <ItemTemplate>
                                    <div class="row musicResultMobile">
                                        <div style="float: left; width: 50%">
                                            <asp:Label runat="server" Text='<%# "Genero: " + Eval("Genero") %>' /><br />
                                            <asp:Label runat="server" Text='<%# "Artista: " + Eval("Artista") %>' /><br />
                                            <asp:Label runat="server" Text='<%# "Album: " + Eval("Album") %>' /><br />
                                            <asp:Label runat="server" Text='<%# "Cancion: " + Eval("Cancion") %>' /><br />
                                        </div>
                                        <div style="float: left; width: 50%">

                                            <audio controls preload="none" style="width: 320px">
                                                <source src='../FileCS.ashx?Id=<%# Eval("IdCancion") %>'>
                                            </audio>
                                            <asp:Button runat="server" Enabled='<%#!(bool) Eval("TieneReporte")%>' Text='<%#(bool) Eval("TieneReporte") ? "Reportada" : "Reportar" %>' CommandArgument='<%# Eval("IdCancion") %>' CssClass='<%# (bool) Eval("TieneReporte") ? "btn btn-sm btn-danger" : "btn btn-sm btn-success" %>' ID="btnReportar" OnClick="btnReportar_OnClick" />
                                        </div>
                                        <br />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

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
