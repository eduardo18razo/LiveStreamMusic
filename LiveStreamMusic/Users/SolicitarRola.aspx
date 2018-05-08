<%@ Page Title="" Language="C#" MasterPageFile="~/WebSite.Master" AutoEventWireup="true" CodeBehind="SolicitarRola.aspx.cs" Inherits="LiveStreamMusic.Web.Users.SolicitarRola" %>
<%@ Register Src="~/UserControls/Altas/UcAltaSolicitudCancion.ascx" TagPrefix="uc1" TagName="UcAltaSolicitudCancion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="webSiteHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="webSiteContent" runat="server">
    <div style="height: 100%; padding-left: 5%; padding-right: 5%; padding-top: 1%; padding-bottom: 1%; -ms-opacity: .9; opacity: .9">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <uc1:UcAltaSolicitudCancion runat="server" ID="UcAltaSolicitudCancion" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
