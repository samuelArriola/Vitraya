using Comunes;
using Entidades.Generales;
using Entidades.trainings;
using Persistencia.Generales;
using Persistencia.trainings;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace Presentacion.trainings
{
    public partial class AdministrarSolicitudes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //metodo que devuelve el listado de todas las solicitudes
        [WebMethod]
        public static List<CPSolicitud> GetSolcitudesCaps(string tema, string fecha1, string lugar, string fecha2)
        {
            return DAOCPSolicitud.GetCPSolicitudes(tema, fecha1, fecha2, lugar);
        }

        [WebMethod]
        public static object[] GetSolicitud(int idSolicitud)
        {
            CPSolicitud solicitud = DAOCPSolicitud.GetSolicitud(idSolicitud);
            List<CPSUBTEMA> subtemas = DAOCPSUBTEMA.GetSubtemas(solicitud.IntOidCpsolicitud);
            CPEJETEMATICO ejeTematico = DAOCPEjeTematico.GetEjeTematico(solicitud.IntOidCPEjeTematico);
            List<GNArchivo> archivos = DAOGNArchivo.Listar(solicitud.IntOidListaArchivos);
            GNArchivo archivoExa = DAOGNArchivo.get(solicitud.IntOidGNAchivo);

            return new object[] { solicitud, subtemas, ejeTematico, archivos, archivoExa };
        }

        /// <summary>
        /// metodo que cambia el estado de la solicitud y crea una capacitacion con la informacion de la solicitud 
        /// </summary>
        /// <param name="idSolicitud"></param>
        [WebMethod]
        public static void AceptarSolicitud(int idSolicitud, string justificacion)
        {
            //se crea la capacitacion con la informacion de la solicitud a traves del id de la solicitud
            DAOCPCapacitacion.SetCapacitacionBySolcitud(idSolicitud);

            //se consulta la Solicitud por el id
            CPSolicitud solicitud = DAOCPSolicitud.GetSolicitud(idSolicitud);

            //se consulta al usuario respozable de la solcitud
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(solicitud.IntGNCodUsu);

            //mesaje que sera envido notificando al usuario respozable que la capacitacion ha sido aceptada 
            string mensaje = $"Señor(a) {usuario.GNNomUsu1}, <br/></br>" +
                $"Buen día,<br/><br/>" +
                $"Se le Notifica que la que la solicitud para la capacitación: {solicitud.StrTema} ha sido aceptada ";
            if (string.IsNullOrEmpty(justificacion))
                mensaje += $"con la siguiente Justificación: <br/><br/> {justificacion}";
            mensaje += "pro favor dirijase la opción Administrar mis capacitaciones del modulo de Capacitaciones para realizar la capacitación";

            //listado de correos electrónicos
            List<string> correos = new List<string>();

            //se agrega el correo electrónico del resposable al listado correos
            correos.Add(usuario.GNCrusu1);

            Email.SendMail(correos, mensaje, $"Respuesta para la solicitud de la capacitación: {solicitud.StrTema}");

        }
    }
}