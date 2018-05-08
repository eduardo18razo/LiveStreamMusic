<%@ Page Title="" Language="C#" MasterPageFile="~/Publico.Master" AutoEventWireup="true" CodeBehind="Identificar.aspx.cs" Inherits="LiveStreamMusic.Web.Identificar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 100%; margin: 0 auto; margin-top: 4%; padding-top: 0%; height: 89%; position: absolute">
        <asp:UpdatePanel ID="upGeneral" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <header class="" id="panelAlertaGeneral" runat="server" visible="False">
                    <div class="alert alert-danger">
                        <div>
                            <div style="float: left">
                                <asp:Image runat="server" ImageUrl="~/Images/error.jpg" />
                            </div>
                            <div style="float: left">
                                <h3>Error</h3>
                            </div>
                            <div class="clearfix clear-fix" />
                        </div>
                        <hr />
                        <asp:Repeater runat="server" ID="rptErrorGeneral">
                            <ItemTemplate>
                                <%# Eval("Detalle")  %>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </header>
                <div class="panel panel-primary" style="width: 750px; margin: 0 auto">
                    <div class="panel-heading">
                        Recuperar cuenta
                    </div>

                    <div class="panel-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Correo electrónico, teléfono, nombre de usuario" CssClass="col-sm-4" />
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" Style="text-transform: none" />
                                </div>
                                <asp:Button runat="server" ID="btnBuscar" Text="Buscar" CssClass="btn btn-success" OnClick="btnBuscar_OnClick" />
                            </div>
                            <div class="form-horizontal">
                                <asp:RadioButtonList runat="server" ID="rbtnLstUsuarios" OnSelectedIndexChanged="rbtnLstUsuarios_OnSelectedIndexChanged" AutoPostBack="True" />
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer">
                        <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar_OnClick" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
