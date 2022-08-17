using Entidades.Auditorias;
using Entidades.Generales;
using Entidades.Procesos;
using Persistencia.Auditorias;
using Persistencia.Generales;
using Persistencia.procesos;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Presentacion.PlanAccion
{
    public partial class AuditoriaExterna : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDdlProcesos();
                CargarDdlEntesAuditores();
                CargarDdlResponsable();
            }


        }
        /// <summary>
        /// Metodo que carga el drop down List de los procesos 
        /// </summary>
        public void CargarDdlProcesos()
        {
            //Se consulta un listado de los procesos activos
            List<PCProceso> procesos = DAOProceso.listar();

            //se borra la infoimacion previa para evitar que se duplique cada ves que recarge la pagina
            ddlProcesos.Items.Clear();

            //se pasa la informacion del listado de los procesos a droop de los proceos
            ddlProcesos.Items.Add(new ListItem("Seleccione", "-1"));
            procesos.ForEach(proceso =>
            {
                ddlProcesos.Items.Add(new ListItem(proceso.StrNomPro, proceso.IntOIdProceso + ""));
            });
        }

        //metodo que carga un listado de los usuarios en el drop de los reponsables 
        protected void CargarDdlResponsable()
        {
            //se limpia el contenido del drop 
            ddlResponsable.Items.Clear();

            //se agrega el promier item de seleccione con valor -1
            ddlResponsable.Items.Add(new ListItem("Seleccione", "-1"));
            
            //se consulta un listado de los uaurios en la base datos y se agreaga un nuevo item por cada usuario en el drop de los responsables
            DAOUsuario.getUsuarios().ForEach(usuario =>
            {
                ddlResponsable.Items.Add(new ListItem(usuario.GNNomUsu1, usuario.GNCodUsu1 + ""));
            });
        }

        //metodo que consulta un listado de los entes auditores y los agrega al drop correspondiente
        protected void CargarDdlEntesAuditores()
        {
            // selimpia la informacion del drop de los entes auditores
            ddlEnteAuditor.Items.Clear();

            //se agrega el primer item seleccione con valor de -1
            ddlEnteAuditor.Items.Add(new ListItem("Seleccione", "-1"));

            //se consulta un listado de los entes auditores y se agrega el drop correspondiente
            DAOEnteAuditor.GetEntesAuditores().ForEach(ente =>
            {
                ddlEnteAuditor.Items.Add(new ListItem(ente.StrNombre));
            });
        }

        /// <summary>
        /// Metodo que verifica que se halla subido un archivo para la auditoria y lo guarda en una variable de Session.
        /// </summary>
        [WebMethod(EnableSession = true)]
        public static List<dynamic> cargarArchivo(GNArchivo archivo)
        {
            //se consulta la lista de archivos en la variable de sission y en caso de que no exita crea una nueva
            List<GNArchivo> archivosSubidos = (List<GNArchivo>)HttpContext.Current.Session["archivosSubidos"] ?? new List<GNArchivo>();

            //se pasa el archivo recien creado a la lista de los archivos
            archivosSubidos.Add(archivo);

            //seguarda la lista de los archivos en la variable de session con el archivo nuevo;
            HttpContext.Current.Session["archivosSubidos"] = archivosSubidos;

            //se crea una copia de la lista de los archivo subidos para evitar datos pesados
            var archivosSubidosClone = new List<dynamic>();
            foreach (var archivoAux in archivosSubidos)
            {
                archivosSubidosClone.Add(new { StrNombre = archivoAux.StrNombre, StrExt = archivoAux.StrExt });
            }

            return archivosSubidosClone;
        }


        [WebMethod(EnableSession = true)]
        public static int SetAuditoriaExterna(Entidades.Auditorias.AuditoriaExterna auditoria, List<Hallazgo> hallazgos)
        {
            //Se consulta los archivo cargados que se encuentran en la variable de session
            List<GNArchivo> archivos = (List<GNArchivo>)HttpContext.Current.Session["archivosSubidos"];

            int OidAuditoria = 0;

            //se crea una instancia de lista de los archivos para la auditoria
            GNListaArchivos listaArchivos = new GNListaArchivos
            {
                IntOidGNModulo = 9,
            };

            //se guardar la lista recien creada en la base de datos
            DAOGNListaArchivos.set(listaArchivos);
            listaArchivos = DAOGNListaArchivos.GetUltimo();

            //en caso de que se hallan subido archivos se guardan en la base de datos 
            if (archivos != null)
            {
                foreach (var archivo in archivos)
                {
                    //se relaciona el archivo con la lista de los archivos
                    archivo.IntOidGNListaArchivos = listaArchivos.IntOidGNListaArchivos;
                    DAOGNArchivo.set(archivo);
                }
            }

            //se relaciona la lista de los archivos con la auditoria
            auditoria.IntOidListaArchivos = listaArchivos.IntOidGNListaArchivos;
            auditoria.IntOidUsuarioCreador = Convert.ToInt32(HttpContext.Current.Session["Admin"]);
            OidAuditoria = DAOAuditoriaExterna.SetAuditoriaExterna(auditoria);

            hallazgos.ForEach(hallazgo =>
            {
                hallazgo.IntContexto = Hallazgo.AUDITORIA_EXTERNA;
                hallazgo.IntInstancia = OidAuditoria;
                DAOHallazgo.SetHallazgo(hallazgo);
            });
            HttpContext.Current.Session.Remove("archivo");

            return OidAuditoria;
        }


        //metodo que elimina un archivo que haya sido subido como anexo a una auditoria
        [WebMethod(EnableSession = true)]
        public static List<dynamic> deleteArchivoSubido(int index)
        {
            //se consulta el listado de los archivos anexos que se encuentran en base de datos 
            List<GNArchivo> archivosSubidos = (List<GNArchivo>)HttpContext.Current.Session["archivosSubidos"] ?? new List<GNArchivo>();
           
            //se valida que el listado de los archivo no este vacio
            if (index > archivosSubidos.Count - 1)
                return new List<dynamic>();
           
            //se elimina el archivo en el listado de acuerdo a la posicion suministrada por el usuario
            archivosSubidos.RemoveAt(index);

            //se vuelve a guardar el listado de los archivos en la variable de session 
            HttpContext.Current.Session["archivosSubidos"] = archivosSubidos;

            //se crea otro listado para clonar la informacion de los archivos que se van a enviar a la vista
            var archivosSubidosClone = new List<dynamic>();

            //para agilizar el proceso de carga de la vista de los archivos subidos se solamente so copea el nombre de los archivos i no su informacion concreata
            foreach (var archivoAux in archivosSubidos)
            {
                archivosSubidosClone.Add(new { StrNombre = archivoAux.StrNombre, StrExt = archivoAux.StrExt });
            }
            return archivosSubidosClone;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }


        //mteoto que retorna un listado de los archivos anexos a la auditoria que se encuentran en la variable de session 
        [WebMethod]
        public static List<GNArchivo> GetArchivosSubidos()
        {
            return (List<GNArchivo>)HttpContext.Current.Session["archivosSubidos"];
        }
    }
}