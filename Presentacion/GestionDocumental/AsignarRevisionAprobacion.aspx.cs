using Entidades.Generales;
using Entidades.GestionDocumental;
using Newtonsoft.Json;
using Persistencia.Generales;
using Persistencia.GestionDocumental;
using Persistencia.procesos;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Presentacion.GestionDocumental
{
    public partial class AsignarRevisionAprobacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDdl();
            }
        }
        //metodo que carda los dropDownList  con los revisores y aprobador
        public void CargarDdl()
        {
            // se obtiene un lista de los usuarios que tienen permiso para revisar un documento
            List<Usuario> usuariosR = DAOUsuario.GetUsuariosRevpGD();

            //se obtine una lista de usuarios que tinen permisos para aprobar un documento 
            List<Usuario> usuariosA = DAOUsuario.GetUsuariosAprGD();

            //se limpian los datos prebios de los drops para evitar que se dupliquen los datos
            ddlRevisores.Items.Clear();
            ddlAprobador.Items.Clear();

            ListItem listItem = new ListItem("Seleccione", "-1");

            ddlRevisores.Items.Add(listItem);
            ddlAprobador.Items.Add(listItem);

            //se pasan los datos a los correspondientes drops
            foreach (var usuario in usuariosA)
            {
                ddlAprobador.Items.Add(new ListItem(usuario.GNNomUsu1, usuario.GNCodUsu1.ToString()));
            }
            foreach (var usuario in usuariosR)
            {
                ddlRevisores.Items.Add(new ListItem(usuario.GNNomUsu1, usuario.GNCodUsu1.ToString()));
            }
        }


        //metodo que devuelve una lista de los documentos que estan pendientes por asignar revisores y aprobador
        [WebMethod]
        public static string GetGocumentos(string NomDoc, string Tipo, string Estado)
        {
            // lista de objetos donde se gurdaran todos los datos de los docuemtos
            List<object> datos = new List<object>();

            List<GDDocumento> documentos = DAOGDDocumento.GetDocumentos(NomDoc, Tipo, Estado);
            foreach (var documeto in documentos)
            {
                //id propio del documento y no del la tabla general de los documentos
                int idDocumento = 0;

                //dependiendo del tipo de documento se consulta el correspondiente id 
                try
                {
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
                }
                catch (Exception ex)
                {

                }

                datos.Add(new object[] { documeto, idDocumento });
            }
            //se retornan los datos en formato JSON
            return JsonConvert.SerializeObject(datos);
        }

        //metodo que asigna revisores y aprobadores
        [WebMethod]
        public static void AsignarRevisores(List<GDRevision> revisiones, GDAprobacion aprobacion, int idDocumento)
        {

            //se asigna el cargo al los revisores deacuerdo al su usuario
            foreach(var revisor in revisiones)
            {
                var usurio = DAOUsuario.getInstance().GetUsuario(revisor.IntOidRevisor);
                revisor.StrCargo = usurio.GnCargo1;
            }

            //se asigna el cargo al aprobador deacuerdo a su usuario
            var usuario = DAOUsuario.getInstance().GetUsuario(aprobacion.IntOidRevisor);
            aprobacion.StrCargo = usuario.GnCargo1;


            //se actualiza el documento indicando que ya tiene revisores 
            GDDocumento documento = DAOGDDocumento.GetDocumento(idDocumento);
            documento.IntEstado = 2;
            DAOGDDocumento.SetUpdate(documento);

            //se guardan las instancias de la revisiones en base de datos
            foreach (var revision in revisiones)
                DAOGDRevision.SetRevision(revision);

            // se gurada el estdo de la aprobacion
            DAOGDAprobacion.SetAprobacion(aprobacion);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = documento.IntOidGDDocumento,
                intOidGNHistorico = 0,
                strAccion = "Modificar",
                strDetalle = $"Se Cambia el estado del documento a revisión",
                dtmFecha = DateTime.Now,
                strEntidad = "GDDocumento"
            });
        }
    }
}