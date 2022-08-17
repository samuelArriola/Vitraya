using Entidades.Generales;
using Entidades.Vacunacion;
using Persistencia.Generales;
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
    public partial class VerRegistro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<RegistroDiarioVac> GetRegistros(DateTime fecha1, DateTime fecha2, string documento, string nombre, string biologico, string etapa, string cantidad, string lugar)
        {
            return DAORegistroDiarioVac.GetRegistrosVacunacion(fecha1, fecha2, documento, nombre, biologico, etapa,"", cantidad, lugar);
        }

        [WebMethod]
        public static bool DeleteRegistro(int idRegistro, string motivo)
        {
            bool result = DAORegistroDiarioVac.DeleteRegistro(idRegistro, motivo);

            if (result)
            {
                Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));

                var historico = new VCHistorico
                {
                    StrAccion = "Eliminar",
                    DtmFecha = DateTime.Now,
                    IntOidRegistroDiarioVac = idRegistro,
                    IntOidUsuario = usuario.GNCodUsu1,
                    StrNombre = usuario.GNNomUsu1
                };

                DAOVCHistorico.SetHistorico(historico);
            }

            return result;
            
        }
       
    }
}