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
    public partial class CSPacienteBB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static void SetAcudienteBB( int CSingresoBB, string CSInomsBB, string CSIidenBB, string CSAidenBB, string CSAtipoBB, string CSAnomsBB, int CSedadBB, string ORDENSALIDA)
        {
                CSPacienteBBController.AcudienteBBSet(CSingresoBB, CSInomsBB, CSIidenBB, CSAidenBB, CSAtipoBB, CSAnomsBB, CSedadBB,  ORDENSALIDA);
        }

        [WebMethod]
        public static int SetAcudienteBBMenor( int CSingresoBB, string CSInomsBB, string CSIidenBB, string CSAidenBB, string CSAtipoBB, string CSAnomsBB, int CSedadBB, string ORDENSALIDA)
        {
             return CSPacienteBBController.AcudienteBBMenorSet(CSingresoBB, CSInomsBB, CSIidenBB, CSAidenBB, CSAtipoBB, CSAnomsBB, CSedadBB, ORDENSALIDA);
        }

        [WebMethod]
        public static List<ControlEntSalModel> GetAcudienteBB( string CSingresoBB)
        {
            return CSPacienteBBController.AcudienteBBGet(CSingresoBB);
        }


        [WebMethod]
        public static List<ControlEntSalModel> GetPacientesIngreso(long Codigo)
        {
            return CSPacienteBBController.GetPacienteIngreso(Codigo);
        }

        [WebMethod]
        public static List<SPacienteBBModel> GetsalidaPorDocBB( string doc)
        {
            return CSPacienteBBController.SalidaPorDocBBGet(doc);
        }
        
        [WebMethod]
        public static List<SPacienteBBModel> GetsalidaBB( string ingreso)
        {
            return CSPacienteBBController.SalidaBBget(ingreso);
        } 
        
        [WebMethod]
        public static List<SPacienteBBModel> GetUpdateAcuBB( string oid)
        {
            return CSPacienteBBController.AcuBBGetUpdate(oid);
        }

         [WebMethod]
        public static void DeleteAcuBB( string oid)
        {
             CSPacienteBBController.AcuBBDelete(oid);
        }

        [WebMethod]
        public static void UpdateAcuBB( int CSAoidEdiBB, string CSAidenIdiBB, string CSATpResEdiCCBB, string CSACCNombreEdiBB )
        {
             CSPacienteBBController.AcuBBUpdate(CSAoidEdiBB, CSAidenIdiBB, CSATpResEdiCCBB, CSACCNombreEdiBB);
        }

        [WebMethod]
        public static int GetCountPacienteSalida(int ingreso)
        {
            return CSPacienteBBController.CountPacienteSalida(ingreso);
        }

    }
}