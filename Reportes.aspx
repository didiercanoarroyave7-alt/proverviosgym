<%@ Page Title="Reportes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="GYMFIT.Reportes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="card"><div class="row"><div><label for="txtDesde">Desde</label><asp:TextBox ID="txtDesde" runat="server" ClientIDMode="Static" TextMode="Date" /></div><div><label for="txtHasta">Hasta</label><asp:TextBox ID="txtHasta" runat="server" ClientIDMode="Static" TextMode="Date" /></div><asp:Button ID="btnFiltrar" runat="server" CssClass="btn" Text="Filtrar" OnClick="btnFiltrar_Click" /></div></div>
<div class="grid"><asp:Literal ID="litResumen" runat="server" /></div>
</asp:Content>
