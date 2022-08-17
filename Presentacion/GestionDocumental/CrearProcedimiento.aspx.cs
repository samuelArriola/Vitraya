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
    public partial class CrearProcedimiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarddl();
                if (verificarDoc())
                    crearDocumento();
                else
                    cargarDatosPro();
            }

            //int IntOidGDSolicitud = Convert.ToInt32(Request["OIdSolicitud"].ToString());
            //GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(Convert.ToString(IntOidGDSolicitud));
            //txtNomProcedimientotitle.InnerText = solicitud.StrNomDoc;
        }

        public void cargarDatosPro()
        {

        }

        public bool verificarDoc()
        {
            int IntOidGDSolicitud = Convert.ToInt32(HttpContext.Current.Request["OIdSolicitud"].ToString()); //obtener id de la solicitud
            return DAOGdDocProcedimiento.GetProcedimientoByIdSol(IntOidGDSolicitud) == null;

        }

        /// <summary>
        /// Consultar los cargos y cargarlos a los ddl! 
        /// </summary>
        public void cargarddl()
        {
            //se consulta los los cargo en la base de datos
            List<Cargo> cargos = DAOCargo.GetCargos();


            List<PCProceso> procesos = DAOProceso.listar();

            var seleccione = new ListItem("Seleccione", "-1");

            seleccione.Attributes.Add("disabled", "disabled");


            ddlResponsable.Items.Clear();
            ddlTalHumano.Items.Clear();
            ddlProcesos.Items.Clear();


            ddlTalHumano.Items.Add(seleccione);
            ddlProcesos.Items.Add(seleccione);
            ddlResponsable.Items.Add(seleccione);

            foreach (var cargo in cargos)
            {
                ListItem item = new ListItem(cargo.StrGnNomCgo, cargo.StrGnNomCgo);
                ddlResponsable.Items.Add(item);
                ddlTalHumano.Items.Add(item);
            }
            
            foreach (var proceso in procesos)
            {
                ddlProcesos.Items.Add(new ListItem(proceso.StrNomPro, proceso.IntOIdProceso + ""));
            }
        }


        /// <summary>
        /// crear documento e inicializar el DocProcedimiento correspondiente.
        /// </summary>
        public void crearDocumento()
        {
            int IntOidGDSolicitud = Convert.ToInt32(Request["OIdSolicitud"].ToString());
            GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(Convert.ToString(IntOidGDSolicitud));
            txtNomProcedimientotitle.InnerText = solicitud.StrNomDoc;


            GDDocumento documentoAux = null;
            if (solicitud.StrTipoSol == "editar")
            {
                documentoAux = DAOGDDocumento.GetDocumento(solicitud.IntOidGDDocE);
            }


            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(solicitud.DblCodUsu));


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
                IntEstado = 0,
                IntConsecutivo = 0,
                IntOidPCProceso = solicitud.IntOidGNProceso,
            };

            DAOGDDocumento.SetDocumento(documento);
            documento = DAOGDDocumento.GetUltDocumento();

            //Crear lista archivos para almacenar el flujograma
            GNListaArchivos listaArchivos = new GNListaArchivos
            {
                IntOidGNModulo = 7,
            };
            DAOGNListaArchivos.set(listaArchivos);
            listaArchivos = DAOGNListaArchivos.GetUltimo();
            GdDocProcedimiento DocProcedimiento;

            //en caso  de que la solicitud realizada se editar
            if (solicitud.StrTipoSol == "editar")
            {
                //se consulta el Procedimiento a editar
                DocProcedimiento = DAOGdDocProcedimiento.getProcedimientoDoc(documentoAux.IntOidGDDocumento + "");

                //se pasa el id del nuevo documeto al procedimiento
                DocProcedimiento.IntOidGDDocumento = documento.IntOidGDDocumento;

                //se pasa la lista de archivos recien creada al nuevo procedimiento
                DocProcedimiento.IntOidGNListaArchivo = listaArchivos.IntOidGNListaArchivos;


                //se pasan la lista de los indicadores del anterior documeto al nuevo documento
                DAOGDListaIndicador.GetListaIndicadores(documentoAux.IntOidGDDocumento).ForEach(Lista =>
                {
                    Lista.IntOIdGDDocumento = documento.IntOidGDDocumento;
                    DAOGDListaIndicador.SetListaIndicador(Lista);
                });
            }
            else
            {
                //en caso de que el tipo de solicitu no sea editar se crea un nuevo procedimiento con informacion vacia
                DocProcedimiento = new GdDocProcedimiento
                {

                    DtFechaAprobacion = Convert.ToDateTime("01/01/1800"),
                    DtFechaC = Convert.ToDateTime("01/01/1800"),
                    DtFechaRevision = Convert.ToDateTime("01/01/1800"),
                    IntOidAprobador = -1,
                    IntOidGDDocumento = documento.IntOidGDDocumento,
                    IntOidGNListaArchivo = listaArchivos.IntOidGNListaArchivos,
                    IntOidRevisor = -1,
                    StrAlcance = "",
                    StrAnexos = "",
                    StrDefiniciones = "[]",
                    StrDocRelacionados = "",
                    StrEntradas = "",
                    StrEstCalidad = "",
                    StrNomAprobador = "",
                    StrNomProcedimiento = documento.StrNomDoc,
                    StrNomProceso = "",//capturar nom Proceso
                    StrNomRevisor = "",
                    StrObjetivo = "",
                    StrProEsperado = "",
                    StrRecursosNecesarios = "",
                    StrRefNormativas = "",
                    StrResponsable = "",
                    StrSalidas = "",
                    StrClientes = "",
                    StrEquipos = "",
                    StrMedicamentos = "",
                    StrProveedores = "",
                    StrRecFin = "",
                    StrRecInfo = "",
                    StrTalentoHumano = "",
                    StrFlujoGrama = "",
                    StrActividad = "",
                    IntOidGDProceso = solicitud.IntOidGNProceso,
                    StrIndicadores = "",
                    StrDocumentosRelacionados = ""
                };
            }

            //se pasan los datos de procedimiento recien creado a la base de datos 
            DAOGdDocProcedimiento.SetDocProcedimiento(DocProcedimiento);
        }

        /// <summary>
        /// actualizar un procedimiento. 
        /// </summary>
        /// <param name="DocProcedimiento"></param>
        /// <returns></returns>
        [WebMethod]
        public static void setUpdateDocPro(GdDocProcedimiento Procedimiento, int idSolicitud, int version)
        {
            
            //se consulta el id del documento a actualizar
            string IntOidGDDocumento = Convert.ToString(DAOGDDocumento.GetDocumentoSol(idSolicitud).IntOidGDDocumento); // obtener id del documento 

            //se obtiene el procedimiento a actulizar para realizar una copida de los oids
            GdDocProcedimiento DocProcedimiento = DAOGdDocProcedimiento.getProcedimientoDoc(IntOidGDDocumento); // agregar id al procedimiento

            //se pasan los oids del procedimiento en la base de datos al procedimiento generado por el usuario
            Procedimiento.IntOIdGdDocprocedimiento = DocProcedimiento.IntOIdGdDocprocedimiento;
            Procedimiento.IntOidGDDocumento = DocProcedimiento.IntOidGDDocumento;
            Procedimiento.IntOidGNListaArchivo = DocProcedimiento.IntOidGNListaArchivo;

            //se eliminan todas las actividades el procedimieto para evitar que se duplique la informacion
            DAOGDActividad.DeleteAtividadByIdDoc(Procedimiento.IntOidGDDocumento);

            

            //se actualiza el procedimiento con la infomacion dada por el usuario
            DAOGdDocProcedimiento.SetUpdate(Procedimiento);

            //se elimina la informacion de la lista de los indicadores para evitar que se repita informacion
            DAOGDListaIndicador.Delete(Procedimiento.IntOidGDDocumento);
            

            var documento = DAOGDDocumento.GetDocumento(Procedimiento.IntOidGDDocumento);
            documento.IntOidPCProceso = Procedimiento.IntOidGDProceso;

            //en casos de que se haya dado un versión se modifica la version segun la version dada
            documento.IntVersion = version == 0 ? documento.IntVersion : version;

            //se actuliza la informacion del documento con el proceso y la version dada
            DAOGDDocumento.SetUpdate(documento);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = documento.IntOidGDDocumento,
                intOidGNHistorico = 0,
                strAccion = "Modificar",
                strDetalle = $"Se actualizó el proceso y versión del documento  {documento.StrNomDoc}",
                dtmFecha = DateTime.Now,
                strEntidad = "GDDocumento"
            });
        }


        /// <summary>
        /// metodo que inicia el flujo de getion documental
        /// </summary>
        /// <param name="idSolicitud"></param>
        [WebMethod]
        public static void EnviarDocumentoRevision(int idSolicitud)
        {
            GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(idSolicitud + "");


            //se consulta el documento por el id de la solicitud
            GDDocumento documento = DAOGDDocumento.GetDocumentoSol(idSolicitud);

            //se consulta el procedimiento por el id de la solicitud

            GdDocProcedimiento procedimiento = DAOGdDocProcedimiento.getProcedimientoDoc(documento.IntOidGDDocumento.ToString());



            //se consulta el proceso al que pertenece el procedimiento
            PCProceso proceso = DAOProceso.BuscarProceso(procedimiento.IntOidGDProceso);

            /********************** En caso de que la solicitud para el documento se editar ***********************/

            if (solicitud.StrTipoSol != "editar")
            {
                UnidadFuncional unidad = DAOUnidadFuncional.GetUnidadFuncional(proceso.IntGnDcDep);

                GNDireccion direccion = DAOGNDireccion.GetGNDireccion(unidad.GnCdAra1);

                string sigla = "PRO-" + direccion.StrSiglaDir + "-" + proceso.StrPrefijo;

                documento.StrCodigoDoc = sigla;
            }

            /**********************************************************************************************************/

            documento.IntEstado = 1;
            List<GDRevision> revisiones = DAOGDRevision.GetGDRevisiones(documento.IntOidGDDocumento);

            if (revisiones.Count == 0)
                documento.IntEstado = 1;
            else
                documento.IntEstado = 2;
            DAOGDDocumento.SetUpdate(documento);

            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = documento.IntOidGDDocumento,
                intOidGNHistorico = 0,
                strAccion = "Modificar",
                strDetalle = $"Se actualizó es estado de preeliminar a {(revisiones.Count == 0 ? "En construcción" : "Revisión")} del documento {documento.StrNomDoc}",
                dtmFecha = DateTime.Now,
                strEntidad = "GDDocumento"
            });

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
        [WebMethod]
        public static string GetProcedimiento(int IdSolicitud)
        {
            GdDocProcedimiento procedimiento = DAOGdDocProcedimiento.GetProcedimientoByIdSol(IdSolicitud);

            List<GDActividad> actividades = DAOGDActividad.GetActividadesByDoc(procedimiento.IntOidGDDocumento);
            List<GDListaIndicador> lista = DAOGDListaIndicador.GetListaIndicadores(procedimiento.IntOidGDDocumento);
            List<GDDocIndicador> indicadores = new List<GDDocIndicador>();

            foreach (var index in lista)
            {
                indicadores.Add(DAOGDDocIndicador.GetIndicador(index.IntOIdGDDocIndicador));
            }

            return JsonConvert.SerializeObject(new object[] { procedimiento, actividades, indicadores });
        }
    }
}