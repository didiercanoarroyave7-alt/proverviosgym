using CapaNegocio;
using System;
using System.Web.UI;

namespace GYMFIT.Pages
{
    public partial class ControlAcceso : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Pages/Login.aspx");
        }

        protected void btnValidar_Click(object sender, EventArgs e)
        {
            var bl = new MembresiaBL();
            var (permitido, mensaje, mem) = bl.ValidarAcceso(txtDocumento.Text);

            pnlResultado.Visible = true;
            pnlResultado.CssClass = permitido ? "alert alert-success text-center py-4" : "alert alert-danger text-center py-4";
            litEstado.Text = permitido ? "✓ ACCESO PERMITIDO" : "✗ ACCESO DENEGADO";
            litDetalle.Text = mensaje;
            litCliente.Text = mem != null ? $"{mem.NombreCliente} · {mem.DocumentoCliente}" : "";
            txtDocumento.Text = "";
            txtDocumento.Focus();
        }
    }
}
