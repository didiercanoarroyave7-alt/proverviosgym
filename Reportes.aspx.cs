using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GYMFIT
{
    public partial class Reportes : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { txtDesde.Text = DateTime.Today.AddDays(-30).ToString("yyyy-MM-dd"); txtHasta.Text = DateTime.Today.ToString("yyyy-MM-dd"); }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e) { }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            DateTime d, h;
            if (!DateTime.TryParse(txtDesde.Text, out d)) d = DateTime.Today.AddDays(-30);
            if (!DateTime.TryParse(txtHasta.Text, out h)) h = DateTime.Today;
            if (d > h) { litResumen.Text = "<p class=\"msg bad\">La fecha inicial no puede ser mayor que la final.</p>"; return; }
            var ps = DatosDemo.Pagos.Where(p => p.Fecha.Date >= d.Date && p.Fecha.Date <= h.Date).ToList();
            int act = DatosDemo.Clientes.Count(c => c.Dias >= 0), por = DatosDemo.Clientes.Count(c => c.Dias >= 0 && c.Dias <= 3);
            litResumen.Text = "<div class=\"grid g4\">" + Ui.Stat(Ui.Dinero(ps.Sum(p => p.Monto)), "Total recaudado (" + ps.Count + " pagos)") + Ui.Stat(act.ToString(), "Membresías activas") + Ui.Stat(por.ToString(), "Próximas a vencer") + "</div>"
                + "<div class=\"card\"><h3>Pagos del periodo</h3>" + Ui.Tabla(new[] { "Fecha", "Cliente", "Plan", "Monto" },
                ps.Select(p => new[] { p.Fecha.ToString("dd/MM/yyyy"), Ui.Esc(p.Cliente), Ui.Esc(p.Plan), Ui.Dinero(p.Monto) }).ToList(), "No hay pagos en ese rango de fechas.") + "</div>";
        }
    }
}
