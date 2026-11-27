using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GYMFIT
{
    public partial class Clientes : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            pnlForm.Visible = Ui.PuedeEditar();
            if (!IsPostBack)
            {
                ddlPlan.DataSource = DatosDemo.Planes; ddlPlan.DataTextField = "Nombre"; ddlPlan.DataValueField = "Nombre"; ddlPlan.DataBind();
                Enlazar();
            }
        }

        void Enlazar()
        {
            IEnumerable<ClienteDemo> l = DatosDemo.Clientes;
            if (Sesion.Rol == "Entrenador") l = l.Where(c => c.Dias >= 0); // el entrenador solo ve clientes activos
            string q = txtBuscar.Text.Trim().ToLower();
            if (q != "") l = l.Where(c => (c.Nombre + c.Documento).ToLower().Contains(q));
            var lista = l.ToList();
            rptClientes.DataSource = lista; rptClientes.DataBind();
            litVacio.Text = lista.Count == 0 ? "<p class=\"empty\">No se encontró ningún cliente con esa búsqueda.</p>" : "";
        }

        void Limpiar()
        {
            hidEdicion.Value = ""; txtDocumento.Enabled = true; ddlPlan.Enabled = true;
            txtDocumento.Text = txtNombre.Text = txtTelefono.Text = txtContacto.Text = txtTelContacto.Text = "";
            btnGuardar.Text = "Registrar y cobrar";
        }

        protected void btnBuscar_Click(object sender, EventArgs e) { Enlazar(); }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool edit = hidEdicion.Value != "";
            string doc = edit ? hidEdicion.Value : txtDocumento.Text.Trim(), nom = txtNombre.Text.Trim(), tel = txtTelefono.Text.Trim();
            if (doc == "" || nom == "" || tel == "") { Ui.Msg(lblMensaje, false, "Completa documento, nombre y teléfono."); return; }
            if (!doc.All(char.IsDigit)) { Ui.Msg(lblMensaje, false, "El documento solo debe tener números."); return; }
            if (edit)
            {
                ClienteDemo c = DatosDemo.Clientes.First(x => x.Documento == doc);
                c.Nombre = nom; c.Telefono = tel; c.Contacto = txtContacto.Text.Trim(); c.TelContacto = txtTelContacto.Text.Trim();
                Ui.Msg(lblMensaje, true, "Cambios guardados.");
            }
            else
            {
                if (DatosDemo.Clientes.Any(x => x.Documento == doc)) { Ui.Msg(lblMensaje, false, "Ese documento ya está registrado."); return; }
                PlanDemo plan = DatosDemo.Planes.First(p => p.Nombre == ddlPlan.SelectedValue);
                var nuevo = new ClienteDemo { Documento = doc, Nombre = nom, Telefono = tel, Contacto = txtContacto.Text.Trim(), TelContacto = txtTelContacto.Text.Trim(), FechaFin = DateTime.Today };
                DatosDemo.Clientes.Insert(0, nuevo);
                DatosDemo.Cobrar(nuevo, plan, "Efectivo");
                Ui.Msg(lblMensaje, true, "Cliente registrado y cobrado. Vence el " + nuevo.FechaFin.ToString("dd/MM/yyyy") + ".");
            }
            Limpiar(); Enlazar();
        }

        protected void rptClientes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string doc = (string)e.CommandArgument;
            ClienteDemo c = DatosDemo.Clientes.FirstOrDefault(x => x.Documento == doc);
            if (c == null) return;
            if (e.CommandName == "eliminar")
            {
                DatosDemo.Clientes.Remove(c);
                if (hidEdicion.Value == doc) Limpiar();
                Ui.Msg(lblMensaje, true, "Cliente eliminado.");
            }
            else if (e.CommandName == "editar")
            {
                hidEdicion.Value = doc; txtDocumento.Text = c.Documento; txtDocumento.Enabled = false;
                txtNombre.Text = c.Nombre; txtTelefono.Text = c.Telefono; txtContacto.Text = c.Contacto; txtTelContacto.Text = c.TelContacto;
                ddlPlan.Enabled = false; btnGuardar.Text = "Guardar cambios";
            }
            Enlazar();
        }
    }
}
