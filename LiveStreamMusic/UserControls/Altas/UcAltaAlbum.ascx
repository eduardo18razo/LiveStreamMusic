<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcAltaAlbum.ascx.cs" Inherits="LiveStreamMusic.Web.UserControls.Altas.UcAltaAlbum" %>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="hfIdArtista"/>
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
                <h3>Nuevo Album</h3>
            </div>
            <div class="panel-body">
                <div class="form-inline">
                    <asp:Label runat="server" CssClass="control-label" Text="Genero"></asp:Label>
                    <asp:TextBox runat="server" ID="txtAlbum" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="panel-footer" style="text-align: center">
                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-success" ID="btnGuardarAlbum" OnClick="btnGuardarAlbum_OnClick" />
                <asp:Button runat="server" CssClass="btn btn-danger" Text="Cancelar" ID="btnCancelarAlbum" OnClick="btnCancelarAlbum_OnClick" />
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>