using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GYMFIT
{
    public partial class Rutinas : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                foreach (var c in DatosDemo.Clientes.Where(x => x.Dias >= 0)) ddlCliente.Items.Add(new ListItem(c.Nombre + " · " + c.Documento, c.Documento));
        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            ClienteDemo c = DatosDemo.Clientes.FirstOrDefault(x => x.Documento == ddlCliente.SelectedValue);
            if (c == null) { Ui.Msg(lblMensaje, false, "Selecciona un cliente activo."); return; }
            if (txtDescripcion.Text.Trim() == "") { Ui.Msg(lblMensaje, false, "Escribe la descripción de la rutina."); return; }
            DatosDemo.Rutinas.Insert(0, new RutinaDemo { Cliente = c.Nombre, Objetivo = ddlObjetivo.SelectedValue, Descripcion = txtDescripcion.Text.Trim(), Observaciones = txtObservaciones.Text.Trim() });
            txtDescripcion.Text = txtObservaciones.Text = "";
            Ui.Msg(lblMensaje, true, "Rutina asignada.");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            litRutinas.Text = DatosDemo.Rutinas.Count == 0 ? "<p class=\"empty\">No hay rutinas asignadas aún.</p>"
                : string.Join("", DatosDemo.Rutinas.Select(r => "<p><b>" + Ui.Esc(r.Cliente) + "</b> · " + Ui.Esc(r.Objetivo) + "<br><span class=\"mut\">" + Ui.Esc(r.Descripcion) + (r.Observaciones != "" ? " · Obs: " + Ui.Esc(r.Observaciones) : "") + "</span></p>"));
        }
    }
}
