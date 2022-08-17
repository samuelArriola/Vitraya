using Entidades.Generales;
using Entidades.trainings;
using Newtonsoft.Json;
using Persistencia.Generales;
using Persistencia.trainings;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace Presentacion.trainings
{
    public partial class InformeExamenes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarInformeExamenes();
        }

        public void cargarInformeExamenes()
        {
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(Session["Admin"]));
            CPEXAMENSOL examenSol = DAOCPEXAMENSOL.getExamenSol(Convert.ToInt32(Request["idExamenSol"]));
            CPAgenda agenda = DAOCPAgenda.GetAgenda(Convert.ToInt32(Request["idAgenda"]));
            CPCAPACITACION capacitacion = DAOCPCapacitacion.GetCapacitacion(agenda.IntOidCPCapacitacion);
            nomExa.InnerText = capacitacion.StrTEMA;
            string table = "<table class=\"table table-bordered\">" +
                "<thead>" +
                "   <tr>" +
                "       <th>Documento</th>" +
                "       <th>Nombre</th>" +
                "       <th>Cargo</th>" +
                "   </tr>" +
                "</thead>" +
                "<tbody>" +
                "   <tr>" +
                "       <td>" + usuario.GNCodUsu1 + "</td>" +
                "       <td>" + usuario.GNNomUsu1 + "</td>" +
                "       <td>" + usuario.GnCargo1 + "</td>" +
                "   </tr>" +
                "</tbody></table>" +
                "<table class=\"table table-bordered\">" +
                "   <thead>" +
                "       <tr>" +
                "           <th>Resultado</th>" +
                "           <th>Fecha de realización</th>" +
                "       </tr>" +
                "   </thead>" +
                "   <tbody>" +
                "       <tr>" +
                "           <td>" + examenSol.IntResultado + "%</td>" +
                "           <td>" + examenSol.DtmFecha.ToString("f") + "</td>" +
                "       </tr>" +
                "   </tbody>" +
                "</table>";
            encabezado.InnerHtml = table;


            firma.InnerHtml = "<img src =\"data:image/;base64," + Convert.ToBase64String(usuario.GNFmUsu1) + "\" width=\"75\" />";
            datosUsuario.InnerHtml = "<p style=\"font-size:11px;margin:0;\">" + usuario.GNNomUsu1 + "</p><p style=\"font-size:11px;margin:0;\">C.C. " + usuario.GNCodUsu1 + "</p>";
        }

        [WebMethod]
        public static string getExamenSol(int idExamensol, int idAgenda)
        {
            CPEXAMENSOL examenSol = DAOCPEXAMENSOL.getExamenSol(idExamensol);
            List<CPRESPUESTA> respuestas = DAOCPRESPUESTA.GetRespExaSol(idExamensol);
            examenSol.Respuestas = respuestas;

            CPEXAMEN examen = DAOCPExamen.getExamen(idAgenda);
            List<CPPREGUNTA> preguntas = DAOCPREGUNTA.getPreguntaSExamen(examen.IntOidCPEXAMEN);
            examen.Preguntas = preguntas;
            foreach (var pregunta in examen.Preguntas)
            {
                List<CPOPCION> opciones = DAOCPOPCION.getOpcionExamenes(pregunta.IntOidCPPREGUNTA);
                pregunta.Opciones = opciones;
            }

            return JsonConvert.SerializeObject(new object[] { examenSol, examen });
        }
    }
}