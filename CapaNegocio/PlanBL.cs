using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class PlanBL
    {
        private readonly PlanDAL dal = new PlanDAL();

        public List<Plan> Listar(bool soloActivos = true)
        {
            return dal.Listar(soloActivos);
        }

        public string Registrar(Plan obj)
        {
            if (string.IsNullOrWhiteSpace(obj.NombrePlan) || obj.DuracionDias <= 0 || obj.Precio < 0)
                return "Nombre, duración (>0) y precio (>=0) son obligatorios.";

            bool ok = dal.Registrar(obj);
            return ok ? "OK" : "No se pudo registrar el plan.";
        }

        public string Editar(Plan obj)
        {
            if (obj.IdPlan <= 0)
                return "Plan inválido.";
            bool ok = dal.Editar(obj);
            return ok ? "OK" : "No se pudo actualizar el plan.";
        }
    }
}
