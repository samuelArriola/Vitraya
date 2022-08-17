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
    public partial class NuevaAgenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod(EnableSession = true)]
        public static List<dynamic> cargarArchivo(GNArchivo archivo)
        {
            //se consulta la lista de archivos en la variable de sission y en caso de que no exita crea una nueva
            List<GNArchivo> archivosSubidos = (List<GNArchivo>)HttpContext.Current.Session["archivosNuevaAgenda"] ?? new List<GNArchivo>();

            //se pasa el archivo recien creado a la lista de los archivos
            archivosSubidos.Add(archivo);

            //seguarda la lista de los archivos en la variable de session con el archivo nuevo;
            HttpContext.Current.Session["archivosNuevaAgenda"] = archivosSubidos;

            //se crea una copia de la lista de los archivo subidos para evitar datos pesados
            var archivosSubidosClone = new List<dynamic>();
            foreach (var archivoAux in archivosSubidos)
            {
                archivosSubidosClone.Add(new { StrNombre = archivoAux.StrNombre, StrExt = archivoAux.StrExt });
            }

            return archivosSubidosClone;
        }

        [WebMethod(EnableSession = true)]
        public static dynamic GetAgenda(int idCapacitacion)
        {
            //se conulta la Agenda a Editar
            CPAgenda agenda = DAOCPAgenda.GetAgendaUlt(idCapacitacion);

            //se consulta los subtemas pertenecientes a la agenda
            List<CPSUBTEMA> subtemas = DAOCPSUBTEMA.GetSubtemasByAgenda(agenda.IntOidCPAgenda);

            //Se cousultan los archvivos de la agenda y se guardan en la variable de Session 
            List<GNArchivo> archivosEditarAgenda = DAOGNArchivo.Listar(agenda.IntOidGNListaArchivos);
            HttpContext.Current.Session["archivosNuevaAgenda"] = archivosEditarAgenda;

            return new { Agenda = agenda, Subtemas = subtemas };
        }

        [WebMethod]
        public static List<GNArchivo> GetArchivosSubidos()
        {
            return (List<GNArchivo>)HttpContext.Current.Session["archivosNuevaAgenda"];
        }

        [WebMethod(EnableSession = true)]
        public static List<dynamic> deleteArchivoSubido(int index)
        {
            List<GNArchivo> archivosSubidos = (List<GNArchivo>)HttpContext.Current.Session["archivosNuevaAgenda"] ?? new List<GNArchivo>();
            if (index > archivosSubidos.Count - 1)
                return new List<dynamic>();
            archivosSubidos.RemoveAt(index);

            HttpContext.Current.Session["archivosNuevaAgenda"] = archivosSubidos;

            var archivosSubidosClone = new List<dynamic>();

            foreach (var archivoAux in archivosSubidos)
            {
                archivosSubidosClone.Add(new { StrNombre = archivoAux.StrNombre, StrExt = archivoAux.StrExt });
            }
            return archivosSubidosClone;
        }

        [WebMethod]
        public static dynamic CreateAgenda(CPAgenda agenda, List<string> Subtemas)
        {

            // listado de los datos que se van a retonar con el id de la capacitacion  a crear
            //y el id de la agenda a crear
            dynamic datos = new { idCapacitacion = 0, idAgenda = 0 };

            try
            {
                //se obtiene el listado de los archivos subidos que se encuentran en la variable de sesión 
                List<GNArchivo> archivos = (List<GNArchivo>)HttpContext.Current.Session["archivosNuevaAgenda"] ?? new List<GNArchivo>();

                //se crea un nuevo listado de los archivos  para la agenda
                GNListaArchivos listaArchivos = new GNListaArchivos
                {
                    IntOidGNModulo = 3,
                };

                //se guarda el listado de los archivos  en la base de datos
                DAOGNListaArchivos.set(listaArchivos);

                //se obtiene la informacion del listado de los archivos recien creado en la base de datos
                listaArchivos = DAOGNListaArchivos.GetUltimo();

                //se asigna el id del listado de los archivos a la agenda de la capacitacion
                agenda.IntOidGNListaArchivos = listaArchivos.IntOidGNListaArchivos;

                //se asigna el id de la lista de los archivos a los archivos subidos y se guardan en la base de datos
                archivos.ForEach(archivo =>
                {
                    archivo.IntOidGNListaArchivos = listaArchivos.IntOidGNListaArchivos;
                    DAOGNArchivo.set(archivo);
                });

                CPCAPACITACION capacitacion = DAOCPCapacitacion.GetCapacitacion(agenda.IntOidCPCapacitacion);


                //se relaciona la lista de los archivo con la agenda de la capacitacion
                agenda.IntOidGNListaArchivos = listaArchivos.IntOidGNListaArchivos;

                agenda.IntOidUsuarioResponsable = capacitacion.IntGNCodUsu;

                agenda.StrResponsable = capacitacion.StrRESPONSABLE;

                //se gurada la agenda en la base de datos y se obtiene el id generado
                int idAgenda = DAOCPAgenda.SetCPAgenda(agenda);
                datos = new { idCapacitacion = agenda.IntOidCPCapacitacion, idAgenda = idAgenda };

                //se guradan los subtemas en la base de datos
                Subtemas.ForEach(Subtema =>
                {
                    DAOCPSUBTEMA.setSubtema(new CPSUBTEMA
                    {
                        IntContexto = 1,
                        IntOidCPInstacia = agenda.IntOidCPCapacitacion,
                        StrSUBTEMA = Subtema,
                        IntOidCPSUBTEMA = 0,
                        IntOidCPAgenda = idAgenda
                    });
                });

            }
            catch (Exception)
            {

            }
            return datos;
        }
    }
}