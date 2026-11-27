using CapaEntidad;
using CapaNegocio;
using System;
using System.Web.UI;

namespace GYMFIT.Pages
{
    public partial class Planes : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Pages/Login.aspx");

            if (!IsPostBack)
                Cargar();
        }

        private void Cargar()
        {
            var bl = new PlanBL();
            gvPlanes.DataSource = bl.Listar(true);
            gvPlanes.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int dias;
            decimal precio;
            if (!int.TryParse(txtDias.Text, out dias) || !decimal.TryParse(txtPrecio.Text, out precio))
            {
                lblMsg.Visible = true;
                lblMsg.CssClass = "alert alert-danger d-block";
                lblMsg.Text = "Duración y precio deben ser números válidos.";
                return;
            }

            var plan = new Plan
            {
                NombrePlan = txtNombre.Text.Trim(),
                DuracionDias = dias,
                Precio = precio,
                Descripcion = txtDesc.Text.Trim()
            };

            var bl = new PlanBL();
            string r = bl.Registrar(plan);

            lblMsg.Visible = true;
            if (r == "OK")
            {
                lblMsg.CssClass = "alert alert-success d-block";
                lblMsg.Text = "Plan creado.";
                txtNombre.Text = txtDias.Text = txtPrecio.Text = txtDesc.Text = "";
                Cargar();
            }
            else
            {
                lblMsg.CssClass = "alert alert-danger d-block";
                lblMsg.Text = r;
            }
        }
    }
}
