using Entidades.Generales;
using Entidades.GestionDocumental;
using Entidades.Procesos;
using Persistencia.Generales;
using Persistencia.GestionDocumental;
using Persistencia.procesos;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace Presentacion.GestionDocumental
{
    public partial class ListadoMaestro1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        //metodo que devuelve una lista de los documentos que estan pendientes por asignar revisores y aprobador
        [WebMethod]
        public static List<dynamic> GetDocumentos(string proceso, string codigo, string nombre, string tipo, string version)
        {
            return DAOGDDocumento.GetListadoMaestro(proceso,  codigo,  nombre,  tipo, version);
        }
    }
}