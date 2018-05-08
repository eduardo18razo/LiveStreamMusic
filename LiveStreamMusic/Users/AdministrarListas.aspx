<%@ Page Title="" Language="C#" MasterPageFile="~/WebSite.Master" AutoEventWireup="true" CodeBehind="AdministrarListas.aspx.cs" Inherits="LiveStreamMusic.Web.Users.AdministrarListas" %>

<%@ Register Src="~/UserControls/Altas/UcAdmonListas.ascx" TagPrefix="uc1" TagName="UcAdmonListas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="webSiteHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="webSiteContent" runat="server">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <uc1:UcAdmonListas runat="server" ID="ucAdmonListas" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
