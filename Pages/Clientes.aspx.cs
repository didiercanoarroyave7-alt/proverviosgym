using CapaEntidad;
using CapaNegocio;
using System;
using System.Web.UI;

namespace GYMFIT.Pages
{
    public partial class Clientes : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Pages/Login.aspx");

            if (!IsPostBack)
                CargarClientes();
        }

        private void CargarClientes()
        {
            var bl = new ClienteBL();
            gvClientes.DataSource = bl.Listar();
            gvClientes.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var cliente = new Cliente
            {
                Documento = txtDocumento.Text.Trim(),
                NombreCompleto = txtNombre.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                ContactoEmergencia = txtContacto.Text.Trim(),
                TelefonoEmergencia = txtTelEmergencia.Text.Trim()
            };

            var bl = new ClienteBL();
            string resultado = bl.Registrar(cliente);

            lblMsg.Visible = true;
            if (resultado == "OK")
            {
                lblMsg.CssClass = "alert alert-success d-block";
                lblMsg.Text = "Cliente registrado correctamente.";
                txtDocumento.Text = txtNombre.Text = txtTelefono.Text = txtContacto.Text = txtTelEmergencia.Text = "";
                CargarClientes();
            }
            else
            {
                lblMsg.CssClass = "alert alert-danger d-block";
                lblMsg.Text = resultado;
            }
        }
    }
}
