using Entidades.Procesos;
using Newtonsoft.Json;
using Persistencia.procesos;
using Persistencia.Procesos;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace Presentacion.Procesos
{
    public partial class ConsultarProcesos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetProcesos(string Tipo, string Nombre)
        {
            List<PCProceso> procesos = DAOProceso.listarFiltro(Nombre);

            foreach (var proceso in procesos)
            {
                proceso.SIPOCs = DAOSIPOC.getSipocs(Convert.ToString(proceso.IntOIdProceso));
            }
            return JsonConvert.SerializeObject(procesos);
        }

        [WebMethod]
        public static string GetDeleteProcesos(string OIdProceso)
        {
            bool isDelete = DAOProceso.DeleteProceso(Convert.ToInt32(OIdProceso));
            return JsonConvert.SerializeObject(isDelete);
        }
    }
}