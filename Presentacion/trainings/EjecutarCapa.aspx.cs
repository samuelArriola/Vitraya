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
    public partial class EjecutarCapa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static dynamic GetMatriculas(string nombre, int idAgenda)
        {
            List<dynamic> matriculas = DAOCPMatricula.GetMatriculasByIdAgenda(idAgenda, nombre);
            CPCAPACITACION capacitacion = DAOCPCapacitacion.GetCapacitacionByAgenda(idAgenda);

            return new { Matriculas = matriculas, Capacitacion = capacitacion };
        }

        [WebMethod]
        public static List<CPSUBTEMA> GetSubtemas(int idAgenda)
        {
            return DAOCPSUBTEMA.GetSubtemasByAgenda(idAgenda);
        }

        [WebMethod]
        public static List<GNArchivo> GetArchivos(int idAgenda)
        {
            var agenda = DAOCPAgenda.GetAgenda(idAgenda);
            return DAOGNArchivo.Listar(agenda.IntOidGNListaArchivos);
        }

        [WebMethod]
        public static void SetAsistencia(bool asistido, int idMatricula)
        {
            CPMatricula matricula = DAOCPMatricula.GetMatricula(idMatricula);
            matricula.BlnAsistido = asistido;
            DAOCPMatricula.UpdataMatricula(matricula);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                dtmFecha = DateTime.Now,
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = matricula.IntOidCPMatricula,
                strAccion = "Modificar",
                strDetalle = $"Se Marca la asistencia al usuario {matricula.StrNOMUSUARIO} en la agenda con el código {matricula.IntOidCPAgenda}",
                strEntidad = "CPMATRICULA"
            });
        }

        /// <summary>
        /// Metodo que cambia el estado de la agenda a finalizada 
        /// </summary>
        /// <param name="idAgenda">id de la agenda a modificar</param>
        [WebMethod]
        public static void FinalizarCapacitacion(int idAgenda)
        {
            CPAgenda agenda = DAOCPAgenda.GetAgenda(idAgenda);
            agenda.IntEstado = 3;
            DAOCPAgenda.UpdataCPAgenda(agenda);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                dtmFecha = DateTime.Now,
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = agenda.IntOidCPAgenda,
                strAccion = "Modificar",
                strDetalle = $"Se cambia el estado de la agenda a finalizado",
                strEntidad = "CPAgenda"
            });
        }
    }
}