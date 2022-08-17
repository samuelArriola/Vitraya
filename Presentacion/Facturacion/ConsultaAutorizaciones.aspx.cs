using Entidades.Facturacion;
using Persistencia.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Facturacion
{
    public partial class ConsultaAutorizaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<EConsultaAutorizaciones> GetHistorico()
        {
            return PConsultaAutorizaciones.GetHistorico();
        }

        [WebMethod]
        public static List<EConsultaAutorizaciones> ConsultaAut(string numeroAutorizacion)
        {
            return PConsultaAutorizaciones.ConsultaAut(numeroAutorizacion);
        }

        [WebMethod]
        public static void SetHistorico(string numeroAutorizacion, string estadoAutorizacion)
        {
            PConsultaAutorizaciones.SetHistorico(numeroAutorizacion, estadoAutorizacion);
        }
        
        [WebMethod]
        public static List<EConsultaAutorizaciones> GetFiltro1(string fecha1, string fecha2)
        {
            return PConsultaAutorizaciones.getFiltro1(fecha1, fecha2);
        }

        [WebMethod]
        public static List<EConsultaAutorizaciones> GetFiltro2(string fecha1, string fecha2)
        {
            return PConsultaAutorizaciones.getFiltro2(fecha1, fecha2);
        }

        [WebMethod]
        public static List<EConsultaAutorizaciones> GetFiltro3(string fecha1, string fecha2)
        {
            return PConsultaAutorizaciones.getFiltro3(fecha1, fecha2);
        }

    }
}