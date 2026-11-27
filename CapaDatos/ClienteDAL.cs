using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ClienteDAL
    {
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"SELECT IdCliente, Documento, NombreCompleto, Telefono, ContactoEmergencia, TelefonoEmergencia, FechaRegistro, Estado 
                                 FROM Clientes WHERE Estado = 1 ORDER BY IdCliente DESC";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Cliente
                        {
                            IdCliente = Convert.ToInt32(dr["IdCliente"]),
                            Documento = dr["Documento"].ToString(),
                            NombreCompleto = dr["NombreCompleto"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            ContactoEmergencia = dr["ContactoEmergencia"].ToString(),
                            TelefonoEmergencia = dr["TelefonoEmergencia"].ToString(),
                            FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]),
                            Estado = Convert.ToBoolean(dr["Estado"])
                        });
                    }
                }
            }
            return lista;
        }

        public Cliente BuscarPorDocumento(string documento)
        {
            Cliente cliente = null;
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"SELECT IdCliente, Documento, NombreCompleto, Telefono, ContactoEmergencia, TelefonoEmergencia, Estado 
                                 FROM Clientes WHERE Documento = @Doc";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Doc", documento);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        cliente = new Cliente
                        {
                            IdCliente = Convert.ToInt32(dr["IdCliente"]),
                            Documento = dr["Documento"].ToString(),
                            NombreCompleto = dr["NombreCompleto"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            ContactoEmergencia = dr["ContactoEmergencia"].ToString(),
                            TelefonoEmergencia = dr["TelefonoEmergencia"].ToString(),
                            Estado = Convert.ToBoolean(dr["Estado"])
                        };
                    }
                }
            }
            return cliente;
        }

        public bool Registrar(Cliente obj)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"INSERT INTO Clientes (Documento, NombreCompleto, Telefono, ContactoEmergencia, TelefonoEmergencia, Estado)
                                 VALUES (@Doc, @Nombre, @Tel, @Contacto, @TelEmergencia, 1)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Doc", obj.Documento);
                cmd.Parameters.AddWithValue("@Nombre", obj.NombreCompleto);
                cmd.Parameters.AddWithValue("@Tel", obj.Telefono);
                cmd.Parameters.AddWithValue("@Contacto", (object)obj.ContactoEmergencia ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TelEmergencia", (object)obj.TelefonoEmergencia ?? DBNull.Value);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
