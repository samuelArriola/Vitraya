using Entidades.Generales;
using Entidades.GestionDocumental;
using Entidades.Power_BI;
using Entidades.Procesos;
using Newtonsoft.Json;
using Persistencia.Generales;
using Persistencia.GestionDocumental;
using Persistencia.Power_BI;
using Persistencia.procesos;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Presentacion.GestionDocumental
{
    public partial class Solicitudes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDdlProcesos();

            }
        }

        public void CargarDdlProcesos()
        {
            List<PCProceso> procesos = DAOProceso.listar();
            ddlProcesos.Items.Clear();
            ddlProcesos.Items.Add(new ListItem("Seleccione ...", "-1"));
            foreach (var proceso in procesos)
            {
                ddlProcesos.Items.Add(new ListItem(proceso.StrNomPro, proceso.IntOIdProceso.ToString()));
            }
        }




        /// <summary>
        /// Metodo que recchaza o acepta una Solicitud de creacion ce documento
        /// </summary>
        [WebMethod]
        public static void RealizarSolicitud(GDSolicitud solicitud)
        {

            //Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"].ToString()));


            //solicitud.DblCodUsu = Convert.ToDouble(usuario.GNCodUsu1);
            //solicitud.StrNomUsu = usuario.GNNomUsu1;
            //solicitud.StrCarUsu = usuario.GnCargo1;

            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(solicitud.DblCodUsu));


            solicitud.DblCodUsu = Convert.ToDouble(usuario.GNCodUsu1);
            solicitud.StrNomUsu = usuario.GNNomUsu1;
            solicitud.StrCarUsu = usuario.GnCargo1;

            DAOGDSolicitud.SetSolicitud(solicitud);


            //se crea una evaluacion para la nueva solicitud
            GDEvaluacion evaluacion = new GDEvaluacion
            {
                StrTipo = "solicitud",
                StrEstado = "Pendiente...",
                SrtInsidencia = "",
                IntOidGDSolicitud = DAOGDSolicitud.GetUltSolicitud().IntOidGDSolicitud
            };

            //se guarda la nueva evaluacion en la base de datos 
            DAOGDEvaluacion.SetEvaluacion(evaluacion);
        }


        [WebMethod]
        public static string GetSolicitudes(string nombre, string tipoSol, DateTime fecha, string tipoDoc, string estado)
        {
            List<GDSolicitud> solicitudes = DAOGDSolicitud.GetSolisitudes(nombre, tipoSol, fecha, tipoDoc, estado);
            return JsonConvert.SerializeObject(solicitudes);
        }

        [WebMethod]
        public static List<GDDocumento> GetDocumentosByTipo(string tipo)
        {
            return DAOGDDocumento.GetDocumentosByTipo(tipo);
        }

        [WebMethod]
        public static void DeleteDocument(int idDocumento, int idSolicitud)
        {
            //se consulta el documento que se va a marcar como eliminado a través de su id
            var documento = DAOGDDocumento.GetDocumento(idDocumento);

            //var documentos = DAOGDDocumento.GetDocumentoByCod()
            //se cambia el estado del documento a eliminado
            documento.IntEstado = GDDocumento.ELIMINADO;

            //se actualiza el documento en la base de datos 
            DAOGDDocumento.SetUpdate(documento);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = documento.IntOidGDDocumento,
                intOidGNHistorico = 0,
                strAccion = "Eliminar",
                strDetalle = $"Se Elimina el documento {documento.StrNomDoc}",
                dtmFecha = DateTime.Now,
                strEntidad = "GDDocumento"
            });

            //se consulta la solicitud desde la base de datos 
            var solicitud = DAOGDSolicitud.GetSolicitud(idSolicitud + "");

            solicitud.StrEstado = "Finalizado";
            DAOGDSolicitud.SetUpdate(solicitud);
        }

        [WebMethod]
        public static void EliminarDocumento(int idSolicitud)
        {
            GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(idSolicitud + "");

            GDDocumento documento = DAOGDDocumento.GetDocumento(solicitud.IntOidGDDocE);

            documento.IntEstado = 5;

            DAOGDDocumento.SetUpdate(documento);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = documento.IntOidGDDocumento,
                intOidGNHistorico = 0,
                strAccion = "Eliminar",
                strDetalle = $"Se Elimina el documento {documento.StrNomDoc}",
                dtmFecha = DateTime.Now,
                strEntidad = "GDDocumento"
            });
        }

        [WebMethod]
        public static List<EAdministrarReportes> getUsuarios()
        {
            return PAdministrarReportes.GetUsuarios();
        }
    }
}