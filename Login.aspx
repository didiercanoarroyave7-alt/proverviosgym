<%@ Page Title="Ingresar" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GYMFIT.Login" %>
<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, viewport-fit=cover" />
    <title>GYMFIT - Ingresar</title>
    <link href="https://fonts.googleapis.com/css2?family=Barlow+Condensed:wght@600;700&family=Barlow:wght@400;500;600&display=swap" rel="stylesheet" />
    <link href="~/Content/gymfit.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnEntrar">
        <div class="login"><div class="card">
            <div class="brand">GYM<b>FIT</b></div>
            <h1 style="margin:.6rem 0 1rem">Ingresa a tu turno</h1>
            <div class="grid">
                <div><label for="txtUsuario">Usuario</label><asp:TextBox ID="txtUsuario" runat="server" ClientIDMode="Static" autocomplete="username" /></div><div><label for="txtClave">Contraseña</label><asp:TextBox ID="txtClave" runat="server" ClientIDMode="Static" TextMode="Password" autocomplete="current-password" /></div><asp:Button ID="btnEntrar" runat="server" CssClass="btn" Text="Entrar" OnClick="btnEntrar_Click" />
            </div>
            <asp:Label ID="lblError" runat="server" CssClass="msg bad" />
            <p class="mut">Usuarios de prueba (clave 1234): admin, recepcion, entrenador, limpieza.</p>
        </div></div>
    </form>
</body>
</html>
