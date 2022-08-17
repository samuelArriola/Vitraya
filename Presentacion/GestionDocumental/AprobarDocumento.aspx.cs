using Entidades.Generales;
using Entidades.GestionDocumental;
using Entidades.trainings;
using Newtonsoft.Json;
using Persistencia.Generales;
using Persistencia.GestionDocumental;
using Persistencia.procesos;
using Persistencia.trainings;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace Presentacion.GestionDocumental
{
    public partial class AprobarDocumento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Meltodo que devuel una lista de los documetos que estan listos para se aprobados al usuario que son asignados
        /// </summary>
        /// <param name="NomDoc"></param>
        /// <param name="Tipo"></param>
        /// <param name="Estado"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public static string GetDocumentos(string NomDoc, string Tipo, string Estado)
        {
            //se crea una lista de objetos donde se guardaran los datos de los documetos
            List<object> datos = new List<object>();

            //se busca una lista de los documentos en las que el usuario aparece como aprobador
            List<GDDocumento> documentos = DAOGDDocumento.GetDDocumentosByIdApro(Convert.ToInt32(HttpContext.Current.Session["Admin"]), NomDoc, Tipo, Estado);
            foreach (var documeto in documentos)
            {
                // id del documeto, no de la tabla en general de los documetos si no del propio documeto 
                int idDocumento = 0;
                switch (documeto.StrTipDoc)
                {
                    //dependiendo del tipo de documeto se procede a consultar id del propio documeto
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
            //se devuelven los datos consultados en formato JSON
            return JsonConvert.SerializeObject(datos);
        }

        /// <summary>
        /// Metodo que cambia el estado del documento dependiendo de si esta aprobado o rechazado
        /// </summary>
        /// <param name="estado"></param>
        /// <param name="idDocumento"></param>
        /// <param name="detalles"></param>
        [WebMethod]
        public static void AprobarRechazarRev(int estado, int idDocumento, string detalles)
        {
            // se obtiene el documeto que se le actualizara el estado
            GDDocumento documento = DAOGDDocumento.GetDocumento(idDocumento);

            //se obtine el usuario que realiza la modificacion al documento
            int idAprobador = Convert.ToInt32(HttpContext.Current.Session["Admin"]);


            //Se obtiene el estado de la apobacion para el usuario en el documento
            GDAprobacion aprobacion = DAOGDAprobacion.GetGDAprobacion(idAprobador, idDocumento);

            //se cambia el estado de la aprovacion dependiendo de los datos suministrados
            aprobacion.IntEstado = estado;
            aprobacion.StrDetalles = detalles;

            // se actuliza el estado de la aprobacion con los datos suministrados
            DAOGDAprobacion.UpdateAprobacion(aprobacion);

            //se actuliza el estado del documento deacuerdo a los datos suministrados por el aprobador del documento
            if (estado == GDRevision.APROBADO)
            {
                GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(documento.IntOidGDSolicitud + "");
                documento.IntEstado = 4;
                if (solicitud.StrTipoSol != "editar")
                    documento.IntConsecutivo = DAOGDDocumento.GetCodigoDocumento(documento.StrCodigoDoc);
                documento.DtFechaE = DateTime.Now;

                GDDivulgacion divulgacion = DAOGDDivulgacion.GetDivulgacionByIdDoc(idDocumento);
                if (divulgacion != null)
                {
                    var ids = DAOCPCapacitacion.CreateCapacitacionFromDoc(idDocumento);
                    JsonConvert.DeserializeObject<List<string>>(divulgacion.StrSubtemas).ForEach(subtema =>
                    {
                        CPSUBTEMA Subtema = new CPSUBTEMA
                        {
                            IntContexto = 1,
                            IntOidCPInstacia = ids[0],
                            StrSUBTEMA = subtema,
                            IntOidCPAgenda = ids[1]
                        };
                        DAOCPSUBTEMA.setSubtema(Subtema);
                    });
                    List<string> cargos = JsonConvert.DeserializeObject<List<string>>(divulgacion.StrCargos);

                    foreach (var cargo in cargos)
                    {
                        Cargo auxCargo = DAOCargo.getCargo(Convert.ToInt32(cargo));
                        DAOCPMatricula.matricularUsuarios(auxCargo.IntGnDcCgo + "", "cargo", ids[0], DateTime.Now, 1, auxCargo.IntGnDcCgo, auxCargo.StrGnNomCgo, ids[1]);
                    }

                }
            }
            else
            {
                documento.IntEstado = 0;
            }
            documento.IntEstado = estado == GDRevision.APROBADO ? 4 : 0;

            //documento.StrCodigoDoc = estado ==
            DAOGDDocumento.SetUpdate(documento);

            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = documento.IntOidGDDocumento,
                intOidGNHistorico = 0,
                strAccion = "Modificar",
                strDetalle = $"Se cambia el estado del documento {documento.StrNomDoc} a {(estado == GDRevision.APROBADO ? "Aprobado" : "Preeliminar")}",
                dtmFecha = DateTime.Now,
                strEntidad = "GDDocumento"
            });
        }
    }
}