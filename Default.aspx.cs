using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GYMFIT
{
    public partial class _Default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            var act = DatosDemo.Clientes.Where(c => c.Dias >= 0).ToList();
            var por = act.Where(c => c.Dias <= 3).ToList();
            litStats.Text = Ui.Stat(act.Count.ToString(), "Clientes con plan vigente") + Ui.Stat(por.Count.ToString(), "Planes por vencer en 3 días")
                + Ui.Stat(Ui.Dinero(DatosDemo.Pagos.Sum(p => p.Monto)), "Total cobrado") + Ui.Stat(DatosDemo.Accesos.Count.ToString(), "Ingresos registrados");
            litPorVencer.Text = por.Count == 0 ? "<p class=\"empty\">Nadie vence en los próximos 3 días.</p>"
                : string.Join("", por.Select(c => "<p>" + Ui.Esc(c.Nombre) + " · " + Ui.Estado(c) + " · " + Ui.Esc(c.Telefono) + "</p>"));
        }
    }
}
