using Newtonsoft.Json;
using Persistencia.Auditorias;
using Persistencia.Generales;
using Persistencia.procesos;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace Presentacion.Auditorias
{
    public partial class VerAuditoriasInternas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Metodo que retorna un listado de objetos anonimos con la informacion de las auditorias en las
        /// que el usuaro logueado es responsable de los hallazgos de dichas auditorias
        /// </summary>
        /// <returns></returns>

        [WebMethod]
        public static List<dynamic> GetAuditoriasInternas()
        {
            List<dynamic> datos = new List<dynamic>();

            DAOAuditoriaInterna.GetAuditoriaInternasByIdRespHall
            (Convert.ToInt32(HttpContext.Current.Session["Admin"])).ForEach(auditoria =>
            {
                var archivos = DAOGNArchivo.Listar(auditoria.IntOidListaArchivos);
                List<string> procesos = new List<string>();

                JsonConvert.DeserializeObject<List<int>>(auditoria.StrProcesos).ForEach(id =>
                {
                    procesos.Add(DAOProceso.BuscarProceso(id).StrNomPro);
                });
                datos.Add(new
                {
                    Auditoria = auditoria,
                    Procesos = procesos,
                    Archivos = archivos
                });
            });
            return datos;
        }
    }
}