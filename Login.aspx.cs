using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GYMFIT
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString["salir"] != null) Session.Abandon();
        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            // TODO: reemplazar por la validación contra SQL Server (CapaNegocio) con contraseña con hash.
            UsuarioDemo u = DatosDemo.Validar(txtUsuario.Text.Trim().ToLower(), txtClave.Text);
            if (u == null)
            {
                lblError.Text = "Usuario o contraseña incorrectos. Revisa los datos e intenta de nuevo.";
                return;
            }
            Session["Usuario"] = u.Usuario; Session["Nombre"] = u.Nombre; Session["Rol"] = u.Rol;
            Response.Redirect("~/" + Sesion.Menus[u.Rol][0]);
        }
    }
}
