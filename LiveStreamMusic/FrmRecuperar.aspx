<%@ Page Title="" Language="C#" MasterPageFile="~/SitePublic.Master" AutoEventWireup="true" CodeBehind="FrmRecuperar.aspx.cs" Inherits="LiveStreamMusic.Web.FrmRecuperar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 100%; margin: 0 auto; margin-top: 4%; padding-top: 0%; height: 89%; position: absolute">
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
        <asp:HiddenField runat="server" ID="hfEsLink" Value="false" />
        <asp:HiddenField runat="server" ID="hfParametrosConfirmados" Value="false" />
        <asp:HiddenField runat="server" ID="hfValueNotivicacion" Value="false" />
        <div class="panel panel-primary" style="width: 750px; margin: 0 auto">
            <div class="panel-heading">
                <asp:Label runat="server" Text="¿Cómo quieres cambiar tu contraseña?"></asp:Label>
            </div>
            <div class="panel-body">
                <div class="form-inline" runat="server" id="divQuestion">
                    <asp:RadioButton runat="server" ID="rbtnCorreo" Text="Quiero recibir un correo con un enlace" GroupName="Options" CssClass="col-sm-12" OnCheckedChanged="rbtnCorreo_OnCheckedChanged" AutoPostBack="True" />
                    <asp:RadioButton runat="server" ID="rbtnSms" Text="Quiero recibir un SMS con mi codigo" GroupName="Options" CssClass="col-sm-12" OnCheckedChanged="rbtnSms_OnCheckedChanged" AutoPostBack="True" Visible="False" />
                    <asp:RadioButton runat="server" ID="rbtnPreguntas" Text="Quiero contestar preguntas" GroupName="Options" CssClass="col-sm-12" OnCheckedChanged="rbtnPreguntas_OnCheckedChanged" AutoPostBack="True" Visible="False" />
                </div>
                <div runat="server" id="divCodigoVerificacion" visible="False">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Ingresa el código de seguridad
                        </div>
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Comprueba si recibiste un correo electrónico con tu código, que debe tener cinco dígitos." CssClass="col-sm-12" />
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Te enviamos el código a:" CssClass="col-sm-12" />
                                </div>
                                <asp:RadioButtonList runat="server" ID="rbtnList" AutoPostBack="True" OnSelectedIndexChanged="rbtnList_OnSelectedIndexChanged" />
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <asp:TextBox runat="server" placeholder="#####" CssClass="form-control" ID="txtCodigo" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Label runat="server" Text=""></asp:Label>
                </div>
                <div runat="server" id="divPreguntas" visible="False">
                    <div class="form-horizontal">
                        <asp:Repeater runat="server" ID="rptPreguntas">
                            <ItemTemplate>
                                <div class="form-group">
                                    <asp:Label runat="server" Text='<%# Eval("Id") %>' ID="lblId" Visible="False" />
                                    <asp:Label runat="server" Text='<%# Eval("IdUsuario") %>' ID="lblIdUsuario" Visible="False" />
                                    <asp:Label runat="server" Text='<%# Eval("Pregunta") %>' class="col-xs-6 col-md-3" ID="lblPregunta" />
                                    <div class="col-sm-9">
                                        <asp:TextBox runat="server" ID="txtRespuesta" CssClass="form-control obligatorio" Style="text-transform: none" />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div runat="server" id="divChangePwd" visible="False">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <asp:Label runat="server" class="col-sm-2 control-label">Contraseña</asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox runat="server" ID="txtContrasena" type="password" CssClass="form-control obligatorio" Style="text-transform: none"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" class="col-sm-2 control-label">Confirmar Contraseña</asp:Label>
                            <div class="col-sm-10">
                                <asp:TextBox runat="server" ID="txtConfirmar" type="password" CssClass="form-control obligatorio" Style="text-transform: none"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-footer">
                <asp:Button runat="server" ID="btncontinuar" Text="Continuar" OnClick="btncontinuar_OnClick" CssClass="btn btn-success" />
                <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" OnClick="btnCancelar_OnClick" CssClass="btn btn-danger" />
            </div>
        </div>
    </div>
</asp:Content>
