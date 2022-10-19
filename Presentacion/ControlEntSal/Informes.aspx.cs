using Entidades.ControlEntSal;
using Persistencia.ControlEntSal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.ControlEntSal
{
    public partial class Informes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static object GetSPacientesReal(DateTime fechaI, DateTime fechaF)
        {
            return InformesController.GetSPacienteReal( fechaI,  fechaF);
        } 

        [WebMethod]
        public static object GetSalidaBB(DateTime fechaI, DateTime fechaF)
        {
            return InformesController.SalidaBBget( fechaI,  fechaF);
        }

         [WebMethod]
        public static object PacientesFugaGet(DateTime fechaI, DateTime fechaF)
        {
            return InformesController.GetPacientesFuga( fechaI, fechaF);
        }

    }
}