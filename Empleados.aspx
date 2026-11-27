<%@ Page Title="Empleados y roles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Empleados.aspx.cs" Inherits="GYMFIT.Empleados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="card"><h3>Registrar empleado</h3><div class="grid g2" style="margin-top:.6rem"><div><label for="txtDocumento">Documento</label><asp:TextBox ID="txtDocumento" runat="server" ClientIDMode="Static" inputmode="numeric" /></div><div><label for="txtNombre">Nombre completo</label><asp:TextBox ID="txtNombre" runat="server" ClientIDMode="Static"  /></div><div><label for="txtUsuario">Usuario</label><asp:TextBox ID="txtUsuario" runat="server" ClientIDMode="Static"  /></div><div><label for="txtClave">Contraseña</label><asp:TextBox ID="txtClave" runat="server" ClientIDMode="Static" TextMode="Password" /></div><div><label for="ddlRol">Rol</label><asp:DropDownList ID="ddlRol" runat="server" ClientIDMode="Static"></asp:DropDownList></div></div><p><asp:Button ID="btnGuardar" runat="server" CssClass="btn" Text="Registrar empleado" OnClick="btnGuardar_Click" /></p><asp:Label ID="lblMensaje" runat="server" CssClass="msg" /></div>
<div class="card"><h3>Empleados</h3><asp:Repeater ID="rptEmp" runat="server" OnItemCommand="rptEmp_ItemCommand">
<HeaderTemplate><div class="wrap"><table><tr><th>Usuario</th><th>Nombre</th><th>Rol</th><th></th></tr></HeaderTemplate>
<ItemTemplate><tr><td><%#: Eval("Usuario") %></td><td><%#: Eval("Nombre") %></td><td><%#: Eval("Rol") %></td><td><asp:LinkButton runat="server" CommandName="eliminar" CommandArgument='<%# Eval("Usuario") %>' CssClass="chip" Text="Eliminar" /></td></tr></ItemTemplate>
<FooterTemplate></table></div></FooterTemplate>
</asp:Repeater></div>
</asp:Content>
