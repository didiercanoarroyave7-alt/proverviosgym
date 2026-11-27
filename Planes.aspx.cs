using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GYMFIT
{
    public partial class Planes : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            pnlAdmin.Visible = Sesion.Rol == "Administrador";
            if (!IsPostBack) Cargar();
        }

        void Cargar()
        {
            ddlCliente.Items.Clear(); ddlPlanCobro.Items.Clear();
            foreach (var c in DatosDemo.Clientes) ddlCliente.Items.Add(new ListItem(c.Nombre + " · " + c.Documento, c.Documento));
            foreach (var p in DatosDemo.Planes) ddlPlanCobro.Items.Add(new ListItem(p.Nombre + " · " + Ui.Dinero(p.Precio), p.Nombre));
        }

        protected void btnGuardarPlan_Click(object sender, EventArgs e)
        {
            int dias; decimal precio; string n = txtNombrePlan.Text.Trim();
            if (n == "" || !int.TryParse(txtDias.Text, out dias) || !decimal.TryParse(txtPrecio.Text, out precio) || dias <= 0 || precio <= 0)
            { Ui.Msg(lblMsgPlan, false, "Escribe un nombre, y días y precio mayores a cero."); return; }
            DatosDemo.Planes.Add(new PlanDemo { Nombre = n, Dias = dias, Precio = precio });
            txtNombrePlan.Text = txtDias.Text = txtPrecio.Text = "";
            Ui.Msg(lblMsgPlan, true, "Plan creado. Ya se puede asignar a los clientes.");
            Cargar();
        }

        protected void btnCobrar_Click(object sender, EventArgs e)
        {
            ClienteDemo c = DatosDemo.Clientes.FirstOrDefault(x => x.Documento == ddlCliente.SelectedValue);
            PlanDemo p = DatosDemo.Planes.FirstOrDefault(x => x.Nombre == ddlPlanCobro.SelectedValue);
            if (c == null || p == null) { Ui.Msg(lblMsgCobro, false, "Selecciona un cliente y un plan."); return; }
            DatosDemo.Cobrar(c, p, ddlMetodo.SelectedValue);
            Ui.Msg(lblMsgCobro, true, "Cobro registrado. La membresía vence el " + c.FechaFin.ToString("dd/MM/yyyy") + ".");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            litPlanes.Text = string.Join("", DatosDemo.Planes.Select(p => "<div class=\"card\"><h3>" + Ui.Esc(p.Nombre) + "</h3><div class=\"price\">" + Ui.Dinero(p.Precio) + "</div><div class=\"mut\">" + p.Dias + (p.Dias > 1 ? " días" : " día") + " de acceso</div></div>"));
            litPagos.Text = Ui.Tabla(new[] { "Fecha", "Cliente", "Plan", "Método", "Monto" },
                DatosDemo.Pagos.Take(8).Select(p => new[] { p.Fecha.ToString("dd/MM/yyyy"), Ui.Esc(p.Cliente), Ui.Esc(p.Plan), Ui.Esc(p.Metodo), Ui.Dinero(p.Monto) }).ToList(), "Todavía no hay cobros.");
        }
    }
}
