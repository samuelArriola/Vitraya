using Entidades.Generales;
using Entidades.GestionDocumental;
using Logica.GestionDocumental;
using Newtonsoft.Json;
using Persistencia.Generales;
using Persistencia.GestionDocumental;
using Persistencia.procesos;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace Presentacion.GestionDocumental
{
    public partial class RevisarDocumento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static string GetGocumentos(string NomDoc, string Tipo, string Estado)
        {

            List<object> datos = new List<object>();
            List<GDDocumento> documentos = DAOGDDocumento.GetDDocumentosByIdRev(Convert.ToInt32(HttpContext.Current.Session["Admin"]), NomDoc, Tipo, Estado);
            foreach (var documeto in documentos)
            {
                int idDocumento = 0;
                switch (documeto.StrTipDoc)
                {
                    case "Indicador":
                        {
                            idDocumento = DAOGDDocIndicador.GetIndicadorByIdDoc(documeto.IntOidGDDocumento).IntOIdGDDocIndicador;
                            break;
                        }
                    case "Proceso":
                        {
                            idDocumento = DAOProceso.GetProcesoByOidSol(documeto.IntOidGDSolicitud).IntOIdProceso;
                            break;
                        }
                    case "Procedimiento":
                        {
                            idDocumento = DAOGdDocProcedimiento.getProcedimientoDoc(documeto.IntOidGDDocumento + "").IntOIdGdDocprocedimiento;
                            break;

                        }
                    case "Protocolo":
                        {
                            idDocumento = DAOGDProtocolo.getProtocolobyidDoc(documeto.IntOidGDDocumento).IntOidGDProtocolo;
                            break;
                        }
                    case "Manual":
                        {
                            idDocumento = DAOGDManual.GetManualByIdDoc(documeto.IntOidGDDocumento).IntOidGDManual;
                            break;
                        }
                }
                datos.Add(new object[] { documeto, idDocumento });
            }
            return JsonConvert.SerializeObject(datos);
        }



        [WebMethod(EnableSession = true)]
        public static void AprobarRechazarRev(int estado, int idDocumento, string detalles)
        {
            int idRevisor = Convert.ToInt32(HttpContext.Current.Session["Admin"]);
            List<GDDocumento> documentos = DAOGDDocumento.GetDDocumentosByIdRev(idRevisor, "", "", "");
            GDDocumento documento = DAOGDDocumento.GetDocumento(idDocumento);

            if (estado == GDRevision.APROBADO)
            {
                GDRevision revision = DAOGDRevision.GetGDRevision(idRevisor, documento.IntOidGDDocumento);
                revision.IntEstado = GDRevision.APROBADO;
                revision.StrDetalles = detalles;

                DAOGDRevision.UpdateRvision(revision);

                if (GestionDocumentalLogica.VerificarRevisiones(documento.IntOidGDDocumento))
                {
                    documento.IntEstado = 3;
                    DAOGDDocumento.SetUpdate(documento);
                    DAOGNHistorico.SetHistorico(new GNHistorico
                    {
                        intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                        intInstancia = documento.IntOidGDDocumento,
                        intOidGNHistorico = 0,
                        strAccion = "Modificar",
                        strDetalle = $"Se actualizó el estado de en revisión a en aprobación para el documento   {documento.StrNomDoc}",
                        dtmFecha = DateTime.Now,
                        strEntidad = "GDDocumento"
                    });
                }
            }
            else
            {
                List<GDRevision> revisiones = DAOGDRevision.GetGDRevisiones(documento.IntOidGDDocumento);
                foreach (var revision in revisiones)
                {
                    if (revision.IntOidRevisor == idRevisor)
                    {
                        revision.StrDetalles = detalles;
                    }
                    revision.IntEstado = GDRevision.NOVISTO;
                    DAOGDRevision.UpdateRvision(revision);
                }
                documento.IntEstado = 0;
                DAOGDDocumento.SetUpdate(documento);
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = documento.IntOidGDDocumento,
                    intOidGNHistorico = 0,
                    strAccion = "Modificar",
                    strDetalle = $"Se actualizó el estado de en revisión a preeliminar del documento {documento.StrNomDoc}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GDDocumento"
                });
            }
        }
    }
}