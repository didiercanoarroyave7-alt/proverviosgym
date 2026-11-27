using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
        public class MembresiaDAL
        {
            // Registrar Membresía y su Pago en una sola operación atómica
            public bool RegistrarMembresiaConPago(Membresia membresia, Pago pago)
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();
                    SqlTransaction transaccion = con.BeginTransaction();

                    try
                    {
                        // 1. Insertar Membresía y obtener su Id generado
                        string queryMembresia = @"INSERT INTO Membresias (IdCliente, IdPlan, FechaInicio, FechaFin, Estado)
                                              OUTPUT INSERTED.IdMembresia
                                              VALUES (@IdCliente, @IdPlan, @FechaInicio, @FechaFin, 'Activa')";

                        SqlCommand cmdMem = new SqlCommand(queryMembresia, con, transaccion);
                        cmdMem.Parameters.AddWithValue("@IdCliente", membresia.IdCliente);
                        cmdMem.Parameters.AddWithValue("@IdPlan", membresia.IdPlan);
                        cmdMem.Parameters.AddWithValue("@FechaInicio", membresia.FechaInicio.Date);
                        cmdMem.Parameters.AddWithValue("@FechaFin", membresia.FechaFin.Date);

                        int idMembresiaGenerada = (int)cmdMem.ExecuteScalar();

                        // 2. Insertar el Pago vinculado a esa Membresía
                        string queryPago = @"INSERT INTO Pagos (IdMembresia, IdUsuarioCobro, Monto, MetodoPago)
                                         VALUES (@IdMembresia, @IdUsuario, @Monto, @Metodo)";

                        SqlCommand cmdPago = new SqlCommand(queryPago, con, transaccion);
                        cmdPago.Parameters.AddWithValue("@IdMembresia", idMembresiaGenerada);
                        cmdPago.Parameters.AddWithValue("@IdUsuario", pago.IdUsuarioCobro);
                        cmdPago.Parameters.AddWithValue("@Monto", pago.Monto);
                        cmdPago.Parameters.AddWithValue("@Metodo", pago.MetodoPago);

                        cmdPago.ExecuteNonQuery();

                        transaccion.Commit();
                        return true;
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }

            // Obtener la última membresía de un cliente para validar acceso en recepción
            public Membresia ObtenerUltimaMembresiaPorDocumento(string documento)
            {
                Membresia mem = null;
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    string query = @"SELECT TOP 1 m.IdMembresia, m.IdCliente, c.NombreCompleto, c.Documento, 
                                        m.IdPlan, p.NombrePlan, m.FechaInicio, m.FechaFin, m.Estado
                                 FROM Membresias m
                                 INNER JOIN Clientes c ON m.IdCliente = c.IdCliente
                                 INNER JOIN Planes p ON m.IdPlan = p.IdPlan
                                 WHERE c.Documento = @Doc
                                 ORDER BY m.FechaFin DESC";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Doc", documento);
                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            mem = new Membresia
                            {
                                IdMembresia = Convert.ToInt32(dr["IdMembresia"]),
                                IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                NombreCliente = dr["NombreCompleto"].ToString(),
                                DocumentoCliente = dr["Documento"].ToString(),
                                IdPlan = Convert.ToInt32(dr["IdPlan"]),
                                NombrePlan = dr["NombrePlan"].ToString(),
                                FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                                FechaFin = Convert.ToDateTime(dr["FechaFin"]),
                                Estado = dr["Estado"].ToString()
                            };
                        }
                    }
                }
                return mem;
            }

            // Guardar el intento de entrada en RegistrosAcceso
            public void RegistrarAcceso(RegistroAcceso acceso)
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    string query = @"INSERT INTO RegistrosAcceso (IdCliente, AccesoPermitido, Observacion)
                                 VALUES (@IdCliente, @Permitido, @Obs)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@IdCliente", acceso.IdCliente);
                    cmd.Parameters.AddWithValue("@Permitido", acceso.AccesoPermitido);
                    cmd.Parameters.AddWithValue("@Obs", (object)acceso.Observacion ?? DBNull.Value);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

