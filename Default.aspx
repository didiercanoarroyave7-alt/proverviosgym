<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GYMFIT._Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="grid g4"><asp:Literal ID="litStats" runat="server" /></div>
<div class="card"><h3>Para avisar hoy</h3><asp:Literal ID="litPorVencer" runat="server" /></div>
</asp:Content>
