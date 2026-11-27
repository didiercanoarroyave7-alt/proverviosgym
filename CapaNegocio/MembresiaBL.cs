using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class MembresiaBL
    {
        private readonly MembresiaDAL dal = new MembresiaDAL();
        private readonly ClienteDAL clienteDal = new ClienteDAL();
        private readonly PlanDAL planDal = new PlanDAL();

        public string Afiliar(int idCliente, int idPlan, DateTime fechaInicio, int idUsuarioCobro, string metodoPago = "Efectivo")
        {
            if (idCliente <= 0 || idPlan <= 0)
                return "Cliente y plan son obligatorios.";

            Plan plan = null;
            foreach (var p in planDal.Listar(true))
            {
                if (p.IdPlan == idPlan) { plan = p; break; }
            }
            if (plan == null)
                return "Plan no encontrado o inactivo.";

            var membresia = new Membresia
            {
                IdCliente = idCliente,
                IdPlan = idPlan,
                FechaInicio = fechaInicio.Date,
                FechaFin = fechaInicio.Date.AddDays(plan.DuracionDias)
            };

            var pago = new Pago
            {
                IdUsuarioCobro = idUsuarioCobro,
                Monto = plan.Precio,
                MetodoPago = metodoPago
            };

            try
            {
                bool ok = dal.RegistrarMembresiaConPago(membresia, pago);
                return ok ? "OK" : "No se pudo registrar la membresía.";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public (bool permitido, string mensaje, Membresia mem) ValidarAcceso(string documento)
        {
            if (string.IsNullOrWhiteSpace(documento))
                return (false, "Ingrese el documento del cliente.", null);

            var mem = dal.ObtenerUltimaMembresiaPorDocumento(documento.Trim());
            if (mem == null)
                return (false, "Cliente no encontrado o sin membresías registradas.", null);

            bool vigente = mem.FechaFin.Date >= DateTime.Today && mem.Estado == "Activa";
            int diasRestantes = (mem.FechaFin.Date - DateTime.Today).Days;

            var acceso = new RegistroAcceso
            {
                IdCliente = mem.IdCliente,
                AccesoPermitido = vigente,
                Observacion = vigente ? "Acceso permitido" : "Plan vencido o inactivo"
            };
            dal.RegistrarAcceso(acceso);

            if (vigente)
                return (true, $"ACCESO PERMITIDO. Días restantes: {diasRestantes}. Plan: {mem.NombrePlan}", mem);
            else
                return (false, $"ACCESO DENEGADO. Membresía vencida el {mem.FechaFin:dd/MM/yyyy}. Plan: {mem.NombrePlan}", mem);
        }

        public List<Membresia> ListarActivas()
        {
            return dal.ListarActivas();
        }
    }
}
