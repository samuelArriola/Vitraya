using Entidades.Auditorias;
using Entidades.Procesos;
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
    public partial class VerAuditorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //metodo que retorna un listado de las auditorias externas
        [WebMethod]
        public static dynamic GetAuditoriaExternas()
        {
            //se crea una lista en la cual se guardara la informacion de las auditorias externas
            List<dynamic> datos = new List<dynamic>();

            //se consulta y recorre el listado de las auditorias externas que corresponden al usuario que se encuentra logueado
            DAOAuditoriaExterna.GetAuditoriaExternasbyRespHall(Convert.ToInt32(HttpContext.Current.Session["Admin"])).ForEach(auditoria =>
            {
                //se consulta un listado de los archivos anexos a las asuditorias
                var archivos = DAOGNArchivo.Listar(auditoria.IntOidListaArchivos);

                //se crea una lista de los procesos
                List<PCProceso> procesos = new List<PCProceso>();

                //como el listado de procesos en la auditoria es el texo de un arreglo se descerializa y se recoren los ids
                JsonConvert.DeserializeObject<List<int>>(auditoria.StrProcesos).ForEach(idProceso =>
                {
                    //por cada id se consulta en base de datos y se guarda la informacion en el listado de procesos
                    procesos.Add(DAOProceso.BuscarProceso(idProceso));
                });

                //se crea un arregli de los auditores a traves del texto de array que se encuentra en la auditoria
                List<string> Auditores = JsonConvert.DeserializeObject<List<string>>(auditoria.StrAuditores);

                //se agrega toda la informacion consultada a la lista de objetos dinamicos
                datos.Add(new
                {
                    Procesos = procesos,
                    Auditores = Auditores,
                    Auditoria = auditoria,
                    Archivos = archivos
                });
            });

            return datos;
        }


         
        /// <summary>
        /// metoto que consulta y retorna un listdo de los hallazagos de una auditoria en particular
        /// </summary>
        /// <param name="idAuditoria"> id del la auditoria</param>
        /// <param name="contexto"> contexto de la uuditoria ya se esterna o interna</param>
        /// <param name="responsable">id del responsable de los hallazagos</param>
        /// <returns></returns>
        [WebMethod]
        public static List<Hallazgo> GetHallazgos(int idAuditoria, int contexto, int responsable)
        {
            return DAOHallazgo.GetHallazgosByidAuditoria(idAuditoria, contexto);
        }
    }
}