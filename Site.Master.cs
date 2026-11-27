using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace GYMFIT
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            string rol = Sesion.Rol;
            if (rol == null) { Response.Redirect("~/Login.aspx"); return; }
            string pag = System.IO.Path.GetFileName(Request.AppRelativeCurrentExecutionFilePath);
            if (!Sesion.Permitida(rol, pag)) Response.Redirect("~/" + Sesion.Menus[rol][0]);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string rol = Sesion.Rol;
            if (rol == null) return;
            litUsuario.Text = Ui.Esc(Sesion.Nombre) + " · " + rol;
            litTitulo.Text = Ui.Esc(Page.Title);
            string actual = System.IO.Path.GetFileName(Request.AppRelativeCurrentExecutionFilePath);
            var sb = new StringBuilder();
            foreach (string p in Sesion.Menus[rol])
                sb.AppendFormat("<a class=\"{0}\" href=\"{1}\">{2}</a>", p == actual ? "on" : "", p, Sesion.Titulos[p]);
            litMenu.Text = sb.ToString();
        }
    }
}
