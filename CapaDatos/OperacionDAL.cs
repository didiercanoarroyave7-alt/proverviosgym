using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class OperacionDAL
    {
        // --- MÓDULO ENTRENADOR (RUTINAS) ---
        public bool GuardarRutina(Rutina obj)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"INSERT INTO Rutinas (IdCliente, IdEntrenador, Objetivo, DescripcionRutina, ObservacionesFisicas)
                                 VALUES (@IdCliente, @IdEntrenador, @Objetivo, @Desc, @Obs)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdCliente", obj.IdCliente);
                cmd.Parameters.AddWithValue("@IdEntrenador", obj.IdEntrenador);
                cmd.Parameters.AddWithValue("@Objetivo", obj.Objetivo);
                cmd.Parameters.AddWithValue("@Desc", obj.DescripcionRutina);
                cmd.Parameters.AddWithValue("@Obs", (object)obj.ObservacionesFisicas ?? DBNull.Value);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<Rutina> ListarRutinasPorCliente(int idCliente)
        {
            List<Rutina> lista = new List<Rutina>();
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"SELECT r.IdRutina, r.Objetivo, r.DescripcionRutina, r.ObservacionesFisicas, r.FechaAsignacion, u.NombreCompleto AS Entrenador
                                 FROM Rutinas r
                                 INNER JOIN Usuarios u ON r.IdEntrenador = u.IdUsuario
                                 WHERE r.IdCliente = @IdCliente
                                 ORDER BY r.FechaAsignacion DESC";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Rutina
                        {
                            IdRutina = Convert.ToInt32(dr["IdRutina"]),
                            Objetivo = dr["Objetivo"].ToString(),
                            DescripcionRutina = dr["DescripcionRutina"].ToString(),
                            ObservacionesFisicas = dr["ObservacionesFisicas"].ToString(),
                            FechaAsignacion = Convert.ToDateTime(dr["FechaAsignacion"]),
                            NombreEntrenador = dr["Entrenador"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        // --- MÓDULO LIMPIADORA / TURNOS Y NOVEDADES ---
        public bool RegistrarNovedad(NovedadPersonal obj)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"INSERT INTO NovedadesPersonal (IdUsuario, TipoRegistro, Area, Detalle)
                                 VALUES (@IdUsuario, @Tipo, @Area, @Detalle)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                cmd.Parameters.AddWithValue("@Tipo", obj.TipoRegistro);
                cmd.Parameters.AddWithValue("@Area", (object)obj.Area ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Detalle", obj.Detalle);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<NovedadPersonal> ListarNovedades()
        {
            List<NovedadPersonal> lista = new List<NovedadPersonal>();
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"SELECT TOP 50 n.IdNovedad, u.NombreCompleto, r.NombreRol, n.TipoRegistro, n.Area, n.Detalle, n.FechaHora
                                 FROM NovedadesPersonal n
                                 INNER JOIN Usuarios u ON n.IdUsuario = u.IdUsuario
                                 INNER JOIN Roles r ON u.IdRol = r.IdRol
                                 ORDER BY n.FechaHora DESC";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new NovedadPersonal
                        {
                            IdNovedad = Convert.ToInt32(dr["IdNovedad"]),
                            NombreUsuario = dr["NombreCompleto"].ToString(),
                            NombreRol = dr["NombreRol"].ToString(),
                            TipoRegistro = dr["TipoRegistro"].ToString(),
                            Area = dr["Area"].ToString(),
                            Detalle = dr["Detalle"].ToString(),
                            FechaHora = Convert.ToDateTime(dr["FechaHora"])
                        });
                    }
                }
            }
            return lista;
        }
    }
}
