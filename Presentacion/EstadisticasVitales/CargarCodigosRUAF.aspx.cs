using Entidades.EstadisticasVitales;
using Logica.EstadisticasVitales;
using Newtonsoft.Json;
using Persistencia.EstadisticasVitales;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace Presentacion.EstadisticasVitales
{
    public partial class CargarCodigosRUAF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(enableSession: true)]
        public static string guardarCodigoRuaf(List<CRCodRuaf> CodRuaf)
        {
            foreach (CRCodRuaf codigoRuaf in CodRuaf)
            {
                codigoRuaf.DoubleGNCodUsu = Convert.ToDouble(HttpContext.Current.Session["admin"]);
                DAOCRCodRuaf.SetCodigoRuaf(codigoRuaf);
            }

            Community.sentEmail(); //validar codigos disponibles, para enviar correo en caso de que hallan pocos.

            return JsonConvert.SerializeObject(CodRuaf);
        }

        [WebMethod(enableSession: true)]
        public static string valCodRuafDup(List<CRCodRuaf> CodRuaf)
        {
            List<CRCodRuaf> codRuafDupBD = new List<CRCodRuaf>();
            List<CRCodRuaf> codRuafBD = DAOCRCodRuaf.GetCodRuafTot();
            foreach (CRCodRuaf codRuaf in CodRuaf)
            {
                foreach (CRCodRuaf codRuafBd in codRuafBD)
                {
                    if (codRuaf.DoubleCRcodRuaf == codRuafBd.DoubleCRcodRuaf)
                    {
                        codRuafDupBD.Add(codRuaf);
                        break;
                    }
                }
            }

            return JsonConvert.SerializeObject(codRuafDupBD);
        }
    }
}