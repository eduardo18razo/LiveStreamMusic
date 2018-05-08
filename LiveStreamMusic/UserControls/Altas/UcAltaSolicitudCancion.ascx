<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcAltaSolicitudCancion.ascx.cs" Inherits="LiveStreamMusic.Web.UserControls.Altas.UcAltaSolicitudCancion" %>

<asp:UpdatePanel runat="server" ID="upSolicitudCancion">
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="hfIdCancion" />

        <section class="module">
            <div class="module-inner">
                <div class="row">
                    <!--DATOS GENERALES-->
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <div class="panel panel-default">
                            <div class="panel-heading panel-heading-theme-1 ">
                                <h4 class="panel-title">
                                    <asp:HyperLink data-toggle="collapse" ID="pnlPrincipal" runat="server" CssClass="panel-toggle no-underline" NavigateUrl="#faq5_1">
                                        <div class="row">
                                            <div class="col-lg-10 col-md-8 col-sm-12 col-xs-12">
                                                <h3 class="TitulosAzul">
                                                    <asp:Label runat="server" Text="Pedir Canción" /></h3>
                                            </div>
                                        </div>
                                    </asp:HyperLink>
                                </h4>
                            </div>
                            <asp:Panel CssClass="panel-collapse collapse in" runat="server">
                                <div class="panel-body">
                                    <div class="panel-group panel-group-theme-1">
                                        <div class="row">
                                            <div class="col-lg-11 col-md-11">
                                                <div class="row" runat="server" id="divTipousuario">
                                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 padding-20-left">
                                                        <asp:Label runat="server" Text="Artista" />
                                                        <asp:TextBox runat="server" ID="txtArtista" placeholder="Nombre que sugieres" CssClass="form-control" onkeydown="return (event.keyCode!=13);" MaxLength="50" />
                                                    </div>
                                                    <div class="col-lg-4 col-md-3 padding-20-left">
                                                        <asp:Label runat="server" Text="Album" />
                                                        <asp:TextBox runat="server" ID="txtAlbum" placeholder="Album que sugieres" CssClass="form-control " onkeydown="return (event.keyCode!=13);" MaxLength="50" />
                                                    </div>

                                                    <div class="col-lg-4 col-md-3 padding-20-left">
                                                        <asp:Label runat="server" Text="Canción" />
                                                        <asp:TextBox runat="server" ID="txtCancion" placeholder="Canción que sugieres" CssClass="form-control" onkeydown="return (event.keyCode!=13);" MaxLength="50" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-11 col-md-3 padding-20-left">
                                                        <asp:Label runat="server" Text="Comentarios" />
                                                        <asp:TextBox runat="server" ID="txtComentarios" placeholder="Que se te ocurre para ayudarnos mas?" Height="120px" TextMode="MultiLine" CssClass="form-control" onkeydown="return (event.keyCode!=13);" MaxLength="50" />
                                                    </div>

                                                    <div class="col-lg-3 col-md-3 padding-20-left">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section class="module" id="divBtnGuardar" runat="server" visible="true">
            <div class="module-inner">
                <div class="row widht100 text-center">
                    <br />
                    <asp:Button runat="server" Text="Aceptar" CssClass="btn btn-primary margin-auto" ID="btnAceptar" OnClick="btnAceptar_OnClick" />
                    <asp:Button runat="server" Text="Cancelar" CssClass="btn btn-default margin-right-15" ID="btnCancelar" OnClick="btnCancelar_OnClick" />
                </div>
            </div>
            <br />
        </section>
    </ContentTemplate>
</asp:UpdatePanel>
