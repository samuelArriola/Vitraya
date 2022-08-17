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
    public partial class CSPaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<ControlEntSalModel> GetPaciente(long Codigo)
        {
            return ControlEntSalController.GetPacientes(Codigo);
        }

        [WebMethod]
        public static int SalidaPaciente(string CScodigoR, string CSiden)
        {
            return ControlEntSalController.InserPciente(CScodigoR, CSiden);
        }

        [WebMethod]
        public static void SPnoCoincide(string CScodigoR, string CSmanilla)
        {
            ControlEntSalController.NoCoincideSP(CScodigoR, CSmanilla);
        }

        [WebMethod]
        public static void SetDarSalidaAcuBB( string oid)
        {
            ControlEntSalController.DarSalidaAcuBBSet( oid );
        }


    }
}