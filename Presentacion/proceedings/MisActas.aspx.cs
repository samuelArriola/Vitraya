using Entidades.Generales;
using Entidades.PlanAccion;
using Persistencia.Generales;
using Persistencia.proceedings;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace Presentacion.proceedings
{
    public partial class MisActas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static List<dynamic> GetActasByUser(string codigo, string nombre, DateTime fecha, string lugar, string estado)
        {
            List<dynamic> datos = new List<dynamic>();

            DAOARActasC.GetActasBySuario(codigo, nombre, fecha, lugar, estado).ForEach(acta =>
            {
                ARActasDM miembro = DAOARactasDM.get(acta.IntOidARActas, Convert.ToInt32(HttpContext.Current.Session["Admin"]));
                datos.Add(new { acta = acta, miembro = miembro });
            });

            return datos;
        }

        [WebMethod]
        public static bool FirmarActa(int idMiembro)
        {
            try
            {
                ARActasDM miembro = DAOARactasDM.Get(idMiembro);

                miembro.BlnFirmado = true;

                DAOARactasDM.update(miembro);

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = miembro.IntOidARActasDM,
                    strAccion = "Modificar",
                    strDetalle = $"El miembro {miembro.StrNombre} firma el acta con codigo {miembro.IntOidARActasC}",
                    strEntidad = "ARActasDM"
                });

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}