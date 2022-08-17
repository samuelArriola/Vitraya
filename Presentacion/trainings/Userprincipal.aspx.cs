using Entidades.trainings;
using Persistencia.Generales;
using Persistencia.trainings;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace Presentacion.trainings
{
    public partial class Userprincipal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetAsistenciaCapVirtualDoc();
            }
        }


        public void SetAsistenciaCapVirtualDoc()
        {
            DAOCPMatricula.SetAsistenciaCapVirtualDoc(Convert.ToInt32(Session["Admin"]));
        }

        [WebMethod(EnableSession = true)]
        public static List<dynamic> getCapacitaionesUsuario(string estado, string tema)
        {
            switch (estado)
            {
                case "matriculado":
                    {
                        return DAOCPAgenda.GEtAgendasByUsaurio(Convert.ToInt32(HttpContext.Current.Session["Admin"]), false, false, tema);
                    }
                case "asistido":
                    {
                        return DAOCPAgenda.GEtAgendasByUsaurio(Convert.ToInt32(HttpContext.Current.Session["Admin"]), false, true, tema);
                    }
                case "firmado":
                    {
                        return DAOCPAgenda.GEtAgendasByUsaurio(Convert.ToInt32(HttpContext.Current.Session["Admin"]), true, true, tema);
                    }
            }

            return new List<dynamic>();
        }
        [WebMethod]
        public static dynamic GetInfoAgenda(int idAgenda, int idListaArchivos)
        {
            return new
            {
                Subtemas = DAOCPSUBTEMA.GetSubtemasByAgenda(idAgenda),
                Archivos = DAOGNArchivo.Listar(idListaArchivos),
                Examenes = DAOCPEXAMENSOL.ListarExamSol(Convert.ToInt32(HttpContext.Current.Session["Admin"]), idAgenda)
            };
        }

        [WebMethod]
        public static void FirmarAsistencia(int idAgenda)
        {
            CPMatricula matricula = DAOCPMatricula.GetMatriculaByAgenda(idAgenda, Convert.ToInt32(HttpContext.Current.Session["Admin"]));
            matricula.BlnFirmado = true;
            DAOCPMatricula.UpdataMatricula(matricula);
        }
    }
}