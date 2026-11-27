using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GYMFIT
{
    public partial class Acceso : Page
    {

        protected void btnVerificar_Click(object sender, EventArgs e)
        {
            string doc = txtDocumento.Text.Trim(), k, t, s;
            ClienteDemo c = DatosDemo.Clientes.FirstOrDefault(x => x.Documento == doc);
            if (doc == "") { k = "warn"; t = "Escribe un documento"; s = "Digita el número de documento del cliente."; }
            else if (c == null) { k = "bad"; t = "No puede entrar"; s = "Documento no registrado. Pide al cliente que pase por recepción."; }
            else if (c.Dias < 0) { k = "bad"; t = "No puede entrar"; s = Ui.Esc(c.Nombre) + " · Plan vencido hace " + (-c.Dias) + " días. Ofrece renovar."; }
            else { k = c.Dias <= 3 ? "warn" : "ok"; t = "Puede entrar"; s = Ui.Esc(c.Nombre) + " · Plan vigente, " + c.Dias + " días restantes" + (c.Dias <= 3 ? ". Avisa que vence pronto." : "."); }
            litResultado.Text = "<div class=\"slab " + k + "\"><div class=\"big\">" + t + "</div><p style=\"margin:.4rem 0 0\">" + s + "</p></div>";
            if (doc != "") DatosDemo.Accesos.Insert(0, new AccesoDemo { Hora = DateTime.Now.ToString("HH:mm"), Cliente = c != null ? c.Nombre : doc, Permitido = k != "bad" });
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            litIngresos.Text = Ui.Tabla(new[] { "Hora", "Cliente", "Resultado" },
                DatosDemo.Accesos.Take(6).Select(a => new[] { a.Hora, Ui.Esc(a.Cliente), "<span class=\"tag " + (a.Permitido ? "ok\">Permitido" : "bad\">Denegado") + "</span>" }).ToList(),
                "Aún no hay ingresos. Verifica un documento para empezar.");
        }
    }
}
