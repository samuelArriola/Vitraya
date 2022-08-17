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
    public partial class ConsultarLotes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarSlcInsumos();
        }

        private void CargarSlcInsumos()
        {
            List<VCInsumo> insumos = DAOVCInsumo.GetInsumos();
            ListItem todos = new ListItem("Todos", "");
            slcInsumo.Items.Clear();
            slcInsumo.Items.Add(todos);

            insumos.ForEach(insumo => slcInsumo.Items.Add(new ListItem(insumo.StrNombre, insumo.IntOidVCInsumo + "")));
        }
        [WebMethod]
        public static List<VCLote> GetLotes(string idInsumo)
        {
            return DAOVCLote.GetLotesByIdInsumo(idInsumo);
        }
    }
}