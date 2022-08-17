using Entidades.PlanAccion;
using Persistencia.proceedings;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace Presentacion.PlanAccion
{
    public partial class AdminPlanAccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Metodo que retorna un listado de los planes de acción para el usuario responsable del seguimiento 
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="lugar"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public static List<PAPlanAccion> GetPlanesAccionByIdSeg(string nombre, string lugar, string fecha)
        {
            int idUsuSeg = Convert.ToInt32(HttpContext.Current.Session["Admin"]);

            return DAOPAPlanAccion.GetPlanesByIdSig(idUsuSeg, nombre, lugar, fecha);
        }
    }
}