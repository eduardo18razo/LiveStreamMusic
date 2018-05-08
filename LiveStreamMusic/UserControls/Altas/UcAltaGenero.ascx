<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcAltaGenero.ascx.cs" Inherits="LiveStreamMusic.Web.UserControls.Altas.UcAltaGenero" %>
<%--<asp:UpdatePanel ID="upAltaArea" runat="server">
    <ContentTemplate>--%>
<header id="panelAlerta" runat="server" visible="false">
    <div class="alert alert-danger" style="background: transparent">
        <asp:Repeater runat="server" ID="rptErrorGeneral">
            <ItemTemplate>
                <ul>
                    <li><%# Container.DataItem %></li>
                </ul>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</header>
<div class="panel panel-primary">
    <div class="panel-heading">
        <h3>Nuevo Genero</h3>
    </div>
    <div class="panel-body">
        <div class="form-inline">
            <asp:Label runat="server" CssClass="control-label" Text="Genero" />
            <asp:TextBox runat="server" ID="txtGenero" CssClass="form-control" />
        </div>
    </div>
    <div class="panel-footer" style="text-align: center">
        <asp:Button runat="server" Text="Agregar" CssClass="btn btn-success" ID="btnGuardarGenero" OnClick="btnGuardarGenero_OnClick" />
        <asp:Button runat="server" CssClass="btn btn-danger" Text="Cancelar" ID="btnCancelarGenero" OnClick="btnCancelarGenero_OnClick" />
    </div>
</div>

<%-- </ContentTemplate>
</asp:UpdatePanel>--%>