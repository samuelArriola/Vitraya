using Entidades.Generales;
using Entidades.GestionDocumental;
using Entidades.Procesos;
using Newtonsoft.Json;
using Persistencia.Generales;
using Persistencia.GestionDocumental;
using Persistencia.procesos;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Presentacion.GestionDocumental
{
    public partial class CrearPolitica : System.Web.UI.Page
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

        //metodo que carga los drops
        public void CargarDdl()
        {
            //se limpian los datos del drop de los procesos para evitar que la informacion se duplique
            ddlProcesos.Items.Clear();

            var itemSeleccione = new ListItem("Seleccione", "-1");

            ddlProcesos.Items.Add(itemSeleccione);

            //se consulta una lista de los pricesos para cargarla al drop de los procesos 
            List<PCProceso> procesos = DAOProceso.listar();
            foreach (var proceso in procesos)
            {
                ddlProcesos.Items.Add(new ListItem(proceso.StrNomPro, proceso.IntOIdProceso + ""));
            }
        }

        //metodo que carga un documento de tipo politica en la base de datos 
        public void CrearDocumento()
        {
            // se consulta la solicitud para crear el documento con la informacion de la solicitud 
            int idSolicitud = Convert.ToInt32(Request["OidSolicitud"]);
            GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(idSolicitud + "");

            //se crea una instancia del documento
            GDDocumento documento = new GDDocumento
            {
                DtFechaE = DateTime.Now,
                IntEstado = 0,
                IntOidGDSolicitud = idSolicitud,
                IntVersion = 1,
                StrCodigoDoc = "",
                StrNomDoc = solicitud.StrNomDoc,
                StrNomSolicitante = solicitud.StrNomUsu,
                StrTipDoc = solicitud.StrTipoDoc,
                StrUniFunSolicitante = "",
                IntConsecutivo = 0,
                IntOidPCProceso = solicitud.IntOidGNProceso
            };
            //se guarda la instacia del documento en la base de datos 
            DAOGDDocumento.SetDocumento(documento);
            documento = DAOGDDocumento.GetDocumentoSol(idSolicitud);

            //se crea una instancia de la politica y se guarda en la base de datos 
            GDPolitica Politica = new GDPolitica
            {
                IntOidGDDocumento = documento.IntOidGDDocumento,
                StrAnexos = "",
                StrAlcance = "",
                StrDesarrollo = "",
                StrGlosario = "",
                StrIntroduccion = "",
                StrMarcoLegal = "",
                StrObjetivos = "",
                IntOidGDPolitica = 0,
                IntOidGDProceso = 0,
                StrObjetivosEsp = "",
                StrNombre = solicitud.StrNomDoc
            };
            DAOGDPolitica.SetPolitica(Politica);
        }

        //metodo que actualiza la polica con la informacion suministrada por el usuario
        [WebMethod]
        public static void UpdatePolitica(GDPolitica Politica, int idSolicitud, int version)
        {
            //se consulta el la información general del documento
            GDDocumento documento = DAOGDDocumento.GetDocumentoSol(idSolicitud);

            GDPolitica PoliticaAux = DAOGDPolitica.GetPoliticaByIdDoc(documento.IntOidGDDocumento);
            Politica.IntOidGDPolitica = PoliticaAux.IntOidGDPolitica;
            Politica.IntOidGDDocumento = PoliticaAux.IntOidGDDocumento;
            DAOGDPolitica.UpdatePolitica(Politica);

            //se verifica si la version del documento ha sido cambiada y se actualiza
            documento.IntOidPCProceso = Politica.IntOidGDProceso;
            documento.IntVersion = version == 0 ? documento.IntVersion : version;
            DAOGDDocumento.SetUpdate(documento);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                intGNCodUsu = Convert.ToInt32(System.Web.HttpContext.Current.Session["Admin"]),
                intInstancia = documento.IntOidGDDocumento,
                intOidGNHistorico = 0,
                strAccion = "Modificar",
                strDetalle = $"Se actulizó el proceso para el documento {documento.StrNomDoc}",
                dtmFecha = DateTime.Now,
                strEntidad = "GDDocumento"
            });
        }

        //metodo que retorna la informacion de la politica en edicion 
        [WebMethod]
        public static string GetPolitica(int idSolicitud)
        {
            GDDocumento documento = DAOGDDocumento.GetDocumentoSol(idSolicitud);
            return JsonConvert.SerializeObject(DAOGDPolitica.GetPoliticaByIdDoc(documento.IntOidGDDocumento));
        }

        //metodo que cambia el estado del documento a: estado en revision 
        [WebMethod]
        public static void EnviarRevision(int idSolicitud)
        {
            //se conulta el documento 
            GDDocumento documento = DAOGDDocumento.GetDocumentoSol(idSolicitud);
            GDPolitica politica = DAOGDPolitica.GetPoliticaByIdDoc(documento.IntOidGDDocumento);

            PCProceso proceso = DAOProceso.BuscarProceso(politica.IntOidGDProceso);

            UnidadFuncional unidad = DAOUnidadFuncional.GetUnidadFuncional(proceso.IntGnDcDep);

            GNDireccion direccion = DAOGNDireccion.GetGNDireccion(unidad.GnCdAra1);

            string sigla = "POL-" + direccion.StrSiglaDir + "-" + proceso.StrPrefijo;

            documento.StrCodigoDoc = sigla;

            documento.IntEstado = 1;
            DAOGDDocumento.SetUpdate(documento);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                intGNCodUsu = Convert.ToInt32(System.Web.HttpContext.Current.Session["Admin"]),
                intInstancia = documento.IntOidGDDocumento,
                intOidGNHistorico = 0,
                strAccion = "Modificar",
                strDetalle = $"Se actulizo el estado del documento de preeliminar a en construción  {documento.StrNomDoc}",
                dtmFecha = DateTime.Now,
                strEntidad = "GDDocumento"
            });

            GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(idSolicitud + "");
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