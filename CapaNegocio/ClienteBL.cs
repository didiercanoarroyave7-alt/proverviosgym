using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class ClienteBL
    {
        private readonly ClienteDAL dal = new ClienteDAL();

        public List<Cliente> Listar()
        {
            return dal.Listar();
        }

        public Cliente BuscarPorDocumento(string documento)
        {
            if (string.IsNullOrWhiteSpace(documento))
                return null;
            return dal.BuscarPorDocumento(documento.Trim());
        }

        public string Registrar(Cliente obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Documento) || string.IsNullOrWhiteSpace(obj.NombreCompleto))
                return "Documento y nombre completo son obligatorios.";

            if (dal.BuscarPorDocumento(obj.Documento) != null)
                return "Ya existe un cliente con ese documento.";

            bool ok = dal.Registrar(obj);
            return ok ? "OK" : "No se pudo registrar el cliente.";
        }
    }
}
