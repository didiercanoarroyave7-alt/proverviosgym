using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaDatos
{
    public class UsuarioDAL
    {
        // Validar credenciales para iniciar sesión
        public Usuario ValidarLogin(string nombreUsuario, string contrasena)
        {
            Usuario usuario = null;
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"SELECT u.IdUsuario, u.Documento, u.NombreCompleto, u.NombreUsuario, u.IdRol, r.NombreRol 
                                 FROM Usuarios u
                                 INNER JOIN Roles r ON u.IdRol = r.IdRol
                                 WHERE u.NombreUsuario = @user AND u.ContrasenaHash = @pass AND u.Estado = 1";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@user", nombreUsuario);
                cmd.Parameters.AddWithValue("@pass", contrasena);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        usuario = new Usuario
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            Documento = dr["Documento"].ToString(),
                            NombreCompleto = dr["NombreCompleto"].ToString(),
                            NombreUsuario = dr["NombreUsuario"].ToString(),
                            IdRol = Convert.ToInt32(dr["IdRol"]),
                            NombreRol = dr["NombreRol"].ToString()
                        };
                    }
                }
            }
            return usuario;
        }



        // Listar todos los empleados con el nombre de su rol
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"SELECT u.IdUsuario, u.Documento, u.NombreCompleto, u.Telefono, u.NombreUsuario, u.IdRol, r.NombreRol, u.Estado 
                                 FROM Usuarios u
                                 INNER JOIN Roles r ON u.IdRol = r.IdRol";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Usuario
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            Documento = dr["Documento"].ToString(),
                            NombreCompleto = dr["NombreCompleto"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            NombreUsuario = dr["NombreUsuario"].ToString(),
                            IdRol = Convert.ToInt32(dr["IdRol"]),
                            NombreRol = dr["NombreRol"].ToString(),
                            Estado = Convert.ToBoolean(dr["Estado"])
                        });
                    }
                }
            }
            return lista;
        }

        // Listar roles para llenar DropDownList en el formulario de empleados
        public List<Rol> ListarRoles()
        {
            List<Rol> lista = new List<Rol>();
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT IdRol, NombreRol, Descripcion FROM Roles";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Rol
                        {
                            IdRol = Convert.ToInt32(dr["IdRol"]),
                            NombreRol = dr["NombreRol"].ToString(),
                            Descripcion = dr["Descripcion"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        // Registrar nuevo empleado
        public bool Registrar(Usuario obj)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"INSERT INTO Usuarios (Documento, NombreCompleto, Telefono, NombreUsuario, ContrasenaHash, IdRol, Estado)
                                 VALUES (@Doc, @Nombre, @Tel, @User, @Pass, @IdRol, 1)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Doc", obj.Documento);
                cmd.Parameters.AddWithValue("@Nombre", obj.NombreCompleto);
                cmd.Parameters.AddWithValue("@Tel", (object)obj.Telefono ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@User", obj.NombreUsuario);
                cmd.Parameters.AddWithValue("@Pass", obj.ContrasenaHash);
                cmd.Parameters.AddWithValue("@IdRol", obj.IdRol);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
