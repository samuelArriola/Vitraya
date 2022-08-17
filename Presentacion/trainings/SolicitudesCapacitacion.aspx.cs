using Entidades.Generales;
using Entidades.trainings;
using Persistencia.Generales;
using Persistencia.trainings;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace Presentacion.trainings
{
    public partial class SolicitudesCapacitacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<CPSolicitud> GetSolicitudes(string tema, string fecha1, string fecha2, string lugar)
        {
            return DAOCPSolicitud.GetCPSolicitudes(tema, fecha1, fecha2, lugar);
        }

        /// <summary>
        /// Metodo que devuelve toda la infomacion registrada en una solicitud
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        [WebMethod]
        public static object[] GetInfoSolicitud(int idSolicitud)
        {
            //objeto con los datos de la  solicitud a consultar
            object[] infoSolicitud = null;
            try
            {
                //se consulta la solicitud
                CPSolicitud solicitud = DAOCPSolicitud.GetSolicitud(idSolicitud);

                //Listado de los subtemas que se abjuntaron a la solicitud de la capacitacion
                List<CPSUBTEMA> subtemas = DAOCPSUBTEMA.GetSubtemas(solicitud.IntOidCpsolicitud);

                //eje tematico de la capacitacion
                CPEJETEMATICO ejeTematico = DAOCPEjeTematico.GetEjeTematico(solicitud.IntOidCPEjeTematico);

                //listado de archivo subidos para la solicitud de la capacitacion
                List<GNArchivo> archivos = DAOGNArchivo.Listar(solicitud.IntOidListaArchivos);

                //archivo que se subio con la descripcion del examen
                GNArchivo archExamen = DAOGNArchivo.get(solicitud.IntOidGNAchivo);

                //se guardan todos los datos consultados anteriormente en la lista de objetos
                infoSolicitud = new object[] { solicitud, subtemas, ejeTematico, archivos, archExamen };
            }
            catch (Exception)
            {
            }
            //se retorna toda la infomacion de la solicitud
            return infoSolicitud;

        }
    }
}