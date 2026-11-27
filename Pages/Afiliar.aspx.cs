using CapaEntidad;
using CapaNegocio;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GYMFIT.Pages
{
    public partial class Afiliar : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("~/Pages/Login.aspx");

            if (!IsPostBack)
            {
                var planBl = new PlanBL();
                ddlPlanes.DataSource = planBl.Listar(true);
                ddlPlanes.DataBind();
                ddlPlanes.Items.Insert(0, new ListItem("-- Seleccione plan --", "0"));
                txtFechaInicio.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            var bl = new ClienteBL();
            Cliente c = bl.BuscarPorDocumento(txtDocumento.Text);
            if (c == null)
            {
                litCliente.Text = "<span class='text-danger'>Cliente no encontrado. Regístrelo primero.</span>";
                hfIdCliente.Value = "";
                btnAfiliar.Enabled = false;
            }
            else
            {
                litCliente.Text = $"<span class='text-success'>{c.NombreCompleto}</span>";
                hfIdCliente.Value = c.IdCliente.ToString();
                btnAfiliar.Enabled = true;
            }
        }

        protected void btnAfiliar_Click(object sender, EventArgs e)
        {
            int idCliente, idPlan, idUsuario;
            if (!int.TryParse(hfIdCliente.Value, out idCliente) || idCliente <= 0)
            {
                Mostrar("Busque y seleccione un cliente válido.", false);
                return;
            }
            if (!int.TryParse(ddlPlanes.SelectedValue, out idPlan) || idPlan <= 0)
            {
                Mostrar("Seleccione un plan.", false);
                return;
            }
            idUsuario = Convert.ToInt32(Session["IdUsuario"]);

            DateTime fecha;
            if (!DateTime.TryParse(txtFechaInicio.Text, out fecha))
                fecha = DateTime.Today;

            var bl = new MembresiaBL();
            string r = bl.Afiliar(idCliente, idPlan, fecha, idUsuario, ddlMetodo.SelectedValue);

            if (r == "OK")
            {
                Mostrar("Membresía y pago registrados correctamente. Fecha de vencimiento calculada automáticamente.", true);
                hfIdCliente.Value = "";
                litCliente.Text = "";
                txtDocumento.Text = "";
                btnAfiliar.Enabled = false;
            }
            else
            {
                Mostrar(r, false);
            }
        }

        private void Mostrar(string msg, bool ok)
        {
            lblMsg.Visible = true;
            lblMsg.CssClass = ok ? "alert alert-success d-block" : "alert alert-danger d-block";
            lblMsg.Text = msg;
        }
    }
}
