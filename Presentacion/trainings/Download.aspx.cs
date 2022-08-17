using System;

namespace Capacitaciones
{
    public partial class Download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //int id = Convert.ToInt32(Request.QueryString["Id"]);

            //Archivo archivo = ArchivosDAL.GetByIdCapacitaciones(id);

            //Response.Clear();

            //Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", archivo.Nombre));
            //Response.ContentType = "application/octet-stream";

            //Response.BinaryWrite(archivo.ContenidoArchivo);
            //Response.End();
        }
    }
}