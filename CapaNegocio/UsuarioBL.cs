using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class UsuarioBL
    {
        private readonly UsuarioDAL dal = new UsuarioDAL();

        public Usuario Login(string usuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
                return null;
            return dal.ValidarLogin(usuario.Trim(), contrasena);
        }

        public List<Usuario> Listar()
        {
            return dal.Listar();
        }
    }
}
