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
        public static int SalidaPaciente(string CScodigoR, string CSiden, string NOMBRE_COMPLETO, long CSingreso)
        {
            return ControlEntSalController.InserPciente(CScodigoR, CSiden, NOMBRE_COMPLETO, CSingreso);
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

         [WebMethod]
        public static int SetDarSalidaAcuBBConBoleta(string ADNINGRES1, string DocResponsable)
        {
            return ControlEntSalController.DarSalidaAcuBBConBoletaSet(ADNINGRES1, DocResponsable);
        }

         [WebMethod]
        public static int GetCountPacienteSalida(int ingreso)
        {
            return ControlEntSalController.CountPacienteSalida( ingreso);
        }

        [WebMethod]
        public static List<SPacienteReal> GetSPacientesReal(string buscar)
        {
            return ControlEntSalController.GetSPacienteReal(buscar);
        }


//--------------------------------------------  CONTROL  ENTRADA-SALIDA DE VISITANTES  -------------------------------------------------//

        [WebMethod]
        public static List<Censo> GetCenso(string buscar, string grupo, string subgrupo)
        {
            return CensoController.CensoGet(buscar, grupo, subgrupo);
        }

        [WebMethod]

        public static List<Censo> GetCensoSubGrupos(string Cod_grupo)
        {
            return CensoController.CensoSubGruposGet(Cod_grupo); 
        }

    
        [WebMethod]
        public static int SetInserVisita(string ADNINGRES1, string Cod_cama, string DocPaciente, string NomPaciente, string DocResponsable, string NombreRes)
        {
            return ControlEntSalController.InserVisita(ADNINGRES1, Cod_cama, DocPaciente, NomPaciente, DocResponsable, NombreRes);
        }


        [WebMethod]
        public static void SetSalidaVisita(string ADNINGRES1)
        {
            ControlEntSalController.SalidaVisitaSet(ADNINGRES1);
        }



    }
}