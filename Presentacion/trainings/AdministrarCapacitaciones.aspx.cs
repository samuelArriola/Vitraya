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
    public partial class AministrarCapacitaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static List<CPCAPACITACION> GetCapacitaciones(string info)
        {
            return DAOCPCapacitacion.GetCapsByResp(info);
        }

        [WebMethod]
        public static List<CPAgenda> GetAgendasByIdCapacitacion(int idCapacitacion)
        {
            return DAOCPAgenda.GetAgendasByCapacitacion(idCapacitacion);
        }

        [WebMethod]
        public static void IniciarCapacitacion(int idAgenda)
        {
            CPAgenda agenda = DAOCPAgenda.GetAgenda(idAgenda);
            agenda.IntEstado = 2;
            DAOCPAgenda.UpdataCPAgenda(agenda);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                dtmFecha = DateTime.Now,
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = agenda.IntOidCPAgenda,
                strAccion = "Modificar",
                strDetalle = $"Se cambia el estado de la agenda a iniciada",
                strEntidad = "CPAgenda"
            });
        }
    }
}