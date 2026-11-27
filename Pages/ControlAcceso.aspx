<%@ Page Title="Control de Acceso" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ControlAcceso.aspx.cs" Inherits="GYMFIT.Pages.ControlAcceso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <h2 class="mb-4">Control de ingreso</h2>
            <div class="card shadow-sm">
                <div class="card-body">
                    <div class="input-group input-group-lg mb-3">
                        <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" placeholder="Número de documento del cliente" autofocus="autofocus" />
                        <asp:Button ID="btnValidar" runat="server" Text="Validar acceso" CssClass="btn btn-primary" OnClick="btnValidar_Click" />
                    </div>

                    <asp:Panel ID="pnlResultado" runat="server" Visible="false" CssClass="alert text-center py-4">
                        <h3 class="mb-2"><asp:Literal ID="litEstado" runat="server" /></h3>
                        <p class="mb-0"><asp:Literal ID="litDetalle" runat="server" /></p>
                        <p class="mt-2 text-muted small"><asp:Literal ID="litCliente" runat="server" /></p>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
