using Entidades.Auditorias;
using Entidades.Procesos;
using Newtonsoft.Json;
using Persistencia.Auditorias;
using Persistencia.Generales;
using Persistencia.procesos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Auditorias
{
    public partial class AdministrarAuditorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Metodo web que consulta un listado de todas las usidtorias externas que exitan
        [WebMethod]
        public static dynamic GetAuditoriaExternas()
        {
            //se crea el arreglo en el cua se van a guradar la informacion de todas las auditorias externas
            List<dynamic> datos = new List<dynamic>();

            //se consulta el listado de las auditorias en base de datos y se recorre su informacion 
            DAOAuditoriaExterna.GetAuditorias().ForEach(auditoria =>
            {

                //se consulta la informacion de los archivos anexos a la auditoria
                var archivos = DAOGNArchivo.Listar(auditoria.IntOidListaArchivos);

                // se crea una lisata vacia de proceso en la cual se gurdaran los procesos que contiene la auditoria 
                List<PCProceso> procesos = new List<PCProceso>();

                //como cada aduidotiria guarda un objeto json como listado de procesos, se deserializa, se consultan por id  y luego se guadan en la lista de los proceso 
                JsonConvert.DeserializeObject<List<int>>(auditoria.StrProcesos).ForEach(idProceso =>
                {
                    //por cada id en la en objeto se consutla el proceso en la base de datos y luego se agrega a la lista
                    procesos.Add(DAOProceso.BuscarProceso(idProceso));
                });

                //se obtiene un listado de los uditores deserealiazando el listado que se encuentra en el objeto de la auditoria 
                List<string> Auditores = JsonConvert.DeserializeObject<List<string>>(auditoria.StrAuditores);

                //se agrega la informacion de los datos de las auditorias 
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

        //metodo que retona un listado de la informacion de las auditorias internas 
        [WebMethod]
        public static dynamic GetAuditoriasInternas()
        {
            //se crea una lista de objetos dinamicos en la cual se gurdara la informacion de las auditorias internas 
            List<dynamic> datos = new List<dynamic>();

            //se consulta un listado de las auditorias y se recore su informacion 
            DAOAuditoriaInterna.GetAuditorias().ForEach(auditoria =>
            {
                //se consulta un listado de los archivos que pertencen a las auditorias 
                var archivos = DAOGNArchivo.Listar(auditoria.IntOidListaArchivos);
               
                //se crea una lista en la cual se la informacin de los procesos que pertenecen a la aditoria
                List<string> procesos = new List<string>();

                //se convierte el texto json de la uditoria como un listado de procesos
                JsonConvert.DeserializeObject<List<int>>(auditoria.StrProcesos).ForEach(id =>
                {
                    //por cada id se consulta en la base de datos y se agrega al listado de los procesos
                    procesos.Add(DAOProceso.BuscarProceso(id).StrNomPro);
                });
                //se agrega toda la informacion consultada al listado dinamico con la informacion de las auditoria 
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