using Entidades.GestionDocumental;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace Presentacion.GestionDocumental
{
    public partial class Repositorio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetArchivo();
        }

        [WebMethod]
        public static List<GDListadoMaestro> loadArchs(string nombre, string codigo, string proceso, string fecha)
        {
            string line = "";
            string aux;
            System.IO.StreamReader file = new System.IO.StreamReader(@"\\10.244.19.194\Repositorio\ListadoMaestro.json");

            while ((aux = file.ReadLine()) != null)
            {
                line += aux;
            }

            try
            {
                List<GDListadoMaestro> listado = JsonConvert.DeserializeObject<List<GDListadoMaestro>>(line);
                List<GDListadoMaestro> listado2 = new List<GDListadoMaestro>();

                foreach (var dat in listado)
                {
                    if (dat.Nombre.ToLower().Contains(nombre.ToLower())
                        && dat.Codigo.ToLower().Contains(codigo.ToLower())
                        && dat.Proceso.ToLower().Contains(proceso.ToLower())
                        && dat.FechaEmision.ToLower().Contains(fecha.ToLower()))
                    {
                        listado2.Add(dat);
                    }
                }

                return listado2;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return null;
            }
            file.Close();
        }

        public ActionResult GetArchivo()
        {


            string nombre = Request["Nombre"];

            if (string.IsNullOrEmpty(nombre))
            {

                return null;
            }

            string path = @"\\10.244.19.194\Repositorio\";

            FileStream archivo = new FileStream(path + nombre, FileMode.Open);


            BinaryReader binaryReader = new BinaryReader(archivo);



            if (archivo != null)
            {
                MemoryStream ms = new MemoryStream();
                archivo.CopyTo(ms);
                archivo.Close();
                binaryReader.Close();
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("content-disposition", $"attachment;filename=\"{nombre}\"");
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                HttpContext.Current.Response.OutputStream.Flush();
                HttpContext.Current.Response.End();
                return new FileStreamResult(HttpContext.Current.Response.OutputStream, "application/octet-stream");
            }
            return null;
        }

    }
}