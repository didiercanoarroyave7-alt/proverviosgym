using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GYMFIT
{
    public partial class Novedades : Page
    {

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtDetalle.Text.Trim() == "") { Ui.Msg(lblMensaje, false, "Escribe el detalle de la novedad."); return; }
            DatosDemo.Novedades.Insert(0, new NovedadDemo { Tipo = ddlTipo.SelectedValue, Area = txtArea.Text.Trim(), Detalle = txtDetalle.Text.Trim(), Hora = DateTime.Now.ToString("HH:mm"), Usuario = Sesion.Nombre });
            txtArea.Text = txtDetalle.Text = "";
            Ui.Msg(lblMensaje, true, "Novedad registrada.");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            litNovedades.Text = DatosDemo.Novedades.Count == 0 ? "<p class=\"empty\">No hay novedades registradas hoy.</p>"
                : string.Join("", DatosDemo.Novedades.Select(n => "<p><b>" + Ui.Esc(n.Tipo) + "</b> · " + Ui.Esc(n.Area == "" ? "General" : n.Area) + " · <span class=\"mut\">" + n.Hora + " · " + Ui.Esc(n.Usuario) + "</span><br>" + Ui.Esc(n.Detalle) + "</p>"));
        }
    }
}
