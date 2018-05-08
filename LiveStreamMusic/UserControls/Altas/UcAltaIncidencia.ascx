<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcAltaIncidencia.ascx.cs" Inherits="LiveStreamMusic.Web.UserControls.Altas.UcAltaIncidencia" %>
<asp:UpdatePanel runat="server" ID="upAltaIncidencia">
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="hfIdCancion" />
        <div class="modal-header">
            <asp:LinkButton class="close" ID="btnClose" OnClick="btnCancelar_OnClick" runat="server"><span aria-hidden="true">&times;</span></asp:LinkButton>
            <h6 class="modal-title" id="modal-new-ticket-label">
                <asp:Label runat="server" Text="Reporta Una Canción" Font-Bold="true" />
            </h6>
        </div>
        <div class="modal-body">
            <div class="row">
                <div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div>
                            <div class="form-group margin-top">
                                Tipo de queja<br />
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlTipoIncidencia" AutoPostBack="True" autofocus="autofocus" OnSelectedIndexChanged="ddlTipoIncidencia_OnSelectedIndexChanged" />
                            </div>
                            <div class="form-group margin-top" runat="server" id="divGeneroIncorrecto" visible="False">
                                Genero que sugieres<br />
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlGeneroSugerido" AutoPostBack="True" />
                            </div>
                            <div class="form-group margin-top" runat="server" id="divArtistaIncorrecto" visible="False">
                                Artista que sugieres<br />
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlArtistaSugerido" AutoPostBack="True" />
                            </div>
                            <div class="form-group margin-top" runat="server" id="divAlbumIncorrecto" visible="False">
                                Album que sugieres<br />
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlAlbumSugerido" AutoPostBack="True" />
                            </div>
                            <div class="form-group margin-top" runat="server" id="divNombre" visible="False">
                                Que nombre sugieres<br />
                                <asp:TextBox runat="server" ID="txtNombreSugerido" placeholder="Nombre que sugieres" CssClass="form-control" onkeydown="return (event.keyCode!=13);" MaxLength="50" Style="text-transform: none" />
                            </div>
                            <p class="text-right margin-top-40">
                                <asp:Button runat="server" Text="Aceptar" CssClass="btn btn-success" ID="btnAceptar" OnClick="btnAceptar_OnClick" />
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
