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
    public partial class CrearProtocolo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDdls();
                
                if(DAOGDDocumento.GetDocumentoSol(Convert.ToInt32(Request["OidSolicitud"])) == null)
                    CrearDocumento();
            }
        }
        public void CrearDocumento()
        {
            int idSolicitud = Convert.ToInt32(Request["OidSolicitud"]);

            GDDocumento documentoAux = null;

            GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(idSolicitud + "");

            if (solicitud.StrTipoSol.ToLower() == "editar")
                documentoAux = DAOGDDocumento.GetDocumento(solicitud.IntOidGDDocE);

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

            DAOGDDocumento.SetDocumento(documento);

            documento = DAOGDDocumento.GetDocumentoSol(idSolicitud);

            GDProtocolo protocolo = null;

            if (solicitud.StrTipoSol == "editar")
            {
                protocolo = DAOGDProtocolo.getProtocolobyidDoc(solicitud.IntOidGDDocE);
                protocolo.IntOidGDDocumento = documento.IntOidGDDocumento;
                
            }
            else
            {
                protocolo = new GDProtocolo
                {
                    IntOidGDDocumento = documento.IntOidGDDocumento,
                    StrAlcance = "",
                    StrAnexos = "",
                    StrDefiniciones = "",
                    StrNombre = documento.StrNomDoc,
                    StrObjetivo = "",
                    StrRecomendaciones = "",
                    StrRecursos = "",
                    StrRefNorm = "",
                    StrResponsable = "",
                    StrRecHumanos = "",
                    StrRecEquiposBiomedicos = "",
                    StrRecInformaticos = "",
                    StrRecMedicamentos = "",
                    IntOidGDProceso = solicitud.IntOidGNProceso,
                    StrActividad = "",
                    StrIndicadores = ""
                };
            }
            DAOGDProtocolo.setProtocolo(protocolo);
        }


        public void cargarDdls()
        {
            ddlTalentoHumano.Items.Clear();
            ddlResponsable.Items.Clear();
            ddlProcesos.Items.Clear();
            ListItem seleccione = new ListItem("Seleccione", "-1");
            seleccione.Attributes.Add("disabled", "");
            seleccione.Selected = true;

            ddlTalentoHumano.Items.Add(seleccione);
            ddlResponsable.Items.Add(seleccione);
            ddlProcesos.Items.Add(seleccione);
            
            List<Cargo> cargos = DAOCargo.GetCargos();

            foreach (var cargo in cargos)
            {
                ListItem listItem = new ListItem(cargo.StrGnNomCgo, cargo.StrGnNomCgo);
                ddlResponsable.Items.Add(listItem);
                ddlTalentoHumano.Items.Add(listItem);
            }

            List<PCProceso> procesos = DAOProceso.listar();
            foreach (var proceso in procesos)
            {
                ddlProcesos.Items.Add(new ListItem(proceso.StrNomPro, proceso.IntOIdProceso + ""));
            }
        }

        [WebMethod]
        public static void UpdateProtocolo(GDProtocolo protocolo,  int idSolicitud, int version)
        {
            //se consulta los datos del protocolo a traves del id de la solicitud 
            GDProtocolo protocoloAux = DAOGDProtocolo.GetProtocoloByIdSol(idSolicitud);

            //se mantienen tanto el id del documento como el id del protocolo
            protocolo.IntOidGDDocumento = protocoloAux.IntOidGDDocumento;
            protocolo.IntOidGDProtocolo = protocoloAux.IntOidGDProtocolo;

            //se actuliza los datos del protocolo en base de datos
            DAOGDProtocolo.UpdateProtocolo(protocolo);

            //se eliminan las actividades para reemplazarlas por las ingresadas por el usuario
            DAOGDActividad.DeleteAtividadByIdDoc(protocolo.IntOidGDDocumento);

            //se elimina el listdo de los indicadores para reemplazarlos por los indicados por el usuario
            DAOGDListaIndicador.Delete(protocolo.IntOidGDDocumento);

            

            // se consulta el documento para actualizar  la version y el proceso si han cambiado
            var documento = DAOGDDocumento.GetDocumento(protocolo.IntOidGDDocumento);
            documento.IntVersion = version == 0 ? documento.IntVersion : version;
            documento.IntOidPCProceso = protocolo.IntOidGDProceso;

            //se actualiza la informacion del documento en la base de datos
            DAOGDDocumento.SetUpdate(documento);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = documento.IntOidGDDocumento,
                intOidGNHistorico = 0,
                strAccion = "Modificar",
                strDetalle = $"Se actulizó el proceso y versión para el documento {documento.StrNomDoc}",
                dtmFecha = DateTime.Now,
                strEntidad = "GDDocumento"
            });
        }

        [WebMethod]
        public static string GetProtocolo(int idSolicitud)
        {
            GDProtocolo protocolo = DAOGDProtocolo.GetProtocoloByIdSol(idSolicitud);
            List<GDActividad> actividades = DAOGDActividad.GetActividadesByDoc(protocolo.IntOidGDDocumento);
            List<GDListaIndicador> listaIndicadores = DAOGDListaIndicador.GetListaIndicadores(protocolo.IntOidGDDocumento);
            List<GDDocIndicador> indicadores = new List<GDDocIndicador>();
            foreach (var idIndicador in listaIndicadores)
            {
                GDDocIndicador indicador = DAOGDDocIndicador.GetIndicador(idIndicador.IntOIdGDDocIndicador);
                if (indicador != null)
                    indicadores.Add(indicador);
            }

            object[] datos = { protocolo, actividades, indicadores };
            return JsonConvert.SerializeObject(datos);
        }

        [WebMethod]
        public static void EnviarRevision(int idSolicitud)
        {
            GDDocumento documento = DAOGDDocumento.GetDocumentoSol(idSolicitud);

            GDProtocolo protocolo = DAOGDProtocolo.getProtocolobyidDoc(documento.IntOidGDDocumento);

            PCProceso proceso = DAOProceso.BuscarProceso(protocolo.IntOidGDProceso);

            UnidadFuncional unidad = DAOUnidadFuncional.GetUnidadFuncional(proceso.IntGnDcDep);

            GNDireccion direccion = DAOGNDireccion.GetGNDireccion(unidad.GnCdAra1);

            string sigla = "PT-" + direccion.StrSiglaDir + "-" + proceso.StrPrefijo;
            documento.StrCodigoDoc = sigla;
            documento.IntEstado = 1;
            DAOGDDocumento.SetUpdate(documento);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = documento.IntOidGDDocumento,
                intOidGNHistorico = 0,
                strAccion = "Modificar",
                strDetalle = $"Se actualizó el estado de preeliminar a en construcción para el documento {documento.StrNomDoc}",
                dtmFecha = DateTime.Now,
                strEntidad = "GDDocumento"
            });

            GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(idSolicitud + "");
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
    }
}