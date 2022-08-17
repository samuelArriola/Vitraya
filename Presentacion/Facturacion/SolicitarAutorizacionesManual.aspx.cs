using Entidades.Facturacion;
using Persistencia.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Facturacion
{
    public partial class SolicitarAutorizacionesManual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<ERegistroAutorizaciones> GetDatosDinamica(string numIngreso)
        {
            return PRegistroAutorizacionesManual.GetDatosDinamica(numIngreso);
        }

        [WebMethod]
        public static List<EBitacoraAutorizaciones> InsertarAutorizacion(string tipoId, string numId, string nombres, string numSolicitud, DateTime fechaSolicitud, string origenAtencion, string ubicacion,
            string tipoServicio, DateTime fechaIngreso, string numIngreso, string prioridad, string contrato, string servicio, string numCama, string diagPrincipal, string diag1, string diag2,
            string nombreIps, string direccionIps, string justificacionClinica, string clasificacionT, string tecnologiaT, string cantidadT, string profesionalSalud, string cargoProfesional)
        {

            return PRegistroAutorizaciones.SetRegistroAutorizacion(tipoId, numId, nombres, numSolicitud, fechaSolicitud, origenAtencion, ubicacion, tipoServicio, fechaIngreso, numIngreso, prioridad, contrato,
                servicio, numCama, diagPrincipal, diag1, diag2, nombreIps, direccionIps, justificacionClinica, clasificacionT, tecnologiaT, cantidadT, profesionalSalud, cargoProfesional);

        }

        [WebMethod]
        public static void InsertarDocumentoOrdenM(string Nombre, string Archivo, string Contenido, string Extension, int OidRegAutorizacion)
        {

            PRegistroAutorizaciones.SetRegistroDocumentoOrdenM(Nombre, Archivo, Contenido, Extension, OidRegAutorizacion);

        }

        [WebMethod]
        public static List<ERegistroAutorizaciones> getCUPS()
        {
            return PRegistroAutorizaciones.GetListaCups();
        }
    }
}