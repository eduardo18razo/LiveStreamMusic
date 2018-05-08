<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcAdmonListas.ascx.cs" Inherits="LiveStreamMusic.Web.UserControls.Altas.UcAdmonListas" %>
<%@ Register Src="~/UserControls/Altas/UcAltaListaReproduccion.ascx" TagPrefix="uc1" TagName="UcAltaListaReproduccion" %>

<div style="height: 100%;">
            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                <ContentTemplate>
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
                    <div class="panel panel-warning">
                        <div class="panel panel-heading" style="text-align: center">
                            <h1>Listas de Reproduccion</h1>
                        </div>
                        <div class="panel panel-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Listas" CssClass="control-label col-sm-2" />
                                    <div class="col-sm-4">
                                        <asp:DropDownList runat="server" ID="ddlListas" CssClass="DropSelect" AutoPostBack="True" OnSelectedIndexChanged="ddlListas_OnSelectedIndexChanged" />
                                    </div>
                                    <asp:Button runat="server" CssClass="btn btn-sm btn-success" Text="Agregar" ID="btnAgregarLista" OnClick="btnAgregarLista_OnClick" />
                                </div>
                            </div>
                            <div class="panel-body">
                                <asp:Repeater runat="server" ID="rptContenidoLista">
                                    <ItemTemplate>
                                        <div class="row">
                                            <asp:Label runat="server" Text='<%#Eval("Cancion.Descripcion") %>'></asp:Label>
                                            <asp:LinkButton runat="server">
                                                <span class="glyphicon glyphicon-remove-circle"></span>
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
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
</div>
