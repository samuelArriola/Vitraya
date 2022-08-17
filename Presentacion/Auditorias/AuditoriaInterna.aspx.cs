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
    public partial class AuditoriaInterna : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDdlProcesos();
                CargarDdlResponsable();
            }

        }
        //metodo que carga un listado de los usuarios en los drops correpondientes 
        protected void CargarDdlResponsable()
        {
            //se consulta un listado de lso usuarios en la base de datos
            List<Usuario> usuarios = DAOUsuario.getUsuarios();
            //se limpia la informacion de los drops 
            ddlResponsable.Items.Clear();
            ddlResponsableHall.Items.Clear();
            //se inserta el primer item seleccione con valor de -1 en los drops correspondientes
            ddlResponsable.Items.Add(new ListItem("Seleccione", "-1"));
            ddlResponsableHall.Items.Add(new ListItem("Seleccione", "-1"));
            
            //por cada usuario se crea un nuevo item para los drops
            usuarios.ForEach(usuario =>
            {
                ddlResponsable.Items.Add(new ListItem(usuario.GNNomUsu1));
                ddlResponsableHall.Items.Add(new ListItem(usuario.GNNomUsu1, usuario.GNCodUsu1 + ""));
            });
        }

        //mteoto que carga un listado de los procesos en el drop correpondiente 
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
        public static int SetAuditoriaInterna(Entidades.Auditorias.AuditoriaInterna auditoria, List<Hallazgo> hallazgos)
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

            //se guarda la auditoria en la base de datos
            OidAuditoria = DAOAuditoriaInterna.SetAuditoriaInterna(auditoria);

            //se guarda los hallazgos en la base de datos
            hallazgos.ForEach(hallazgo =>
            {
                //se indica que el hallazago pertenece a una auditoria Interna
                hallazgo.IntContexto = Hallazgo.AUDITORIA_INTERNA;

                //se relalciona el hallazgo con la auditoria
                hallazgo.IntInstancia = OidAuditoria;

                //se guarda el hallazgo en la base de datos
                DAOHallazgo.SetHallazgo(hallazgo);
            });

            //se eliminan los archivos de la variable de session
            HttpContext.Current.Session.Remove("archivosSubidos");


            return OidAuditoria;
        }

        //metodo que se encarga de eliminar un archivo que se cargo como anexo a la auditoria
        [WebMethod(EnableSession = true)]
        public static List<dynamic> deleteArchivoSubido(int index)
        {
            //se consulta el listado de los archivos en la variable de session 
            List<GNArchivo> archivosSubidos = (List<GNArchivo>)HttpContext.Current.Session["archivosSubidos"] ?? new List<GNArchivo>();
            
            //se verifica que el listado no se encietre vacio, y en caso contrario se elmina el archivo correspondiente a la posicion suminstrada
            if (index > archivosSubidos.Count - 1)
                return new List<dynamic>();
            archivosSubidos.RemoveAt(index);

            //se recarga el listado en la variable de session 
            HttpContext.Current.Session["archivosSubidos"] = archivosSubidos;

            //se crea una lista como copia de la lista de los archivos
            var archivosSubidosClone = new List<dynamic>();

            //se gurada la informacion de la lista de los archivos en la lista copia a exepcion del dato binario para
            //hacer la carga nas rapida en la vista del usuario
            foreach (var archivoAux in archivosSubidos)
            {
                archivosSubidosClone.Add(new { StrNombre = archivoAux.StrNombre, StrExt = archivoAux.StrExt });
            }
            return archivosSubidosClone;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }


        //metodo que retorna un listado de los archivos que se encuentran en la variable de session 
        [WebMethod]
        public static List<GNArchivo> GetArchivosSubidos()
        {
            return (List<GNArchivo>)HttpContext.Current.Session["archivosSubidos"];
        }
    }
}