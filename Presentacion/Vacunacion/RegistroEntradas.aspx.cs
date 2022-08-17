using Entidades.Vacunacion;
using Persistencia.Vacunacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Vacunacion
{
    public partial class RegistroEntradas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        

        [WebMethod]
        public static List<VCInsumo> GetInsumos(string tipo)
        {
            return DAOVCInsumo.GetInsumos(tipo);
        }
        [WebMethod]
        public static  VCLote GetLoteExt(int idInsumo, string numLote)
        {
            return DAOVCLote.GetLote(idInsumo, numLote);
        }

        [WebMethod]
        public static void SetEntrada(List<VCLote> lotes, VCDetEntLot detalle, List<VCEntradaLote> entradaLotes)
        {
            detalle.IntOidUsuario = Convert.ToInt32(HttpContext.Current.Session["Admin"]);

            int idDetalle = DAOVCDetEntLot.SerDetEntLot(detalle);

            lotes.ForEach(lote => {
                int idLote = DAOVCLote.SetLote(lote);
                VCEntradaLote entrda = new VCEntradaLote
                {
                    FltCantidad = lote.IntTotalIngresado,
                    IntOidVCDetEntLot = idDetalle,
                    IntOidVCLote = idLote
                };
                DAOVCEntradaLote.SetEntradaLote(entrda);
            });

            entradaLotes.ForEach(entrada => {
                entrada.IntOidVCDetEntLot = idDetalle;
                DAOVCEntradaLote.SetEntradaLote(entrada);
                VCLote lote = DAOVCLote.GetLote(entrada.IntOidVCLote);
                lote.IntTotalIngresado += (int)entrada.FltCantidad;
                DAOVCLote.updateLote(lote);
            });
        }

        [WebMethod]
        public static List<dynamic> GetLotes(string name)
        {
            return DAOVCLote.GetLotes(name);
        }

        [WebMethod]
        public static dynamic GetLote(int idLote)
        {
            return DAOVCLote.GetLoteWidthInfoInsumo(idLote);
        }
    }
}