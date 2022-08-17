using Entidades.Generales;
using Entidades.GestionDocumental;
using Entidades.Procesos;
using Newtonsoft.Json;
using Persistencia.Generales;
using Persistencia.GestionDocumental;
using Persistencia.procesos;
using Persistencia.Procesos;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Presentacion.GestionDocumental
{
    public partial class CrearManual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDdl();
                if (DAOGDDocumento.GetDocumentoSol(Convert.ToInt32(Request["OidSolicitud"])) == null)
                {
                    CrearDocumento();
                }
            }
        }

        /// <summary>
        /// metodo que carga los drops con la informacion necesaria
        /// </summary>
        public void CargarDdl()
        {
            //se limpian los datos que continen los ddl para evitar que se repita la informacion 
            ddlProcesos.Items.Clear();
            ddlProcs.Items.Clear();

            ListItem itemSeleccione = new ListItem("Seleccione", "-1");
            itemSeleccione.Attributes.Add("disabled", "disabled");

            ddlProcesos.Items.Add(itemSeleccione);
            ddlProcs.Items.Add(itemSeleccione);


            //se consulta la lista de procesos que se va  a mostrar en el drop de los procesos
            List<PCProceso> procesos = DAOProceso.listar();

            //se pasan los datos de la lista de los procesos al drop de los procesos
            foreach (var proceso in procesos)
            {
                ddlProcesos.Items.Add(new ListItem(proceso.StrNomPro, proceso.IntOIdProceso + ""));
            }
            //se consulta una lista de los normagramas en la base de datos 


            List<GDProtocolo> protocolos = DAOGDProtocolo.GetGDProtocolos();
            List<GdDocProcedimiento> procedimientos = DAOGdDocProcedimiento.GetProcedimientos();


            ListItem item = new ListItem("Protocolos");

            ddlProcs.Items.Add(item);
            foreach (var protocolo in protocolos)
            {
                ddlProcs.Items.Add(new ListItem(protocolo.StrNombre));
            }

            ListItem item2 = new ListItem("Procedimientos");
            ddlProcs.Items.Add(item2);
            foreach (var procedimiento in procedimientos)
            {
                ddlProcs.Items.Add(new ListItem(procedimiento.StrNomProcedimiento));
            }
        }
        /// <summary>
        /// metodo que crea un docuneto de tipo manual en la base de datos
        /// </summary>
        public void CrearDocumento()
        {
            //se consulta el id de la solicitud que se encuentra en como parametro en la url 
            int idSolicitud = Convert.ToInt32(Request["OidSolicitud"]);

            //se consulta la solicitud por el id 
            GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(idSolicitud + "");

            //en caso de que el tipo de solicitud sea editar
            GDDocumento documentoAux = null; // documento auxiliar para consultar los datos del documento a editar
            if (solicitud.StrTipoSol == "editar")
            {
                //se consulta la informacion del documento que se requiere editar
                documentoAux = DAOGDDocumento.GetDocumento(solicitud.IntOidGDDocE);
            }

            //se crea un nuevo documento
            GDDocumento documento = new GDDocumento
            {
                DtFechaE = DateTime.Now,
                IntEstado = 0,
                IntOidGDSolicitud = idSolicitud,
                IntVersion = solicitud.StrTipoSol == "crear" ? 1 : documentoAux.IntVersion + 1, //en caso de que la solicitud sea editar se incrementa en 1 la version del documento a editar
                StrCodigoDoc = solicitud.StrTipoSol == "crear" ? "" : documentoAux.StrCodigoDoc, //en caso de que la solicitud sea editar se mantiene el codigo del documento a editar
                StrNomDoc = solicitud.StrNomDoc,
                StrNomSolicitante = solicitud.StrNomUsu,
                StrTipDoc = solicitud.StrTipoDoc,
                StrUniFunSolicitante = "",
                IntConsecutivo = 0,
                IntOidPCProceso = solicitud.IntOidGNProceso
            };

            //se crea un nuevo documento con la en la base de datos
            DAOGDDocumento.SetDocumento(documento);

            //se consulta el documento recien creado en la base de datos para extraer el oid del documento recien creado
            documento = DAOGDDocumento.GetDocumentoSol(idSolicitud);

            //se crea el manual
            GDManual manual = null;


            if (solicitud.StrTipoSol == "editar")
            {
                //en caso de que la solicitud sea de tipo editar se crea una instancia de tipo manual con la informacion del manual a editar
                manual = DAOGDManual.GetManualByIdDoc(solicitud.IntOidGDDocE);

                //se pasa el oid del  nuevo documento al manual recien creado
                manual.IntOidGDDocumento = documento.IntOidGDDocumento;
            }
            else
            {
                //en caso de que la solcitud se de tipo crear se crea una instacia de manual con informacion vacia
                manual = new GDManual
                {
                    IntOidGDDocumento = documento.IntOidGDDocumento,
                    StrAnexos = "",
                    StrAlcance = "",
                    StrDesarrollo = "",
                    StrFormatos = "",
                    StrGlosario = "",
                    StrIntroduccion = "",
                    StrMarcoLegal = "",
                    StrObjetivos = "",
                    StrNombre = solicitud.StrNomDoc,
                    StrTalentoHumano = "",
                    StrRecInfo = "",
                    StrRecFin = "",
                    StrProcs = "",
                    StrEquipos = "",
                    StrMedicamentos = "",
                    IntOidGDProceso = 0,

                };
            }

            //se guarda la el manual recien creado en la base de datos
            DAOGDManual.SetManual(manual);
        }

        //metodo que altualiza el manual con la informacion suministrada pro el usuario
        [WebMethod]
        public static void UpdateManual(GDManual manual, int idSolicitud, int version)
        {
            //se consulta el documento por el id de la solicitud
            GDDocumento documento = DAOGDDocumento.GetDocumentoSol(idSolicitud);

            //se crea un documento de tipo manual auxiliar para evitar que se pierdan datos importantes
            GDManual manualAux = DAOGDManual.GetManualByIdDoc(documento.IntOidGDDocumento);
            manual.IntOidGDManual = manualAux.IntOidGDManual;
            manual.IntOidGDDocumento = manualAux.IntOidGDDocumento;

            //en caso de quel proceso en el manual haya cambiado se actualiza tambien en el documento en general 
            documento.IntOidPCProceso = manual.IntOidGDProceso;

            //en caso de que el el usuario haya modificado la versión
            documento.IntVersion = version == 0 ? documento.IntVersion : version;

            DAOGDDocumento.SetUpdate(documento);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = documento.IntOidGDDocumento,
                intOidGNHistorico = 0,
                strAccion = "Modificar",
                strDetalle = $"Se Cambión el proceso del documento {documento.StrNomDoc}",
                dtmFecha = DateTime.Now,
                strEntidad = "GDDocumento"
            });

            //se actualiza el manual con la informacion suministrada por el usuario
            DAOGDManual.UpdateManual(manual);
        }

        //metodo que retorna el manual que sera editado 
        [WebMethod]
        public static string GetManual(int idSolicitud)
        {
            GDDocumento documento = DAOGDDocumento.GetDocumentoSol(idSolicitud);
            return JsonConvert.SerializeObject(DAOGDManual.GetManualByIdDoc(documento.IntOidGDDocumento));
        }

        //metodo que que envia el manual a revision 
        [WebMethod]
        public static void EnviarRevision(int idSolicitud)
        {
            //se consulta la solicitud a la que pertenece el documento
            GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(idSolicitud + "");

            //se consulta el documento que sera enviado a revision 
            GDDocumento documento = DAOGDDocumento.GetDocumentoSol(idSolicitud);

            //se consulta el contenido del manual
            GDManual manual = DAOGDManual.GetManualByIdDoc(documento.IntOidGDDocumento);

            if (solicitud.StrTipoSol == "crear")
            {
                //se consulta el proceso al cual pertenece el manual para parametrizar el codigo del documento a traves 
                //del prefijo del documento
                PCProceso proceso = DAOProceso.BuscarProceso(manual.IntOidGDProceso);

                UnidadFuncional unidad = DAOUnidadFuncional.GetUnidadFuncional(proceso.IntGnDcDep);

                GNDireccion direccion = DAOGNDireccion.GetGNDireccion(unidad.GnCdAra1);

                //se crea el codigo del documeto con la informacion consultada prebiamente
                string sigla = "MA-" + direccion.StrSiglaDir + "-" + proceso.StrPrefijo;


                documento.StrCodigoDoc = sigla;
            }

            //se cambia el estado del documeto para que sea enviado a revision
            documento.IntEstado = 1;

            //se actualiza el documento con la nueva informacion en la base de datos
            DAOGDDocumento.SetUpdate(documento);


            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(solicitud.DblCodUsu));

            //se envian las la notificacion
            //DAOUsuario.GetUsuariosAdminGD().ForEach(usuarioAux => {
            //    List<string> correos = new List<string>();
            //    correos.Add(usuarioAux.GNCrusu1);

            //    string mensaje = $"<p>Señor(a) {usuarioAux.GNNomUsu1} se le iforma que el usuario {usuario.GNNomUsu1} ha enviado el documento {solicitud.StrNomDoc} a revisión, " +
            //    $"<br/>favor ingresar al modulo de gestión documental para asignar los revisores y el aprobador</p>";

            //    Email.SendMail(correos, mensaje, "Notificación de documento en revisión");

            //});
        }
    }
}