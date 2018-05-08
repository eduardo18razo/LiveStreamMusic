<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcAgregarCancionLista.ascx.cs" Inherits="LiveStreamMusic.Web.UserControls.Altas.UcAgregarCancionLista" %>
<%@ Register Src="~/UserControls/Altas/UcAltaListaReproduccion.ascx" TagPrefix="uc1" TagName="UcAltaListaReproduccion" %>
<asp:UpdatePanel runat="server" ID="upAgregarCancion">
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="hfIdCacnion" />
        <div class="modal-header">
            <asp:LinkButton class="close" ID="btnClose" OnClick="btnCancelar_OnClick" runat="server"><span aria-hidden="true">&times;</span></asp:LinkButton>
            <h6 class="modal-title" id="modal-new-ticket-label">
                <asp:Label runat="server" Text="Agregar a..." Font-Bold="true" />
            </h6>
        </div>
        <div class="modal-body">
            <div class="row">
                <div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div>
                            <div class="form-group margin-top">
                                Lista<br />
                                <asp:DropDownList runat="server" ID="ddlListas" CssClass="form-control" />
                            </div>
                            <p class="margin-top-20">
                                <asp:Button runat="server" ID="btnCrearLista" Text="Nueva Lista" OnClick="btnCrearLista_OnClick" CssClass="btn btn-sm btn-warning" />
                            </p>
                            <p class="text-right margin-top-40">
                                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-success" ID="btnAgregarCancion" OnClick="btnAgregarCancion_OnClick" />
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>


<div class="modal fade" id="modalAltaLista" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="modal-dialog modal-sm" style="width: 370px">
                <div class="modal-content">
                    <uc1:UcAltaListaReproduccion runat="server" ID="ucAltaListaReproduccion" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
