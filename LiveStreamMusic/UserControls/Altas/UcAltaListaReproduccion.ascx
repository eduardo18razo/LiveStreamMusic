<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcAltaListaReproduccion.ascx.cs" Inherits="LiveStreamMusic.Web.UserControls.Altas.UcAltaListaReproduccion" %>
<asp:UpdatePanel ID="upAltaArea" runat="server">
    <ContentTemplate>
        <div class="modal-header">
            <asp:LinkButton class="close" ID="btnClose" OnClick="btnCancelarLista_OnClick" runat="server"><span aria-hidden="true">&times;</span></asp:LinkButton>
            <h6 class="modal-title" id="modal-new-ticket-label">
                <asp:Label runat="server" Text="Nueva Lista de Reproducción" Font-Bold="true" />
            </h6>
        </div>
        <div class="modal-body">
            <div class="row">
                <div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div>
                            <div class="form-group margin-top">
                                <asp:Label runat="server" CssClass="control-label" Text="Nombre" />
                    <asp:TextBox runat="server" ID="txtnombreLista" CssClass="form-control" />
                            </div>
                            <p class="text-right margin-top-40">
                                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-success" ID="btnGuardarLista" OnClick="btnGuardarLista_OnClick" />
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
