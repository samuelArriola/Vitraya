using Logica.EstadisticasVitales;
using Newtonsoft.Json;
using Persistencia.EstadisticasVitales;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace Presentacion.EstadisticasVitales
{
    public partial class EliminarCodigos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(enableSession: true)]
        public static string EliminarCodigoRuafs(List<double> numCodRepBD)
        {

            List<string> CodsNoDeleted = new List<string>();

            foreach (var wi in numCodRepBD)
            {
                if (!DAOCRCodRuaf.eliminarCodRuaf(wi.ToString()))
                {
                    CodsNoDeleted.Add(wi.ToString());
                }
            }

            Community.sentEmail(); //validar codigos disponibles, para enviar correo en caso de que hallan pocos. 

            return JsonConvert.SerializeObject(CodsNoDeleted);
        }

    }

}