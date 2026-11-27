using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;

namespace GYMFIT
{
    // Datos de demostración en memoria. Reemplazar por CapaNegocio / CapaDatos (SQL Server) cuando estén listas.
    public class UsuarioDemo { public string Documento { get; set; } public string Nombre { get; set; } public string Usuario { get; set; } public string Clave { get; set; } public string Rol { get; set; } }
    public class ClienteDemo
    {
        public string Documento { get; set; } public string Nombre { get; set; } public string Telefono { get; set; }
        public string Contacto { get; set; } public string TelContacto { get; set; } public DateTime FechaFin { get; set; }
        public int Dias { get { return (int)(FechaFin.Date - DateTime.Today).TotalDays; } }
    }
    public class PlanDemo { public string Nombre { get; set; } public int Dias { get; set; } public decimal Precio { get; set; } }
    public class PagoDemo { public DateTime Fecha { get; set; } public string Cliente { get; set; } public string Plan { get; set; } public string Metodo { get; set; } public decimal Monto { get; set; } }
    public class RutinaDemo { public string Cliente { get; set; } public string Objetivo { get; set; } public string Descripcion { get; set; } public string Observaciones { get; set; } }
    public class NovedadDemo { public string Tipo { get; set; } public string Area { get; set; } public string Detalle { get; set; } public string Hora { get; set; } public string Usuario { get; set; } }
    public class AccesoDemo { public string Hora { get; set; } public string Cliente { get; set; } public bool Permitido { get; set; } }

    public static class DatosDemo
    {
        public static List<UsuarioDemo> Usuarios = new List<UsuarioDemo> {
            new UsuarioDemo { Documento = "10000001", Nombre = "Carlos Administrador", Usuario = "admin", Clave = "1234", Rol = "Administrador" },
            new UsuarioDemo { Documento = "10000002", Nombre = "Laura Recepción", Usuario = "recepcion", Clave = "1234", Rol = "Recepcionista" },
            new UsuarioDemo { Documento = "10000003", Nombre = "Andrés Entrenador", Usuario = "entrenador", Clave = "1234", Rol = "Entrenador" },
            new UsuarioDemo { Documento = "10000004", Nombre = "Marta Servicios", Usuario = "limpieza", Clave = "1234", Rol = "Limpiadora" } };
        public static List<PlanDemo> Planes = new List<PlanDemo> {
            new PlanDemo { Nombre = "Pase diario", Dias = 1, Precio = 8000 }, new PlanDemo { Nombre = "Plan quincenal", Dias = 15, Precio = 35000 },
            new PlanDemo { Nombre = "Plan mensual", Dias = 30, Precio = 60000 }, new PlanDemo { Nombre = "Plan trimestral", Dias = 90, Precio = 160000 } };
        public static List<ClienteDemo> Clientes = new List<ClienteDemo> {
            new ClienteDemo { Documento = "1017001", Nombre = "Juan Pérez", Telefono = "3101234567", Contacto = "Ana Pérez", TelContacto = "3100000001", FechaFin = DateTime.Today.AddDays(18) },
            new ClienteDemo { Documento = "1017002", Nombre = "María Gómez", Telefono = "3157654321", Contacto = "Luis Gómez", TelContacto = "3100000002", FechaFin = DateTime.Today.AddDays(-5) },
            new ClienteDemo { Documento = "1017003", Nombre = "Camilo Restrepo", Telefono = "3209988776", Contacto = "", TelContacto = "", FechaFin = DateTime.Today.AddDays(2) } };
        public static List<PagoDemo> Pagos = new List<PagoDemo> {
            new PagoDemo { Fecha = DateTime.Today.AddDays(-12), Cliente = "Juan Pérez", Plan = "Plan mensual", Metodo = "Efectivo", Monto = 60000 },
            new PagoDemo { Fecha = DateTime.Today.AddDays(-20), Cliente = "María Gómez", Plan = "Plan quincenal", Metodo = "Transferencia", Monto = 35000 } };
        public static List<RutinaDemo> Rutinas = new List<RutinaDemo>();
        public static List<NovedadDemo> Novedades = new List<NovedadDemo>();
        public static List<AccesoDemo> Accesos = new List<AccesoDemo>();

        public static UsuarioDemo Validar(string usuario, string clave)
        {
            return Usuarios.FirstOrDefault(u => u.Usuario == usuario && u.Clave == clave);
        }
        // La fecha fin siempre es FechaInicio + días del plan (si ya tiene plan vigente, se suma al final).
        public static void Cobrar(ClienteDemo c, PlanDemo p, string metodo)
        {
            DateTime b = c.FechaFin.Date > DateTime.Today ? c.FechaFin.Date : DateTime.Today;
            c.FechaFin = b.AddDays(p.Dias);
            Pagos.Insert(0, new PagoDemo { Fecha = DateTime.Today, Cliente = c.Nombre, Plan = p.Nombre, Metodo = metodo, Monto = p.Precio });
        }
    }

    public static class Sesion
    {
        public static readonly Dictionary<string, string[]> Menus = new Dictionary<string, string[]> {
            { "Administrador", new[] { "Default.aspx", "Acceso.aspx", "Clientes.aspx", "Planes.aspx", "Rutinas.aspx", "Novedades.aspx", "Empleados.aspx", "Reportes.aspx" } },
            { "Recepcionista", new[] { "Acceso.aspx", "Clientes.aspx", "Planes.aspx", "Novedades.aspx" } },
            { "Entrenador", new[] { "Clientes.aspx", "Rutinas.aspx" } },
            { "Limpiadora", new[] { "Novedades.aspx" } } };
        public static readonly Dictionary<string, string> Titulos = new Dictionary<string, string> {
            { "Default.aspx", "Inicio" }, { "Acceso.aspx", "Control de acceso" }, { "Clientes.aspx", "Clientes" }, { "Planes.aspx", "Planes y pagos" },
            { "Rutinas.aspx", "Rutinas" }, { "Novedades.aspx", "Novedades" }, { "Empleados.aspx", "Empleados y roles" }, { "Reportes.aspx", "Reportes" } };
        public static string Rol { get { return HttpContext.Current.Session["Rol"] as string; } }
        public static string Nombre { get { return HttpContext.Current.Session["Nombre"] as string; } }
        public static string Usuario { get { return HttpContext.Current.Session["Usuario"] as string; } }
        public static bool Permitida(string rol, string pagina) { return Menus.ContainsKey(rol) && Menus[rol].Contains(pagina); }
    }

    public static class Ui
    {
        static readonly CultureInfo Co = new CultureInfo("es-CO");
        public static string Esc(string s) { return HttpUtility.HtmlEncode(s ?? ""); }
        public static string Dinero(decimal n) { return "$" + n.ToString("N0", Co); }
        public static bool PuedeEditar() { return Sesion.Rol == "Administrador" || Sesion.Rol == "Recepcionista"; }
        public static void Msg(Label l, bool ok, string t) { l.Text = Esc(t); l.CssClass = "msg " + (ok ? "ok" : "bad"); }
        public static string Stat(string valor, string texto) { return "<div class=\"card\"><div class=\"stat\">" + valor + "</div><div class=\"mut\">" + texto + "</div></div>"; }
        public static string Estado(ClienteDemo c)
        {
            int d = c.Dias;
            if (d < 0) return "<span class=\"tag bad\">Vencida</span>";
            if (d <= 3) return "<span class=\"tag warn\">Vence en " + d + " d</span>";
            return "<span class=\"tag ok\">Vigente · " + d + " d</span>";
        }
        // Las celdas deben llegar ya codificadas con Esc().
        public static string Tabla(string[] cabeza, List<string[]> filas, string vacio)
        {
            if (filas.Count == 0) return "<p class=\"empty\">" + vacio + "</p>";
            var sb = new StringBuilder("<div class=\"wrap\"><table><tr>");
            foreach (var h in cabeza) sb.Append("<th>" + h + "</th>");
            sb.Append("</tr>");
            foreach (var f in filas) { sb.Append("<tr>"); foreach (var c in f) sb.Append("<td>" + c + "</td>"); sb.Append("</tr>"); }
            return sb.Append("</table></div>").ToString();
        }
    }
}
