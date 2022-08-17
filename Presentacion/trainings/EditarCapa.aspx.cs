using Entidades.Generales;
using Entidades.trainings;
using Persistencia.Generales;
using Persistencia.trainings;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace Presentacion.trainings
{
    public partial class EditarCapa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        [WebMethod(EnableSession = true)]
        public static List<dynamic> cargarArchivo(GNArchivo archivo)
        {
            //se consulta la lista de archivos en la variable de sission y en caso de que no exita crea una nueva
            List<GNArchivo> archivosSubidos = (List<GNArchivo>)HttpContext.Current.Session["archivosEditarAgenda"] ?? new List<GNArchivo>();

            //se pasa el archivo recien creado a la lista de los archivos
            archivosSubidos.Add(archivo);

            //seguarda la lista de los archivos en la variable de session con el archivo nuevo;
            HttpContext.Current.Session["archivosEditarAgenda"] = archivosSubidos;

            //se crea una copia de la lista de los archivo subidos para evitar datos pesados
            var archivosSubidosClone = new List<dynamic>();
            foreach (var archivoAux in archivosSubidos)
            {
                archivosSubidosClone.Add(new { StrNombre = archivoAux.StrNombre, StrExt = archivoAux.StrExt });
            }

            return archivosSubidosClone;
        }
        /// <summary>
        /// memtodo que retorna todos los archivos guardados en la variable de sesión
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<GNArchivo> GetArchivosSubidos()
        {
            return (List<GNArchivo>)HttpContext.Current.Session["archivosEditarAgenda"];
        }

        /// <summary>
        /// metodo que retorna un listado de los usuarios en la base de datos consultandolos por el nombre
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<dynamic> GetUsers(string name)
        {
            List<dynamic> data = new List<dynamic>();
            DAOUsuario.GetListadoUsuarios(name).ForEach(usuario => data.Add(new { text = usuario.GNNomUsu1, value = usuario.GNCodUsu1 }));
            return data;
        }



        /// <summary>
        /// metodo que elimina un archivo de la variable de sesión
        /// </summary>
        /// <param name="index">indice del archivo en la lista de los archivo que se va a eliminar</param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public static List<dynamic> deleteArchivoSubido(int index)
        {
            //se verifica el la lista de los archivos se encuentre en la variable de sesión, de lo contrario de retorna un lista vacia
            List<GNArchivo> archivosSubidos = (List<GNArchivo>)HttpContext.Current.Session["archivosEditarAgenda"] ?? new List<GNArchivo>();
            if (index > archivosSubidos.Count - 1)
                return new List<dynamic>();

            //se elimina el archivo en la posición indicada 
            archivosSubidos.RemoveAt(index);

            //se gurada la lista de los archivos nuevamente en la variable de sesión
            HttpContext.Current.Session["archivosEditarAgenda"] = archivosSubidos;


            //se retorna la lista de los archivos
            var archivosSubidosClone = new List<dynamic>();

            foreach (var archivoAux in archivosSubidos)
            {
                archivosSubidosClone.Add(new { StrNombre = archivoAux.StrNombre, StrExt = archivoAux.StrExt });
            }
            return archivosSubidosClone;
        }

        [WebMethod(EnableSession = true)]
        public static dynamic GetAgenda(int idAgenda)
        {
            //se conulta la Agenda a Editar
            CPAgenda agenda = DAOCPAgenda.GetAgenda(idAgenda);

            //se consulta los subtemas pertenecientes a la agenda
            List<CPSUBTEMA> subtemas = DAOCPSUBTEMA.GetSubtemasByAgenda(idAgenda);

            //Se cousultan los archvivos de la agenda y se guardan en la variable de Session 
            List<GNArchivo> archivosEditarAgenda = DAOGNArchivo.Listar(agenda.IntOidGNListaArchivos);
            HttpContext.Current.Session["archivosEditarAgenda"] = archivosEditarAgenda;

            return new { Agenda = agenda, Subtemas = subtemas };
        }
        /// <summary>
        /// Metodo que actuliza las agendas
        /// </summary>
        /// <param name="agenda"></param>
        /// <param name="subtemas"></param>
        [WebMethod(EnableSession = true)]
        public static void UpdateAgtenda(CPAgenda agenda, List<string> subtemas)
        {
            //se crea una instacia de agenda con la informacion antigua de la bd de la agenda a actulizar
            CPAgenda auxAgenda = DAOCPAgenda.GetAgenda(agenda.IntOidCPAgenda);

            //se pasan los datos que no se modificaran 
            agenda.IntOidCPCapacitacion = auxAgenda.IntOidCPCapacitacion;
            agenda.IntOidUsuarioResponsable = auxAgenda.IntOidUsuarioResponsable;
            agenda.StrResponsable = auxAgenda.StrResponsable;
            agenda.IntOidGNListaArchivos = auxAgenda.IntOidGNListaArchivos;
            agenda.IntOidCPExamen = auxAgenda.IntOidCPExamen;
            agenda.IntEstado = auxAgenda.IntEstado;

            //se actualiza la agenda en base datos
            DAOCPAgenda.UpdataCPAgenda(agenda);


            //se cargan los nuevos archivos para la agenda
            List<GNArchivo> archivos = (List<GNArchivo>)HttpContext.Current.Session["archivosEditarAgenda"];

            List<int> lstArchNoElim = new List<int>();
            if (archivos != null)
                foreach (var archivo in archivos)
                {
                    if (archivo.IntOidGNArchivo != 0)
                        lstArchNoElim.Add(archivo.IntOidGNArchivo);
                }

            var strLstArchNomElim = string.Join<int>(",", lstArchNoElim);

            DAOGNArchivo.DeleteArchivoByIdListaArch(agenda.IntOidGNListaArchivos, strLstArchNomElim);

            if (archivos != null)
                foreach (var archivo in archivos)
                {
                    if (archivo.IntOidGNArchivo == 0)
                    {
                        archivo.IntOidGNListaArchivos = agenda.IntOidGNListaArchivos;
                        DAOGNArchivo.set(archivo);
                    }
                }

            //se eliminan todos los subtemas que se habian cargado
            DAOCPSUBTEMA.DeleteSubtemaByIdAgenda(agenda.IntOidCPAgenda);

            //se cargan los nuevos subtemas para la agenda
            subtemas.ForEach(subtema =>
            {
                DAOCPSUBTEMA.setSubtema(new CPSUBTEMA
                {
                    IntOidCPAgenda = agenda.IntOidCPAgenda,
                    IntContexto = 1,
                    IntOidCPInstacia = agenda.IntOidCPCapacitacion,
                    StrSUBTEMA = subtema,
                });
            });
        }
    }
}