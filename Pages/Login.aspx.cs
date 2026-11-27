using CapaEntidad;
using CapaNegocio;
using System;
using System.Web.UI;

namespace GYMFIT.Pages
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
                Response.Redirect("~/Default.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            var bl = new UsuarioBL();
            Usuario u = bl.Login(txtUsuario.Text, txtClave.Text);

            if (u == null)
            {
                lblError.Text = "Usuario o contraseña incorrectos.";
                lblError.Visible = true;
                return;
            }

            Session["Usuario"] = u;
            Session["IdUsuario"] = u.IdUsuario;
            Session["Nombre"] = u.NombreCompleto;
            Session["Rol"] = u.NombreRol;
            Session["IdRol"] = u.IdRol;

            Response.Redirect("~/Default.aspx");
        }
    }
}
