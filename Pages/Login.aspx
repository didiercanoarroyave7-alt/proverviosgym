<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GYMFIT.Pages.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GYMFIT - Iniciar sesión</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body { background: linear-gradient(135deg, #1a1a2e 0%, #16213e 50%, #0f3460 100%); min-height: 100vh; }
        .login-card { max-width: 400px; margin: 80px auto; border-radius: 12px; box-shadow: 0 10px 40px rgba(0,0,0,.3); }
        .brand { font-weight: 700; letter-spacing: 2px; color: #e94560; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="card login-card">
                <div class="card-body p-4">
                    <h3 class="text-center brand mb-1">GYMFIT</h3>
                    <p class="text-center text-muted mb-4">Sistema de gestión de gimnasio</p>

                    <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger d-block" Visible="false" />

                    <div class="mb-3">
                        <label class="form-label">Usuario</label>
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="admin" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Contraseña</label>
                        <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password" placeholder="••••••" />
                    </div>
                    <asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="btn btn-danger w-100" OnClick="btnLogin_Click" />
                    
                    <hr />
                    <small class="text-muted">
                        Prueba: <strong>admin / admin123</strong> o <strong>recepcion / 1234</strong>
                    </small>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
