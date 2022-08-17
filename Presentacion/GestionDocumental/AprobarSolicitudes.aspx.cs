using Entidades.Generales;
using Entidades.GestionDocumental;
using Newtonsoft.Json;
using Persistencia.Generales;
using Persistencia.GestionDocumental;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace Presentacion.GestionDocumental
{
    public partial class AprobarSolicitudes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// metodo que devuelve una lista de solicitudes pendientes
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="tipoSol"></param>
        /// <param name="fecha"></param>
        /// <param name="tipoDoc"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetSolicitudes(string nombre, string tipoSol, DateTime fecha, string tipoDoc, string estado)
        {
            List<GDSolicitud> solicitudes = DAOGDSolicitud.GetSolisitudes(nombre, tipoSol, fecha, tipoDoc, estado);
            return JsonConvert.SerializeObject(solicitudes);
        }

        //metodo que aprueba o rechaza una solicitud deacuedo a lo establecido por el usuario para la solicitud
        [WebMethod]
        public static string SetEvaluacion(string oidSolicitud, string incidencia, string estado)
        {

            //se obtiene la solicitud a actualizar
            GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(oidSolicitud);

            //se obtine la evalucion que pertenece a esta solicitud
            GDEvaluacion Evaluacion = DAOGDEvaluacion.GetUltEvalucion(Convert.ToInt32(oidSolicitud));

            //se cambian los estados de la sulicitud y la evalucion de esta dependiendo de los datos dados
            solicitud.StrEstado = estado;
            solicitud.StrIncidencia = incidencia;
            Evaluacion.StrEstado = estado;
            Evaluacion.SrtInsidencia = incidencia;

            //se actiliza la solicitud y su evalucion en base de datos
            DAOGDSolicitud.SetUpdate(solicitud);
            DAOGDEvaluacion.Setupdate(Evaluacion);

            Usuario elaborador = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(solicitud.DblCodUsu));

            List<string> correos = new List<string>();

            correos.Add(elaborador.GNCrusu1);

            //if (estado == "Aprobado")
            //{
            //    string mensaje = $"" +
            //        $"<p>Buen día,<br/></p>" +
            //        $"Se le informa que la solicitud para la elaboración del documento {solicitud.StrNomDoc} a sido aprobada ";

            //    if (string.IsNullOrEmpty(incidencia))
            //    {
            //        mensaje += $"<p>Recordandole que tiene las siguientes Anotaciones: <br/></p>" +
            //            $"<p>{incidencia}.</p>";
            //    }

            //    Email.SendMail(correos, mensaje, $"Solicitud de creación del documento {solicitud.StrNomDoc} ha sido aceptada");
            //}
            //else
            //{
            //    string mensaje = $"" +
            //        $"<p>Buen día,<br/></p>" +
            //        $"Se le informa que la solicitud para la elaboración del documento {solicitud.StrNomDoc} a sido rechazada"+
            //        $"<p>Recordandole que tiene las siguientes Anotaciones: <br/></p>" +
            //        $"<p>{incidencia}.</p>";

            //    Email.SendMail(correos, mensaje, $"Solicitud de creación del documento {solicitud.StrNomDoc} ha sido recahazada");
            //}
            return JsonConvert.SerializeObject("");
        }
    }
}