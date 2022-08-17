
using Entidades.Generales;
using Persistencia.Generales;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Generales_1._0.Home.dashboard.production.screens.proceedings
{
    public partial class Archivos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetArchivo();
        }

        public ActionResult GetArchivo()
        {
            int id = Convert.ToInt32(Request["id"]);

            GNArchivo archivo = DAOGNArchivo.get(id);
            if (archivo != null)
            {
                MemoryStream ms = new MemoryStream(archivo.AbteArchivo, 0, 0, true, true);
                HttpContext.Current.Response.ContentType = archivo.StrContenido;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + archivo.StrNombre + "." + archivo.StrExt);
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                HttpContext.Current.Response.OutputStream.Flush();
                HttpContext.Current.Response.End();
                return new FileStreamResult(HttpContext.Current.Response.OutputStream, archivo.StrContenido);
            }
            return null;
        }
    }
}