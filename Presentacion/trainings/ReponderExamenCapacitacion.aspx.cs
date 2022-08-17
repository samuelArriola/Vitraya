using Entidades.trainings;
using Newtonsoft.Json;
using Persistencia.trainings;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace Presentacion.trainings
{
    public partial class ReponderExamenCapacitacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [WebMethod]
        public static string getExamenCapacitacion(int idCapacitacion)
        {
            CPEXAMEN examen = DAOCPExamen.getExamen(idCapacitacion);
            List<CPPREGUNTA> preguntas = DAOCPREGUNTA.getPreguntaSExamen(examen.IntOidCPEXAMEN);
            examen.Preguntas = preguntas;
            foreach (var pregunta in examen.Preguntas)
            {
                List<CPOPCION> opciones = DAOCPOPCION.getOpcionExamenes(pregunta.IntOidCPPREGUNTA);
                pregunta.Opciones = opciones;
            }


            HttpContext.Current.Session["examen"] = examen;
            return JsonConvert.SerializeObject(examen);
        }

        [WebMethod]
        public static string responderExamenCapacitacion(CPEXAMENSOL examenSol, int idAgenda)
        {
            int sumRespuestas = 0;
            CPEXAMEN examen = (CPEXAMEN)HttpContext.Current.Session["examen"];

            examenSol.IntIDMATRICULA = DAOCPMatricula.GetMatricula(Convert.ToInt32(HttpContext.Current.Session["Admin"].ToString()), idAgenda).IntOidCPMatricula;
            examenSol.IntOidPCEXAMEN = examen.IntOidCPEXAMEN;
            examenSol.DtmFecha = DateTime.Now;
            foreach (CPPREGUNTA pregunta in examen.Preguntas)
            {
                foreach (CPOPCION opcion in pregunta.Opciones)
                {
                    foreach (CPRESPUESTA respuesta in examenSol.Respuestas)
                    {
                        if (respuesta.IntOidCPOPCION == opcion.IntOidOPCION && opcion.IsCorrecta)
                            sumRespuestas++;
                    }
                }
            }
            float resultado = 10 / float.Parse(examen.Preguntas.Count.ToString()) * float.Parse((sumRespuestas * 10).ToString());
            examenSol.IntResultado = (int)resultado;
            DAOCPEXAMENSOL.setExamenSol(examenSol);
            CPEXAMENSOL auxExamenSol = DAOCPEXAMENSOL.getExamenSolUlt();
            foreach (CPRESPUESTA respuesta in examenSol.Respuestas)
            {
                respuesta.IntOidCPEXAMENSOL = auxExamenSol.IntOidCPEXAMENSOL;
                DAOCPRESPUESTA.setRespuestaExamCapa(respuesta);
            }

            return "{\"nota\":" + auxExamenSol.IntResultado + ",\"isAprobado\":" + ((auxExamenSol.IntResultado >= examen.IntNumApro) ? "true" : "false") + "}";

        }
    }
}