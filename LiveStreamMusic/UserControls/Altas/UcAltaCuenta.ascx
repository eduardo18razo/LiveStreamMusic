<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcAltaCuenta.ascx.cs" Inherits="LiveStreamMusic.Web.UserControls.Altas.UcAltaCuenta" %>
<asp:UpdatePanel runat="server" ID="updateAltaCuenta">
    <ContentTemplate>
        <div class="row">
            <div>
                <div class="row">
                    <div class="form-group">
                        <div class="col-sm-12 col-md-12 col-lg-12">
                            <label for="txtNombre" class="col-sm-12 col-md-12 col-lg-12">Nombre</label>
                            <asp:TextBox class="form-control" ID="txtNombre" ClientIDMode="Static" runat="server" onkeydown="return (event.keyCode!=13);" />
                        </div>
                        <div class="col-sm-12 col-md-12 col-lg-12">
                            <label for="txtApellido" class="col-sm-12 col-md-12 col-lg-12">Apellido Paterno</label>
                            <asp:TextBox class="form-control" ID="txtApellido" ClientIDMode="Static" runat="server" onkeydown="return (event.keyCode!=13);" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <label for="txtCorreo" class="col-sm-12 col-md-12 col-lg-12">Correo</label>
                    <asp:TextBox class="form-control" type="email" ID="txtCorreo" ClientIDMode="Static" runat="server" onkeydown="return (event.keyCode!=13);" />
                </div>
                <div class="row">
                    <label for="txtConfimacionCorreo" class="col-sm-12 col-md-12 col-lg-12">Confirma tu correo</label>
                    <asp:TextBox class="form-control" type="email" ID="txtConfimacionCorreo" ClientIDMode="Static" runat="server" onkeydown="return (event.keyCode!=13);" />
                </div>
                <div class="row">
                    <label for="txtPassword" class="col-sm-12 col-md-12 col-lg-12">Contraseña</label>
                    <asp:TextBox class="form-control" ID="txtPassword" ClientIDMode="Static" TextMode="Password" runat="server" onkeypress="return (event.keyCode!=13)" MaxLength="50" />
                </div>
                <div class="row">
                    <label for="txtConfirmaPassword" class="col-sm-12 col-md-12 col-lg-12">Confirma Contraseña</label>
                    <asp:TextBox class="form-control" ID="txtConfirmaPassword" ClientIDMode="Static" TextMode="Password" runat="server" onkeypress="return (event.keyCode!=13)" MaxLength="50" />
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
