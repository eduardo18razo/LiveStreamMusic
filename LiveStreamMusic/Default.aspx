<%@ Page Title="" Language="C#" MasterPageFile="~/Publico.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LiveStreamMusic.Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Nop</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="full">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>

                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <section class="module">
                            <div class="module-inner">
                                <div class="module-content">
                                    <div runat="server" id="divCategoriaReference">
                                        <h4 class="text-center title">Encuentra toda tu musica
                                            <asp:HyperLink runat="server" Text="Registrate" NavigateUrl="~/Registro.aspx"></asp:HyperLink>
                                        </h4>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <section class="module">
                            <div class="module-inner">
                                <div class="module-content">
                                    <div style="height: 415px;">
                                        <asp:Image runat="server" style="height: 415px;" CssClass="col-lg-12" ImageUrl="https://previews.123rf.com/images/dorian2013/dorian20131704/dorian2013170400004/75711169-m%C3%BAsica-collage-en-una-gran-pared-de-ladrillo-graffiti.jpg" />
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

</asp:Content>
