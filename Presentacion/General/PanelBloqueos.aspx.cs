using Entidades.Generales;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace Presentacion.General
{
    public partial class PanelBloqueos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<ScriptBloqueo> ObtenerBloqueos()
        {
            return DAONABloqueos.GetBloqueos();
        }

        [WebMethod]
        public static void ActualizarBloqueos(int id, string estado)
        {

            DAONABloqueos.UpdateBloqueos(id, estado);
        }

        [WebMethod]
        public static List<GNOpciones> ObtenerOpcionesBloqueadas()
        {

            return DAOGNOpciones.listar();
        }

        [WebMethod]
        public static void ActualizarOpcionesBloqueadas(int id, int estado)
        {

            DAOGNOpciones.UpdateEstadoOpcion(estado, id);
        }
    }
}