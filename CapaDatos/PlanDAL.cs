using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaDatos
{
    public class PlanDAL
    {
        public List<Plan> Listar(bool soloActivos = true)
        {
            List<Plan> lista = new List<Plan>();
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT IdPlan, NombrePlan, DuracionDias, Precio, Descripcion, Estado FROM Planes";
                if (soloActivos)
                {
                    query += " WHERE Estado = 1";
                }

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Plan
                        {
                            IdPlan = Convert.ToInt32(dr["IdPlan"]),
                            NombrePlan = dr["NombrePlan"].ToString(),
                            DuracionDias = Convert.ToInt32(dr["DuracionDias"]),
                            Precio = Convert.ToDecimal(dr["Precio"]),
                            Descripcion = dr["Descripcion"].ToString(),
                            Estado = Convert.ToBoolean(dr["Estado"])
                        });
                    }
                }
            }
            return lista;
        }

        public bool Registrar(Plan obj)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"INSERT INTO Planes (NombrePlan, DuracionDias, Precio, Descripcion, Estado)
                                 VALUES (@Nombre, @Dias, @Precio, @Desc, 1)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", obj.NombrePlan);
                cmd.Parameters.AddWithValue("@Dias", obj.DuracionDias);
                cmd.Parameters.AddWithValue("@Precio", obj.Precio);
                cmd.Parameters.AddWithValue("@Desc", (object)obj.Descripcion ?? DBNull.Value);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Editar(Plan obj)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = @"UPDATE Planes 
                                 SET NombrePlan = @Nombre, DuracionDias = @Dias, Precio = @Precio, Descripcion = @Desc, Estado = @Estado
                                 WHERE IdPlan = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", obj.IdPlan);
                cmd.Parameters.AddWithValue("@Nombre", obj.NombrePlan);
                cmd.Parameters.AddWithValue("@Dias", obj.DuracionDias);
                cmd.Parameters.AddWithValue("@Precio", obj.Precio);
                cmd.Parameters.AddWithValue("@Desc", (object)obj.Descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", obj.Estado);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Borrado lógico para no dañar el historial de membresías
        public bool CambiarEstado(int idPlan, bool estado)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "UPDATE Planes SET Estado = @Estado WHERE IdPlan = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", idPlan);
                cmd.Parameters.AddWithValue("@Estado", estado);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
