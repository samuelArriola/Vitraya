using Persistencia.proceedings;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace Presentacion.proceedings
{
    public partial class InformeAsistentesSinFirma : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static List<dynamic> GetAsistentesSinFirma(string nombreUsuario, string documento, string sigla, string nombreActa)
        {
            return DAOARactasDM.GetAsistentesSinFirma(nombreUsuario, documento, sigla, nombreActa);
        }
    }
}