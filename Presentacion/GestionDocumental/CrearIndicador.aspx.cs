using Entidades.Generales;
using Entidades.GestionDocumental;
using Entidades.Procesos;
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
    public partial class CrearIndicador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (verificarDoc())
                {
                    crearDocumento();
                }
                cargarddl();
            }
        }
        /// <summary>
        /// metodo que verifica si existe un documento en base de datos para la solicitud indicada
        /// </summary>
        /// <returns></returns>
        public bool verificarDoc()
        {
            int idSolicitud = Convert.ToInt32(Request["OIdSolicitud"].ToString());
            return (DAOGDDocumento.GetDocumentoSol(idSolicitud) == null);
        }

        /// <summary>
        /// metodo que carga los drops son su respectiva informacion
        /// </summary>
        public void cargarddl()
        {

            //se consulta una lista de los cargos para mostrala en los drops correspondientes
            List<Cargo> cargos = DAOCargo.GetCargos();

            //se limpian los datos de los drops para evitar que la informacion se duplique
            ddlResponsable.Items.Clear();
            ddlActores.Items.Clear();
            ddlResponsableAna.Items.Clear();
            ddlResponsableMed.Items.Clear();
            ddlProcesos.Items.Clear();

            ListItem itemSelecione = new ListItem("Seleccione", "-1");

            itemSelecione.Attributes.Add("disabled", "disabled");

            ddlResponsable.Items.Add(itemSelecione);
            ddlActores.Items.Add(itemSelecione);
            ddlResponsableAna.Items.Add(itemSelecione);
            ddlResponsableMed.Items.Add(itemSelecione);
            ddlProcesos.Items.Add(itemSelecione);


            //por cada elmeto en la lista de los cargos se carga un nuevo item en los drops
            foreach (var cargo in cargos)
            {
                ListItem item = new ListItem(cargo.StrGnNomCgo, cargo.StrGnNomCgo);
                ddlResponsable.Items.Add(item);
                ddlResponsableAna.Items.Add(item);
                ddlActores.Items.Add(item);
                ddlResponsableMed.Items.Add(item);
            }

            //se consulta una lista de los dominios pra cargarala en el drop de los dominios 
            List<GDDominio> dominios = DAOGDDominio.GetGDDominios();
            ddlDominio.Items.Clear();

            //se pasan los datos de la lista de los dominios  al drop de los dominios 
            foreach (var dominio in dominios)
            {
                ddlDominio.Items.Add(new ListItem(dominio.StrNombre, dominio.StrNombre));
            }

            //se conuslta una lista de los procesos para cargarla al drop de los procesos
            List<PCProceso> procesos = DAOProceso.listar();

            //se pasan los datos de la lista de los proceso al drop del los procesos
            foreach (var proceso in procesos)
            {
                ddlProcesos.Items.Add(new ListItem(proceso.StrNomPro, proceso.IntOIdProceso + ""));
            }
        }


        //metodo que crea un documento en base de datos 
        public void crearDocumento()
        {
            // se obtine la solicitud a la que pertenece el documento
            int IntOidGDSolicitud = Convert.ToInt32(Request["OIdSolicitud"].ToString());
            GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(Convert.ToString(IntOidGDSolicitud));

            GDDocumento documentoAux = null;

            if (solicitud.StrTipoSol.ToLower() == "editar")
            {
                documentoAux = DAOGDDocumento.GetDocumento(solicitud.IntOidGDDocE);
            }
            txtNomProcedimientotitle.InnerText = solicitud.StrNomDoc;

            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(solicitud.DblCodUsu));

            //se crea una intancia del documento
            GDDocumento documento = new GDDocumento
            {
                IntOidGDSolicitud = IntOidGDSolicitud,
                IntVersion = (solicitud.StrTipoSol.ToLower() == "crear") ? 1 : documentoAux.IntVersion + 1,
                StrTipDoc = solicitud.StrTipoDoc,
                StrCodigoDoc = solicitud.StrTipoSol == "crear" ? "" : documentoAux.StrCodigoDoc,
                DtFechaE = DateTime.Now,
                StrNomDoc = solicitud.StrNomDoc,
                StrNomSolicitante = solicitud.StrNomUsu,
                StrUniFunSolicitante = usuario.GnUnfun1,
                IntOidPCProceso = solicitud.IntOidGNProceso,
                IntEstado = 0,
                IntConsecutivo = 0
            };

            // se guarda la informacion de la instancia del documento en la base de datos 
            DAOGDDocumento.SetDocumento(documento);
            documento = DAOGDDocumento.GetUltDocumento();
            IniciarIndicador(documento, solicitud);
        }


        /// <summary>
        /// metodo que crea un documento tipo indicador en la base de datos 
        /// </summary>
        /// <param name="documento"></param>
        /// <param name="solicitud"></param>
        public void IniciarIndicador(GDDocumento documento, GDSolicitud solicitud)
        {
            GDDocIndicador indicador;
            if (solicitud.StrTipoSol.ToLower() == "editar")
            {
                var documentoAux = DAOGDDocumento.GetDocumento(solicitud.IntOidGDDocE);
                indicador = DAOGDDocIndicador.GetIndicadorByIdDoc(documentoAux.IntOidGDDocumento);
                indicador.IntOidGDDocumento = documento.IntOidGDDocumento;
            }
            else
            {
                //se cea una instancia del documento de tipo indicador
                indicador = new GDDocIndicador
                {
                    IntOidGDDocumento = documento.IntOidGDDocumento,
                    IntOidProceso = solicitud.IntOidGNProceso,
                    IntOidRevisor = 0,
                    IntOidAprovador = 0,
                    StrNomDoc = documento.StrNomDoc,
                    StrJustificacion = "",
                    StrCodSOGC = "",
                    StrDescNum = "",
                    StrOrInfoNum = "",
                    StrFuentPrimNum = "",
                    StrDescDen = "",
                    StrOrInfoDen = "",
                    StrFuentPrimDen = "",
                    StrUniMedicion = "",
                    StrFactor = "",
                    StrPeriodicidad = "",
                    StrResponsable = "",
                    StrFormulaCalc = "",
                    StrEstandar = "",
                    StrTendencia = "",
                    StrTipGrafica = "",
                    StrInterpretacion = "",
                    StrResponsableMed = "",
                    StrResponsableAna = "",
                    StrActores = "",
                    StrVigilancia = "",
                    StrNomRevisor = "",
                    StrTasa = "",
                    StrNomAprovador = "",
                    StrTipo = "",
                    DtmFechaRevision = Convert.ToDateTime("01/01/1800"),
                    DtmFechaAprovacion = Convert.ToDateTime("01/01/1800"),
                    DtmFecha = Convert.ToDateTime("01/01/1800"),
                    StrDominio = "",
                    IntOidGDProceso = solicitud.IntOidGNProceso,
                };

                // se guarda la infomacion del la instacia en la base de datos 
            }
            DAOGDDocIndicador.set(indicador);
        }

        //metodo que actualiza los datos del indicador en la base de datos con los datos suministrados por el usuario
        [WebMethod]
        public static string UpdateIndicador(GDDocIndicador indicador, int idSolicitud, int version)
        {
            //se obtine el indicador que se desea aclizar 
            GDDocumento Documento = DAOGDDocumento.GetDocumentoSol(idSolicitud);

            //se valida que se envio una version y en caso de que se asi se cambia la version del documento por enviado por el usuario
            Documento.IntVersion = version == 0 ? Documento.IntVersion : version;

            //en caso de que el proceso del indicador cambie tambien se cambia en el Documento general
            Documento.IntOidPCProceso = indicador.IntOidProceso;

            DAOGDDocumento.SetUpdate(Documento);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = Documento.IntOidGDDocumento,
                intOidGNHistorico = 0,
                strAccion = "Modificar",
                strDetalle = $"Se modificó el codigo del proceso del documento {Documento.StrNomDoc}",
                dtmFecha = DateTime.Now,
                strEntidad = "GDDocumento"
            });

            int IntOidGDDocumento = Documento.IntOidGDDocumento;

            GDDocIndicador indicadorAux = DAOGDDocIndicador.GetIndicadorByIdDoc(IntOidGDDocumento);

            //se rescatan los datos que no se desean cambiar
            indicador.IntOIdGDDocIndicador = indicadorAux.IntOIdGDDocIndicador;
            indicador.IntOidGDDocumento = indicadorAux.IntOidGDDocumento;


            //se realizar una actualizacion del indicador en la base de datos 
            DAOGDDocIndicador.UpdateIndicador(indicador);

            return "";
        }


        //metodo que cambia el estado del documento para que se enviado a revision
        [WebMethod]
        public static void EnviarRevision(int idSolicitud)
        {
            GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(idSolicitud + "");
            //se obtiene el documento que se desea actualizar 
            GDDocumento documento = DAOGDDocumento.GetDocumentoSol(idSolicitud);

            GDDocIndicador indicador = DAOGDDocIndicador.GetIndicadorByIdDoc(documento.IntOidGDDocumento);


            PCProceso proceso = DAOProceso.BuscarProceso(indicador.IntOidProceso);

            UnidadFuncional unidad = DAOUnidadFuncional.GetUnidadFuncional(proceso.IntGnDcDep);

            GNDireccion direccion = DAOGNDireccion.GetGNDireccion(unidad.GnCdAra1);

            if (solicitud.StrTipoSol != "editar")
            {
                string sigla = "FI-" + direccion.StrSiglaDir.ToUpper() + "-" + proceso.StrPrefijo.ToUpper();
                documento.StrCodigoDoc = sigla;
            }

            //se busca si ya existen revisores para el documento 
            List<GDRevision> revisiones = DAOGDRevision.GetGDRevisiones(documento.IntOidGDDocumento);
            if (revisiones.Count == 0) //en caso de que no hayan revisores se envia para asignar revisores
                documento.IntEstado = 1;

            else // en caso que exitan revisores al documento se envia a revision
                documento.IntEstado = 2;
            DAOGDDocumento.SetUpdate(documento);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = documento.IntOidGDDocumento,
                intOidGNHistorico = 0,
                strAccion = "Modificar",
                strDetalle = $"Se cambia el estado del documento de preeliminar a en construcción {documento.StrNomDoc}",
                dtmFecha = DateTime.Now,
                strEntidad = "GDDocumento"
            });

            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(solicitud.DblCodUsu));

            ////se envian las la notificacion
            //DAOUsuario.GetUsuariosAdminGD().ForEach(usuarioAux => {
            //    List<string> correos = new List<string>();
            //    correos.Add(usuarioAux.GNCrusu1);

            //    string mensaje = $"<p>Señor(a) {usuarioAux.GNNomUsu1} se le iforma que el usuario {usuario.GNNomUsu1} ha enviado el documento {solicitud.StrNomDoc} a revisión, " +
            //    $"<br/>favor ingresar al modulo de gestión documental para asignar los revisores y el aprobador</p>";

            //    Email.SendMail(correos, mensaje, "Notificación de documento en revisión");

            //});
        }

        /// <summary>
        /// metodo que retorna todos los datos del indicador
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        [WebMethod]
        public static string CargarIndicador(int idSolicitud)
        {
            int IntOidGDDocumento = DAOGDDocumento.GetDocumentoSol(idSolicitud).IntOidGDDocumento;
            GDDocIndicador indicador = DAOGDDocIndicador.GetIndicadorByIdDoc(IntOidGDDocumento);
            PCProceso proceso = DAOProceso.BuscarProceso(indicador.IntOidProceso);
            return JsonConvert.SerializeObject(new object[] { indicador, proceso });
        }
    }
}