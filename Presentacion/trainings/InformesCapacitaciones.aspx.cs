using Persistencia.trainings;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace Presentacion.trainings
{
    public partial class InformesCapacitaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<dynamic> GetInformeAsistencia(string tema, string documento, string nomUsuario, string unidad)
        {
            return DAOCPMatricula.GetInformeMatricula(tema, documento, nomUsuario, unidad);
        }
    }
}