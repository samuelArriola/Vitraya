using Entidades.Facturacion;
using Entidades.Generales;
using Persistencia.Facturacion;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Facturacion
{
    public partial class BitacoraAutorizaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static GNPermisos GetPermisos(string linkOpcion)
        {
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));
            int IdRol = usuario.CodigoR;

            return DAOGNPermisos.GetPermiso(IdRol, linkOpcion);
        }

        [WebMethod]
        public static List<EBitacoraAutorizaciones> getBitacora()
        {
            return PBitacoraAutorizaciones.GetAutorizaciones();
        }

        [WebMethod]
        public static List<EBitacoraAutorizaciones> GetDetalles(int id)
        {
            return PBitacoraAutorizaciones.GetDetalles(id);
        }

        [WebMethod]
        public static List<EBitacoraAutorizaciones> GetDetallesTecnologias(int id)
        {
            return PBitacoraAutorizaciones.GetDetallesTecno(id);
        }

        [WebMethod]
        public static List<EBitacoraAutorizaciones> GetArchivosAut(int id)
        {
            return PBitacoraAutorizaciones.GetArchivosAut(id);
        }

        [WebMethod]
        public static void SetAprobAut(int idAutorizacion, DateTime fechaAprobacion, string numAutorizacion)
        {
            PBitacoraAutorizaciones.SetAprobarAutorizacion(idAutorizacion, fechaAprobacion, numAutorizacion);
        }

        [WebMethod]
        public static void SetAnularAut(int idAutorizacion, DateTime fechaAnulacion, string motivoAnulacion)
        {
            PBitacoraAutorizaciones.SetAnularAutorizacion(idAutorizacion, fechaAnulacion, motivoAnulacion);
        }

        [WebMethod]
        public static void SetObservacion(int idAutorizacion, string observacion)
        {
            PBitacoraAutorizaciones.setObservacionAut(idAutorizacion, observacion);
        }

        [WebMethod]
        public static List<EBitacoraAutorizaciones> GetObservaciones(int AutId)
        {
            return PBitacoraAutorizaciones.getObservaciones(AutId);
        }

        [WebMethod]
        public static void InsertarDocumentoAprobacionAut(string Nombre, string Archivo, string Contenido, string Extension, int OidRegAutorizacion)
        {
            PBitacoraAutorizaciones.SetRegistroDocumentoAprobAut(Nombre, Archivo, Contenido, Extension, OidRegAutorizacion);
        }

        [WebMethod]
        public static List<EBitacoraAutorizaciones> GetArchivosAprobAut(int id)
        {
            return PBitacoraAutorizaciones.GetArchivosAprobAut(id);
        }

        [WebMethod]
        public static List<EBitacoraAutorizaciones> obtenerinfoFiltro(string numId, string NomPacien, string NumSolic, string NumIngreso, string NumAut, string EstAut)
        {
            return PBitacoraAutorizaciones.filtro1(numId, NomPacien, NumSolic, NumIngreso, NumAut, EstAut);
        }

        [WebMethod]
        public static List<EBitacoraAutorizaciones> obtenerinfoFiltro2(DateTime fechaI, DateTime fechaF)
        {
            return PBitacoraAutorizaciones.filtro2(fechaI, fechaF);
        }

        [WebMethod]
        public static List<EBitacoraAutorizaciones> obtenerinfoFiltro3(DateTime fechaI, DateTime fechaF)
        {
            return PBitacoraAutorizaciones.filtro3(fechaI, fechaF);
        }

        [WebMethod]
        public static void UpdateInformacion(int AutId, string tipoId, string numId, string nombres, string numSolicitud, DateTime fechaSolicitud, string ubicacion, string NumIngreso, DateTime fechaIngreso, string servicio, string numCama, string diagPrincipal, string clasificacionT, string tecnologiaT, string cantidadT)
        {
            PBitacoraAutorizaciones.updateInformacion(AutId, tipoId, numId, nombres, numSolicitud, fechaSolicitud, ubicacion, NumIngreso, fechaIngreso, servicio, numCama, diagPrincipal, clasificacionT, tecnologiaT, cantidadT);
        }

        [WebMethod]
        public static List<EBitacoraAutorizaciones> GetValidacionRep(string numIngreso, string numAutorizacion)
        {
            return PBitacoraAutorizaciones.validadorAutRepetidas(numIngreso, numAutorizacion);
        }

        [WebMethod]
        public static List<ERegistroAutorizaciones> getCUPS()
        {
            return PRegistroAutorizaciones.GetListaCups();
        }

    }
}