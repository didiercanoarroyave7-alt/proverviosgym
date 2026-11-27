<%@ Page Title="Control de acceso" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Acceso.aspx.cs" Inherits="GYMFIT.Acceso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="card"><div class="row"><div><label for="txtDocumento">Documento del cliente</label><asp:TextBox ID="txtDocumento" runat="server" ClientIDMode="Static" inputmode="numeric" placeholder="Ej. 1017001" /></div><asp:Button ID="btnVerificar" runat="server" CssClass="btn" Text="Verificar" OnClick="btnVerificar_Click" /></div><asp:Literal ID="litResultado" runat="server" /></div>
<div class="card"><h3>Últimos ingresos</h3><asp:Literal ID="litIngresos" runat="server" /></div>
</asp:Content>
