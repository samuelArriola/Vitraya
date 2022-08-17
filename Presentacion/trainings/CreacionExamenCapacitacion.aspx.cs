using Entidades.trainings;
using Newtonsoft.Json;
using Persistencia.trainings;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace Presentacion.trainings
{
    public partial class CreacionExamenCapacitacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// metodo que recibe un examen 
        /// </summary>
        /// <param name="examen"></param>
        /// <returns></returns>
        [WebMethod(enableSession: true)]
        public static string crearExamen(CPEXAMEN examen, int idAgenda)
        {
            //se consulta si exite un examen para la capacitacion 
            CPEXAMEN examenAux = DAOCPExamen.GetExamenByAgenda(idAgenda);
            if (examenAux != null)
            {
                //se caso de que exita un examen se elimina para reemplazarlos por el nuevo
                DAOCPExamen.DeleleteExamen(examen.IntOidInstancia, examen.IntContexto);
            }
            // guardan los datos del examen dado por el usuario
            DAOCPExamen.setExamen(examen);
            CPEXAMEN CPExamen = DAOCPExamen.getExamenUltimo();
            foreach (var pregunta in examen.Preguntas)
            {
                //en caso de que exita un examen para esta capacitacion
                if (examenAux != null)
                {
                    //se eliminan las preguntas de este examen para reemplazarlas por las nuevas
                    DAOCPREGUNTA.DeletePregunta(pregunta.IntOidCPPREGUNTA);

                    //se eliminan las opciones de este examen para reemplazarlas por las nuevas
                    DAOCPOPCION.DeleteOpcionByIdPre(pregunta.IntOidCPPREGUNTA);
                }

                //se pasa el oid del examen a la pregunta 
                pregunta.IntOidCPEXAMEN = CPExamen.IntOidCPEXAMEN;
                DAOCPREGUNTA.setPreguntaExamen(pregunta);
                CPPREGUNTA CPPregunta = DAOCPREGUNTA.getPreguntaExamenUlt();
                foreach (var opcion in pregunta.Opciones)
                {
                    opcion.IntOidCPPREGUNTA = CPPregunta.IntOidCPPREGUNTA;
                    DAOCPOPCION.setOpcionExamen(opcion);
                }
            }

            //Se modifica el id del examen
            CPAgenda agenda = DAOCPAgenda.GetAgenda(idAgenda);

            if (agenda != null)
            {
                agenda.IntOidCPExamen = CPExamen.IntOidCPEXAMEN;
                DAOCPAgenda.UpdataCPAgenda(agenda);
            }
            return JsonConvert.SerializeObject(CPExamen);
        }


        /// <summary>
        /// Metodo que obtine un examen para luego editarlo
        /// </summary>
        /// <param name="idCapacitacion"></param>
        /// <param name="contexto"></param>
        /// <returns></returns>
        [WebMethod]
        public static string getExamenCapacitacion(int idCapacitacion, int contexto, int idAgenda)
        {
            CPEXAMEN examen = null;

            if ((examen = DAOCPExamen.GetExamenByAgenda(idAgenda)) == null)
            {
                examen = DAOCPExamen.getExamen(idCapacitacion, contexto);
            }

            //en caso de que no exista el examen se retorna un elemento vacio
            if (examen == null)
            {
                return null;
            }

            //se consulta las preguntas del examen
            List<CPPREGUNTA> preguntas = DAOCPREGUNTA.getPreguntaSExamen(examen.IntOidCPEXAMEN);

            //se agregan la preguntas al examen
            examen.Preguntas = preguntas;

            //se consultan la opciones de las preguntas  y se agregan a esta
            foreach (var pregunta in examen.Preguntas)
            {
                List<CPOPCION> opciones = DAOCPOPCION.getOpcionExamenes(pregunta.IntOidCPPREGUNTA);
                pregunta.Opciones = opciones;
            }

            HttpContext.Current.Session["examen"] = examen;

            //se retorna la informacion del examen
            return JsonConvert.SerializeObject(examen);
        }
    }
}