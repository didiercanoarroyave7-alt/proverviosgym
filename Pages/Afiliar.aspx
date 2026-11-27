<%@ Page Title="Afiliar membresía" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Afiliar.aspx.cs" Inherits="GYMFIT.Pages.Afiliar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-4">Afiliar / renovar membresía</h2>

    <div class="card shadow-sm" style="max-width: 500px;">
        <div class="card-body">
            <asp:Label ID="lblMsg" runat="server" CssClass="alert d-block" Visible="false" />

            <div class="mb-3">
                <label class="form-label">Documento del cliente *</label>
                <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" />
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary btn-sm mt-1" OnClick="btnBuscar_Click" />
                <asp:Literal ID="litCliente" runat="server" />
            </div>

            <asp:HiddenField ID="hfIdCliente" runat="server" />

            <div class="mb-3">
                <label class="form-label">Plan *</label>
                <asp:DropDownList ID="ddlPlanes" runat="server" CssClass="form-select" DataTextField="NombrePlan" DataValueField="IdPlan" />
            </div>

            <div class="mb-3">
                <label class="form-label">Fecha inicio</label>
                <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" TextMode="Date" />
            </div>

            <div class="mb-3">
                <label class="form-label">Método de pago</label>
                <asp:DropDownList ID="ddlMetodo" runat="server" CssClass="form-select">
                    <asp:ListItem>Efectivo</asp:ListItem>
                    <asp:ListItem>Transferencia</asp:ListItem>
                    <asp:ListItem>Tarjeta</asp:ListItem>
                </asp:DropDownList>
            </div>

            <asp:Button ID="btnAfiliar" runat="server" Text="Registrar membresía y pago" CssClass="btn btn-primary w-100" OnClick="btnAfiliar_Click" Enabled="false" />
        </div>
    </div>
</asp:Content>
