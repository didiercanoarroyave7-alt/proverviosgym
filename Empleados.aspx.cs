using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GYMFIT
{
    public partial class Empleados : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { ddlRol.DataSource = Sesion.Menus.Keys; ddlRol.DataBind(); }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string doc = txtDocumento.Text.Trim(), nom = txtNombre.Text.Trim(), us = txtUsuario.Text.Trim().ToLower();
            if (doc == "" || nom == "" || us == "" || txtClave.Text == "") { Ui.Msg(lblMensaje, false, "Completa todos los campos."); return; }
            if (DatosDemo.Usuarios.Any(u => u.Usuario == us || u.Documento == doc)) { Ui.Msg(lblMensaje, false, "Ese usuario o documento ya existe."); return; }
            // TODO: guardar la contraseña con hash (SHA256/BCrypt) desde CapaNegocio.
            DatosDemo.Usuarios.Add(new UsuarioDemo { Documento = doc, Nombre = nom, Usuario = us, Clave = txtClave.Text, Rol = ddlRol.SelectedValue });
            txtDocumento.Text = txtNombre.Text = txtUsuario.Text = "";
            Ui.Msg(lblMensaje, true, "Empleado registrado.");
        }

        protected void rptEmp_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string us = (string)e.CommandArgument;
            if (us == Sesion.Usuario) { Ui.Msg(lblMensaje, false, "No puedes eliminar tu propio usuario."); return; }
            DatosDemo.Usuarios.RemoveAll(u => u.Usuario == us);
            Ui.Msg(lblMensaje, true, "Empleado eliminado.");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            rptEmp.DataSource = DatosDemo.Usuarios.ToList(); rptEmp.DataBind();
        }
    }
}
