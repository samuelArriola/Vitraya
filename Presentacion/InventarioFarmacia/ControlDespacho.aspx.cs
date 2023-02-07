using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades.InventarioFarmacia;
using Persistencia.InventarioFarmacia;

namespace Presentacion.InventarioFarmacia
{
    public partial class ControlDespacho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<ControlDespModel> SuministrosGet(string oid_suministro)
        {
            return ControlDespControler.GetSuminustros(oid_suministro);
        }

        [WebMethod]
        public static Boolean Login(string user, string pass)
        {
             return ControlDespControler.loginFirma(user, pass);
        } 
        
        [WebMethod]
        public static void InserSuministro( string OIDSUMINISTRO, long CONSECUTIVO, long DOCUMENTO_PAC, long USUARIOFIRMA, char CPAC, char CCANT, char CVIAADMIN , char CDOSIS, string OBSPRO )
        {

           ControlDespControler.setSuministro(OIDSUMINISTRO, CONSECUTIVO, DOCUMENTO_PAC, USUARIOFIRMA, CPAC, CCANT, CVIAADMIN , CDOSIS, OBSPRO );

        } 

        [WebMethod]
        public static int CountSuministros( string OIDSUMINISTRO)
        {

           return ControlDespControler.CountSuministro(OIDSUMINISTRO);

        }


    }
}