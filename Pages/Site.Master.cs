using System;
using System.Web.UI;

namespace GYMFIT
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
                return;
            }
            litUsuario.Text = Session["Nombre"] + " (" + Session["Rol"] + ")";
        }

        protected void lnkSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Pages/Login.aspx");
        }
    }
}
